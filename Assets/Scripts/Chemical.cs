public class Status
{
    public Type StatusType { get; }

    public enum Type
    {
        empty,
        red,
        blue,
        yellow
    }

    public Status(Type statusType)
    {
        StatusType = statusType;
    }
    
}
