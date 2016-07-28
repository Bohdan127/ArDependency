namespace DataParser.Extensions
{
    public static class MarathonTags
    {
        public static readonly string NameTeam = "member-name nowrap";// <div class="today-member-name nowrap " data-ellipsis="{}">;
        public static readonly string Date = "<td class=\"date\">";
        public static readonly string EventID = "data-event-treeId";
        public static readonly string Coff = "data-selection-price=\"";
        public static readonly string TypeCoff = "<span class=\"hint\">";
        public static readonly string Fora = "data-market-type=\"HANDICAP\"";
        public static readonly string Total = "data-market-type=\"TOTAL\"";
        public static readonly string Liga = "<span class=\"nowrap\">";
    }
}