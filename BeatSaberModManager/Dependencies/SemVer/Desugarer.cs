using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SemVer
{
    internal static class Desugarer
    {
        private const string versionChars = @"[0-9a-zA-Z\-\+\.\*]";

        // Allows patch-level changes if a minor version is specified
        // on the comparator. Allows minor-level changes if not.
        public static Tuple<int, Comparator[]> TildeRange(string spec)
        {
            string pattern = String.Format(@"^\s*~\s*({0}+)\s*", versionChars);

            var regex = new Regex(pattern);
            var match = regex.Match(spec);
            if (!match.Success)
            {
                return null;
            }

            Version minVersion = null;
            Version maxVersion = null;

            var version = new PartialVersion(match.Groups[1].Value);
            if (version.Minor.HasValue)
            {
                // Doesn't matter whether patch version is null or not,
                // the logic is the same, min patch version will be zero if null.
                minVersion = version.ToZeroVersion();
                maxVersion = new Version(version.Major.Value, version.Minor.Value + 1, 0);
            }
            else
            {
                minVersion = version.ToZeroVersion();
                maxVersion = new Version(version.Major.Value + 1, 0, 0);
            }

            return Tuple.Create(
                    match.Length,
                    minMaxComparators(minVersion, maxVersion));
        }

        // Allows changes that do not modify the left-most non-zero digit
        // in the [major, minor, patch] tuple.
        public static Tuple<int, Comparator[]> CaretRange(string spec)
        {
            string pattern = String.Format(@"^\s*\^\s*({0}+)\s*", versionChars);

            var regex = new Regex(pattern);
            var match = regex.Match(spec);
            if (!match.Success)
            {
                return null;
            }

            Version minVersion = null;
            Version maxVersion = null;

            var version = new PartialVersion(match.Groups[1].Value);

            if (version.Major.Value > 0)
            {
                // Don't allow major version change
                minVersion = version.ToZeroVersion();
                maxVersion = new Version(version.Major.Value + 1, 0, 0);
            }
            else if (!version.Minor.HasValue)
            {
                // Don't allow major version change, even if it's zero
                minVersion = version.ToZeroVersion();
                maxVersion = new Version(version.Major.Value + 1, 0, 0);
            }
            else if (!version.Patch.HasValue)
            {
                // Don't allow minor version change, even if it's zero
                minVersion = version.ToZeroVersion();
                maxVersion = new Version(0, version.Minor.Value + 1, 0);
            }
            else if (version.Minor > 0)
            {
                // Don't allow minor version change
                minVersion = version.ToZeroVersion();
                maxVersion = new Version(0, version.Minor.Value + 1, 0);
            }
            else
            {
                // Only patch non-zero, don't allow patch change
                minVersion = version.ToZeroVersion();
                maxVersion = new Version(0, 0, version.Patch.Value + 1);
            }

            return Tuple.Create(
                    match.Length,
                    minMaxComparators(minVersion, maxVersion));
        }

        public static Tuple<int, Comparator[]> HyphenRange(string spec)
        {
            string pattern = String.Format(@"^\s*({0}+)\s+\-\s+({0}+)\s*", versionChars);

            var regex = new Regex(pattern);
            var match = regex.Match(spec);
            if (!match.Success)
            {
                return null;
            }

            PartialVersion minPartialVersion = null;
            PartialVersion maxPartialVersion = null;

            // Parse versions from lower and upper ranges, which might
            // be partial versions.
            try
            {
                minPartialVersion = new PartialVersion(match.Groups[1].Value);
                maxPartialVersion = new PartialVersion(match.Groups[2].Value);
            }
            catch (ArgumentException)
            {
                return null;
            }

            // Lower range has any non-supplied values replaced with zero
            var minVersion = minPartialVersion.ToZeroVersion();

            Comparator.Operator maxOperator = maxPartialVersion.IsFull()
                ? Comparator.Operator.LessThanOrEqual : Comparator.Operator.LessThan;

            Version maxVersion = null;

            // Partial upper range means supplied version values can't change
            if (!maxPartialVersion.Major.HasValue)
            {
                // eg. upper range = "*", then maxVersion remains null
                // and there's only a minimum
            }
            else if (!maxPartialVersion.Minor.HasValue)
            {
                maxVersion = new Version(maxPartialVersion.Major.Value + 1, 0, 0);
            }
            else if (!maxPartialVersion.Patch.HasValue)
            {
                maxVersion = new Version(maxPartialVersion.Major.Value, maxPartialVersion.Minor.Value + 1, 0);
            }
            else
            {
                // Fully specified max version
                maxVersion = maxPartialVersion.ToZeroVersion();
            }
            return Tuple.Create(
                    match.Length,
                    minMaxComparators(minVersion, maxVersion, maxOperator));
        }

        public static Tuple<int, Comparator[]> StarRange(string spec)
        {
            // Also match with an equals sign, eg. "=0.7.x"
            string pattern = String.Format(@"^\s*=?\s*({0}+)\s*", versionChars);

            var regex = new Regex(pattern);
            var match = regex.Match(spec);

            if (!match.Success)
            {
                return null;
            }

            PartialVersion version = null;
            try
            {
                version = new PartialVersion(match.Groups[1].Value);
            }
            catch (ArgumentException)
            {
                return null;
            }

            // If partial version match is actually a full version,
            // then this isn't a star range, so return null.
            if (version.IsFull())
            {
                return null;
            }

            Version minVersion = null;
            Version maxVersion = null;
            if (!version.Major.HasValue)
            {
                minVersion = version.ToZeroVersion();
                // no max version
            }
            else if (!version.Minor.HasValue)
            {
                minVersion = version.ToZeroVersion();
                maxVersion = new Version(version.Major.Value + 1, 0, 0);
            }
            else
            {
                minVersion = version.ToZeroVersion();
                maxVersion = new Version(version.Major.Value, version.Minor.Value + 1, 0);
            }

            return Tuple.Create(
                    match.Length,
                    minMaxComparators(minVersion, maxVersion));
        }

        private static Comparator[] minMaxComparators(Version minVersion, Version maxVersion,
                Comparator.Operator maxOperator=Comparator.Operator.LessThan)
        {
            var minComparator = new Comparator(
                    Comparator.Operator.GreaterThanOrEqual,
                    minVersion);
            if (maxVersion == null)
            {
                return new [] { minComparator };
            }
            else
            {
                var maxComparator = new Comparator(
                        maxOperator, maxVersion);
                return new [] { minComparator, maxComparator };
            }
        }
    }
}
