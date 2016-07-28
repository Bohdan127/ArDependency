namespace DataParser.Extensions
{
    public static class HelperParse
    {
        public static string Substrings(this string line, string start, string end = "</")
        {
            string replaceStartElement = "@@";
            string replaceEndElement = "##";
            line = line.Replace(start, replaceStartElement).Replace(end, replaceEndElement);
            int indexStart = line.IndexOf(replaceStartElement) + replaceStartElement.Length;
            int indexEnd = line.IndexOf(replaceEndElement);
            return line.Substring(indexStart, indexEnd - indexStart);
        }

        public static bool _Contains(this string line, params string[] elements)
        {
            foreach (var e in elements)
                if (!line.Contains(e)) return false;
            return true;
        }

        public static string GetEventID(this string line)
        {
            string eventid = null;
            int start = line.IndexOf(MarathonTags.EventID) + MarathonTags.EventID.Length + 2;
            line = line.Substring(start);
            eventid = line.Substring(0, line.IndexOf("\""));
            return eventid;
        }
    }
}