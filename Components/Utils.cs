using System;
using System.Text;
using System.Web;

namespace BigRedCloud.Api.Components
{
    internal static class Utils
    {
        public static string EncodeToBase64(string source)
        {
            byte[] encodedBytes = Encoding.ASCII.GetBytes(source);
            return Convert.ToBase64String(encodedBytes);
        }

        public static string ConvertTimestampToBase64UrlString(byte[] timestampAsBytes)
        {
            string timestampAsBase64String = Convert.ToBase64String(timestampAsBytes);
            return HttpUtility.UrlEncode(timestampAsBase64String);
        }
    }
}
