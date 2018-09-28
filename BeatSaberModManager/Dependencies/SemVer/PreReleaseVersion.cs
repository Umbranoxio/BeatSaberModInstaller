using System;
using System.Collections.Generic;
using System.Linq;

namespace SemVer
{
    static internal class PreReleaseVersion
    {
        public static int Compare(string a, string b)
        {
            if (a == null && b == null)
            {
                return 0;
            }
            else if (a == null)
            {
                // No pre-release is > having a pre-release version
                return 1;
            }
            else if (b == null)
            {
                return -1;
            }
            else
            {
                foreach (var c in IdentifierComparisons(Identifiers(a), Identifiers(b)))
                {
                    if (c != 0)
                    {
                        return c;
                    }
                }
                return 0;
            }
        }

        public static string Clean(string input)
        {
            var identifierStrings = Identifiers(input).Select(i => i.Clean());
            return String.Join(".", identifierStrings.ToArray());
        }

        private class Identifier
        {
            public bool IsNumeric { get; set; }
            public int IntValue { get; set; }
            public string Value { get; set; }

            public Identifier(string input)
            {
                Value = input;
                SetNumeric();
            }

            public string Clean()
            {
                return IsNumeric ? IntValue.ToString() : Value;
            }

            private void SetNumeric()
            {
                int x;
                bool couldParse = Int32.TryParse(Value, out x);
                IsNumeric = couldParse && x >= 0;
                IntValue = x;
            }
        }

        private static IEnumerable<Identifier> Identifiers(string input)
        {
            foreach (var identifier in input.Split('.'))
            {
                yield return new Identifier(identifier);
            }
        }

        private static IEnumerable<int> IdentifierComparisons(
                IEnumerable<Identifier> aIdentifiers, IEnumerable<Identifier> bIdentifiers)
        {
            foreach (var identifiers in ZipIdentifiers(aIdentifiers, bIdentifiers))
            {
                var a = identifiers.Item1;
                var b = identifiers.Item2;
                if (a == b)
                {
                    yield return 0;
                }
                else if (a == null)
                {
                    yield return -1;
                }
                else if (b == null)
                {
                    yield return 1;
                }
                else
                {
                    if (a.IsNumeric && b.IsNumeric)
                    {
                        yield return a.IntValue.CompareTo(b.IntValue);
                    }
                    else if (!a.IsNumeric && !b.IsNumeric)
                    {
                        yield return String.CompareOrdinal(a.Value, b.Value);
                    }
                    else if (a.IsNumeric && !b.IsNumeric)
                    {
                        yield return -1;
                    }
                    else // !a.IsNumeric && b.IsNumeric
                    {
                        yield return 1;
                    }
                }
            }
        }

        // Zip identifier sets until both have been exhausted, returning null
        // for identifier components not in one set.
        private static IEnumerable<Tuple<Identifier, Identifier>> ZipIdentifiers(
                IEnumerable<Identifier> first, IEnumerable<Identifier> second)
        {
            using (var ie1 = first.GetEnumerator())
            using (var ie2 = second.GetEnumerator())
            {
                while (ie1.MoveNext())
                {
                    if (ie2.MoveNext())
                    {
                        yield return Tuple.Create(ie1.Current, ie2.Current);
                    }
                    else
                    {
                        yield return Tuple.Create<Identifier, Identifier>(ie1.Current, null);
                    }
                }
                while (ie2.MoveNext())
                {
                    yield return Tuple.Create<Identifier, Identifier>(null, ie2.Current);
                }
            }
        }
    }
}
