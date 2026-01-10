using DEV.Model;
using DEV.Model.Common;
using DEV.Common;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Resources;
using System.Text.RegularExpressions;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Linq;

namespace DEV.API.SERVICE.Controllers
{
    public class TestMasterValidation
    {
        // Test Master //
        public static ErrorResponse InsertTest(objtest req)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();
            Regex _csvcheck = new Regex(@"^(=|\+|\-|@)");

            if(req.IsApproval == false || req.IsApproval == true && req.IsReject == false)
            {
                if (req.deptNo == 0)
                    errors.Add("Dept No is required");
                if (string.IsNullOrEmpty(req.testName) || req.testName.TrimStart() == string.Empty)
                    errors.Add("Test Name is required");
                if (string.IsNullOrEmpty(req.testDisplayName) || req.testDisplayName.TrimStart() == string.Empty)
                    errors.Add("Test Display Name is required");
                //if (string.IsNullOrEmpty(req.testCode) || req.testCode.TrimStart() == string.Empty)
                //    errors.Add("Test Code is Required");
                if (string.IsNullOrEmpty(req.gender) || req.gender.TrimStart() == string.Empty)
                    errors.Add("Gender is required");
                if (string.IsNullOrEmpty(req.resultType) || req.resultType.TrimStart() == string.Empty)
                    errors.Add("Result Type is Required");
                if (string.IsNullOrEmpty(req.fromDate) || req.fromDate.TrimStart() == string.Empty || req.fromDate == null)
                    errors.Add("From Date is Required");

                if (req.isFormulaFor == true)
                {
                    string formulaText = "";
                    bool isFormulaValid = ValidateFormula(formulaText);

                    if (!isFormulaValid)
                    {
                        errors.Add("Invalid formula");
                        return errorResponse;
                    }
                    else
                    {
                        errors.Add("Enter formula");
                        return errorResponse;
                    }
                }

                if (_csvcheck.IsMatch(req.testName ?? "") || _csvcheck.IsMatch(req.testDisplayName ?? "") ||
                    _csvcheck.IsMatch(req.testShortName ?? "") || _csvcheck.IsMatch(req.volume ?? "") ||
                    _csvcheck.IsMatch(req.barcodePrefix ?? "") || _csvcheck.IsMatch(req.barcodeSuffix ?? ""))
                    errors.Add("Special character not allowed");

                if (req.lsttestrefrange != null && req.lsttestrefrange.Any())
                {
                    var agetodaysinvalidlst = req.lsttestrefrange.Where(j => j.ageToType == "D" && j.ageTo > 73048).ToList();
                    var agetoyearsinvalidlst = req.lsttestrefrange.Where(j => j.ageToType == "Y" && j.ageTo > 200).ToList();
                    var agetomonthsinvalidlst = req.lsttestrefrange.Where(j => j.ageToType == "M" && j.ageTo > 2400).ToList();

                    if (agetodaysinvalidlst.Any() || agetoyearsinvalidlst.Any() || agetomonthsinvalidlst.Any())
                    {
                        errors.Add("Reference Range AgeTo shouldn't over than 200 years");
                    }
                }

                if (req.lsttestanalyrange != null && req.lsttestanalyrange.Any())
                {
                    var anaagetodaysinvalidlst = req.lsttestanalyrange.Where(j => j.ageToType == "D" && j.ageTo > 73048).ToList();
                    var anaagetoyearsinvalidlst = req.lsttestanalyrange.Where(j => j.ageToType == "Y" && j.ageTo > 200).ToList();
                    var anaagetomonthsinvalidlst = req.lsttestanalyrange.Where(j => j.ageToType == "M" && j.ageTo > 2400).ToList();

                    if (anaagetodaysinvalidlst.Any() || anaagetoyearsinvalidlst.Any() || anaagetomonthsinvalidlst.Any())
                    {
                        errors.Add("Analytical Measuring Range Age To shouldn't over than 200 years");
                    }
                }

                if (req.isSelectMultiSample == true)
                {
                    var selecteddata = req.lstmultisamplesreferencelist.Where(d => d.sampleNo <= 0).ToList();
                    if (selecteddata.Any())
                    {
                        errors.Add("Please Select Sample in Multi Sample List");
                    }
                }

                if (req.isSelectMultiSample == true && req.sampleNo == 0 || req.sampleNo.ToString().Trim() == string.Empty)
                {
                    errors.Add("Please select primary sample for the test");
                }

                if (req.lstmultisamplesreferencelist != null && req.lstmultisamplesreferencelist.Any() && req.sampleNo > 0 && req.containerNo > 0)
                {
                    var match = req.lstmultisamplesreferencelist
                        .Any(d => d.sampleNo == req.sampleNo && d.containerNo == req.containerNo);
                    if (match)
                    {
                        errors.Add("Selected sample and tube type should not be a primary sample and tube type");
                    }
                }

                if (req.lstmultisamplesreferencelist != null && req.lstmultisamplesreferencelist.Any() && req.sampleNo > 0 && req.containerNo > 0)
                {
                    //var match = req.lstmultisamplesreferencelist
                    //    .Any(d => d.sampleNo == req.sampleNo);
                    var dublicatecontainer = req.lstmultisamplesreferencelist.GroupBy(x => new { x.containerNo, x.sampleNo }).Where(x => x.Count() > 1).Select(x => x.Key).Count();
                    if (dublicatecontainer > 0)
                    {
                        errors.Add("Duplicate sample's with tube type is could not allowed");
                    }
                }

                if (req.decimalPoint.ToString().Trim() != string.Empty)
                {
                    if (req.decimalPoint.ToString().Contains('.'))
                    {
                        errors.Add("Decimal value not allowed in DecimalPoint");
                        req.decimalPoint = 0;
                    }
                    else if (req.decimalPoint > 5)
                    {
                        errors.Add("Decimal Point should be lesser than or equal to 5");
                        req.decimalPoint = 0;
                    }
                }

                DateTime fromDate, toDate;
                if (!string.IsNullOrEmpty(req.fromDate) && DateTime.TryParse(req.fromDate, out fromDate) &&
                    !string.IsNullOrEmpty(req.toDate) && DateTime.TryParse(req.toDate, out toDate))
                {
                    int outt = DateDiffInDays(fromDate, toDate);
                    if (outt < 0)
                    {
                        errors.Add("The From Date should be less than the To Date");
                        req.toDate = null;
                    }
                }

                if (req.DeltaRange < 0 || req.DeltaRange > 100)
                {
                    errors.Add("Invalid Percentage");
                    req.DeltaRange = 0;
                }
                else
                {
                    req.DeltaRange = Math.Round(req.DeltaRange, 2);
                }

                if (req.lsttestrefrange != null && req.lsttestrefrange.Any() && req.lsttestanalyrange != null && req.lsttestanalyrange.Any() &&
                    req.lstmultisamplesreferencelist != null && req.lstmultisamplesreferencelist.Any())
                {
                    var ageTestRef = req.lsttestrefrange.Where(i => i.ageFrom < 0 && i.ageTo < 0).ToList();
                    var ageAnalyRange = req.lsttestanalyrange.Where(j => j.ageFrom < 0 && j.ageTo < 0).ToList();
                    var ageMultiRefList = req.lstmultisamplesreferencelist.Where(k => k.ageFrom < 0 && k.ageTo < 0).ToList();

                    if (ageTestRef.Any() || ageAnalyRange.Any() || ageMultiRefList.Any())
                    {
                        errors.Add("Negative Age Values are not allowed");
                    }
                }
            }

