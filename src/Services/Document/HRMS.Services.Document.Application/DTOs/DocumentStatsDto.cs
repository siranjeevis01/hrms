namespace HRMS.Services.Document.Application.DTOs;

public class DocumentStatsDto
{
    public int TotalDocuments { get; set; }
    public int ActiveDocuments { get; set; }
    public int ArchivedDocuments { get; set; }
    public int DeletedDocuments { get; set; }
    public long TotalFileSize { get; set; }
    public int TotalFolders { get; set; }
    public int TotalTemplates { get; set; }
    public int TotalShares { get; set; }
    public int TotalAccessLogs { get; set; }
    public Dictionary<string, int> DocumentsByCategory { get; set; } = new();
    public Dictionary<string, int> DocumentsByContentType { get; set; } = new();
}
