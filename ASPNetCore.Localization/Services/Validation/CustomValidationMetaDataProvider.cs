using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASPNetCore.Localization.Services.Validation
{
    public class CustomValidationMetaDataProvider : IValidationMetadataProvider
    {

        public Dictionary<Type,string> ErrorMessages { get; set; }

        public CustomValidationMetaDataProvider()
        {
            ErrorMessages = new Dictionary<Type, string> {
                { typeof(RequiredAttribute) , "RequiredError"},
                { typeof(MaxLengthAttribute) , "MaxLengthError" }
            };
        }

        public void CreateValidationMetadata(ValidationMetadataProviderContext context)
        {
            foreach (var attribute in context.Attributes)
            {
                var validationAttribute = attribute as ValidationAttribute;

                if (validationAttribute != null)
                {
                    var type = validationAttribute.GetType();

                    if (ErrorMessages.TryGetValue(type, out string key)) {
                        validationAttribute.ErrorMessage = key;
                    }
                }
            }
        }
    }
}
