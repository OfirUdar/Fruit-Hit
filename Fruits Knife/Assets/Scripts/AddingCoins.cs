using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class AddingCoins : MonoBehaviour
{
    public SoundsManager soundsManager;
    public TextMeshProUGUI textCoins;
    private int amountCoins;
    private int amountCoinsTemp;


    public void AddCoins()
    {
        amountCoins = PlayerPrefs.GetInt("AmountCoins", 0);
        amountCoinsTemp= PlayerPrefs.GetInt("AmountCoinsTemp", 0);
        StartCoroutine(UpdateCoinsText(amountCoinsTemp, 0.01f));
        PlayerPrefs.SetInt("AmountCoins", PlayerPrefs.GetInt("AmountCoins", 0) + PlayerPrefs.GetInt("AmountCoinsTemp", 0));
        PlayerPrefs.SetInt("AmountCoinsTemp", 0);
        
    }

    IEnumerator UpdateCoinsText(int addingCoins, float time)
    {
        if(addingCoins>0)
        {
            for (int i = 0; i < addingCoins; i++)
            {
                amountCoins++;
                textCoins.text = amountCoins.ToString();
                yield return new WaitForSeconds(time);

            }
        }
        else
        {
            for (int i = addingCoins; i < 0; i++)
            {
                amountCoins--;
                textCoins.text = amountCoins.ToString();
                yield return new WaitForSeconds(time);

            }
        }
       
        PlayerPrefs.SetInt("AmountCoins", amountCoins);
        PlayerPrefs.SetInt("AmountCoinsTemp", 0);
        this.gameObject.SetActive(false);
    }

    public void PlaySound()
    {
        soundsManager.PlayCollectCoins();
    }
}
