using System.ComponentModel.DataAnnotations;

namespace Service.Model.Integration
{
    public enum nsourcesystem
    {
        RCMS = 1,
        SAP = 2,
        EMR = 3,
        BB = 4,
        LIS = 5,
        RC = 6,
        MA = 7
    }
    public enum nitemtype
    {
        Test = 1,
        Package = 2,
        OptionalPackage = 3,
        OptedOut = 4
    }
    public enum ngender
    {
        Male = 1,
        Female = 2,
        Unknown = 3
    }
    public enum nnatureofrequest
    {
        Routine = 1,
        Urgent = 2
    }
    public enum nnatureofspecimen
    {
        Fasting = 1,
        NonFasting = 2
    }

    public class RequiredRCMSAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var visit = (visitdetails)validationContext.ObjectInstance;

            if (visit != null)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("This field is mandatory for RCMS");
            }
        }
    }

    public class RequiredEMRAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var visit = (visitdetails)validationContext.ObjectInstance;

            if (visit != null)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("This field is mandatory for EMR");
            }
        }
    }
    public class RequiredBBAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var visit = (patientdetails)validationContext.ObjectInstance;

            //if (visit != null && visit.sourcesystem == nsourcesystem.BB)
            //{
            //    return ValidationResult.Success;
            //}
            //else
            //{
            //    return new ValidationResult("This field is mandatory for EMR");
            //}
            return ValidationResult.Success;
        }
    }
}
