namespace BizzPo.Core.Domain
{
    public class ExceptionMessage
    {
        public string Message { get; set; }
        public string ClassName { get; set; }
        public string FilePath { get; set; }
        public int FileNumber { get; set; }
    }
}