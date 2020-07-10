using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ADialog
{
    [Tooltip("name of character")]
    public string Name;
    [Tooltip("contents of dialog")]
    public string[] Contexts;
}

[System.Serializable]
public class ADialogEvent
{
    public string Name;
    public int IndexFrom;
    public int IndexTo;
    public ADialog[] Dialogs;
}