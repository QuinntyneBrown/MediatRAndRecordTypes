// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;


namespace MediatRAndRecordTypes.IntegrationTests;

public static class ConsultsControllerEndpoints
{
    public static class Post
    {
        public static readonly string CreateConsult = "api/consults";
    }

    public static class Put
    {
        public static readonly string Reschedule = "api/consults";
    }

    public static class Delete
    {
        public static string ConsultBy(Guid consultId)
            => $"api/consults/{consultId}";
    }

    public static class Get
    {
        public static readonly string Consults = "api/consults";
        public static string ConsultBy(Guid consultId)
            => $"api/consults/{consultId}";
    }
}

