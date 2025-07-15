namespace Example_EF_1.EasyIrriot.Domain.Models.Exceptions;

public class ExceptionsCreateThingsCommand : Exception
{
    public ExceptionsCreateThingsCommand() : base("Not Things found"){}
    public ExceptionsCreateThingsCommand(string message) : base(message){}
    public ExceptionsCreateThingsCommand(string message, Exception innerException) : base(message, innerException){}
}