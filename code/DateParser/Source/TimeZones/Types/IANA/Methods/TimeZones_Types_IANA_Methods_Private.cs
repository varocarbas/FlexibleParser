namespace FlexibleParser
{
    internal partial class TimeZoneIANAInternal
    {
        //Most of members of the TimeZoneIANAEnum are associated with different types of geographical information, 
        //but some of them (e.g., the ones starting with "Etc_") aren't. The goal of this method is to differentiate 
        //the members of this last group in order to avoid problematic situations like missing elements in certain
        //collection.
        private static bool HasNoGeo(TimeZoneIANAEnum iana)
        {
            string temp = iana.ToString().ToLower();

            return
            (
                temp.StartsWith("etc") || temp.StartsWith("pst") ||
                temp.StartsWith("mst") || temp.StartsWith("cst") ||
                temp.StartsWith("est")
            );
        }
    }
}
