public class GymExceptions : Exception
{
    public string? ErrorCode { get; }

    public GymExceptions(string message, string? errorCode = null) : base(message)
    {
        ErrorCode = errorCode;
    }
}

