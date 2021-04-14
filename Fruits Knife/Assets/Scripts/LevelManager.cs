using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelManager : MonoBehaviour
{
    public GameObject[] wayPoints;
    public StageManager stageManager;
    public ScoreManager scoreManager;
    public Animator panelTransition;
    public SoundsManager soundsManager;
    public Slider sliderStages;


    [SerializeField]
    private int level = 0;
    private int stage=0;

    private int indexWayPoints = 0;
    private int numNextLevel=4;
    

    private void OnEnable()
    {
        stageManager.OnPassStage += PassStage;
    }
    private void OnDisable()
    {
        stageManager.OnPassStage -= PassStage;
    }


    private void Start()
    {
        sliderStages.maxValue = numNextLevel+1;
        sliderStages.value = 0;
        CreateLevel();
    }

    private void Update()
    {
        if (sliderStages.value < stage)
            sliderStages.value = Mathf.Lerp(sliderStages.value, stage, Time.deltaTime * 4);
    }

    public void CreateLevel()
    {
        if (transform.childCount > 0)
            Destroy(transform.GetChild(0).gameObject);
        CreateWayPoints(indexWayPoints);
        stageManager.SetWayPoints(wayPoints[indexWayPoints]);
        CreateStage();
        scoreManager.levelText.text = "LEVEL: " + (level + 1);
    }

    private void CreateStage()
    {
        stageManager.CreateStage(stage);
    }



    private void CreateWayPoints(int index)
    {
        if (index >= 0 && index < wayPoints.Length)
            Instantiate(wayPoints[index], wayPoints[index].GetComponent<Transform>().position, Quaternion.identity,transform);
    }

    private void PassStage()
    {
        panelTransition.SetTrigger("Fade In and Out");
        if(stage>=numNextLevel)
        {
            level++;
            stage = 0;
            if (indexWayPoints + 1 < wayPoints.Length)
                indexWayPoints++;
            else
                indexWayPoints = 0;
            CreateLevel();
            soundsManager.PlayNextLevel();
            if (PlayerPrefs.GetString("VibrationMute", "false").Equals("false"))
                Vibration.Vibrate(200);
            sliderStages.value = 0;
        }
        else
        {
            stage++;
            CreateStage();
            if(level>0&&level % 2 == 0&&stage ==numNextLevel)
                 stageManager.CreateMaster(level);
        }     
    }

    public void Restart()
    {
        stageManager.RemoveFruits();
        //panelTransition.SetTrigger("Fade In and Out");
        level = 0;
        stage = 0;
        indexWayPoints = 0;
        scoreManager.score = 0;
        sliderStages.maxValue = numNextLevel + 1;
        sliderStages.value = 0;
        stageManager.isGameOver = false;
        CreateLevel();
}

}
