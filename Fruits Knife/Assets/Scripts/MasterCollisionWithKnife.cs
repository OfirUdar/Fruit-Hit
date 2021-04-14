using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MasterCollisionWithKnife : MonoBehaviour
{
    public Animator anim;
    public Slider sliderHealth;
    public GameObject particles;
    public int life = 1;

    private Animator animCam;
    private bool startCheck = false;





    public delegate void OnDestroyListner();
    public static event OnDestroyListner OnDestroy;

    public delegate void OnDamagedListner();
    public static event OnDamagedListner OnDamaged;

    private void Start()
    {
        animCam = Camera.main.GetComponent<Animator>();
        Invoke("StartCheckTrue", 0.4f);
        sliderHealth.maxValue = life;
        sliderHealth.value = life;
    }

    private void Update()
    {
        if (sliderHealth.value > life)
            sliderHealth.value = Mathf.Lerp(sliderHealth.value, life, Time.deltaTime * 4);
    }


    private void StartCheckTrue()
    {
        startCheck = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Knife") && startCheck)
        {
            life--;
            anim.SetTrigger("Damaged");
            if (life <= 0)
            {
                animCam.SetTrigger("Shake");
                Instantiate(particles, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
                if (OnDestroy != null)
                    OnDestroy();
            }  
            else
            {
                if (OnDamaged != null)
                    OnDamaged();
            }
           
        }
    }
}
