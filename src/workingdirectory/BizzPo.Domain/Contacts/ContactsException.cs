using System.Runtime.CompilerServices;
using BizzPo.Domain.Seedwork;

namespace BizzPo.Domain.Contacts
{
    public class ContactsException : DomainException
    {
        public ContactsException(
            string message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
            : base(FormatMessage(message, memberName, sourceFilePath, sourceLineNumber))
        {
        }
    }
}
