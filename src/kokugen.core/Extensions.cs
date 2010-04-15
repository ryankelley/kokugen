using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using FubuMVC.Core;
using Kokugen.Core.Domain;

namespace Kokugen.Core
{
    public static class Extensions
    {
        public static bool IsDateTime(this Type type)
        {
            return typeof (DateTime) == type.GetType();
        }

        public static bool IsIntegerBased(this Type type)
        {
            return type == typeof (Int32) || type == typeof(Int16) || type == typeof(Int64) || type == typeof(int);
        }

        public static string ToGravatarHash(this string email)
        {
            var hasher = new System.Security.Cryptography.MD5CryptoServiceProvider();
            var hash = new StringBuilder();

            foreach (byte b in hasher.ComputeHash(Encoding.UTF8.GetBytes(email.ToLower())))
            {
                hash.Append(b.ToString("x2").ToLower());
            }

            return hash.ToString();
        }

        public static bool IsFloatingPoint(this Type type)
        {
            return type == typeof (decimal) || type == typeof (double) || type == typeof (Decimal) || type == typeof (Double) || type == typeof (float);
        }
        public static bool ContainsEntity<T>(this IList<T> list, Domain.Entity value)
            where T : Domain.Entity
        {
            foreach (var entity in list)
            {
                if (entity.Id == value.Id) return true;
            }
            return false;
        }

        public static bool IsNotEmpty(this Guid value)
        {
            if (value == Guid.Empty) return false;

            return true;
        }

        public static bool IsEmpty(this Guid value)
        {
            if (value == null || value == Guid.Empty) return true;

            return false;
        }


        public static bool IsYear(this string value)
        {
            var isYear = new Regex(@"^(19|20)\d{2}$", RegexOptions.Compiled);
            var success = isYear.IsMatch(value);
            return success;
        }

        public static string ConvertToValidWebAddress(this string value)
        {
            if (string.IsNullOrEmpty(value)) return "";

            var https = "https://";
            var http = "http://";
            var isHttps = false;

            if (!string.IsNullOrEmpty(value) && value.Contains(https)) isHttps = true;

            value = value.Replace(https, "");
            value = value.Replace(http, "");

            if (isHttps)
            {
                return https + value;
            }
            return http + value;
        }

        public static bool IsGuid(this string value)
        {
            if (value == null) return false;

            var isGuid = new Regex(@"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$", RegexOptions.Compiled);
            return (isGuid.IsMatch(value));
        }

        public static string FormatAsSlug(this string value)
        {
            value = Regex.Replace(value, @"[^A-Za-z0-9\s-]", "").Trim();
            return Regex.Replace(value, @"[\s-]+", "-");
        }

        public static string RemoveAdwords(this string inputString)
        {
            return Regex.Replace(inputString, @"((^|\s)ad(vert)?[0-9A-Za-z]*)", @"$2", RegexOptions.IgnoreCase);
        }

        //Todo: Let's make sure we really need FormatAsPermalink
        //Note: This really isn't an extension method
        public static string FormatAsPermalink(string Year, string Month, string Day, string Slug)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}/{1}/{2}/{3}", Year, Month, Day, Slug);
        }

        public static string ToAbbreviation(this string value)
        {
            return value = Regex.Replace(value, @"[^A-Z]", "").Trim();
        }

        public static Guid ToGuid(this string value)
        {
            if (value == null) return Guid.Empty;
            return value.IsGuid() ? new Guid(value) : Guid.Empty;
        }

        public static string ToShortDateAndTimeString(this DateTime dateTime)
        {
            return dateTime.ToShortDateString() + " " + dateTime.ToShortTimeString();
        }

        public static bool CheckEquality<ENTITYTYPE>(this Domain.Entity source, object obj) where ENTITYTYPE : Domain.Entity
        {
            var other = obj as ENTITYTYPE;

            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(source, other)) return true;

            var otherIsTransient = Equals(other.Id, default(Guid));
            var thisIsTransient = Equals(source.Id, default(Guid));

            if (otherIsTransient && thisIsTransient)
                return ReferenceEquals(source, other);

            return other.Id.Equals(source.Id);
        }

        public static Random random = new Random();

        public static IList<T> Shuffle<T>(this IList<T> toShuffle)
        {
            var deck = new List<T>(toShuffle);
            var n = deck.Count;

            for (int i = 0; i < n; ++i)
            {
                int r = i + (int)(random.Next(n - i));
                T t = deck[r];
                deck[r] = deck[i];
                deck[i] = t;
            }

            return deck;
        }


        public static bool HasAttribute(this MemberInfo type, MemberInfo attributeType)
        {
            var hasAttribute = false;
            type.GetCustomAttributes(false).Each(attribute => { if (attribute.GetType() == attributeType) hasAttribute = true; });
            return hasAttribute;
        }

        public static IEnumerable<T> NaturalSort<T>(this IEnumerable<T> toBeSorted)
        {
            var baseList = toBeSorted as List<T>;
            if (baseList != null)
            {
                using (var comparer = new NaturalComparer())
                {
                    baseList.Sort(comparer as IComparer<T>);
                }
                return baseList.AsEnumerable();
            }
            return toBeSorted;
        }

        //[DebuggerStepThrough]
        //public static void Each<T>(this IEnumerable<T> values, Action<T> eachAction)
        //{
        //    foreach (T item in values)
        //    {
        //        eachAction(item);
        //    }
        //}

        //[DebuggerStepThrough]
        //public static IEnumerable Each(this IEnumerable values, Action<object> eachAction)
        //{
        //    foreach (object item in values)
        //    {
        //        eachAction(item);
        //    }

        //    return values;
        //}
    }      
}