using System;
using System.Text.RegularExpressions;

namespace Restaurant365.Utility
{
    public static class ExtensionMethods
    {
        public static List<string> GetDelimiterList(this string input, bool supportNewLineDelimiter)
        {
            var delimiterList = new string[] { "," }.ToList();

            //Add new line delimiter, if requested
            if (supportNewLineDelimiter)
                delimiterList.Add(@"\n");

            if (input != null && input.StartsWith("//"))
            {
                //Retrieve the delimiter string
                var delimiterInput = input.Between(@"//", @"\n"); 

                //retrieve single custom delimiter of any length or single character
                var singleDelimiterFromDelimiterInput = delimiterInput.StartsWith("[") ? delimiterInput.Between("[", "]") : delimiterInput[0].ToString(); 

                if (!string.IsNullOrEmpty(singleDelimiterFromDelimiterInput) && !delimiterList.Contains(singleDelimiterFromDelimiterInput))
                {
                    delimiterList.Add(singleDelimiterFromDelimiterInput.ToString());
                }

            }

            return delimiterList;    
        }

        static string Between(this string input, string charFrom, string charTo)
        {
            int posFrom = input.IndexOf(charFrom) + charFrom.Length - 1;
            if (posFrom != -1) //if found char
            {
                int posTo = input.IndexOf(charTo, posFrom + 1);
                if (posTo != -1) //if found char
                {
                    return input.Substring(posFrom + 1, posTo - posFrom - 1);
                }
            }

            return string.Empty;
        }
    }
}