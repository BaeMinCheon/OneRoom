using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADialogParser : MonoBehaviour
{
    public ADialog[] Parse(string Filename)
    {
        List<ADialog> ReturnValue = new List<ADialog>();
        TextAsset FileContent = Resources.Load<TextAsset>(Filename);
        char[] LineSeparators = new char[] { '\n' };
        char[] ColumnSeparators = new char[] { ',' };
        string[] Lines = FileContent.text.Split(LineSeparators);
        int Index = 1;
        while(Index < Lines.Length)
        {
            ADialog Dialog = new ADialog();
            List<string> Contexts = new List<string>();
            List<int> IDs = new List<int>();
            string Line = Lines[Index];
            string[] Columns = Line.Split(ColumnSeparators);
            Dialog.Name = Columns[1];
            Contexts.Add(GetPreprocessedString(Columns[2]));
            IDs.Add(GetPreprocessedID(Columns[3]));
            Index += 1;
            while (Index < Lines.Length)
            {
                Line = Lines[Index];
                Columns = Line.Split(ColumnSeparators);
                if (Columns[0].Length == 0)
                {
                    Contexts.Add(GetPreprocessedString(Columns[2]));
                    IDs.Add(GetPreprocessedID(Columns[3]));
                    Index += 1;
                }
                else
                {
                    break;
                }
            }
            Dialog.Contexts = Contexts.ToArray();
            Dialog.SpriteIDs = IDs.ToArray();
            ReturnValue.Add(Dialog);
        }
        return ReturnValue.ToArray();
    }

    private string GetPreprocessedString(string Input)
    {
        string ReturnValue = Input.Replace('`', ',');
        ReturnValue = ReturnValue.Replace("\\n", "\n");
        return ReturnValue;
    }

    private int GetPreprocessedID(string Input)
    {
        int Result = -1;
        int.TryParse(Input, out Result);
        int ReturnValue = 0;
        if (Result <= 0)
        {
            ReturnValue = 0;
        }
        else
        {
            ReturnValue = Result;
        }
        return ReturnValue;
    }
}
