using System;
namespace Common.Messages
{
    public class ICommandResponse : HttpResponseMessage
    {
        public List<ValidationError> Errors { get; set; } = new List<ValidationError>();
        public bool IsValid { get { return Errors.Any(); } }

        public ICommandResponse()
        {

        }
    }


    public class ValidationError
    {
        public string Field { get; set; }
        public string Message { get; set; }
    }
}