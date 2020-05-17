using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AObjectManager : MonoBehaviour
{
    [SerializeField]
    private ABlocker[] Blockers;
    [SerializeField]
    private GameObject[] Crosshairs;

    public void UpdateBlockers(Vector2 Directions)
    {
        EDirection DirectionX = EDirection.None;
        switch (Directions.x)
        {
            case -1.0f:
                DirectionX = EDirection.Left;
                break;
            case +1.0f:
                DirectionX = EDirection.Right;
                break;
            case 0.0f:
                break;
            default:
                print("UpdateBlockers(): invalid Directions.x");
                break;
        }
        EDirection DirectionY = EDirection.None;
        switch (Directions.y)
        {
            case -1.0f:
                DirectionY = EDirection.Down;
                break;
            case +1.0f:
                DirectionY = EDirection.Up;
                break;
            case 0.0f:
                break;
            default:
                print("UpdateBlockers(): invalid Directions.y");
                break;
        }

        foreach (ABlocker Blocker in Blockers)
        {
            Blocker.TurnOnWhenEqualTo(DirectionX, DirectionY);
        }
    }

    public void UpdateCrosshairs()
    {
        foreach (GameObject Crosshair in Crosshairs)
        {
            Crosshair.SetActive(!Crosshair.activeSelf);
        }
    }
}
