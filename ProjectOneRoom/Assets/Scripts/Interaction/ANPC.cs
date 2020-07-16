using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ANPC : AInteractable
{
    private SpriteRenderer SpriteComponent = null;
    private Sprite DefaultSprite = null;

    public override string GetInteractionType()
    {
        return "NPC";
    }

    public void SetSpriteID(int ID)
    {
        if(ID <= 0)
        {
            SpriteComponent.sprite = DefaultSprite;
        }
        else
        {
            Sprite NewSprite = Resources.Load<Sprite>("Characters/" + GetName() + ID.ToString());
            SpriteComponent.sprite = NewSprite;
        }
    }

    private void Start()
    {
        SpriteComponent = GetComponentInChildren<SpriteRenderer>();
        DefaultSprite = Resources.Load<Sprite>("Characters/" + GetName() + "0");
    }
}
