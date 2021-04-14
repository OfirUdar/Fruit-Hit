using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineWayPoints : MonoBehaviour
{
    public Transform[] wayPoints;
    public LineRenderer line;
    private void Start()
    { 
        line.positionCount=wayPoints.Length+1;
        for (int i = 0; i < wayPoints.Length; i++)
            line.SetPosition(i, wayPoints[i].position);
        line.SetPosition(wayPoints.Length, wayPoints[0].position);
    }


}
