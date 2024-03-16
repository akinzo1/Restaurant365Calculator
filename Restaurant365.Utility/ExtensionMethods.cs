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
                var delimiterInput = input.Between(@"//", @"\n").FirstOrDefault() ?? ""; 

                //retrieve single custom delimiter or multiple delimiters of multiple characters
                var singleDelimiterFromDelimiterInput = delimiterInput.StartsWith("[") ? delimiterInput.Between("[", "]") : new List<string> { delimiterInput[0].ToString() };

                if (singleDelimiterFromDelimiterInput.Any())
                {
                    delimiterList.AddRange(singleDelimiterFromDelimiterInput);
                }

            }

            return delimiterList.Distinct().ToList();    
        }

        private static List<string> Between(this string body, string start, string end)
        {
            var matched = new List<string>();

            int indexStart;
            int indexEnd;

            bool exit = false;
            while (!exit)
            {
                indexStart = body.IndexOf(start);

                if (indexStart != -1)
                {
                    indexEnd = indexStart + body.Substring(indexStart).IndexOf(end);
                    matched.Add(body.Substring(indexStart + start.Length, indexEnd - indexStart - start.Length));
                    body = body.Substring(indexEnd + end.Length);
                }
                else
                {
                    exit = true;
                }
            }

            return matched;
        }
    }
}