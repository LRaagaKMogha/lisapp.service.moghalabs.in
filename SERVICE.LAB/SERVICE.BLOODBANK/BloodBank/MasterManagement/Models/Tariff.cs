using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MasterManagement.Contracts;

namespace MasterManagement.Models
{
    public class Tariff
    {
        public Int64 Id { get; set; }
        public Int64 ProductId { get; set; }
        public Int64 ResidenceId { get; set; }
        public decimal MRP { get; set; }
        public DateTime LastModifiedDateTime { get; set; } = DateTime.Now;
        public bool IsActive { get; set; }
        public string ServiceType { get; set; }
        public Tariff()
        {

        }
        public Tariff(Int64 identifier, Int64 productId, Int64 residenceId, decimal mrp, bool isActive, string serviceType)
        {
            Id = identifier;
            ProductId = productId;
            ResidenceId = residenceId;
            MRP = mrp;
            IsActive = isActive;
            ServiceType = serviceType;
        }

        public static ErrorOr<Tariff> Create(Int64 productId, Int64 residenceId, decimal mrp, bool isActive, string serviceType, Int64? identifier = null)
        {
            List<Error> errors = new();
            if (errors.Count > 0)
            {
                return errors;
            }
            return new Tariff(identifier ?? 0, productId, residenceId, mrp, isActive, serviceType);
        }
        public static ErrorOr<Tariff> From(UpsertTariffRequest request)
        {
            return Create(request.ProductId, request.ResidenceId, request.MRP, request.IsActive, request.ServiceType);
        }

        public static ErrorOr<Tariff> From(Int64 id, UpsertTariffRequest request)
        {
            return Create(request.ProductId, request.ResidenceId, request.MRP, request.IsActive, request.ServiceType, id);
        }

    }
}