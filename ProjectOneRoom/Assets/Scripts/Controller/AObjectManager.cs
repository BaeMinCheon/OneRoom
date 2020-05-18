using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RotaryHeart.Lib.SerializableDictionary;

[System.Serializable]
public class StringGameobjectDictionary : SerializableDictionaryBase<string, GameObject>
{
}

public class AObjectManager : MonoBehaviour
{
    [SerializeField]
    private ABlocker[] Blockers = null;
    [SerializeField]
    private StringGameobjectDictionary CrosshairDictionary = null;
    [SerializeField]
    private StringGameobjectDictionary ParticleDictionary = null;
    [SerializeField]
    private StringGameobjectDictionary ToolTipDictionary = null;

    private AInteractor LastInteractorWithRaycast;

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

    public void UpdateCrosshairs(bool IsInteractable)
    {
        if(IsInteractable)
        {
            CrosshairDictionary["Normal"].SetActive(false);
            CrosshairDictionary["Interact"].SetActive(true);
        }
        else
        {
            CrosshairDictionary["Normal"].SetActive(true);
            CrosshairDictionary["Interact"].SetActive(false);
        }
    }

    public void UpdateToolTip(bool IsInteractable)
    {
        Animator ToolTipAnimator = ToolTipDictionary["ToolTipAnimator"].GetComponent<Animator>();
        Text ToolTipText = ToolTipDictionary["ToolTipText"].GetComponent<Text>();
        if (IsInteractable)
        {
            Vector3 TargetPosition = LastInteractorWithRaycast.GetLastTargetPositionOfRaycast();
            ToolTipAnimator.Play("FadeIn");
            ToolTipAnimator.transform.position = Camera.main.WorldToScreenPoint(TargetPosition);
            ToolTipText.text = RequestLastTargetNameOfRaycast();
        }
        else
        {
            ToolTipAnimator.Play("FadeOut");
        }
    }

    public void PlayParticle(string Name)
    {
        GameObject Particle = ParticleDictionary[Name];
        Particle.SetActive(true);
        ParticleSystem ParticleComponent = Particle.GetComponent<ParticleSystem>();
        ParticleComponent.Play();
    }

    public void SetLastInteractorWithRaycast(AInteractor NewInteractor)
    {
        LastInteractorWithRaycast = NewInteractor;
    }

    public Vector3 RequestLastTargetPositionOfRaycast()
    {
        return LastInteractorWithRaycast.GetLastTargetPositionOfRaycast();
    }

    public string RequestLastTargetNameOfRaycast()
    {
        return LastInteractorWithRaycast.GetLastTargetOfRaycast().GetName();
    }
}