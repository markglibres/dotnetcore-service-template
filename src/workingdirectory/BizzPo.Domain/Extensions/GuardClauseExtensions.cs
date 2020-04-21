using System;
using Ardalis.GuardClauses;
using BizzPo.Domain.Seedwork;

namespace BizzPo.Domain.Extensions
{
    public static class GuardClauseExtensions
    {
        public static void Empty<T>(this IGuardClause guardClause, string input, string parameterName)
            where T: DomainException
        {
            if (!string.IsNullOrWhiteSpace(input)) return;

            var message = $"{parameterName} cannot be null or empty";
            ThrowError<T>(message);
        }

        public static void Empty<T>(this IGuardClause guardClause, Guid input, string parameterName)
            where T : DomainException
        {
            if (!Guid.Empty.Equals(input)) return;

            var message = $"{parameterName} cannot be an empty GUID";
            ThrowError<T>(message);
        }

        private static void ThrowError<T>(string message)
            where T : DomainException
        {
            throw (T)Activator.CreateInstance(typeof(T), new object[] { message });
        }
    }
}
