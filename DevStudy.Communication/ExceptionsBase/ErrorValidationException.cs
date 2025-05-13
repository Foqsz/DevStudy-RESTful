namespace DevStudy.Exceptions.ExceptionsBase;

public class ErrorValidationException : GymExceptions
{
    public string? ErrorCode { get; }
    public string? Field { get; }
    public string? Message { get; }
    public ErrorValidationException(string message, string? field = null, string? errorCode = null) : base(message, errorCode)
    {
        Field = field;
        Message = message;
        ErrorCode = errorCode;
    }
} 
