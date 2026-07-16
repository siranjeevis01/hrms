using HRMS.Shared.Kernel.Enums;

namespace HRMS.Shared.Kernel.Common;

public class PaginationRequest
{
    private int _pageNumber = 1;
    private int _pageSize = 10;

    public int PageNumber
    {
        get => _pageNumber;
        set => _pageNumber = value < 1 ? 1 : value;
    }

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value is < 1 or > 100 ? 10 : value;
    }

    public string? SearchTerm { get; set; }
    public string? SortBy { get; set; }
    public SortOrder SortOrder { get; set; } = SortOrder.Ascending;

    public int Skip => (PageNumber - 1) * PageSize;
}
