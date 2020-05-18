using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AInteractable : MonoBehaviour
{
    [SerializeField]
    private string NameText = string.Empty;

    public virtual string GetInteractionType()
    {
        return "None";
    }

    public string GetName()
    {
        return NameText;
    }
}
