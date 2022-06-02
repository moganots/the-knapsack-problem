using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.utilities
{
    public static class Helpers
    {
        public static bool IsSet(this object value)
        {
            return !(value == null);
        }
        public static bool IsNotSet(this object value)
        {
            return !IsSet(value);
        }
        public static bool IsSet(this string value)
        {
            return !string.IsNullOrEmpty(value);
        }
        public static bool IsNotSet(this string value)
        {
            return !IsSet(value);
        }
        public static bool HasItems(this object[] array)
        {
            return IsSet(array) && !(array.Length == 0);
        }
        public static bool IsEmpty(this object[] array)
        {
            return !HasItems(array);
        }
        public static bool HasItems(this string[] array)
        {
            return IsSet(array) && !(array.Length == 0);
        }
        public static bool IsEmpty(this string[] array)
        {
            return !HasItems(array);
        }
        public static bool HasItems<T>(this IEnumerable<T> list)
        {
            return IsSet(list) && !(list.Count() == 0);
        }
        public static bool IsEmpty<T>(this IEnumerable<T> list)
        {
            return !HasItems(list);
        }
        public static bool HasItems<T>(this List<T> list)
        {
            return IsSet(list) && !(list.Count == 0);
        }
        public static bool IsEmpty<T>(this List<T> list)
        {
            return !HasItems(list);
        }
        public static List<T> IfEmpty<T>(this List<T> list, List<T> useList = default)
        {
            return (!IsSet(list) || !HasItems(list)) ? useList : list;
        }
        public static IEnumerable<T> IfEmpty<T>(this IEnumerable<T> list, List<T> useList = default)
        {
            return (!IsSet(list) || !HasItems(list)) ? useList : list;
        }
        public static object GetElementAt(this string value, int index, char delimiter = ',')
        {
            var values = IsSet(value) ? value.Split(delimiter) : default;
            return HasItems(values) && IsInRange(index, values.Length) ? values[index] : default;
        }
        public static bool IsInRange(this int index, int length)
        {
            return index <= length;
        }
        public static string AsString(this object value)
        {
            return value.ToString();
        }
        public static int AsInt32(this object value)
        {
            int result;
            return (Int32.TryParse(AsString(value), out result)) ? result: 0;
        }
    }
}
