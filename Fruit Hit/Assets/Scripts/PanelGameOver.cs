using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PanelGameOver : MonoBehaviour
{
    public Animator anim;
    public GameObject btnCollectCoins;
    public TextMeshProUGUI amountCoinsTempText;
    public TextMeshProUGUI score;
    public GameObject addingCoins;

    public LevelManager levelManager;
    
    private void OnEnable()
    {
        addingCoins.SetActive(false);
        btnCollectCoins.SetActive((PlayerPrefs.GetInt("AmountCoinsTemp", 0) != 0));
        amountCoinsTempText.text = "Collect " + PlayerPrefs.GetInt("AmountCoinsTemp", 0);
        score.text=levelManager.scoreManager.score.ToString();

        if (levelManager.scoreManager.score > PlayerPrefs.GetInt("HighestScore", 0))
            PlayerPrefs.SetInt("HighestScore", (int)levelManager.scoreManager.score);
        if (levelManager.scoreManager.score > 2500)
            PlayerPrefs.SetString("IsTutorial", "false");
    }

    public void SetTriggerEnd()
    {
        anim.SetTrigger("End");
    }

    public void ClosePanelGameOver()
    {
        this.gameObject.SetActive(false);
    }

    public void SetActiveAddingCoins()
    {
        addingCoins.SetActive(true);
        btnCollectCoins.SetActive(false);
    }

    public void Restart()
    {
        SetTriggerEnd();
        levelManager.Restart();
    }
    public void Instagram()
    {
        Application.OpenURL("https://www.instagram.com/udargames/");
    }
    public void Share()
    {
        string subject = "Fruit Hit";
        string body = "My Score: " + score.text + " \n https://play.google.com/store/apps/details?id=com.UdarGames.FruitHit \n Try it!";

        //execute the below lines if being run on a Android device
#if UNITY_ANDROID
        //Refernece of AndroidJavaClass class for intent
        AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
        //Refernece of AndroidJavaObject class for intent
        AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
        //call setAction method of the Intent object created
        intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
        //set the type of sharing that is happening
        intentObject.Call<AndroidJavaObject>("setType", "text/plain");
        //add data to be passed to the other activity i.e., the data to be sent
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), subject);
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), body);
        //get the current activity
        AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
        //start the activity by sending the intent data
        currentActivity.Call("startActivity", intentObject);
#endif
    }
}
