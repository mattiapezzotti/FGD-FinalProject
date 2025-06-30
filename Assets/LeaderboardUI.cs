using UnityEngine;
using TMPro;

public class LeaderboardUI : MonoBehaviour
{
    private TextMeshProUGUI titleText;
    private TextMeshProUGUI listText;

    void Awake()
    {
        titleText = transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        listText = transform.Find("ListText").GetComponent<TextMeshProUGUI>();
    }

    public void ShowLeaderboard(float currentTime)
    {
        float[] bestTimes = LeaderboardManager.LoadTimes();
        float[] newTimes = LeaderboardManager.AddTime(bestTimes, currentTime);

        LeaderboardManager.SaveTimes(newTimes);

        string title = $"Your Time: {FormatTime(currentTime)}";
        string subtitle = "Beat your best time!";
        string list = "";

        bool currentInTop = false;
        bool currentIsBest = Mathf.Approximately(newTimes[0], currentTime);

        for (int i = 0; i < newTimes.Length; i++)
        {
            string entry = $"{i + 1}. {FormatTime(newTimes[i])}";
            if (Mathf.Approximately(newTimes[i], currentTime))
            {
                entry = $"<b><color=yellow>{entry}</color></b>";
                currentInTop = true;
            }
            list += entry + "\n";
        }

        if (currentIsBest)
        {
            title = $"Best Time: {FormatTime(currentTime)}";
            subtitle = ""; // Nessun "beat your best time"
        }

        if (!string.IsNullOrEmpty(subtitle))
        {
            title += "\n" + subtitle;
        }    

        if (!currentInTop)
        {
            Debug.Log("Il tempo attuale non è entrato nella top 3.");
        }
    
        titleText.text = title;
        listText.text = list;
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        return $"{minutes:00}:{seconds:00}";
    }
}