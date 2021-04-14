using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float speed;
    public Vector3 rightPos;
    public Vector3 leftPos;
    public bool isShaking;

    private bool isRight;
    private bool isLeft;


    private void Start()
    {
        isRight = true;
    }



    private void Update()
    {
        if (isShaking)
            Shake();
        else
            transform.position = Vector3.Lerp(transform.position, Vector2.zero, speed*3 * Time.deltaTime);
    }


    private void Shake()
    {
        if (isRight)
            transform.position = Vector3.Lerp(transform.position, rightPos, speed * Time.deltaTime);
        if (isLeft)
            transform.position = Vector3.Lerp(transform.position, leftPos, speed * Time.deltaTime);

        if (isRight && Mathf.Abs(transform.position.x - rightPos.x) < 0.1f)
        {
            isRight = false;
            isLeft = true;
        }
        if (isLeft && Mathf.Abs(transform.position.x - leftPos.x) < 0.1f)
        {
            isLeft = false;
            isRight = true;
        }
    }

}
