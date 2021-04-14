using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelShop : MonoBehaviour
{
    public Image[] knives;
    public Color knifeSelection;
    public Color knifeUnselection;
    public GameObject buyKnife;

    public TextMeshProUGUI textCoins;
    public GameObject addingCoins;
    public SoundsManager soundsManager;
    public MenuManager menuManager;

    private void Start()
    {
        SetLock();
        KnifeSelected(PlayerPrefs.GetInt("NumKnifeDefualt", 0));
        
    }


    private void OnEnable()
    {
        AdManager.OnRewardVideo += OnRewardVideo;
    }
    private void OnDisable()
    {
        AdManager.OnRewardVideo -= OnRewardVideo;
    }


    private void SetLock()
    {
        for(int i=1;i<knives.Length;i++)
        {
            if (PlayerPrefs.GetString("Lock" + i, "true").Equals("false"))
                knives[i].transform.GetChild(1).gameObject.SetActive(false);
            else
                knives[i].transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    private int index=0;

    public void KnifeSelected(int i)
    {
        foreach(Image knife in knives)
            knife.color = knifeUnselection;
        knives[i].color = knifeSelection;
        index = i;  
        if (!knives[i].transform.GetChild(1).gameObject.activeSelf) // if there is no lock
        {
            buyKnife.SetActive(false);
            PlayerPrefs.SetInt("NumKnifeDefualt", i);
            menuManager.SetKnifeDefualt();
        }
        else
            buyKnife.SetActive(true);

    }

    public void BuyKnife(int price)
    {
        if(PlayerPrefs.GetInt("AmountCoins",0)>=price)
        {
            //buy it!
            PlayerPrefs.SetInt("AmountCoins", PlayerPrefs.GetInt("AmountCoins", 0) - price);
            PlayerPrefs.SetString("Lock" + index, "false");
            knives[index].transform.GetChild(1).gameObject.SetActive(false);
            PlayerPrefs.SetInt("NumKnifeDefualt", index);
            buyKnife.SetActive(false);
            textCoins.text = PlayerPrefs.GetInt("AmountCoins", 0).ToString();
            menuManager.SetKnifeDefualt();
            // sound of buying
            soundsManager.PlayBuy();
        }
        else
        {
            //display error or sound..
            soundsManager.PlayError();
        }
    }

    public void GetCoinsByVideo()
    {
        if (PlayerPrefs.GetString("RemoveADS", "false").Equals("true"))
            OnRewardVideo();
    }

    private void OnRewardVideo()
    {
        PlayerPrefs.SetInt("AmountCoinsTemp", PlayerPrefs.GetInt("AmountCoinsTemp", 0) + 35);
        addingCoins.SetActive(true);
    }

}
