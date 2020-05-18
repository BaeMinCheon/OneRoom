using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EDirection
{
    None,
    Left,
    Right,
    Up,
    Down
};

public class ABlocker : MonoBehaviour
{
    [SerializeField]
    private Image BlockImage = null;
    [SerializeField]
    private EDirection Direction = EDirection.None;

    public void TurnOnWhenEqualTo(EDirection DirectionX, EDirection DirectionY)
    {
        if((DirectionX == Direction) || (DirectionY == Direction))
        {
            BlockImage.enabled = true;
        }
        else
        {
            BlockImage.enabled = false;
        }
    }
}
