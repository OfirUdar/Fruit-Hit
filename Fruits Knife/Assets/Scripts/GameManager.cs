using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject panelBeforeGameOver;
    public GameObject panelGameOver;
    public GameObject panelRateUs;
    private bool isContinued = false;
    public ScoreManager scoreManager;

    public TextMeshProUGUI amountCoinsText;

    public GameObject panelTutorial;
    public AdManager adManager;

    private void Start()
    {
        amountCoinsText.text= PlayerPrefs.GetInt("AmountCoins", 0).ToString();
        panelTutorial.SetActive(PlayerPrefs.GetString("IsTutorial", "true").Equals("true"));
    }

   
    private void OnEnable()
    {
        StageManager.OnGameOver += GameOver;
        AdManager.OnRewardVideo += Continue_Get5Knives;
    }

    private void OnDisable()
    {
        StageManager.OnGameOver -= GameOver;
        AdManager.OnRewardVideo -= Continue_Get5Knives;
    }




    private void GameOver()
    {

        if (!isContinued)
        {
            if (PlayerPrefs.GetInt("HighestScore", 0) < 8000 && PlayerPrefs.GetInt("HighestScore", 0) > 500)
            {
                if (Random.Range(0, 5) == 1)
                    panelRateUs.SetActive(true);
                else
                {
                    if (scoreManager.score > 1500)
                        panelBeforeGameOver.SetActive(true);
                    else
                        panelGameOver.SetActive(true);
                }
                    
            }
            else
            {
                if(scoreManager.score>1500)
                     panelBeforeGameOver.SetActive(true);
                else
                    panelGameOver.SetActive(true);

            }
              
        }
        else
        {
            panelGameOver.SetActive(true);
            isContinued = false;
        }
       
        if (PlayerPrefs.GetInt("HighestScore", 0) > 3500) 
             PlayerPrefs.SetString("IsTutorial", "false");

        if (adManager!=null&&Random.Range(0, 5) == 2)
            adManager.ShowInterstitial();

    }


    public Animator transitionBtwScenes;

    public void Home()
    {
        if (Random.Range(0, 5) == 2)
            adManager.ShowInterstitial();
        transitionBtwScenes.SetTrigger("End");
        Invoke("TransmitScene", 0.3f);
    }

    public void TransmitScene()
    {
        SceneManager.LoadScene("Menu");
    }

    public void CloseTutorial()
    {
        panelTutorial.SetActive(false);
    }

    public void Get5KnivesByVideo()
    {
        if (PlayerPrefs.GetString("RemoveADS", "false").Equals("true"))
            Continue_Get5Knives();
    }

    private void Continue_Get5Knives()
    {
        isContinued = true;
        panelBeforeGameOver.SetActive(false);
        panelGameOver.SetActive(false);
        panelRateUs.SetActive(false);
    }

}
