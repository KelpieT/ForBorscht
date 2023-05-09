using UnityEngine;

public static class Tools
{
    public static Vector3 RandomVectorXZ()
    {
        return new Vector3(Random.Range(-1f, 1), 0, (Random.Range(-1f, 1f)));
    }
    public static int ToMilliseconds(this int seconds)
    {
        return seconds * 1000;
    }
    public static int ToMilliseconds(this float seconds)
    {
        return (int)(seconds * 1000);
    }
}
