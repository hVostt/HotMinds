﻿using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace HotMinds.Collections
{
    /// <summary>
    ///     Common collections (enumerable) extensions.
    /// </summary>
    public static class CommonCollectionExtensions
    {
        /// <summary>
        ///     Check if collection (enumerable) is null or empty.
        /// </summary>
        /// <param name="collection">Source collection.</param>
        /// <typeparam name="T">Collection items type.</typeparam>
        /// <returns>True, if <paramref name="collection"/> is null or empty.</returns>
        public static bool IsEmpty<T>([CanBeNull] this IEnumerable<T> collection)
        {
            if (collection == null) return true;
            var readOnlyCollection = collection as IReadOnlyCollection<T>;
            if (readOnlyCollection != null)
            {
                return readOnlyCollection.Count == 0;
            }
            return !collection.Any();
        }

        /// <summary>
        ///     Get value from dictionary by key or default value. Supports only IReadOnlyDictionary implementations.
        /// </summary>
        /// <param name="dictionary">Source dictionary, can be null.</param>
        /// <param name="key">Search key, can be null.</param>
        /// <param name="defaultValue">Default value, can be null.</param>
        /// <typeparam name="TKey">Key type.</typeparam>
        /// <typeparam name="TValue">Value type.</typeparam>
        /// <returns>
        ///     Returns specified default value, if soucre dictionaty is null, or key is null, or key not found in source dictionary.
        /// </returns>
        [CanBeNull]
        public static TValue GetOrDefault<TKey, TValue>([CanBeNull] this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, [CanBeNull] TKey key,
            TValue defaultValue = default(TValue))
        {
            var readOnlyDictionary = dictionary as IReadOnlyDictionary<TKey, TValue>;
            if (readOnlyDictionary != null && key != null)
            {
                TValue value;
                if (readOnlyDictionary.TryGetValue(key, out value))
                {
                    return value;
                }
            }
            return defaultValue;
        }
    }
}
