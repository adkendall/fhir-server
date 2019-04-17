﻿// -------------------------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// -------------------------------------------------------------------------------------------------

using System;
using Microsoft.Health.Fhir.Api.Features.ActionResults;
using Microsoft.Health.Fhir.Api.Features.Headers;
using Microsoft.Health.Fhir.Core.Features.Operations;
using Microsoft.Health.Fhir.Core.Features.Routing;
using Microsoft.Net.Http.Headers;
using NSubstitute;
using Xunit;

namespace Microsoft.Health.Fhir.Api.UnitTests.Features.Headers
{
    public class OperationResultExtensionTests
    {
        [Fact]
        public void GivenAnOperationResult_WhenSettingAContentLocationHeader_ThenOperationResultHasAContentLocationHeader()
        {
            string opName = OperationsConstants.Export;
            string id = Guid.NewGuid().ToString();
            var operationResultUrl = new Uri($"http://localhost/{OperationsConstants.Operations}/{opName}/{id}");

            var urlResolver = Substitute.For<IUrlResolver>();
            urlResolver.ResolveOperationResultUrl(Arg.Any<string>(), Arg.Any<string>()).Returns(operationResultUrl);

            var operationResult = OperationResult.Accepted().SetContentLocationHeader(urlResolver, opName, id);

            Assert.Equal(operationResultUrl.AbsoluteUri, operationResult.Headers[HeaderNames.ContentLocation]);
        }

        [Fact]
        public void GivenAnOperationResult_WhenSettingAContentTypeHeader_ThenOperationResultHasAContentTypeHeader()
        {
            string contentTypeValue = "application/json";
            var operationResult = OperationResult.Accepted().SetContentTypeHeader(contentTypeValue);

            Assert.Equal(contentTypeValue, operationResult.Headers[HeaderNames.ContentType]);
        }
    }
}
