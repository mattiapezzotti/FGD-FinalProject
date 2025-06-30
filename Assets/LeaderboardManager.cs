using UnityEngine;

public static class LeaderboardManager
{
    private const string key = "BestTimes";
    private const int maxEntries = 3;

    public static float[] LoadTimes()
    {
        string raw = PlayerPrefs.GetString(key, "");
        if (string.IsNullOrEmpty(raw)) return new float[0];

        string[] parts = raw.Split(';');
        float[] times = new float[parts.Length];
        for (int i = 0; i < parts.Length; i++)
        {
            float.TryParse(parts[i], out times[i]);
        }
        return times;
    }

    public static void SaveTimes(float[] times)
    {
        string raw = string.Join(";", times);
        PlayerPrefs.SetString(key, raw);
        PlayerPrefs.Save();
    }

    public static float[] AddTime(float[] oldTimes, float newTime)
    {
        var list = new System.Collections.Generic.List<float>(oldTimes);
        list.Add(newTime);
        list.Sort();
        if (list.Count > maxEntries)
            list = list.GetRange(0, maxEntries);
        return list.ToArray();
    }
}