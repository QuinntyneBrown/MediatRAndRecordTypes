using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MediatRAndRecordTypes.Api.Models
{
    [Owned]
    public class DateRange : ValueObject
    {
        [JsonProperty]
        public DateTime StartDate { get; private set; }
        [JsonProperty]
        public DateTime EndDate { get; private set; }
        public int Days => (EndDate.Date - StartDate.Date).Days;
        public int Hours => (int)(EndDate - StartDate).TotalHours;

        protected DateRange()
        {

        }

        private DateRange(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return StartDate;
            yield return EndDate;
        }

        public static Result<DateRange> Create(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
                return Result.Failure<DateRange>("Start Date should be less than End Date");

            return Result.Success(new DateRange(startDate, endDate));
        }
    }
}
