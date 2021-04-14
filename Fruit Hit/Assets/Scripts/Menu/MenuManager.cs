
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Animator transitionBtwScenes;
    public TextMeshProUGUI amountCoinsText;
    public TextMeshProUGUI highestScoreText;

    public GameObject[] knives;
    public GameObject knifePlacement;


    public Button[] removeADS;

    private void Start()
    {
        foreach (Button b in removeADS)
            b.interactable = PlayerPrefs.GetString("RemoveADS", "false").Equals("false");
    }


    private void OnEnable()
    {
        amountCoinsText.text = PlayerPrefs.GetInt("AmountCoins", 0).ToString();
        highestScoreText.text = PlayerPrefs.GetInt("HighestScore", 0).ToString();
        SetKnifeDefualt();
    }


    public void Play()
    {
        transitionBtwScenes.SetTrigger("End");
        Invoke("TransmitScene", 0.3f);
    }

    public void TransmitScene()
    {
        SceneManager.LoadScene("Main");
    }

    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
    }


    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

    public void Instagram()
    {
        Application.OpenURL("https://www.instagram.com/udargames/");
    }

    public void SetKnifeDefualt()
    {
        if (knifePlacement.transform.childCount > 0)
            Destroy(knifePlacement.transform.GetChild(0).gameObject);
       GameObject knife= Instantiate(knives[PlayerPrefs.GetInt("NumKnifeDefualt", 0)], transform.position, Quaternion.identity, knifePlacement.transform);
       knife.transform.GetChild(0).GetComponent<KnifeLauncher>().enabled = false;
       knife.transform.GetChild(0).GetComponent<TrailRenderer>().enabled = false;

    }

}
