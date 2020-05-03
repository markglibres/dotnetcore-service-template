using System;
using System.Dynamic;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BizzPo.Core.Infrastructure.Repository.EntityFramework
{
    public class DynamicPropertyValueConverter : ValueConverter<object, string>
    {
        private static readonly Expression<Func<string, object>> DeserializeValueFromDb = value =>
            JsonConvert.DeserializeObject<ExpandoObject>(value, new ExpandoObjectConverter());

        private static readonly Expression<Func<object, string>> SerializeValueToDb = value =>
            JsonConvert.SerializeObject(value);

        public DynamicPropertyValueConverter(ConverterMappingHints mappingHints = default)
            : base(SerializeValueToDb, DeserializeValueFromDb, mappingHints)
        {
        }
    }
}