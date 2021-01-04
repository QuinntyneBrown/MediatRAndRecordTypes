using MediatRAndRecordTypes.Api.Features;
using System;

namespace MediatRAndRecordTypes.Testing.Builders
{
    public class ConsultDtoBuilder
    {
        private ConsultDto _consultDto;

        public static ConsultDto WithDefaults()
        {
            return new ConsultDto(default, default, DateTime.UtcNow, DateTime.UtcNow.AddHours(1));
        }

        public ConsultDtoBuilder()
        {
            _consultDto = WithDefaults();
        }

        public ConsultDto Build()
        {
            return _consultDto;
        }
    }
}
