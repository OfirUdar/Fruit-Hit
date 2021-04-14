using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class KnifeLauncher : MonoBehaviour
{
    private Vector3 startPosition;
    private Vector3 endPosition;
    private float speedForce;

    public Transform previewLaunch;
    public GameObject touchMarkPrefab;
    private GameObject touchMark;
    public float distanceCancel=0.3f;

    private bool isShoot = false;
    private bool isHitted = false;

    public delegate void OnShootListener();
    public static event OnShootListener OnShoot;

    public delegate void OnHitListener();
    public static event OnHitListener OnHit;

    public delegate void OnMissedListener();
    public static event OnMissedListener OnMissed;

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (isShoot)
        {
            transform.position += transform.up * speedForce * Time.deltaTime;
        }
        else
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            if (Input.GetMouseButtonDown(0))
            {
                //start touch
                startPosition = mousePosition;
                //startPosition = transform.position;
                //previewLaunch.gameObject.SetActive(true);
                touchMark = Instantiate(touchMarkPrefab, startPosition, Quaternion.identity);
            }
            else
                if (Input.GetMouseButton(0))
            {
                //dragging
                endPosition = mousePosition;
                Vector2 direction = endPosition - startPosition;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.AngleAxis(angle - 270, Vector3.forward);
                transform.rotation = rotation;

                // Preview Launch

                previewLaunch.gameObject.SetActive(Vector2.Distance(startPosition, endPosition) > distanceCancel);
                

                 float scalePreviewLaunch = direction.magnitude;
                if (scalePreviewLaunch > 2)
                    scalePreviewLaunch = 2;
                else
                    if (scalePreviewLaunch < 0.9f)
                    scalePreviewLaunch = .9f;

                previewLaunch.localScale = new Vector2(scalePreviewLaunch, scalePreviewLaunch);
                previewLaunch.GetComponent<Animator>().speed = scalePreviewLaunch / 1.5f;


            }
            else
                if (Input.GetMouseButtonUp(0))
            {
                Destroy(touchMark);
                if (previewLaunch.gameObject.activeSelf)
                {
                    //end touch and launch knife
                    previewLaunch.gameObject.SetActive(false);
                    speedForce = (endPosition - startPosition).magnitude * 10;
                    if (speedForce < 13)
                        speedForce = 13;
                    if (speedForce > 60)
                        speedForce = 60;
                    isShoot = true;
                    Destroy(this.transform.parent.gameObject, 1);
                    Invoke("IsMissed", 0.5f);
                    if (OnShoot != null)
                        OnShoot();
                }
                
            }
        }
       

       
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fruit")|| collision.CompareTag("Master"))
        {
            isHitted = true;
            if (OnHit != null)
                OnHit();
        }
    }
    private void IsMissed()
    {
        if (!isHitted)
            if (OnMissed != null)
                OnMissed();
    }
}
