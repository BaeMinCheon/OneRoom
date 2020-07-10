using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AInteractionEvent : MonoBehaviour
{
    [SerializeField]
    private ADialogEvent DialogEvent = null;

    public ADialog[] GetDialogs()
    {
        return DialogEvent.Dialogs;
    }

    private void Start()
    {
        DialogEvent.Dialogs = ADatabaseManager.Instance.GetDialogs(DialogEvent.IndexFrom, DialogEvent.IndexTo);
    }
}
