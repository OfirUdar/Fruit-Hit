using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFruits : MonoBehaviour
{
    public GameObject[] fruits;
    public GameObject[] masters;

    private Transform[] wayPoints;
    private int index = 0;




    
    public void SetWayPoints(GameObject way_points)
    {
        wayPoints = new Transform[way_points.transform.childCount];
        for (int i = 0; i < way_points.transform.childCount; i++)
            wayPoints[i] = way_points.transform.GetChild(i);
    }


   

   /* private Vector2 GetStartPosition()
    {
        int randomPoint1 = Random.Range(0, wayPoints.Length);
        int randomPoint2 = 0;
        if (randomPoint1 - 1 < 0)
            randomPoint2 = wayPoints.Length - 1;
        else
            if (randomPoint1 + 1 >= wayPoints.Length)
            randomPoint2 = 0;
        else
            randomPoint2 = randomPoint1 + 1;


        if (randomPoint1 > randomPoint2)
            index = randomPoint1;
        else
            index = randomPoint2;
        if (randomPoint1 == wayPoints.Length - 1 && randomPoint2 == 0)
            index = randomPoint2;
        if (randomPoint2 == wayPoints.Length - 1 && randomPoint1 == 0)
            index = randomPoint1;

        float randomPointX = Random.Range(wayPoints[randomPoint1].position.x, wayPoints[randomPoint2].position.x);
        float randomPointY = Random.Range(wayPoints[randomPoint1].position.y, wayPoints[randomPoint2].position.y);

        Vector2 fruitPos = new Vector2(randomPointX, randomPointY);
        return fruitPos;
    }

    private Vector2 GetStartPosition2()
    {
        Vector2 fruitPos=Vector2.zero;
        for (int i=0;i<wayPoints.Length-1;i++)
        {
            if (Random.Range(0, 2) == 1) //50 % 
            {
                float randomPointX = Random.Range(wayPoints[i].position.x, wayPoints[i + 1].position.x);
                float randomPointY = Random.Range(wayPoints[i].position.y, wayPoints[i + 1].position.y);
                fruitPos = new Vector2(randomPointX, randomPointY);
                index = i + 1;
            }
            else
                if (fruitPos == Vector2.zero && i == wayPoints.Length - 2)
                {
                      fruitPos = wayPoints[wayPoints.Length-1].position;
                      index = 0;
                }
                   
        }
        return fruitPos;
    }*/


    public void CreateRandomFruits(int amount,int speed)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject fruit = fruits[Random.Range(0, fruits.Length)];
            GameObject ob=Instantiate(fruit, GetObjectPosition(), Quaternion.identity, transform);
            MoveByWayPoints obScript = ob.GetComponent<MoveByWayPoints>();
            obScript.wayPoints = wayPoints;
            obScript.index = index;
            obScript.speed = speed;
        }

    }

    public void CreateRandomMaster(int level)
    {
         GameObject master = masters[Random.Range(0, masters.Length)];
         GameObject ob = Instantiate(master, GetObjectPosition(), Quaternion.identity, transform);
         MasterMoveByWayPoints obScript = ob.GetComponent<MasterMoveByWayPoints>();
         obScript.wayPoints = wayPoints;
         obScript.index = index;
        int minTime = 7;
        if (level <= minTime)
            minTime = level;
         obScript.timeChangeDirection = Random.Range(7,7.2f-minTime);
         int maxSpeed=7;
        if (level <= maxSpeed)
            maxSpeed = level;
         obScript.speed = (Random.Range(1, maxSpeed));
        

    }





    private Vector2 GetTwoIndexes()
    {
        int a = Random.Range(0, wayPoints.Length);
        int b;
        if (a + 1 >= wayPoints.Length)
            b = 0;
        else
            b = a + 1;
        index = b;
        return new Vector2(a, b);
    }

    private float GetIncline(Vector2 a, Vector2 b)
    {
        float incline;
        float xDiff, yDiff;
        xDiff = a.x - b.x;
        yDiff = a.y - b.y;
        incline = yDiff / xDiff;
        return incline;
    }

    private Vector2 GetObjectPosition()
    {
        Vector2 indexes = GetTwoIndexes();
        Vector2 point1 = wayPoints[(int)indexes.x].position;
        Vector2 point2 = wayPoints[(int)indexes.y].position;

        float m = GetIncline(point1, point2);
        
        float y, x;
        if(point1.x!=point2.x)
        {
            x = Random.Range(point1.x, point2.x);
            y = m * (x - point1.x) + point1.y;
        }
        else
        {
            x = point1.x;
            y= Random.Range(point1.y, point2.y);
        }
        return new Vector2(x, y);
    }

}
