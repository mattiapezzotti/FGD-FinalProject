using System.Collections.Generic;
using UnityEngine;

public class GameEnviroment
{
    private static GameEnviroment instance;
    private List<GameObject> wayPoints = new List<GameObject>();
    public List<GameObject> WayPoints
    {
        get { return wayPoints; }
    }

    public static GameEnviroment Singleton
    {
        get
        {
            if (instance == null)
            {
                instance = new GameEnviroment();
                instance.WayPoints.AddRange(GameObject.FindGameObjectsWithTag("Waypoint"));
            }
            return instance;
        }
    }

}
