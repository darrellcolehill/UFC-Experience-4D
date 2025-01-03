using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public bool isDebug = true;
    public Transform[] waypoints;

    public int Length
    {
        get 
        {
            return waypoints.Length;
        }
    }

    public Vector3 getPoint(int index)
    {
        return waypoints[index].position;
    }

    private void OnDrawGizmos()
    {
        if(!isDebug)
        {
            return;
        }

        for(int i = 1; i < waypoints.Length; i++)
        {
            Debug.DrawLine(waypoints[i - 1].position, waypoints[i].position, Color.red);
        }
    }
}
