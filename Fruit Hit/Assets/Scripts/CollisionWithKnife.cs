using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionWithKnife : MonoBehaviour
{
    public GameObject particles;

    private Animator animCam;
    private bool startCheck = false;


    public delegate void OnDestroyListner();
    public static event OnDestroyListner OnDestroy;


    private void Start()
    {
        animCam = Camera.main.GetComponent<Animator>();
        Invoke("StartCheckTrue", 0.4f);
    }
    private void StartCheckTrue()
    {
        startCheck = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Knife")&&startCheck)
        {
            animCam.SetTrigger("Shake");
            Instantiate(particles, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            if (OnDestroy != null)
                OnDestroy();
        }
    }
}
