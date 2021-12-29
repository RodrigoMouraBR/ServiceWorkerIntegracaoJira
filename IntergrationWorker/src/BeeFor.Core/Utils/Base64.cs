namespace BeeFor.Core.Utils
{
    public class Base64
    {
        public static string Encode(string normalString, bool urlEncode)
        {
            byte[] normalStringBytes =
                    System.Text.Encoding.UTF8.GetBytes(normalString);

            string base64String =
                    System.Convert.ToBase64String(normalStringBytes);

            if (urlEncode == true)
            {
                base64String =
                      System.Web.HttpUtility.UrlEncode(base64String);

            }

            return base64String;
        }

        public static string Decode(string base64String, bool urlDecode)
        {
            if (urlDecode == true)
            {
                base64String =
                      System.Web.HttpUtility.UrlDecode(base64String);

            }

            byte[] normalStringBytes =
                    System.Convert.FromBase64String(base64String);

            string normalString =
                    System.Text.Encoding.UTF8.GetString(normalStringBytes);

            return normalString;
        }
    }
}
