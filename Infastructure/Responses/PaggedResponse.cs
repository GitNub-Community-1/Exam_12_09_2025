namespace Infastructure.Responses;

public class PagedResponse<T>
{
    public T Data { get; set; } = default!;
    public int Page { get; set; }
    public int Size { get; set; }
    public int TotalRecords { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalRecords / Size);
}