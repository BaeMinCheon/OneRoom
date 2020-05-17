using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Transform CrosshairTransform;

    void Update()
    {
        UpdateCrosshairTransform();
    }

    private void UpdateCrosshairTransform()
    {
        int HalfWidth = Screen.width / 2;
        int HalfHeight = Screen.height / 2;
        float NewMousePositionX = Input.mousePosition.x - HalfWidth;
        float NewMousePositionY = Input.mousePosition.y - HalfHeight;
        float Padding = 50.0f;
        NewMousePositionX = Mathf.Clamp(NewMousePositionX, -HalfWidth + Padding, +HalfWidth - Padding);
        NewMousePositionY = Mathf.Clamp(NewMousePositionY, -HalfHeight + Padding, +HalfHeight - Padding);
        Vector2 NewPosition = new Vector2(NewMousePositionX, NewMousePositionY);
        CrosshairTransform.localPosition = NewPosition;
    }
}
