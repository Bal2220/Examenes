namespace Example_EF_1.EasyIrriot.Domain.Models.Exceptions;

public class ExceptionsCreateThingStatesCommand : Exception
{
    public ExceptionsCreateThingStatesCommand() : base("Not Thing States Found"){}
    public ExceptionsCreateThingStatesCommand(string message) : base(message){}
    public ExceptionsCreateThingStatesCommand(string message, Exception innerException) : base(message, innerException){}
}