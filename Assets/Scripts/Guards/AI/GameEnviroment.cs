using System.Collections.Generic;
using UnityEngine;


//DA MODIFICARE! MODIFICARE LA LOGICA AGGIUNGENDO UNA MAPPA<LISTA, INDEX>
public class GameEnviroment
{
    private static GameEnviroment instance;
    private Dictionary<int, List<GameObject>> npcWaypoints = new Dictionary<int, List<GameObject>>();
    private Dictionary<int, int> currentWaypointIndices = new Dictionary<int, int>();
    private const int numNpc = 2; // Modifica questo valore in base al numero di NPC

    public static GameEnviroment Singleton
    {
        get
        {
            if (instance == null)
            {
                instance = new GameEnviroment();
                instance.InitializeWaypoints();
            }
            return instance;
        }
    }

    private void InitializeWaypoints()
    {
        npcWaypoints.Clear();
        currentWaypointIndices.Clear();
        for (int i = 0; i < numNpc; i++)
        {
            string tag = $"WayPoint{i}";
            GameObject[] found = GameObject.FindGameObjectsWithTag(tag);

            // Ordina l'array alfabeticamente per nome
            System.Array.Sort(found, (a, b) => string.Compare(a.name, b.name, System.StringComparison.Ordinal));

            npcWaypoints[i] = new List<GameObject>(found);
            currentWaypointIndices[i] = 0;
        }
    }

    public List<GameObject> GetWaypointList(int npcNum)
    {
        if (npcWaypoints.ContainsKey(npcNum))
            return npcWaypoints[npcNum];
        return null;
    }

    public int GetCurrentWaypointIndex(int npcNum)
    {
        if (currentWaypointIndices.ContainsKey(npcNum))
            return currentWaypointIndices[npcNum];
        return -1;
    }

    public void SetCurrentWaypointIndex(int npcNum)
    {
        if (npcWaypoints.ContainsKey(npcNum) && npcWaypoints[npcNum].Count > 0)
        {
            currentWaypointIndices[npcNum] = (currentWaypointIndices[npcNum] + 1) % npcWaypoints[npcNum].Count;
        }
    }

    public void SetIndexToNearestWP(int npcNum, Vector3 npcPosition)
    {
        if (npcWaypoints.ContainsKey(npcNum) && npcWaypoints[npcNum].Count > 0)
        {
            float minDistance = Vector3.Distance(npcPosition, npcWaypoints[npcNum][0].transform.position);
            int closestIndex = 0;
            for (int i = 1; i < npcWaypoints[npcNum].Count; i++)
            {
                float distance = Vector3.Distance(npcPosition, npcWaypoints[npcNum][i].transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestIndex = i;
                }
            }
            currentWaypointIndices[npcNum] = closestIndex;
        }
    }

    public static void Reset()
    {
        instance = null;
    }
}
