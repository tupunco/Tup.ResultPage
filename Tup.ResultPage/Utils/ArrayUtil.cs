using System;
using System.Collections.Generic;

namespace Tup.ResultPage.Utils
{
    /// <summary>
    /// Array 工具类
    /// </summary>
    public static class ArrayUtil
    {
        /// <summary>Converts an array of one type to an array of another type.</summary>
        /// <returns>An array of the target type containing the converted elements from the source array.</returns>
        /// <param name="array">The one-dimensional, zero-based <see cref="T:System.Array" /> to convert to a target type.</param>
        /// <param name="converter">A <see cref="T:System.Converter`2" /> that converts each element from one type to another type.</param>
        /// <typeparam name="TInput">The type of the elements of the source array.</typeparam>
        /// <typeparam name="TOutput">The type of the elements of the target array.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        ///   <paramref name="array" /> is null.-or-<paramref name="converter" /> is null.</exception>
        public static TOutput[] ConvertAll<TInput, TOutput>(this TInput[] array, Converter<TInput, TOutput> converter)
        {
            if (array == null)
                throw new ArgumentNullException("array");
            if (converter == null)
                throw new ArgumentNullException("converter");

            TOutput[] array2 = new TOutput[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                array2[i] = converter(array[i]);
            }
            return array2;
        }
        /// <summary>Determines whether the specified array contains elements that match the conditions defined by the specified predicate.</summary>
        /// <returns>true if <paramref name="array" /> contains one or more elements that match the conditions defined by the specified predicate; otherwise, false.</returns>
        /// <param name="array">The one-dimensional, zero-based <see cref="T:System.Array" /> to search.</param>
        /// <param name="match">The <see cref="T:System.Predicate`1" /> that defines the conditions of the elements to search for.</param>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        ///   <paramref name="array" /> is null.-or-<paramref name="match" /> is null.</exception>
        public static bool Exists<T>(this T[] array, Predicate<T> match)
        {
            return ArrayUtil.FindIndex<T>(array, match) != -1;
        }

        #region FindIndex
        /// <summary>Searches for an element that matches the conditions defined by the specified predicate, and returns the zero-based index of the first occurrence within the entire <see cref="T:System.Array" />.</summary>
        /// <returns>The zero-based index of the first occurrence of an element that matches the conditions defined by <paramref name="match" />, if found; otherwise, –1.</returns>
        /// <param name="array">The one-dimensional, zero-based <see cref="T:System.Array" /> to search.</param>
        /// <param name="match">The <see cref="T:System.Predicate`1" /> that defines the conditions of the element to search for.</param>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        ///   <paramref name="array" /> is null.-or-<paramref name="match" /> is null.</exception>
        public static int FindIndex<T>(this T[] array, Predicate<T> match)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }
            return ArrayUtil.FindIndex<T>(array, 0, array.Length, match);
        }
        /// <summary>Searches for an element that matches the conditions defined by the specified predicate, and returns the zero-based index of the first occurrence within the range of elements in the <see cref="T:System.Array" /> that starts at the specified index and contains the specified number of elements.</summary>
        /// <returns>The zero-based index of the first occurrence of an element that matches the conditions defined by <paramref name="match" />, if found; otherwise, –1.</returns>
        /// <param name="array">The one-dimensional, zero-based <see cref="T:System.Array" /> to search.</param>
        /// <param name="startIndex">The zero-based starting index of the search.</param>
        /// <param name="count">The number of elements in the section to search.</param>
        /// <param name="match">The <see cref="T:System.Predicate`1" /> that defines the conditions of the element to search for.</param>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        ///   <paramref name="array" /> is null.-or-<paramref name="match" /> is null.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="array" />.-or-<paramref name="count" /> is less than zero.-or-<paramref name="startIndex" /> and <paramref name="count" /> do not specify a valid section in <paramref name="array" />.</exception>
        public static int FindIndex<T>(this T[] array, int startIndex, int count, Predicate<T> match)
        {
            if (array == null)
                throw new ArgumentNullException("array");
            if (startIndex < 0 || startIndex > array.Length)
                throw new ArgumentOutOfRangeException("startIndex", "ArgumentOutOfRange_Index");
            if (count < 0 || startIndex > array.Length - count)
                throw new ArgumentOutOfRangeException("count", "ArgumentOutOfRange_Count");
            if (match == null)
                throw new ArgumentNullException("match");

            int num = startIndex + count;
            for (int i = startIndex; i < num; i++)
            {
                if (match(array[i]))
                {
                    return i;
                }
            }
            return -1;
        }
        #endregion
    }
}
