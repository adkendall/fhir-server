﻿// -------------------------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// -------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Health.Fhir.Api.Features.Audit;

namespace Microsoft.Health.Fhir.Tests.E2E.Rest.Audit
{
    public class AuditEntry
    {
        public AuditEntry(AuditAction auditAction, string action, string resourceType, Uri requestUri, HttpStatusCode? statusCode, string correlationId, IReadOnlyCollection<KeyValuePair<string, string>> claims)
        {
            AuditAction = auditAction;
            Action = action;
            ResourceType = resourceType;
            RequestUri = requestUri;
            StatusCode = statusCode;
            CorrelationId = correlationId;
            Claims = claims;
        }

        public AuditAction AuditAction { get; }

        public string Action { get; }

        public string ResourceType { get; }

        public Uri RequestUri { get; }

        public HttpStatusCode? StatusCode { get; }

        public string CorrelationId { get; }

        public IReadOnlyCollection<KeyValuePair<string, string>> Claims { get; }
    }
}
