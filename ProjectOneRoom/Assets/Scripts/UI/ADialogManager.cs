using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ADialogManager : MonoBehaviour
{
    [SerializeField]
    private GameObject UICanvas;
    [SerializeField]
    private GameObject DialogObject;
    [SerializeField]
    private Text NameText;
    [SerializeField]
    private Text DescriptonText;

    public void ShowDialog()
    {
        HideAllExceptForDialog();
        NameText.text = RequestNameText();
        DescriptonText.text = RequestDescriptionText();
    }

    public void ShowDialog(string Name, string Description)
    {
        HideAllExceptForDialog();
        NameText.text = Name;
        DescriptonText.text = Description;
    }

    public void HideDialog()
    {
        ShowAllExceptForDialog();
    }

    private string RequestNameText()
    {
        return "";
    }

    private string RequestDescriptionText()
    {
        return "";
    }

    private void HideAllExceptForDialog()
    {
        int NumberOfChildren = UICanvas.transform.childCount;
        for(int Index = 0; Index < NumberOfChildren; ++Index)
        {
            UICanvas.transform.GetChild(Index).gameObject.SetActive(false);
        }
        DialogObject.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void ShowAllExceptForDialog()
    {
        int NumberOfChildren = UICanvas.transform.childCount;
        for (int Index = 0; Index < NumberOfChildren; ++Index)
        {
            UICanvas.transform.GetChild(Index).gameObject.SetActive(true);
        }
        DialogObject.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
    }
}
