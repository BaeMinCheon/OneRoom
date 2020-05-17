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
    private Image BlockImage;
    [SerializeField]
    private EDirection Direction;

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
