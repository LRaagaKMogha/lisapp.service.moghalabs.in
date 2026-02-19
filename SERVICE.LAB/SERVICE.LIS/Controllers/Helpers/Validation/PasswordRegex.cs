using System.Text.RegularExpressions;

namespace Service.API.SERVICE.Controllers
{
    public static partial class PasswordRegex
    {
        [GeneratedRegex(@"^(=|\+|\-|@)")]
        public static partial Regex CsvCheck();

        [GeneratedRegex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{12,})")]
        public static partial Regex PasswordRule();
    }
}