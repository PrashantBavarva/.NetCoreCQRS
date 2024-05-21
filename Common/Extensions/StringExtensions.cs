using System.Text;
using System.Text.Json;

namespace Common.Extensions;


    public static class StringExtensions
    {
        public static string IfNullOrEmpty(this string str, string defaultVal) => string.IsNullOrEmpty(str) ? defaultVal : str;
        public static bool IsNullOrEmpty(this string str) => string.IsNullOrEmpty(str);

        public static T Deserialize<T>(this string src) where T:class
        {
            return JsonSerializer.Deserialize<T>(src, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
        public static string Base64Encode(this string plainText) {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }       
        public static string Base64Decode(this string base64EncodedData) {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }
        public static string ToShortEnvName(this string env)
        {
            return env switch
            {
                "Development" => "dev",
                "Staging" => "stg",
                "Production" => "prod",
                _ => env
            };
        }
    }