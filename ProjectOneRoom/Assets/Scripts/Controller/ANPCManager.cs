using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ANPCManager : MonoBehaviour
{
    private string PreviousName = string.Empty;
    private ANPC[] NPCs = null;

    public void UpdateNPC(string TargetName, int ID)
    {
        if(PreviousName.Length > 0)
        {
            UpdateNPC_Impl(PreviousName, 0);
        }
        PreviousName = TargetName;
        UpdateNPC_Impl(TargetName, ID);
    }

    private void Start()
    {
        NPCs = FindObjectsOfType<ANPC>();
    }

    private void UpdateNPC_Impl(string TargetName, int ID)
    {
        foreach (ANPC NPC in NPCs)
        {
            string NPCName = NPC.GetName();
            if(NPCName.CompareTo(TargetName) == 0)
            {
                NPC.SetSpriteID(ID);
            }
        }
    }
}
