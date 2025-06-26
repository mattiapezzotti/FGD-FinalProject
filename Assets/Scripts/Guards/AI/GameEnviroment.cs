using System.Collections.Generic;
using UnityEngine;


//aggiungere un bool per quale guardia sta usando la classe?
public class GameEnviroment
{
    private static GameEnviroment instance;
    private List<GameObject> wayPoints1 = new List<GameObject>();
    private List<GameObject> wayPoints2 = new List<GameObject>();
    private int currentWaypointIndex1 = 0;
    private int currentWaypointIndex2 = 0;


    public List<GameObject> WayPoints1
    {
        get { return wayPoints1; }
    }
    public List<GameObject> WayPoints2
    {
        get { return wayPoints2; }
    }

    public int CurrentWaypointIndex1 { get { return currentWaypointIndex1; } 
        set { currentWaypointIndex1 = value; } }
    public int CurrentWaypointIndex2{ get { return currentWaypointIndex2; }
        set { currentWaypointIndex2 = value; } }


    public List<GameObject> GetWaypointList(int npcNum)
    {

        if (npcNum == 0)
            return WayPoints1;
        else if (npcNum == 1)
            return WayPoints2;
        else
            return null;
    }

    public int CurrentWaypointIndex(int npcNum)
    {
        if (npcNum == 0)
            return CurrentWaypointIndex1;
        else if (npcNum == 1)
            return CurrentWaypointIndex2;
        else
            return -1; // Invalid NPC number
    }

    public void SetCurrentWaypointIndex(int npcNum)
    {
        if (npcNum == 0)
        {
            CurrentWaypointIndex1 = (CurrentWaypointIndex1 + 1) % WayPoints1.Count;

        }
        else if (npcNum == 1)
        {
            CurrentWaypointIndex2= (CurrentWaypointIndex2 + 1) % WayPoints2.Count;
        }
    }

    public void SetIndexFromChase(int npcNum, Vector3 npcPosition)
    {
        if (npcNum == 0)
        {
            float minDistance = Vector3.Distance(npcPosition, WayPoints1[0].transform.position);
            int closestIndex = 0;
            for (int i = 1; i < WayPoints1.Count; i++)
            {
                float distance = Vector3.Distance(npcPosition, WayPoints1[i].transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestIndex = i;
                }
            }
            CurrentWaypointIndex1 = closestIndex;
        }
        else if (npcNum == 1)
        {
            float minDistance = Vector3.Distance(npcPosition, WayPoints2[0].transform.position);
            int closestIndex = 0;
            for (int i = 1; i < WayPoints2.Count; i++)
            {
                float distance = Vector3.Distance(npcPosition, WayPoints2[i].transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestIndex = i;
                }
            }
            CurrentWaypointIndex2 = closestIndex;
        }
    }

    public static GameEnviroment Singleton
    {
        get
        {
            if (instance == null)
            {
                instance = new GameEnviroment();
                instance.WayPoints1.AddRange(GameObject.FindGameObjectsWithTag("WayPoint"));
                instance.WayPoints2.AddRange(GameObject.FindGameObjectsWithTag("WayPoint2"));
            }
            return instance;
        }
    }

}
