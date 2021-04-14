
using UnityEngine;
using TMPro;
public class CombosManager : MonoBehaviour
{
    public ParticleSystem lightning1, lightning2;
    public CameraShake camShake;
    public Animator animComboWord;
    public TextMeshProUGUI comboWordTxt;

    public int comboLevel;
    private void OnEnable()
    {
        KnifeLauncher.OnHit += OnKnifeHit;
        KnifeLauncher.OnMissed += OnKnifeMissed;
        StageManager.OnGameOver += EndCombo;

    }

   

    private void OnDisable()
    {
        KnifeLauncher.OnHit -= OnKnifeHit;
        KnifeLauncher.OnMissed -= OnKnifeMissed;
        StageManager.OnGameOver -= EndCombo;
    }



    private void Start()
    {
        EndCombo();
    }


    private void OnKnifeHit()
    {
        comboLevel++;
        RiseCombo(comboLevel);
    }

    private void OnKnifeMissed()
    {
        if(comboLevel>=10&&comboLevel<=15)
            ShowComboWord("Loser");
        else
          if (comboLevel>15&&comboLevel<=20)   
              ShowComboWord("Cool");
            else
                if (comboLevel > 20 && comboLevel <= 25)
                      ShowComboWord("Amazing!");
              else
                   if (comboLevel > 25 && comboLevel <= 30)
                       ShowComboWord("Great!");
                    else
                         if (comboLevel > 30 && comboLevel <= 35)
                              ShowComboWord("Unbelievable!");
                        else
                             if (comboLevel > 35)
                                 ShowComboWord("Fanastic!");

                  
        EndCombo();
    }

    private void StartCombo()
    {
        lightning1.gameObject.SetActive(true);
        lightning2.gameObject.SetActive(true);
        camShake.isShaking = true;
    }

    public void RiseCombo(int levelCombo)
    {
        if (levelCombo == 1)
            StartCombo();
        RaiseLighting(levelCombo);
        RaiseShakeScreen(levelCombo);
        ShowComboWord("x " + levelCombo);
    }

    public void EndCombo()
    {
        comboLevel = 0;
        lightning1.gameObject.SetActive(false);
        lightning2.gameObject.SetActive(false);
        camShake.isShaking = false;
        
    }

    private void RaiseLighting(int levelCombo)
    {
        int maxParticles = 40;
        if (levelCombo * 2 < maxParticles)
            maxParticles = levelCombo * 2;
        lightning1.maxParticles = maxParticles;
        lightning2.maxParticles = maxParticles;
    }

    private void RaiseShakeScreen(int levelCombo)
    {
        float maxSpeed = 10;
        if (levelCombo / 2.5f < maxSpeed)
            maxSpeed = levelCombo / 2.5f;
        if (maxSpeed < 4)
            maxSpeed = 4;
        camShake.speed = maxSpeed;
        if (levelCombo < 8)
            camShake.speed = 0;
    }

    private void ShowComboWord(string word)
    {
        animComboWord.enabled = true;
        animComboWord.Rebind();
        comboWordTxt.text = word;
    }

}