            else if(req.IsApproval == true && req.IsReject == true)
            {
                if (req.deptNo == 0)
                    errors.Add("Dept No is required");
                if (string.IsNullOrEmpty(req.testName) || req.testName.TrimStart() == string.Empty)
                    errors.Add("Test Name is required");
                if (string.IsNullOrEmpty(req.testDisplayName) || req.testDisplayName.TrimStart() == string.Empty)
                    errors.Add("Test Display Name is required");
                //if (string.IsNullOrEmpty(req.testCode) || req.testCode.TrimStart() == string.Empty)
                //    errors.Add("Test Code is Required");
                if (string.IsNullOrEmpty(req.gender) || req.gender.TrimStart() == string.Empty)
                    errors.Add("Gender is required");
                if (string.IsNullOrEmpty(req.resultType) || req.resultType.TrimStart() == string.Empty)
                    errors.Add("Result Type is Required");
                if (string.IsNullOrEmpty(req.fromDate) || req.fromDate.TrimStart() == string.Empty || req.fromDate == null)
                    errors.Add("From Date is Required");

                if (req.isFormulaFor == true)
                {
                    string formulaText = "";
                    bool isFormulaValid = ValidateFormula(formulaText);

                    if (!isFormulaValid)
                    {
                        errors.Add("Invalid formula");
                        return errorResponse;
                    }
                    else
                    {
                        errors.Add("Enter formula");
                        return errorResponse;
                    }
                }

                if (_csvcheck.IsMatch(req.testName ?? "") || _csvcheck.IsMatch(req.testDisplayName ?? "") ||
                    _csvcheck.IsMatch(req.testShortName ?? "") || _csvcheck.IsMatch(req.volume ?? "") ||
                    _csvcheck.IsMatch(req.barcodePrefix ?? "") || _csvcheck.IsMatch(req.barcodeSuffix ?? ""))
                    errors.Add("Special character not allowed");

                if (req.lsttestrefrange != null && req.lsttestrefrange.Any())
                {
                    var agetodaysinvalidlst = req.lsttestrefrange.Where(j => j.ageToType == "D" && j.ageTo > 73048).ToList();
                    var agetoyearsinvalidlst = req.lsttestrefrange.Where(j => j.ageToType == "Y" && j.ageTo > 200).ToList();
                    var agetomonthsinvalidlst = req.lsttestrefrange.Where(j => j.ageToType == "M" && j.ageTo > 2400).ToList();

                    if (agetodaysinvalidlst.Any() || agetoyearsinvalidlst.Any() || agetomonthsinvalidlst.Any())
                    {
                        errors.Add("Reference Range AgeTo shouldn't over than 200 years");
                    }
                }

                if (req.lsttestanalyrange != null && req.lsttestanalyrange.Any())
                {
                    var anaagetodaysinvalidlst = req.lsttestanalyrange.Where(j => j.ageToType == "D" && j.ageTo > 73048).ToList();
                    var anaagetoyearsinvalidlst = req.lsttestanalyrange.Where(j => j.ageToType == "Y" && j.ageTo > 200).ToList();
                    var anaagetomonthsinvalidlst = req.lsttestanalyrange.Where(j => j.ageToType == "M" && j.ageTo > 2400).ToList();

                    if (anaagetodaysinvalidlst.Any() || anaagetoyearsinvalidlst.Any() || anaagetomonthsinvalidlst.Any())
                    {
                        errors.Add("Analytical Measuring Range Age To shouldn't over than 200 years");
                    }
                }

                if (req.isSelectMultiSample == true)
                {
                    var selecteddata = req.lstmultisamplesreferencelist.Where(d => d.sampleNo <= 0).ToList();
                    if (selecteddata.Any())
                    {
                        errors.Add("Please Select Sample in Multi Sample List");
                    }
                }

                if (req.isSelectMultiSample == true && req.sampleNo == 0 || req.sampleNo.ToString().Trim() == string.Empty)
                {
                    errors.Add("Please select primary sample for the test");
                }

                if (req.lstmultisamplesreferencelist != null && req.lstmultisamplesreferencelist.Any() && req.sampleNo > 0 && req.containerNo > 0)
                {
                    var match = req.lstmultisamplesreferencelist
                        .Any(d => d.sampleNo == req.sampleNo && d.containerNo == req.containerNo);
                    if (match)
                    {
                        errors.Add("Selected sample and tube type should not be a primary sample and tube type");
                    }
                }

                if (req.lstmultisamplesreferencelist != null && req.lstmultisamplesreferencelist.Any() && req.sampleNo > 0 && req.containerNo > 0)
                {
                    //var match = req.lstmultisamplesreferencelist
                    //    .Any(d => d.sampleNo == req.sampleNo);
                    //if (match)
                    //{
                    //    errors.Add("Duplicate sample's with tube type is could not allowed");
                    //}
                    var dublicatecontainer = req.lstmultisamplesreferencelist.GroupBy(x => new { x.containerNo, x.sampleNo }).Where(x => x.Count() > 1).Select(x => x.Key).Count();
                    if (dublicatecontainer > 0)
                    {
                        errors.Add("Duplicate sample's with tube type is could not allowed");
                    }
                }

                if (req.decimalPoint.ToString().Trim() != string.Empty)
                {
                    if (req.decimalPoint.ToString().Contains('.'))
                    {
                        errors.Add("Decimal value not allowed in DecimalPoint");
                        req.decimalPoint = 0;
                    }
                    else if (req.decimalPoint > 5)
                    {
                        errors.Add("Decimal Point should be lesser than or equal to 5");
                        req.decimalPoint = 0;
                    }
                }

                DateTime fromDate, toDate;
                if (!string.IsNullOrEmpty(req.fromDate) && DateTime.TryParse(req.fromDate, out fromDate) &&
                    !string.IsNullOrEmpty(req.toDate) && DateTime.TryParse(req.toDate, out toDate))
                {
                    int outt = DateDiffInDays(fromDate, toDate);
                    if (outt < 0)
                    {
                        errors.Add("The From Date should be less than the To Date");
                        req.toDate = null;
                    }
                }

                if (req.DeltaRange < 0 || req.DeltaRange > 100)
                {
                    errors.Add("Invalid Percentage");
                    req.DeltaRange = 0;
                }
                else
                {
                    req.DeltaRange = Math.Round(req.DeltaRange, 2);
                }

                if (req.lsttestrefrange != null && req.lsttestrefrange.Any() && req.lsttestanalyrange != null && req.lsttestanalyrange.Any() &&
                    req.lstmultisamplesreferencelist != null && req.lstmultisamplesreferencelist.Any())
                {
                    var ageTestRef = req.lsttestrefrange.Where(i => i.ageFrom < 0 && i.ageTo < 0).ToList();
                    var ageAnalyRange = req.lsttestanalyrange.Where(j => j.ageFrom < 0 && j.ageTo < 0).ToList();
                    var ageMultiRefList = req.lstmultisamplesreferencelist.Where(k => k.ageFrom < 0 && k.ageTo < 0).ToList();

                    if (ageTestRef.Any() || ageAnalyRange.Any() || ageMultiRefList.Any())
                    {
                        errors.Add("Negative Age Values are not allowed");
                    }
                }

                if (string.IsNullOrEmpty(req.RejectReason) || req.RejectReason.TrimStart() == string.Empty)
                    errors.Add("Reject Reason is required");
            }

            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }

