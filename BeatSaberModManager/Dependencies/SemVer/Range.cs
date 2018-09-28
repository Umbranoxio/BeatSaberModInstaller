using System;
using System.Collections.Generic;
using System.Linq;

namespace SemVer
{
    /// <summary>
    /// Specifies valid versions.
    /// </summary>
    public class Range : IEquatable<Range>
    {
        private readonly ComparatorSet[] _comparatorSets;

        private readonly string _rangeSpec;

        /// <summary>
        /// Construct a new range from a range specification.
        /// </summary>
        /// <param name="rangeSpec">The range specification string.</param>
        /// <param name="loose">When true, be more forgiving of some invalid version specifications.</param>
        /// <exception cref="System.ArgumentException">Thrown when the range specification is invalid.</exception>
        public Range(string rangeSpec, bool loose=false)
        {
            _rangeSpec = rangeSpec;
            var comparatorSetSpecs = rangeSpec.Split(new [] {"||"}, StringSplitOptions.None);
            _comparatorSets = comparatorSetSpecs.Select(s => new ComparatorSet(s)).ToArray();
        }

        private Range(IEnumerable<ComparatorSet> comparatorSets)
        {
            _comparatorSets = comparatorSets.ToArray();
            _rangeSpec = string.Join(" || ", _comparatorSets.Select(cs => cs.ToString()).ToArray());
        }

        /// <summary>
        /// Determine whether the given version satisfies this range.
        /// </summary>
        /// <param name="version">The version to check.</param>
        /// <returns>true if the range is satisfied by the version.</returns>
        public bool IsSatisfied(Version version)
        {
            return _comparatorSets.Any(s => s.IsSatisfied(version));
        }

        /// <summary>
        /// Determine whether the given version satisfies this range.
        /// With an invalid version this method returns false.
        /// </summary>
        /// <param name="versionString">The version to check.</param>
        /// <param name="loose">When true, be more forgiving of some invalid version specifications.</param>
        /// <returns>true if the range is satisfied by the version.</returns>
        public bool IsSatisfied(string versionString, bool loose=false)
        {
            try
            {
                var version = new Version(versionString, loose);
                return IsSatisfied(version);
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        /// <summary>
        /// Return the set of versions that satisfy this range.
        /// </summary>
        /// <param name="versions">The versions to check.</param>
        /// <returns>An IEnumerable of satisfying versions.</returns>
        public IEnumerable<Version> Satisfying(IEnumerable<Version> versions)
        {
            return versions.Where(IsSatisfied);
        }

        /// <summary>
        /// Return the set of version strings that satisfy this range.
        /// Invalid version specifications are skipped.
        /// </summary>
        /// <param name="versions">The version strings to check.</param>
        /// <param name="loose">When true, be more forgiving of some invalid version specifications.</param>
        /// <returns>An IEnumerable of satisfying version strings.</returns>
        public IEnumerable<string> Satisfying(IEnumerable<string> versions, bool loose=false)
        {
            return versions.Where(v => IsSatisfied(v, loose));
        }

        /// <summary>
        /// Return the maximum version that satisfies this range.
        /// </summary>
        /// <param name="versions">The versions to select from.</param>
        /// <returns>The maximum satisfying version, or null if no versions satisfied this range.</returns>
        public Version MaxSatisfying(IEnumerable<Version> versions)
        {
            return Satisfying(versions).Max();
        }

        /// <summary>
        /// Return the maximum version that satisfies this range.
        /// </summary>
        /// <param name="versionStrings">The version strings to select from.</param>
        /// <param name="loose">When true, be more forgiving of some invalid version specifications.</param>
        /// <returns>The maximum satisfying version string, or null if no versions satisfied this range.</returns>
        public string MaxSatisfying(IEnumerable<string> versionStrings, bool loose=false)
        {
            var versions = ValidVersions(versionStrings, loose);
            var maxVersion = MaxSatisfying(versions);
            return maxVersion == null ? null : maxVersion.ToString();
        }

        /// <summary>
        /// Calculate the intersection between two ranges.
        /// </summary>
        /// <param name="other">The Range to intersect this Range with</param>
        /// <returns>The Range intersection</returns>
        public Range Intersect(Range other)
        {
            var allIntersections = _comparatorSets.SelectMany(
                thisCs => other._comparatorSets.Select(thisCs.Intersect))
                .Where(cs => cs != null).ToList();

            if (allIntersections.Count == 0)
            {
                return new Range("<0.0.0");
            }
            return new Range(allIntersections);
        }

        /// <summary>
        /// Returns the range specification string used when constructing this range.
        /// </summary>
        /// <returns>The range string</returns>
        public override string ToString()
        {
            return _rangeSpec;
        }

        public bool Equals(Range other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }
            var thisSet = new HashSet<ComparatorSet>(_comparatorSets);
            return thisSet.SetEquals(other._comparatorSets);
        }

        public override bool Equals(object other)
        {
            return Equals(other as Range);
        }

        public static bool operator ==(Range a, Range b)
        {
            if (ReferenceEquals(a, null))
            {
                return ReferenceEquals(b, null);
            }
            return a.Equals(b);
        }

        public static bool operator !=(Range a, Range b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            // XOR is commutative, so this hash code is independent
            // of the order of comparators.
            return _comparatorSets.Aggregate(0, (accum, next) => accum ^ next.GetHashCode());
        }

        // Static convenience methods

        /// <summary>
        /// Determine whether the given version satisfies a given range.
        /// With an invalid version this method returns false.
        /// </summary>
        /// <param name="rangeSpec">The range specification.</param>
        /// <param name="versionString">The version to check.</param>
        /// <param name="loose">When true, be more forgiving of some invalid version specifications.</param>
        /// <returns>true if the range is satisfied by the version.</returns>
        public static bool IsSatisfied(string rangeSpec, string versionString, bool loose=false)
        {
            var range = new Range(rangeSpec);
            return range.IsSatisfied(versionString);
        }

        /// <summary>
        /// Return the set of version strings that satisfy a given range.
        /// Invalid version specifications are skipped.
        /// </summary>
        /// <param name="rangeSpec">The range specification.</param>
        /// <param name="versions">The version strings to check.</param>
        /// <param name="loose">When true, be more forgiving of some invalid version specifications.</param>
        /// <returns>An IEnumerable of satisfying version strings.</returns>
        public static IEnumerable<string> Satisfying(string rangeSpec, IEnumerable<string> versions, bool loose=false)
        {
            var range = new Range(rangeSpec);
            return range.Satisfying(versions);
        }

        /// <summary>
        /// Return the maximum version that satisfies a given range.
        /// </summary>
        /// <param name="rangeSpec">The range specification.</param>
        /// <param name="versionStrings">The version strings to select from.</param>
        /// <param name="loose">When true, be more forgiving of some invalid version specifications.</param>
        /// <returns>The maximum satisfying version string, or null if no versions satisfied this range.</returns>
        public static string MaxSatisfying(string rangeSpec, IEnumerable<string> versionStrings, bool loose=false)
        {
            var range = new Range(rangeSpec);
            return range.MaxSatisfying(versionStrings);
        }

        private IEnumerable<Version> ValidVersions(IEnumerable<string> versionStrings, bool loose)
        {
            foreach (var v in versionStrings)
            {
                Version version = null;

                try
                {
                    version = new Version(v, loose);
                }
                catch (ArgumentException) { } // Skip

                if (version != null)
                {
                    yield return version;
                }
            }
        }
    }
}
