using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Attendance.Domain.Entities;

public class WifiNetwork : BaseEntity
{
    public Guid CompanyId { get; private set; }
    public Guid? BranchId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Ssid { get; private set; } = string.Empty;
    public string Bssid { get; private set; } = string.Empty;
    public bool IsPrimary { get; private set; }
    public bool IsActive { get; private set; }

    private WifiNetwork() { }

    public static WifiNetwork Create(
        Guid companyId,
        Guid? branchId,
        string name,
        string ssid,
        string bssid,
        bool isPrimary = false,
        Guid? tenantId = null)
    {
        var network = new WifiNetwork
        {
            CompanyId = companyId,
            BranchId = branchId,
            Name = name,
            Ssid = ssid,
            Bssid = bssid.ToUpperInvariant(),
            IsPrimary = isPrimary,
            IsActive = true
        };

        if (tenantId.HasValue)
            network.TenantId = tenantId.Value;

        return network;
    }

    public void Update(string name, string ssid, string bssid, bool isPrimary)
    {
        Name = name;
        Ssid = ssid;
        Bssid = bssid.ToUpperInvariant();
        IsPrimary = isPrimary;
    }

    public void Activate() => IsActive = true;

    public void Deactivate() => IsActive = false;

    public bool Matches(string ssid, string? bssid = null)
    {
        if (!string.Equals(Ssid, ssid, StringComparison.OrdinalIgnoreCase))
            return false;

        if (string.IsNullOrEmpty(bssid))
            return true;

        return string.Equals(Bssid, bssid.ToUpperInvariant(), StringComparison.OrdinalIgnoreCase);
    }
}
