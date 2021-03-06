﻿using System.Collections.Generic;
using System.Linq;

namespace Onism.Cldr.Extensions
{
    internal static class ListExtensions
    {
        public static bool IsSameAs<T>(this List<T> list1, List<T> list2)
        {
            return list1.Count == list2.Count && list1.Except(list2).IsEmpty();
        }

        public static bool TryGetElement<T>(this List<T> list, int index, out T element)
        {
            if (index >= 0 && index < list.Count)
            {
                element = list[index];
                return true;
            }
            else
            {
                element = default(T);
                return false;
            }
        }
    }

    internal static class DictionaryExtensions
    {
        /// <summary>
        /// Gets the id associated with the specified key. If the key is missing,
        /// it is added and its new id returned.
        /// </summary>
        public static int GetOrAddId<TKey>(this Dictionary<TKey, int> dict, TKey key)
        {
            int id;

            if (dict.TryGetValue(key, out id))
                return id;

            // no removal will be done, so it's safe
            id = dict.Count;

            dict.Add(key, id);
            return id;
        }

        /// <summary>
        /// Gets a flag indicating whether two dictionaries have exactly same contents by using the default
        /// equality comparer to compare values.
        /// </summary>
        public static bool IsSameAs<TKey, TValue>(this Dictionary<TKey, TValue> dict1, Dictionary<TKey, TValue> dict2)
        {
            return dict1.Count == dict2.Count && dict1.Except(dict2).IsEmpty();
        }
    }
}