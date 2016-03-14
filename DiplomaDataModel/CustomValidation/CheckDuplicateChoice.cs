using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DiplomaDataModel.CustomValidation
{
    public class CheckDuplicateChoice : ValidationAttribute
    {
        private string _otherOptionId;

        public CheckDuplicateChoice(string otherOptionId) : base("Cannot select one options twice or more")
        {
            _otherOptionId = otherOptionId.ToString();
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //string input = value.ToString();
            if (value != null)
            {
                var otherProperty = validationContext.ObjectInstance.GetType()
                         .GetProperty(_otherOptionId);

                var otherPropertyValue = otherProperty
                              .GetValue(validationContext.ObjectInstance, null);

                if (value.Equals(otherPropertyValue))
                {
                    return new ValidationResult(
                      FormatErrorMessage(validationContext.DisplayName));
                }

            }
            return ValidationResult.Success;
        }
    }
}