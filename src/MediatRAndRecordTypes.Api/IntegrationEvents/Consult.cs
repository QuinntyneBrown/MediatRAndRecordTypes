// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace MediatRAndRecordTypes.Api.IntegrationEvents;

public record ConsultRescheduled(Guid ConsultId, DateTime StartDate, DateTime EndDate) : INotification;

