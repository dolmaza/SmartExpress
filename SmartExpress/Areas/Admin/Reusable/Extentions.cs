using System;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;

namespace SmartExpress.Admin.Reusable
{
    public static class Extentions
    {
        public static string Shorten(this string str, int maxLength)
        {
            if (str.Length > maxLength)
            {
                str = $"{str.Substring(0, maxLength)} ...";
            }
            return str;
        }

        public static string StripHtml(this string str)
        {
            str = Regex.Replace(str, "<[^>]*(>|$)", string.Empty);
            return str;
        }

        public static decimal? ToDecimal(this string str)
        {
            decimal? dcm = null;
            if (str != null)
            {
                if (str.Contains("."))
                {
                    str = str.Replace(".", ",");

                }

                dcm = decimal.Parse(str);
            }
            return dcm;
        }

        public static string ToJson(this object obj)
        {
            try
            {
                var js = new JavaScriptSerializer();
                var json = js.Serialize(obj);
                return json;

            }
            catch (Exception)
            {
                return null;
            }


        }

        public static DateTime? ToDateTime(this string str)
        {
            var date = string.IsNullOrEmpty(str) ? (DateTime?)null : DateTime.Parse(str);

            return date;
        }

    }
}