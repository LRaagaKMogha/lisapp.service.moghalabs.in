using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DEV.API.SERVICE.Shared.Masters
{
    public class CustomBadRequest : ValidationProblemDetails
    {
        public CustomBadRequest(ActionContext context)
        {
            Title = "Invalid arguments to the API";
            Detail = "The inputs supplied to the API are invalid";
            Status = 400;
            ConstructErrorMessages(context);
            Type = context.HttpContext.TraceIdentifier;
        }

        private void ConstructErrorMessages(ActionContext context)
        {
            foreach (var keyModelStatePair in context.ModelState)
            {
                var key = keyModelStatePair.Key;
                var errors = keyModelStatePair.Value.Errors;
                if (errors != null && errors.Count > 0)
                {
                    if (errors.Count == 1)
                    {
                        var errorMessage = GetErrorMessage(errors[0]);
                        Errors.Add(key, new[] { errorMessage });
                    }
                    else
                    {
                        var errorMessages = new string[errors.Count];
                        for (var i = 0; i < errors.Count; i++)
                        {
                            errorMessages[i] = GetErrorMessage(errors[i]);
                        }

                        Errors.Add(key, errorMessages);
                    }
                }
            }
        }

        string GetErrorMessage(ModelError error)
        {
            var invalidPropertyName = ExtractPropertyName(error.ErrorMessage);
            return !string.IsNullOrEmpty(invalidPropertyName) ?
                $"The input property '{invalidPropertyName}' was not valid." :
                error.ErrorMessage;
        }

        static string ExtractPropertyName(string errorMessage)
        {
            string pattern = @"Path: \$.(\w+)";
            Match match = Regex.Match(errorMessage, pattern);
            if (match.Success)
            {
                return match.Groups[1].Value;
            }
            return string.Empty;
        }
    }
}