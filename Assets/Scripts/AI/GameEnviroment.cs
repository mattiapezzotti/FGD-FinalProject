using System.Collections.Generic;
using UnityEngine;

public class GameEnviroment
{
    private static GameEnviroment instance;
    private List<GameObject> wayPoints = new List<GameObject>();
    private int currentWaypointIndex = 0;
    public List<GameObject> WayPoints
    {
        get { return wayPoints; }
    }

    public int CurrentWaypointIndex { get { return currentWaypointIndex; } 
        set { currentWaypointIndex = value; } }

    public static GameEnviroment Singleton
    {
        get
        {
            if (instance == null)
            {
                instance = new GameEnviroment();
                instance.WayPoints.AddRange(GameObject.FindGameObjectsWithTag("WayPoint"));
            }
            return instance;
        }
    }

}
