namespace FlexibleParser
{
    internal partial class DatesInternal
    {
        //This method is only called from Common.PerformComparison, where all the preliminary/null checks were
        //already performed.
        public static int PerformComparisonValid(DateP first, DateP second)
        {
            return first.Value.CompareTo
            (
                second.AdaptTimeToOffset(first.TimeZoneOffset).Value
            );
        }
    }
}
