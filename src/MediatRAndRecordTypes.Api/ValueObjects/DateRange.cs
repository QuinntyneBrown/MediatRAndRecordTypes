using CSharpFunctionalExtensions;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MediatRAndRecordTypes.Api.ValueObjects
{
    public class DateRange : ValueObject
    {
        public const int MaxLength = 250;
        [JsonProperty]
        public string Value { get; private set; }

        protected DateRange()
        {

        }

        private DateRange(string value)
        {
            Value = value;
        }

        public static Result<DateRange> Create(string value)
        {
            value = (value ?? string.Empty).Trim();

            if (value.Length == 0)
                return Result.Failure<DateRange>("DateRange should not be empty.");

            if (value.Length > MaxLength)
                return Result.Failure<DateRange>("DateRange name is too long.");

            return Result.Success(new DateRange(value));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator string(DateRange dateRange)
        {
            return dateRange.Value;
        }

        public static explicit operator DateRange(string dateRange)
        {
            return Create(dateRange).Value;
        }
    }
}
