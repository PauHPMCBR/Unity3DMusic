using UnityEngine;
using System.Collections;

public class SpeakerController : MonoBehaviour
{

    float speed = 9.0f;
    Resolution resolution;
    int centerX, centerY;
    static bool autoMoving = true;
    static float tick = 0;
    static float autoSpeed;
    static float distance = 2.5f;
    static GameObject distanceSlider;

    public static void setDistance(float f)
    {
        distance = f * 5;
    }

    public static void resetAutoMovement()
    {
        float prov = Random.value * 0.5f - 0.25f;
        autoSpeed = 0.5f + prov;
        if (Random.value > 0.5) autoSpeed *= -1;
        Debug.Log("autoSpeed is currently: " + autoSpeed);
    }

    public static void toggleAutoMovement()
    {
        autoMoving = !autoMoving;
        distanceSlider.SetActive(autoMoving);
    }

    void setNewPos()
    {
        if (!autoMoving) return;
        float x = Mathf.Sin(tick) * distance;
        float y = Mathf.Cos(tick) * distance;
        tick += autoSpeed * Time.deltaTime;
        transform.position = new Vector3(x, y, -1);
    }

    private void Start()
    {
        resolution = Screen.currentResolution;
        centerX = resolution.width/2;
        centerY = resolution.height/2;
        resetAutoMovement();
        distanceSlider = GameObject.Find("Distance");
    }

    void Update()
    {
        setNewPos(); //update auto movement first

        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
                {
                    var pos = touch.position;
                    Vector3 touchedPos = new Vector3(((float)(pos.x-centerX)/(float)resolution.width)*10, ((float)(pos.y-centerY)/(float)resolution.width)*10, -1);
                    if (Vector2.Distance(new Vector2(touchedPos.x, touchedPos.y), new Vector2(0,0)) <5)
                        transform.position = touchedPos;
                }
            }
        }
        else
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            transform.Translate(new Vector3(horizontal, vertical, 0) * (speed * Time.deltaTime));
        }


    }
}