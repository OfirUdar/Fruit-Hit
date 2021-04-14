using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullScreenBackground : MonoBehaviour
{
    public Camera cam;
    public float camSizeDefulat=5;
    SpriteRenderer bk;
    private void Awake()
    {
        bk = GetComponent<SpriteRenderer>();
        ResizeBackground();
    }
    private void LateUpdate()
    {
        if(cam.orthographicSize!= camSizeDefulat)
              ResizeBackground();
    }

    void ResizeBackground()
    {
         bk = GetComponent<SpriteRenderer>();
        if (bk == null) return;

        transform.localScale = new Vector3(1, 1, 1);

        float width = bk.sprite.bounds.size.x;
        float height = bk.sprite.bounds.size.y;
       

        float worldScreenHeight = Camera.main.orthographicSize * 2f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        Vector3 xWidth = transform.localScale;
        xWidth.x = worldScreenWidth / width;
        transform.localScale = xWidth;
        //transform.localScale.x = worldScreenWidth / width;
        Vector3 yHeight = transform.localScale;
        yHeight.y = worldScreenHeight / height;
        transform.localScale = yHeight;
        //transform.localScale.y = worldScreenHeight / height;

    }
}
