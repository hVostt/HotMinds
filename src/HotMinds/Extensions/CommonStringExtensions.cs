﻿using System;
using System.IO;
using System.Text;
using JetBrains.Annotations;

namespace HotMinds.Extensions
{
    /// <summary>
    ///     Common string extensions.
    /// </summary>
    public static class CommonStringExtensions
    {
        /// <summary>
        ///     Get the default value if the source string is empty or null.
        /// </summary>
        /// <param name="str">
        ///     Source string.</param>
        /// <param name="defaultValue">
        ///     Default value.</param>
        /// <returns>Source string or default value.</returns>
        public static string DefaultIfNullOrEmpty([CanBeNull] this string str, string defaultValue = default(string))
        {
            return string.IsNullOrEmpty(str) ? defaultValue : str;
        }

        /// <summary>
        ///     Get the default value if the source string is empty, whitespaces or null.
        /// </summary>
        /// <param name="str">
        ///     Source string.</param>
        /// <param name="defaultValue">
        ///     Default value.</param>
        /// <returns>Source string or default value.</returns>
        public static string DefaultIfNullOrWhiteSpace([CanBeNull] this string str, string defaultValue = default(string))
        {
            return string.IsNullOrWhiteSpace(str) ? defaultValue : str;
        }

        /// <summary>
        ///     Gets the string length, or zero if string is null.
        /// </summary>
        /// <param name="str">Source string.</param>
        /// <returns>The string length or zero.</returns>
        public static int GetLength([CanBeNull] this string str)
        {
            return str?.Length ?? 0;
        }

        /// <summary>
        ///     Extension analog of String.IsNullOrEmpty.
        /// </summary>
        /// <param name="str">Source string.</param>
        /// <returns>True if <paramref name="str"/> is null or empty string.</returns>
        public static bool IsNullOrEmpty([CanBeNull] this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        ///     Extension analog of String.IsNullOrWhiteSpace.
        /// </summary>
        /// <param name="str">Source string.</param>
        /// <returns>True if <paramref name="str"/> is null, empty or whitespace string.</returns>
        public static bool IsNullOrWhiteSpace([CanBeNull] this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        ///     Convert string into Stream.
        /// </summary>
        /// <param name="s">Source string.</param>
        /// <param name="encoding">Encoding for convert string to Stream. Default is UTF-8.</param>
        /// <returns>Stream, with source string content. If string is null or empty, returns empty stream.</returns>
        [NotNull]
        public static Stream ToStream([CanBeNull] this string s, Encoding encoding = null)
        {
            var stream = string.IsNullOrEmpty(s)
                ? new MemoryStream()
                : new MemoryStream((encoding ?? Encoding.UTF8).GetBytes(s));
            stream.Seek(0L, SeekOrigin.Begin);
            return stream;
        }

        /// <summary>
        ///     If the string length is greater than the specified length, truncate it.
        /// </summary>
        /// <param name="str">Source string.</param>
        /// <param name="length">Maximum length of the string.</param>
        /// <returns>The original or the truncated string.</returns>
        [CanBeNull]
        public static string Limit([CanBeNull] this string str, int length)
        {
            if (length <= 0) throw new ArgumentException("Trucate length must be greater than zero.", nameof(length));
            if (str == null) return null;
            return str.Length > length ? str.Substring(0, length) : str;
        }

        /// <summary>
        ///     If the string length is greater than the specified length, truncate it and append ellipsis.
        /// </summary>
        /// <param name="str">Source string.</param>
        /// <param name="length">Maximum length of the string.</param>
        /// <param name="ellipsis">Ellipsis symbol, default is "…".</param>
        /// <returns>The original or the truncated string with appended ellipsis.</returns>
        [CanBeNull]
        public static string Truncate([CanBeNull] this string str, int length, [NotNull] string ellipsis = "…")
        {
            if (ellipsis == null) throw new ArgumentNullException(nameof(ellipsis));
            if (ellipsis.Length == 0) return str.Limit(length);
            if (length <= ellipsis.Length)
                throw new ArgumentException(
                    $"Trucate length must be greater than ellipsis length ({ellipsis.Length}).",
                    nameof(length));
            if (str == null) return null;
            if (str.Length > length)
            {
                var newLength = length - ellipsis.Length;
                return str.Substring(0, newLength) + ellipsis;
            }
            return str;
        }

        /// <summary>
        ///     Trim the string and replace whitespaces between words by a single space (collapse spaces).
        /// </summary>
        /// <param name="str">Source string.</param>
        /// <returns>
        ///     Trimmed string and collapsed spaces or an empty string if source string
        ///     is null, empty or whitespaces only.</returns>
        [CanBeNull]
        public static string TrimCollapse([CanBeNull] this string str)
        {
            if (str == null) return null;
            if (str.Length == 0) return string.Empty;
            var builder = new StringBuilder();
            for (var i = 0; i < str.Length; ++i)
            {
                if (!Char.IsWhiteSpace(str, i))
                {
                    var b = i;
                    while (++i < str.Length && !Char.IsWhiteSpace(str, i)) { }
                    builder.Append(" ").Append(str.Substring(b, i - b));
                }
            }
            return builder.Length > 0
                ? builder.ToString(1, builder.Length - 1)
                : string.Empty;
        }
    }
}
