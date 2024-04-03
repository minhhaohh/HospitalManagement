namespace Hospital.Domain.Objects
{
    public interface IPagedList<T> : IEnumerable<T>
    {
        int Page { get; }

        int Records { get; }

        int TotalCount { get; }

        int Total { get; }

        bool HasPreviousPage { get; }

        bool HasNextPage { get; }

        List<T> Rows { get; }
    }
}
