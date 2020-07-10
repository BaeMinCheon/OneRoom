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
            string Line = Lines[Index];
            string[] Columns = Line.Split(ColumnSeparators);
            Dialog.Name = Columns[1];
            Contexts.Add(GetPreprocessedString(Columns[2]));
            Index += 1;
            while (Index < Lines.Length)
            {
                Line = Lines[Index];
                Columns = Line.Split(ColumnSeparators);
                if (Columns[0].Length == 0)
                {
                    Contexts.Add(GetPreprocessedString(Columns[2]));
                    Index += 1;
                }
                else
                {
                    break;
                }
            }
            Dialog.Contexts = Contexts.ToArray();
            ReturnValue.Add(Dialog);
        }
        return ReturnValue.ToArray();
    }

    private string GetPreprocessedString(string Input)
    {
        string ReturnValue = Input.Replace('`', ',');
        return ReturnValue;
    }
}
