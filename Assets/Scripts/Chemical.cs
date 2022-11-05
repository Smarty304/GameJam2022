public class Chemical
{
    public Type StatusType { get; }

    public enum Type
    {
        empty,
        red,
        blue,
        yellow
    }

    public Chemical(Type statusType)
    {
        StatusType = statusType;
    }
    
}
