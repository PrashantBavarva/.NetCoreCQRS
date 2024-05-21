namespace Infrastructure.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message):base(message)
    {
        
    }   
}

public class DeewanSmsNotFoundException : NotFoundException
{
    public DeewanSmsNotFoundException():base("Sms not found")
    {
        
    }   
}
public class DeewanUnDeliveredFoundException : NotFoundException
{
    public DeewanUnDeliveredFoundException():base("Message state is undelivered")
    {
        
    }   
}