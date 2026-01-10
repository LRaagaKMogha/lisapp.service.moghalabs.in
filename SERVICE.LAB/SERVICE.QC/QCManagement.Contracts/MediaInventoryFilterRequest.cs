namespace QCManagement.Contracts
{
    public record MediaInventoryFilterRequest(
        DateTime? StartDate,
        DateTime? EndDate,
        bool showAllActive,
        Int64? MediaId, 
        string? MediaLotNo,
        string? ActiveStatus
    );

}