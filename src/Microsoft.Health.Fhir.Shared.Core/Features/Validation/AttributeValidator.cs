﻿// -------------------------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// -------------------------------------------------------------------------------------------------
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EnsureThat;
using FluentValidation;
using FluentValidation.Results;
using FluentValidation.Validators;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Validation;
using Microsoft.Health.Fhir.Core.Models;
using Task = System.Threading.Tasks.Task;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace Microsoft.Health.Fhir.Core.Features.Validation
{
    public class AttributeValidator : IPropertyValidator
    {
        public PropertyValidatorOptions Options { get; set; } = PropertyValidatorOptions.Empty;

        public IEnumerable<ValidationFailure> Validate(PropertyValidatorContext context)
        {
            EnsureArg.IsNotNull(context, nameof(context));

            if (context.PropertyValue is ResourceElement typedElement)
            {
                var resource = typedElement.Instance.ToPoco<Resource>();
                var results = new List<ValidationResult>();

                if (!DotNetAttributeValidation.TryValidate(resource, results, recurse: false))
                {
                    foreach (var error in results)
                    {
                        yield return new ValidationFailure(error.MemberNames?.FirstOrDefault(), error.ErrorMessage);
                    }
                }
            }
        }

        public Task<IEnumerable<ValidationFailure>> ValidateAsync(PropertyValidatorContext context, CancellationToken cancellation)
        {
            return Task.FromResult(Validate(context));
        }

        public bool ShouldValidateAsync(ValidationContext context)
        {
            EnsureArg.IsNotNull(context, nameof(context));

            return context.InstanceToValidate is ResourceElement;
        }
    }
}
