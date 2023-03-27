// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using MediatR;
using MediatRAndRecordTypes.Api.IntegrationEvents;


namespace MediatRAndRecordTypes.Api.NotificationHandlers;

internal class ConsultRescheduledHandler : INotificationHandler<ConsultRescheduled>
{

    public ConsultRescheduledHandler()
    {

    }

    public async Task Handle(ConsultRescheduled notification, CancellationToken cancellationToken)
    {

    }
}