            return errorResponse;
        }
        private static int DateDiffInDays(DateTime a, DateTime b)
        {
            return (b.Date - a.Date).Days;
        }

        public static bool ValidateFormula(string formulaText)
        {
            var lstOperatorChars = new List<string> { "+", "-", "*", "/", "=", "(", ")", "%", "^" };
            string pattern = @"^[0-9\s" + string.Join(@"\", lstOperatorChars) + "]+$";
            if (!Regex.IsMatch(formulaText, pattern))
            {
                return false;
            }
            try
            {
                DataTable table = new DataTable();
                var value = table.Compute(formulaText, string.Empty);
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Insert Template Text //
        public static ErrorResponse InsertTemplateText(lstTemplateList req)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (string.IsNullOrEmpty(req.templateName) || req.templateName.TrimStart() == string.Empty)
                errors.Add("Template Name is required");
            //if (string.IsNullOrEmpty(req.templateText) || req.templateText.TrimStart() == string.Empty)
            //    errors.Add("Template Text is required");
            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }

        // Insert Test Formula //
        public static ErrorResponse InsertTestFormula(SaveFormulaRequest req)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (string.IsNullOrEmpty(req.formula) || req.formula.TrimStart() == string.Empty)
                errors.Add("Formula Text is required");

            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }

        // Sub-Test Master //
        public static ErrorResponse InsertSubTest(objsubtest req)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();
            Regex _csvcheck = new Regex(@"^(=|\+|\-|@)");

            if (req.testNo == 0)
                errors.Add("Test No is required");
            if (req.departmentNo == 0)
                errors.Add("Dept No is required");
            if (string.IsNullOrEmpty(req.subTestName) || req.subTestName.TrimStart() == string.Empty)
                errors.Add("Sub Test Name is required");
            if (string.IsNullOrEmpty(req.testDisplayName) || req.testDisplayName.TrimStart() == string.Empty)
                errors.Add("Test Display Name is required");
            if (string.IsNullOrEmpty(req.resultType) || req.resultType.TrimStart() == string.Empty)
                errors.Add("ResultType is Required");
            if (_csvcheck.IsMatch(req.subTestName.ToSubstring(req.subTestName == null ? 0 : req.subTestName.Length)) || _csvcheck.IsMatch(req.testDisplayName.ToSubstring(req.testDisplayName == null ? 0 : req.testDisplayName.Length)) ||
               _csvcheck.IsMatch(req.testShortName.ToSubstring(req.testShortName == null ? 0 : req.testShortName.Length)))
                errors.Add("Special character not allowed");

            if (req.lsttestrefrange != null && req.lsttestrefrange.Any())
            {
                var agetodaysinvalidlst = req.lsttestrefrange.Where(j => j.ageToType == "D" && j.ageTo > 73048).ToList();
                var agetoyearsinvalidlst = req.lsttestrefrange.Where(j => j.ageToType == "Y" && j.ageTo > 200).ToList();
                var agetomonthsinvalidlst = req.lsttestrefrange.Where(j => j.ageToType == "M" && j.ageTo > 2400).ToList();

                if (agetodaysinvalidlst.Any() || agetoyearsinvalidlst.Any() || agetomonthsinvalidlst.Any())
                {
                    errors.Add("Reference Range AgeTo shouldn't over than 200 years");
                }
            }

            if (req.decimalPoint.ToString().Trim() != string.Empty)
            {
                if (req.decimalPoint.ToString().Contains('.'))
                {
                    errors.Add("Decimal value not allowed in DecimalPoint");
                    req.decimalPoint = 0;
                }
                else if (req.decimalPoint > 5)
                {
                    errors.Add("Decimal Point should be lesser than or equal to 5");
                    req.decimalPoint = 0;
                }
            }

            if (req.DeltaRange < 0 || req.DeltaRange > 100)
            {
                errors.Add("Invalid Percentage");
                req.DeltaRange = 0;
            }
            else
            {
                req.DeltaRange = Math.Round(req.DeltaRange, 2);
            }

            if (req.lsttestrefrange != null && req.lsttestrefrange.Any())
            {
                var ageTestRef = req.lsttestrefrange.Where(i => i.ageFrom < 0 && i.ageTo < 0).ToList();

                if (ageTestRef.Any())
                {
                    errors.Add("Negative Age Values are not allowed");
                }
            }

            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }

        // Group & Package Master //
        public static ErrorResponse InsertGroupPackage(objgrppkg req)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();
            Regex _csvcheck = new Regex(@"^(=|\+|\-|@)");

            if (string.IsNullOrEmpty(req.pageCode) || req.pageCode.TrimStart() == string.Empty)
                errors.Add("PageCode is Required");
            if (req.pageCode.Equals("GRPMAS") && (req.deptNo == 0))
                errors.Add("Dept No is required");
            if (req.pageCode.Equals("GRPMAS") && string.IsNullOrEmpty(req.serviceName) || req.serviceName.TrimStart() == string.Empty)
                errors.Add("Service Name is required");
            if (req.pageCode.Equals("GRPMAS") && string.IsNullOrEmpty(req.displayName) || req.displayName.TrimStart() == string.Empty)
                errors.Add("Display Name is required");
            if (req.pageCode.Equals("PKGMAS") && string.IsNullOrEmpty(req.serviceName) || req.serviceName.TrimStart() == string.Empty)
                errors.Add("Service Name is required");
            if (req.pageCode.Equals("PKGMAS") && string.IsNullOrEmpty(req.displayName) || req.displayName.TrimStart() == string.Empty)
                errors.Add("Display Name is required");
            if (req.lstgrppkgservice == null || req.lstgrppkgservice.Count < 1)
            {
                if (req.pageCode.Equals("GRPMAS"))
                    errors.Add("Add more than two tests");
                else if (req.pageCode.Equals("PKGMAS"))
                    errors.Add("Add more than two tests/groups");
            }
            if (req.pageCode.Equals("GRPMAS") && (string.IsNullOrEmpty(req.FromDate) || req.FromDate.TrimStart() == string.Empty))
                errors.Add("From Date is required");
            if (req.pageCode.Equals("PKGMAS") && (string.IsNullOrEmpty(req.FromDate) || req.FromDate.TrimStart() == string.Empty))
                errors.Add("From Date is required");
            if (_csvcheck.IsMatch(req.serviceName.ToSubstring(req.serviceName == null ? 0 : req.serviceName.Length)) || _csvcheck.IsMatch(req.displayName.ToSubstring(req.displayName == null ? 0 : req.displayName.Length)) ||
                _csvcheck.IsMatch(req.shortName.ToSubstring(req.shortName == null ? 0 : req.shortName.Length)))
                errors.Add("Special character not allowed");

            DateTime fromDate, toDate;
            if (req.pageCode.Equals("GRPMAS") && !string.IsNullOrEmpty(req.FromDate) && DateTime.TryParse(req.FromDate, out fromDate) &&
                !string.IsNullOrEmpty(req.ToDate) && DateTime.TryParse(req.ToDate, out toDate))
            {
                int outt = DateDiffInDays(fromDate, toDate);
                if (outt < 0)
                {
                    errors.Add("The From Date should be less than the To Date in Group Master");
                    req.ToDate = null;
                }
            }

            if (req.pageCode.Equals("PKGMAS") && !string.IsNullOrEmpty(req.FromDate) && DateTime.TryParse(req.FromDate, out fromDate) &&
                !string.IsNullOrEmpty(req.ToDate) && DateTime.TryParse(req.ToDate, out toDate))
            {
                int outt = DateDiffInDays(fromDate, toDate);
                if (outt < 0)
                {
                    errors.Add("The From Date should be less than the To Date in Package Master");
                    req.ToDate = null;
                }
            }

            // Validate for duplicate services
            for (int i = 0; i < req.lstgrppkgservice.Count; i++)
            {
                var v = req.lstgrppkgservice[i];
                if (v.serviceNo > 0)
                {
                    if (req.pageCode == "GRPMAS")
                    {
                        var isduplicate = req.lstgrppkgservice.Where(x => x.serviceNo == v.serviceNo && x.serviceType == v.serviceType).ToList();
                        if (isduplicate.Count > 1)
                        {
                            errors.Add("This service already exists");
                            break;
                        }
                    }
                    else if (req.pageCode == "PKGMAS")
                    {
                        var isduplicate = req.lstgrppkgservice.Where(x => x.serviceNo == v.serviceNo && x.serviceType == v.serviceType).ToList();
                        if (isduplicate.Count > 1)
                        {

                            errors.Add("This service already exists");
                            break;
                        }
                    }
                }
            }

            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }

        // Integration Package Mapping Master //
        public static ErrorResponse InsertIntegrationPackage(IntegrationPackageReq req)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (string.IsNullOrEmpty(req.SourceCode) || req.SourceCode.TrimStart() == string.Empty)
                errors.Add("Source Code is required");
            if (req.TestNo == 0)
                errors.Add("Test No is required");

            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }
    }
}