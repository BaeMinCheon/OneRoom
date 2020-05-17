using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Transform CrosshairTransform;
    [SerializeField]
    private Transform CameraTransform;
    [SerializeField]
    private float CameraRotationRate;
    private Vector2 CurrentViewAngle;
    [SerializeField]
    private Vector2 ViewAngleLimit;

    void Update()
    {
        MoveCrosshair();
        TurnHead();
    }

    private void MoveCrosshair()
    {
        Vector2 HalfScreen = new Vector2(Screen.width / 2, Screen.height / 2);
        float Padding = 50.0f;
        Vector2 NewPosition = new Vector2(Input.mousePosition.x - HalfScreen.x, Input.mousePosition.y - HalfScreen.y);
        NewPosition.x = Mathf.Clamp(NewPosition.x, -HalfScreen.x + Padding, +HalfScreen.x - Padding);
        NewPosition.y = Mathf.Clamp(NewPosition.y, -HalfScreen.y + Padding, +HalfScreen.y - Padding);
        CrosshairTransform.localPosition = NewPosition;
    }

    private void TurnHead()
    {
        Vector2 HalfScreen = new Vector2(Screen.width / 2, Screen.height / 2);
        float Padding = 100.0f;
        Vector2 CrosshairPosition = CrosshairTransform.localPosition;
        float Yaw = CurrentViewAngle.x;
        float Pitch = CurrentViewAngle.y;
        // is turning down
        if(CrosshairPosition.y < (-HalfScreen.y + Padding))
        {
            Yaw += CameraRotationRate;
        }
        // is turning up
        if(CrosshairPosition.y > (+HalfScreen.y - Padding))
        {
            Yaw -= CameraRotationRate;
        }
        // is turning left
        if(CrosshairPosition.x < (-HalfScreen.x + Padding))
        {
            Pitch -= CameraRotationRate;
        }
        // is turning right
        if(CrosshairPosition.x > (+HalfScreen.x - Padding))
        {
            Pitch += CameraRotationRate;
        }
        float YawLimit = ViewAngleLimit.x / 2.0f;
        float PitchLimit = ViewAngleLimit.y / 2.0f;
        if(Mathf.Abs(Yaw) < YawLimit)
        {
            CurrentViewAngle.x = Yaw;
        }
        if(Mathf.Abs(Pitch) < PitchLimit)
        {
            CurrentViewAngle.y = Pitch;
        }
        CameraTransform.localEulerAngles = new Vector3(CurrentViewAngle.x, CurrentViewAngle.y, 0.0f);
    }
}
