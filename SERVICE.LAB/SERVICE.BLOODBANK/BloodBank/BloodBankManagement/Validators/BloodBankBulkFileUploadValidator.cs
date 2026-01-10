using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloodBankManagement.Contracts;
using BloodBankManagement.Helpers;
using BloodBankManagement.Models;
using BloodBankManagement.ServiceErrors;
using BloodBankManagement.Services.StartupServices;
using FluentValidation;
using Shared;

namespace BloodBankManagement.Validators
{
    public class BloodBankBulkFileUploadValidator : BaseValidator<List<BulkFileUpload>>
    {
        public BloodBankBulkFileUploadValidator(HttpContext httpContext) : base(httpContext)
        {
            RuleFor(x => x)           
            .Must(x => 
            {
                return x.All(fileUpload => fileUpload.FileType == "jpg" || fileUpload.FileType == "jpeg" || fileUpload.FileType == "png" || fileUpload.FileType == "pdf" ||
                fileUpload.FileType == "xls" || fileUpload.FileType == "xlsx" || fileUpload.FileType == "doc" || fileUpload.FileType == "docx");
            }
            ).WithErrorCode(Errors.BBRBulkFileUpload.FileFormat.Code).WithMessage(Errors.BBRBulkFileUpload.FileFormat.Description);
        }
    }
}
