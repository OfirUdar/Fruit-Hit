using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public SpwanKnifes spawnKnifes;
    public SpawnFruits spawnFruits;
    private GameObject spawnFruitsOb;

    public bool isGameOver = false;
   

    public delegate void OnPassStageListener();
    public event OnPassStageListener OnPassStage;

    public delegate void OnGameOverListener();
    public static event OnGameOverListener OnGameOver;

    private void Start()
    {
        spawnFruitsOb = spawnFruits.gameObject;
    }

    private void OnEnable()
    {
        AdManager.OnRewardVideo += Continue_Get5Knives;
    }

    private void OnDisable()
    {
        AdManager.OnRewardVideo -= Continue_Get5Knives;
    }
    private void Update()
    {
        if (spawnFruitsOb.transform.childCount == 0)
        {
            if (OnPassStage != null)
                OnPassStage();
        }
         else
        {
            if(!isGameOver)
                 IsGameOver();
        }

        

    }

    

    public void SetWayPoints(GameObject way_points)
    {
        spawnFruits.SetWayPoints(way_points);
    }
    public void CreateStage(int stage)
    {
       
        //create fruits
        int amountFruits = CreateFruits(stage);

        //create knifes
        CreateKnifes(stage, amountFruits-(stage/2));

    }
    
    public void CreateMaster(int level)
    {
        spawnFruits.CreateRandomMaster(level);
    }

    private int CreateFruits(int stage) //return the number of created fruits
    {
        int speed;
        if (PlayerPrefs.GetString("IsTutorial", "true").Equals("true"))
            speed = Random.Range(2, stage+1);
        else
            speed = Random.Range(2, stage + 2);


        int amountFruits = Random.Range(3, stage+2);
        spawnFruits.CreateRandomFruits(amountFruits, speed);
        return amountFruits;
    }

    private void CreateKnifes(int stage, int amountFruits)
    {
        if(spawnKnifes.amount_Knifes==0)
            spawnKnifes.CreateKnife();
        if (PlayerPrefs.GetString("IsTutorial", "true").Equals("true"))
            spawnKnifes.amount_Knifes += amountFruits+3;
        else
            spawnKnifes.amount_Knifes += amountFruits + 1;
        spawnKnifes.amount_Knifes += Random.Range(0, 2);//bouns

        spawnKnifes.DisplayAmountKnifes();
        
    }

    private void IsGameOver()
    {
        if(spawnKnifes.amount_Knifes==0)
             Invoke("IsFruitsOver", 0.5f);
    }
   private void IsFruitsOver()
    {
        if (spawnKnifes.amount_Knifes == 0 && spawnFruitsOb.transform.childCount > 0)
        {
            if(!isGameOver)
            {
                isGameOver = true;
                if (OnGameOver != null)
                    OnGameOver();
            }
           
        }
           
    }
    

    public void RemoveFruits()
    {
        for (int i = 0; i < spawnFruits.transform.childCount; i++)
            Destroy(spawnFruits.transform.GetChild(i).gameObject);

    }


    public void Get5KnivesByVideo()
    {
        if (PlayerPrefs.GetString("RemoveADS", "false").Equals("true"))
            Continue_Get5Knives();
    }

    private void Continue_Get5Knives()
    {
        isGameOver = false;
        spawnKnifes.CreateKnife();
        spawnKnifes.amount_Knifes += 6;
        spawnKnifes.DisplayAmountKnifes();

    }
}
