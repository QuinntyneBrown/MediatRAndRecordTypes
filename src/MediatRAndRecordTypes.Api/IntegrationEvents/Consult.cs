// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using MediatR;
using MediatRAndRecordTypes.Api.Models;


namespace MediatRAndRecordTypes.Api.IntegrationEvents;

public record ConsultRescheduled(Consult Consult) : INotification;

