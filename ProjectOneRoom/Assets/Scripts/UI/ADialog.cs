using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ADialog
{
    [HideInInspector]
    public string Name;
    [HideInInspector]
    public string[] Contexts;
}

[System.Serializable]
public class ADialogEvent
{
    public string Name;
    public int IndexBegin;
    public int IndexEnd;
    public ADialog[] Dialogs;
    public Transform[] CameraTargets;
}