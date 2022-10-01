using UnityEngine;

public class LoadData : MonoBehaviour
{
    private void Awake()
    {
        Data.Instance.level = LoadIntStat("level", Data.Instance.level, 1);
        Data.Instance.cimsCounts = LoadIntStat("cimsCounts", Data.Instance.cimsCounts, 0);
        Data.Instance.Woody = LoadIntStat("woody", Data.Instance.Woody, 15);
        Data.Instance.Stoney = LoadIntStat("stoney", Data.Instance.Stoney, 13);
        Data.Instance.Metaly = LoadIntStat("metaly", Data.Instance.Metaly, 11);
        Data.Instance.Mucus = LoadIntStat("mucus", Data.Instance.Mucus, 15);
        Data.Instance.Meat = LoadIntStat("meat", Data.Instance.Meat, 13);
        Data.Instance.DungInviting = LoadIntStat("dungInviting", Data.Instance.DungInviting, 1);
    }

    public int LoadIntStat(string key, int stat, int defaultValue)
    {
        if (PlayerPrefs.HasKey(key))
        {
            stat = PlayerPrefs.GetInt(key);
            return stat;
        }
        else
        {
            stat = defaultValue;
            return stat;
        }
    }
    public float LoadFloatStat(string key, float stat, float defaultValue)
    {
        if (PlayerPrefs.HasKey(key))
        {
            stat = PlayerPrefs.GetFloat(key);
            return stat;
        }
        else
        {
            stat = defaultValue;
            return stat;
        }
    }
    public double LoadDoubleStat(string key, string stat, string defaultValue)
    {
        if (PlayerPrefs.HasKey(key))
        {
            stat = PlayerPrefs.GetString(key);
            return double.Parse(stat);
        }
        else
        {
            stat = defaultValue;
            return double.Parse(stat);
        }
    }
    public string LoadStringStat(string key, string stat, string defaultValue)
    {
        if (PlayerPrefs.HasKey(key))
        {
            stat = PlayerPrefs.GetString(key);
            return stat;
        }
        else
        {
            stat = defaultValue;
            return stat;
        }
    }
    public bool LoadBoolStat(string key, bool stat, bool defaultValue)
    {
        if (PlayerPrefs.HasKey(key))
        {
            stat = PlayerPrefs.GetInt(key) == 1;
            return stat;
        }
        else
        {
            stat = defaultValue;
            return stat;
        }
    }
}
