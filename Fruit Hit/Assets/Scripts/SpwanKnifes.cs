using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpwanKnifes : MonoBehaviour
{
    public GameObject[] knives;


    public int amount_Knifes=0;
    public TextMeshPro amount_Knifes_Text;
    public SoundsManager soundsManager;
    private int indexKnife;


    private void OnEnable()
    {
        indexKnife = PlayerPrefs.GetInt("NumKnifeDefualt", 0);
        KnifeLauncher.OnShoot += OnKnifeShoot;
        CollisionWithKnife.OnDestroy += OnDestroyFruit;
        MasterCollisionWithKnife.OnDamaged += OnDamagedMaster;
        MasterCollisionWithKnife.OnDestroy += OnDestroyMaster;    
    }
    private void OnDisable()
    {
        KnifeLauncher.OnShoot -= OnKnifeShoot;
        CollisionWithKnife.OnDestroy -= OnDestroyFruit;
        MasterCollisionWithKnife.OnDamaged -= OnDamagedMaster;
        MasterCollisionWithKnife.OnDestroy -= OnDestroyMaster;
    }

    private void OnKnifeShoot()
    {
        soundsManager.PlayKnifeSlash(Random.Range(0.9f, 2.5f));
        if (amount_Knifes >1)
            CreateKnife();
        DisplayAmountKnifes();
        if (PlayerPrefs.GetString("VibrationMute", "false").Equals("false"))
            Vibration.Vibrate(10);
    }

    private void OnDestroyFruit()
    {
        soundsManager.PlaySqualch(Random.Range(0.7f, 2.5f));
    }

    private void OnDamagedMaster()
    {
        soundsManager.PlayHit(Random.Range(0.7f, 2.5f));
    }

    private void OnDestroyMaster()
    {
        soundsManager.PlaySqualch(Random.Range(0.7f, 2.5f));
    }

    public void CreateKnife()
    {
        Instantiate(knives[indexKnife],this.transform.position, Quaternion.Euler(Vector2.up),this.transform);
    }

    public void DisplayAmountKnifes()
    {
        amount_Knifes--;
        amount_Knifes_Text.text = "x " + amount_Knifes;
    }
}
