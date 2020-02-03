
public static class PlayerStats
{
    private static int hits, misses, seconds_survived, meter_max, meter_val, points;

    public static int Hits
    {
        get
        {
            return hits;
        }
        set
        {
            hits = value;
        }
    }

    public static int Misses
    {
        get
        {
            return misses;
        }
        set
        {
            misses = value;
        }
    }

    public static int Seconds_Survived
    {
        get
        {
            return seconds_survived;
        }
        set
        {
            seconds_survived = value;
        }
    }

    public static int Meter_Max
    {
        get
        {
            return meter_max;
        }
        set
        {
            meter_max = value;
        }
    }

    public static int Meter_Val
    {
        get
        {
            return meter_val;
        }
        set
        {
            meter_val = value;
        }
    }

    public static int Points
    {
        get
        {
            return points;
        }
        set
        {
            points = value;
        }
    }
}
