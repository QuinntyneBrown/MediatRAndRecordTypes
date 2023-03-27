// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;


namespace MediatRAndRecordTypes.Api.Features;

public record ConsultDto(Guid ConsultId, Guid CustomerId, DateTime StartDate, DateTime EndDate);

