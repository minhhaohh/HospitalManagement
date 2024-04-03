namespace Hospital.Domain.Objects
{
    public interface IPagingParams
    {
        int PageIndex { get; set; }
        int PageSize { get; set; }
    }
}
