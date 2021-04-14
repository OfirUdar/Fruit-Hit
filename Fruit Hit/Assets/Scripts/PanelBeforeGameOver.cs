using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelBeforeGameOver : MonoBehaviour
{
    public Animator anim;
    public GameObject panelGameOver;

    private void OnEnable()
    {
        Invoke("SetTriggerEnd", 10f);
    }
    private void OnDisable()
    {
        CancelInvoke();
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
