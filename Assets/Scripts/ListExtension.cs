using System;
using System.Collections.Generic;

namespace Assets.Scripts
{
    internal static class ListExtension
    {
        public static bool ContainsList<T>(this List<T> list, List<T> otherList) where T : IComparable
        {
            if (otherList.Count > list.Count)
                return false;
            for (int i = 0; i < list.Count; i++)
            {
                if (i == otherList.Count)
                    return true;
                if (list[i].CompareTo(otherList[i]) != 0)
                    return false;
            }
            return true;
        }
    }
}