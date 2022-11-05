using UnityEngine;

public class Chemical
{
    public Type StatusType { get; }

    public enum Type
    {
        empty,
        red,
        blue,
        yellow,
        white,
        green
    }

    public Chemical(Type statusType)
    {
        StatusType = statusType;
    }

    public static Color GetColor(Type type)
    {
        switch (type)
        {
            case Type.red:
                return Color.red;
            case Type.yellow:
                return Color.yellow;
            case Type.blue:
                return Color.blue;
            case Type.white:
                return Color.white;
            case Type.green:
                return Color.green;
        }
        return Color.black;
    }
    
}
