namespace Hospital.Domain.Objects
{
    public interface IPagedList<T>
    {
        /// <summary>
        /// Current page
        /// </summary>
        int Page { get; }

        /// <summary>
        /// Total records on 1 page
        /// </summary>
        int Records { get; }

        /// <summary>
        /// Total records
        /// </summary>
        int TotalCount { get; }

        /// <summary>
        /// Total page
        /// </summary>
        int Total { get; }

        /// <summary>
        /// Has previous page
        /// </summary>
        bool HasPreviousPage { get; }

        /// <summary>
        /// Has next page
        /// </summary>
        bool HasNextPage { get; }

        /// <summary>
        /// Data
        /// </summary>
        List<T> Rows { get; }
    }
}
