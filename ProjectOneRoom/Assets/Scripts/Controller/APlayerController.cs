using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class APlayerController : MonoBehaviour
{
    [SerializeField]
    private AObjectManager ObjectManager = null;
    [SerializeField]
    private Transform CrosshairTransform = null;
    [SerializeField]
    private Transform CameraTransform = null;
    [SerializeField]
    private float CameraMoveSpeed = float.NaN;
    [SerializeField]
    private Vector2 CameraMaxOffset = Vector2.zero;
    private TransformProperty PreviousCameraTransformProperty = null;
    private int CurrentCameraTargettingID = 0;

    private class TransformProperty
    {
        public Vector3 Position;
        public Quaternion Rotation;
        public Vector3 Scale;
    }

    public void MoveCameraFrontOf(Transform Target)
    {
        if(Target != null)
        {
            if(PreviousCameraTransformProperty == null)
            {
                PreviousCameraTransformProperty = new TransformProperty();
                SetPropertyWithTransform(CameraTransform, PreviousCameraTransformProperty);
            }
            LerpTransformWithProperty(CameraTransform, GetTransformPropertyFrontOf(Target));
        }
        else if(PreviousCameraTransformProperty != null)
        {
            LerpTransformWithProperty(CameraTransform, PreviousCameraTransformProperty);
            PreviousCameraTransformProperty = null;
        }
        else
        {
            // do nothing
        }
    }

    private void Update()
    {
        MoveCrosshair();
        MoveCamera();
    }

    private void MoveCrosshair()
    {
        Vector2 HalfScreen = new Vector2(Screen.width / 2.0f, Screen.height / 2.0f);
        float Padding = 50.0f;
        Vector2 NewPosition = new Vector2(Input.mousePosition.x - HalfScreen.x, Input.mousePosition.y - HalfScreen.y);
        NewPosition.x = Mathf.Clamp(NewPosition.x, -HalfScreen.x + Padding, +HalfScreen.x - Padding);
        NewPosition.y = Mathf.Clamp(NewPosition.y, -HalfScreen.y + Padding, +HalfScreen.y - Padding);
        CrosshairTransform.localPosition = NewPosition;
    }

    private void MoveCamera()
    {
        Vector2 Directions = GetCameraDirections();
        Vector2 BlockedDirections = Vector2.zero;
        Vector3 DistanceX = CameraTransform.right * Directions.x * CameraMoveSpeed * Time.deltaTime;
        if (Mathf.Abs(CameraTransform.localPosition.x + DistanceX.x) <= CameraMaxOffset.x)
        {
            CameraTransform.Translate(DistanceX);
        }
        else
        {
            BlockedDirections.x = Directions.x;
        }
        Vector3 DistanceY = CameraTransform.up * Directions.y * CameraMoveSpeed * Time.deltaTime;
        if(Mathf.Abs(CameraTransform.localPosition.y + DistanceY.y) <= CameraMaxOffset.y)
        {
            CameraTransform.Translate(DistanceY);
        }
        else
        {
            BlockedDirections.y = Directions.y;
        }
        ObjectManager.UpdateBlockers(BlockedDirections);
    }

    private Vector2 GetCameraDirections()
    {
        Vector2 HalfScreen = new Vector2(Screen.width / 2.0f, Screen.height / 2.0f);
        Vector2 CrosshairPosition = CrosshairTransform.localPosition;
        float Padding = 100.0f;
        // -1: Left, 0: None, +1: Right
        int DirectionX = 0;
        // -1: Down, 0: None, +1: Up
        int DirectionY = 0;
        if (CrosshairPosition.x < (-HalfScreen.x + Padding))
        {
            DirectionX -= 1;
        }
        if (CrosshairPosition.x > (+HalfScreen.x - Padding))
        {
            DirectionX += 1;
        }
        if (CrosshairPosition.y < (-HalfScreen.y + Padding))
        {
            DirectionY -= 1;
        }
        if (CrosshairPosition.y > (+HalfScreen.y - Padding))
        {
            DirectionY += 1;
        }
        return new Vector2(DirectionX, DirectionY);
    }

    private TransformProperty GetTransformPropertyFrontOf(Transform Target)
    {
        TransformProperty ReturnValue = new TransformProperty();
        Vector3 TargetPosition = Target.position;
        Vector3 FrontPosition = TargetPosition + Target.forward;
        Vector3 Direction = (TargetPosition - FrontPosition).normalized;
        ReturnValue.Position = FrontPosition;
        ReturnValue.Rotation = Quaternion.LookRotation(Direction);
        return ReturnValue;
    }

    private async void LerpTransformWithProperty(Transform Target, TransformProperty Property)
    {
        CurrentCameraTargettingID += 1;
        int ID = CurrentCameraTargettingID;
        while(Target.position != Property.Position)
        {
            if(ID == CurrentCameraTargettingID)
            {
                Target.position = Vector3.MoveTowards(Target.position, Property.Position, 0.1f);
                Target.rotation = Quaternion.Lerp(Target.rotation, Property.Rotation, 0.1f);
                await Task.Delay((int)(Time.deltaTime * 1000));
            }
            else
            {
                break;
            }
        }
    }

    private void SetPropertyWithTransform(Transform Target, TransformProperty Property)
    {
        Property.Position = Target.position;
        Property.Rotation = Target.rotation;
        Property.Scale = Target.localScale;
    }

    private void SetTransformWithProperty(Transform Target, TransformProperty Property)
    {
        Target.position = Property.Position;
        Target.rotation = Property.Rotation;
        Target.localScale = Property.Scale;
    }
}
