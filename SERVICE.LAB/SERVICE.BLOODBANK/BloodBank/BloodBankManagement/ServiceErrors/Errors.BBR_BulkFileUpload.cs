using ErrorOr;

namespace BloodBankManagement.ServiceErrors
{
    public static partial class Errors
    {
        public static class BBRBulkFileUpload
        {
            public static Error FileFormat => Error.NotFound(
                code: "BulkFileUpload.FileType",
                description: "Invalid file format");
        }
    }
}
