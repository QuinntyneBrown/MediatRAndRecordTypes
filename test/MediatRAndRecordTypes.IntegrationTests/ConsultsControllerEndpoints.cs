using System;

namespace MediatRAndRecordTypes.IntegrationTests
{
    public static class ConsultsControllerEndpoints
    {
        public static class Post
        {
            public static string CreateConsult = "api/consults";
        }

        public static class Put
        {
            public static string Reschedule = "api/consults";
        }

        public static class Delete
        {
            public static string ConsultBy(Guid consultId)
            {
                return $"api/consults/{consultId}";
            }
        }

        public static class Get
        {
            public static string Consults = "api/consults";
            public static string ConsultBy(Guid consultId)
            {
                return $"api/consults/{consultId}";
            }
        }
    }
}
