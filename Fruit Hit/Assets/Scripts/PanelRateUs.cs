using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelRateUs : MonoBehaviour
{
    public Animator anim;
    public Image[] starsColor;
    public Color colorStarSellected;
    public Color colorStarUnSellected;

    public GameObject panelGameOver;

    private void OnEnable()
    {
        for (int i = 0; i < starsColor.Length; i++)
            starsColor[i].color = colorStarUnSellected;
    }

    public void SetStarColor(int starNum)
    {
        for (int i = 0; i < starsColor.Length; i++)
            starsColor[i].color = colorStarUnSellected;
        for(int i=0;i<starNum;i++)
            starsColor[i].color= colorStarSellected;

    }

    public void RateUs()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.UdarGames.FruitHit");
        SetTriggerEnd();
    }

    public void SetTriggerEnd()
    {
        anim.SetTrigger("End");
    }

    public void OpenPanelGameOver()
    {
        this.gameObject.SetActive(false);
        panelGameOver.SetActive(true);
    }

}
