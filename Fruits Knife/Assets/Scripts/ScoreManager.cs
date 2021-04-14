using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreManager : MonoBehaviour
{

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI levelText;
    public long score=0;


    private void Start()
    {
        UpdateScoreText();
    }
    private void OnEnable()
    {
        CollisionWithKnife.OnDestroy += OnDestroyFruit;
    }

    private void OnDisable()
    {
        CollisionWithKnife.OnDestroy -= OnDestroyFruit;
        if (score > PlayerPrefs.GetInt("HighestScore", 0))
            PlayerPrefs.SetInt("HighestScore", (int)score);
    }

    private void OnDestroyFruit()
    {
        if(Random.Range(0, 4)==1)
             PlayerPrefs.SetInt("AmountCoinsTemp", PlayerPrefs.GetInt("AmountCoinsTemp",0)+1);
        SetScore(Random.Range(49,65));
    }

    private void SetScore(int addScore)
    {
        StartCoroutine(UpdateScoreText(addScore, 0.01f));
       
    }
    IEnumerator UpdateScoreText(int addScore,float time)
    {
        for (int i = 0; i < addScore; i++)
        {
            score++;
            UpdateScoreText();
            yield return new WaitForSeconds(time);

        }

    }
    private void UpdateScoreText()
    {
         scoreText.text = score.ToString();
    }
}
