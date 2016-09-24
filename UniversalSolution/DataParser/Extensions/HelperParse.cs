using FormulasCollection.Models;

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
        public static string TagsContent(this string line, string nameTag)
        {
            string findTag = "";
            int index = 0;
            bool selectContent = false;
            string result = "";
            foreach (var l in line)
            {
                if (findTag.Length > nameTag.Length)
                    return null;
                if (findTag == nameTag)
                {
                    if (selectContent && l != '\"')
                        result += l;
                    if (l == '\"')
                        selectContent = !selectContent;
                    if (!selectContent && !string.IsNullOrEmpty(result))
                        return result;
                }
                else
                {
                    if (l == nameTag[index])
                    {
                        findTag += l;
                        index++;
                    }
                }
            }
            return result;


        }

        public static string TagsContent2(this string line, string nameTag)
        {
            string findTag = "";
            int index = 0;
            bool selectContent = false;
            string result = "";
            foreach (var l in line)
            {
                if (findTag.Length > nameTag.Length)
                    return null;
                if (findTag == nameTag)
                {
                    if (selectContent && l != '\'')
                        result += l;
                    if (l == '\'')
                        selectContent = !selectContent;
                    if (!selectContent && !string.IsNullOrEmpty(result))
                        return result;
                }
                else
                {
                    if (l == nameTag[index])
                    {
                        findTag += l;
                        index++;
                    }
                }
            }
            return result;


        }

        public static bool CheckFullData(this DataMarathonForAutoPlays obj)
        {
            return obj != null &&
                   obj.cid != null &&
                   obj.epr != null &&
                   obj.ewc != null &&
                   obj.ewf != null &&
                   obj.mn != null &&
                   obj.prices != null &&
                   obj.prt != null &&
                   obj.selection_key != null &&
                   obj.sn != null;
        }
    }
}