using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AObject : AInteractable
{
    public override string GetInteractionType()
    {
        return "Object";
    }
}
