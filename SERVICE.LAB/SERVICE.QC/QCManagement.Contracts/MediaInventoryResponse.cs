namespace QCManagement.Contracts
{
    public record MediaInventoryResponse(
        long Identifier,
        long BatchId,
        DateTime ReceivedDateAndTime,
        long MediaId,
        string MediaLotNumber,
        DateTime ExpirationDateAndTime,
        string Colour,
        string Crack,
        string Contaminate,
        string Leakage,
        string Turbid,
        string VolumeOrAgarThickness,
        string Sterlity,
        string Vividity,
        string Remarks,
        string Status,
        long ModifiedBy,
        string ModifiedByUserName,
        DateTime LastModifiedDateTime,
        bool IsActive
    );
}
