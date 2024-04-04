namespace Hospital.Domain.Objects
{
    public class PagedList<T> : IPagedList<T>
    {
        /// <summary>
        /// Current page
        /// </summary>
        public int Page { get; }

        /// <summary>
        /// Total records on 1 page
        /// </summary>
        public int Records { get; }

        /// <summary>
        /// Total records
        /// </summary>
        public int TotalCount { get; }

        /// <summary>
        /// Total page
        /// </summary>
        public int Total => (int)Math.Ceiling((float)TotalCount / (float)Records);

        /// <summary>
        /// Has previous page
        /// </summary>
        public bool HasPreviousPage => Page > 1;

        /// <summary>
        /// Has next page
        /// </summary>
        public bool HasNextPage => Page < Total;

        /// <summary>
        /// Data
        /// </summary>
        public List<T> Rows { get; } = new List<T>();

        public PagedList(List<T> source, int pageIndex, int pageSize, int totalCount)
        {
            Page = pageIndex;
            Records = pageSize;
            TotalCount = totalCount;
            Rows.AddRange(source);
        }
    }
}
