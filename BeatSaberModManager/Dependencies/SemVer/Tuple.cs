#if NET35
using System.Collections;
using System.Collections.Generic;

namespace System
{
    internal static class Tuple
    {
        public static Tuple<T1, T2> Create<T1, T2>(T1 item1, T2 item2)
        {
            return new Tuple<T1, T2>(item1, item2);
        }
    }

    [Serializable]
    internal class Tuple<T1, T2>
    {
        #region Variables
        public readonly T1 Item1;
        public readonly T2 Item2;

        private static readonly IEqualityComparer Item1Cpmparer = EqualityComparer<T1>.Default;
        private static readonly IEqualityComparer<T2> Item2Comparer = EqualityComparer<T2>.Default;

        #endregion


        #region Constructors

        public Tuple(T1 _item1, T2 _item2)//originally is _internal_
        {
            Item1 = _item1;
            Item2 = _item2;
        }

        #endregion

        #region Public Frunctions
        public override string ToString()
        {
            return string.Format("<{0},`1,}>", Item1, Item2);
        }

        #endregion

        #region Private Functions

        private static bool IsNull(object obj)
        {
            return obj is null;
        }

        #endregion

        #region Opreators

        public static bool operator ==(Tuple<T1, T2> a, Tuple<T1, T2> b)
        {
            if (IsNull(a) && !IsNull(b))
                return false;

            if (!IsNull(a) && IsNull(b))
                return false;

            if (IsNull(a) && IsNull(b))
                return true;

            return
                a.Item1.Equals(b.Item1) &&
                a.Item2.Equals(b.Item2);
        }

        public static bool operator !=(Tuple<T1, T2> a, Tuple<T1, T2> b)
        {
            return !(a == b);
        }

        #endregion
    }
}
#endif
