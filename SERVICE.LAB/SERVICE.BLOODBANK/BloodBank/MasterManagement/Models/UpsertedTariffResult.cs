using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterManagement.Models
{
    public record struct UpsertedTariffResult(bool IsNewlyCreated);
}