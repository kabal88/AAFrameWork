namespace Helpers
{
    public static class StringExtensions
    {
        public static string ToCamelCase(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            return char.ToLower(str[0]) + str.Substring(1);
        }

        public static string ToPascalCase(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            return char.ToUpper(str[0]) + str.Substring(1);
        }


        public static bool IsNullOrEmpty(this string str)
        {
            var result = string.IsNullOrEmpty(str);

            return result;
        }
        
        public static string Bold(this string str)
        {
            return "<b>" + str + "</b>";
        }

        public static string Color(this string str, string clr)
        {
            return $"<color={clr}>{str}</color>";
        }

        public static string Italic(this string str)
        {
            return "<i>" + str + "</i>";
        }

        public static string Size(this string str, int size)
        {
            return $"<size={size}>{str}</size>";
        }

        public static string NextLine(this string str)
        {
            return str + "\n";
        }
    }
}