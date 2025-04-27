using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFit : MonoBehaviour
{
    public GameObject backgroundImage;
    public Camera mainCam;

	// Use this for initialization
	void Start () {
        scaleBackground();
        AddCollider();
	}

    public void AddCollider () 
    {
      var bottomLeft = (Vector2)mainCam.ScreenToWorldPoint(new Vector3(0, 0, mainCam.nearClipPlane));
      var topLeft = (Vector2)mainCam.ScreenToWorldPoint(new Vector3(0, mainCam.pixelHeight, mainCam.nearClipPlane));
      var topRight = (Vector2)mainCam.ScreenToWorldPoint(new Vector3(mainCam.pixelWidth, mainCam.pixelHeight, mainCam.nearClipPlane));
      var bottomRight = (Vector2)mainCam.ScreenToWorldPoint(new Vector3(mainCam.pixelWidth, 0, mainCam.nearClipPlane));

      // add or use existing EdgeCollider2D
      var edge = GetComponent<EdgeCollider2D>();

      var edgePoints = new [] {bottomLeft,topLeft,topRight,bottomRight, bottomLeft};
      edge.points = edgePoints;
    }

    public void scaleBackground(){
        Vector2 deviceScreenResolution = new Vector2(Screen.width, Screen.height);
        float srcHeight = Screen.height;
        float srcWidth = Screen.width;
        float DEVICE_SCREEN_ASPECT = srcWidth / srcHeight;
        mainCam.aspect = DEVICE_SCREEN_ASPECT;
        float camHeight = 100.0f * mainCam.orthographicSize * 2.0f;
        float camWidth = camHeight * DEVICE_SCREEN_ASPECT;
        SpriteRenderer backgroundImageSR = backgroundImage.GetComponent<SpriteRenderer>();
        float bgImgH = backgroundImageSR.sprite.rect.height;
        float bgImgW = backgroundImageSR.sprite.rect.width;
        float bgImg_scale_ratio_Height = camHeight / bgImgH;
        float bgImg_scale_ratio_Width = camWidth / bgImgW;
        backgroundImage.transform.localScale = new Vector3(bgImg_scale_ratio_Width, bgImg_scale_ratio_Height, 1);
    }
}
