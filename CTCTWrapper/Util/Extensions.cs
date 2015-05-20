using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.IO;
using Newtonsoft.Json;

namespace CTCT.Util
{
    /// <summary>
    /// Extansions class.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// ISO-8601 date time format string.
        /// </summary>
        public const string ISO8601 = "yyyy-MM-ddTHH:mm:ss.fffZ";

        /// <summary>
        /// Converts a DateTime object to an ISO8601 representation.
        /// </summary>
        /// <param name="dateTime">DateTime.</param>
        /// <returns>Returns the ISO8601 string representation for the provided datetime.</returns>
        public static string ToISO8601String(this DateTime? dateTime)
        {
            return dateTime.HasValue ? dateTime.Value.ToString(ISO8601, CultureInfo.InvariantCulture) : null;
        }

        /// <summary>
        /// Gets the DateTime from an ISO8601 string.
        /// </summary>
        /// <param name="str">String.</param>
        /// <returns>Returns a datetime object.</returns>
        public static DateTime? FromISO8601String(this string str)
        {
            DateTime dt;

            return DateTime.TryParseExact(str, ISO8601, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt) ? dt : (DateTime?)null;
        }

        /// <summary>
        /// Converts a list of strings to JSON representation.
        /// </summary>
        /// <param name="list">The string list.</param>
        /// <returns>Returns the JSON representation of the list.</returns>
        public static string ToJSON(this IList<string> list)
        {
            var json = JsonConvert.SerializeObject(list);
            return json;
        }

        /// <summary>
        /// Converts a list T of type Component to JSON representation.
        /// </summary>
        /// <typeparam name="T">Type of Component</typeparam>
        /// <param name="list">The string list.</param>
        /// <returns>Returns the JSON representation of the list.</returns>
        public static string ToJSON<T>(this IList<T> list) where T : Components.Component
        {
            var json = JsonConvert.SerializeObject(list);
            return json;
        }

        /// <summary>
        /// Converts a string to its enum representation.
        /// </summary>
        /// <typeparam name="T">Enum type.</typeparam>
        /// <param name="s">String to convert.</param>
        /// <returns>Returns the enum value.</returns>
        public static T ToEnum<T>(this string s) where T : struct, IConvertible
        {
            T t;
            return (TryParseEnum<T>(s, true, out t) ? t : default(T));
        }

        public static bool IsNullOrWhiteSpace(this string value)
        {
            if (value == null) return true;
            return string.IsNullOrEmpty(value.Trim());
        }

        private static bool TryParseEnum<TEnum>(string value, bool ignoreCase, out TEnum result) where TEnum : struct
        {

            var enumType = typeof(TEnum);
            if (!enumType.IsEnum)
                throw new ArgumentException(string.Format("Type '{0}' is not an enum", enumType.FullName));




            if (IsNullOrWhiteSpace(value))
            {
                result = default(TEnum);
                return false;
            }



            try
            {
                result = (TEnum)Enum.Parse(enumType, value, ignoreCase);
                return true;
            }
            catch (Exception)
            {
                result = default(TEnum);
                return false;
            }


        }
    }
}
