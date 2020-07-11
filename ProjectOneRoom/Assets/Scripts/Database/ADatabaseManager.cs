using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADatabaseManager : MonoBehaviour
{
    public static ADatabaseManager Instance = null;
    public static bool IsInstanceInitialized = false;

    [SerializeField]
    private string DialogFilename = "";
    private Dictionary<int, ADialog> DialogMap = new Dictionary<int, ADialog>();

    public ADialog[] GetDialogs(int Begin, int End)
    {
        List<ADialog> ReturnValue = new List<ADialog>();
        for(int Index = Begin; Index < End; ++Index)
        {
            ReturnValue.Add(DialogMap[Index]);
        }
        return ReturnValue.ToArray();
    }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            ADialogParser Parser = GetComponent<ADialogParser>();
            ADialog[] Dialogs = Parser.Parse(DialogFilename);
            for(int Index = 0; Index < Dialogs.Length; ++Index)
            {
                DialogMap.Add(Index, Dialogs[Index]);
            }
            IsInstanceInitialized = true;
        }
    }
}
