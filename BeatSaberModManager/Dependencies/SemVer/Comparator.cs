using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SemVer
{
    internal class Comparator : IEquatable<Comparator>
    {
        public readonly Operator ComparatorType;

        public readonly Version Version;

        private const string pattern = @"
            \s*
            ([=<>]*)                # Comparator type (can be empty)
            \s*
            ([0-9a-zA-Z\-\+\.\*]+)  # Version (potentially partial version)
            \s*
            ";

        public Comparator(string input)
        {
            var regex = new Regex(String.Format("^{0}$", pattern),
                    RegexOptions.IgnorePatternWhitespace);
            var match = regex.Match(input);
            if (!match.Success)
            {
                throw new ArgumentException(String.Format("Invalid comparator string: {0}", input));
            }

            ComparatorType = ParseComparatorType(match.Groups[1].Value);
            var partialVersion = new PartialVersion(match.Groups[2].Value);

            if (!partialVersion.IsFull())
            {
                // For Operator.Equal, partial versions are handled by the StarRange
                // desugarer, and desugar to multiple comparators.

                switch (ComparatorType)
                {
                    // For <= with a partial version, eg. <=1.2.x, this
                    // means the same as < 1.3.0, and <=1.x means <2.0
                    case Operator.LessThanOrEqual:
                        ComparatorType = Operator.LessThan;
                        if (!partialVersion.Major.HasValue)
                        {
                            // <=* means >=0.0.0
                            ComparatorType = Operator.GreaterThanOrEqual;
                            Version = new Version(0, 0, 0);
                        }
                        else if (!partialVersion.Minor.HasValue)
                        {
                            Version = new Version(partialVersion.Major.Value + 1, 0, 0);
                        }
                        else
                        {
                            Version = new Version(partialVersion.Major.Value, partialVersion.Minor.Value + 1, 0);
                        }
                        break;
                    case Operator.GreaterThan:
                        ComparatorType = Operator.GreaterThanOrEqual;
                        if (!partialVersion.Major.HasValue)
                        {
                            // >* is unsatisfiable, so use <0.0.0
                            ComparatorType = Operator.LessThan;
                            Version = new Version(0, 0, 0);
                        }
                        else if (!partialVersion.Minor.HasValue)
                        {
                            // eg. >1.x -> >=2.0
                            Version = new Version(partialVersion.Major.Value + 1, 0, 0);
                        }
                        else
                        {
                            // eg. >1.2.x -> >=1.3
                            Version = new Version(partialVersion.Major.Value, partialVersion.Minor.Value + 1, 0);
                        }
                        break;
                    default:
                        // <1.2.x means <1.2.0
                        // >=1.2.x means >=1.2.0
                        Version = partialVersion.ToZeroVersion();
                        break;
                }
            }
            else
            {
                Version = partialVersion.ToZeroVersion();
            }
        }

        public Comparator(Operator comparatorType, Version comparatorVersion)
        {
            if (comparatorVersion == null)
            {
                throw new NullReferenceException("Null comparator version");
            }
            ComparatorType = comparatorType;
            Version = comparatorVersion;
        }

        public static Tuple<int, Comparator> TryParse(string input)
        {
            var regex = new Regex(String.Format("^{0}", pattern),
                    RegexOptions.IgnorePatternWhitespace);

            var match = regex.Match(input);

            return match.Success ?
                Tuple.Create(
                    match.Length,
                    new Comparator(match.Value))
                : null;
        }

        private static Operator ParseComparatorType(string input)
        {
            switch (input)
            {
                case (""):
                case ("="):
                    return Operator.Equal;
                case ("<"):
                    return Operator.LessThan;
                case ("<="):
                    return Operator.LessThanOrEqual;
                case (">"):
                    return Operator.GreaterThan;
                case (">="):
                    return Operator.GreaterThanOrEqual;
                default:
                    throw new ArgumentException(String.Format("Invalid comparator type: {0}", input));
            }
        }

        public bool IsSatisfied(Version version)
        {
            switch(ComparatorType)
            {
                case(Operator.Equal):
                    return version == Version;
                case(Operator.LessThan):
                    return version < Version;
                case(Operator.LessThanOrEqual):
                    return version <= Version;
                case(Operator.GreaterThan):
                    return version > Version;
                case(Operator.GreaterThanOrEqual):
                    return version >= Version;
                default:
                    throw new InvalidOperationException("Comparator type not recognised.");
            }
        }

        public enum Operator
        {
            Equal = 0,
            LessThan,
            LessThanOrEqual,
            GreaterThan,
            GreaterThanOrEqual,
        }

        public override string ToString()
        {
            string operatorString = null;
            switch(ComparatorType)
            {
                case(Operator.Equal):
                    operatorString = "=";
                    break;
                case(Operator.LessThan):
                    operatorString = "<";
                    break;
                case(Operator.LessThanOrEqual):
                    operatorString = "<=";
                    break;
                case(Operator.GreaterThan):
                    operatorString = ">";
                    break;
                case(Operator.GreaterThanOrEqual):
                    operatorString = ">=";
                    break;
                default:
                    throw new InvalidOperationException("Comparator type not recognised.");
            }
            return String.Format("{0}{1}", operatorString, Version);
        }

        public bool Equals(Comparator other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }
            return ComparatorType == other.ComparatorType && Version == other.Version;
        }

        public override bool Equals(object other)
        {
            return Equals(other as Comparator);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}
