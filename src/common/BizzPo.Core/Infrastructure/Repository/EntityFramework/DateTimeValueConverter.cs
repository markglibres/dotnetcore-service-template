using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BizzPo.Core.Infrastructure.Repository.EntityFramework
{
    public class DateTimeValueConverter : ValueConverter<DateTime, DateTime>
    {
        private static readonly Expression<Func<DateTime, DateTime>> DeserializeValueFromDb =
            value => DateTime.SpecifyKind(value, DateTimeKind.Utc);

        private static readonly Expression<Func<DateTime, DateTime>> SerializeValueToDb = value => value;

        public DateTimeValueConverter(ConverterMappingHints mappingHints = default)
            : base(SerializeValueToDb, DeserializeValueFromDb, mappingHints)
        {
        }
    }
}
