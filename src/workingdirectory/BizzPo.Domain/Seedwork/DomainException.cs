using System;
using Newtonsoft.Json;

namespace BizzPo.Domain.Seedwork
{
    public abstract class DomainException : Exception
    {
        protected DomainException()
        {
        }

        protected DomainException(string message) : base(message)
        {
        }

        protected static string FormatMessage(
            string message,
            string memberName,
            string sourceFilePath,
            int sourceLineNumber,
            string exception = "",
            string stackTrace = "")
        {
            var error = new BusinessErrorMessage
            {
                Message = message,
                ClassName = memberName,
                FilePath = sourceFilePath,
                FileNumber = sourceLineNumber,
                ExceptionMessage = exception
            };

            return JsonConvert.SerializeObject(error);
        }
    }

    public class BusinessErrorMessage
    {
        public string Message { get; set; }
        public string ClassName { get; set; }
        public string FilePath { get; set; }
        public int FileNumber { get; set; }
        public string ExceptionMessage { get; set; }
    }
}
