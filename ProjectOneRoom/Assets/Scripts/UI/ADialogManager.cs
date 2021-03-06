﻿using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ADialogManager : MonoBehaviour
{
    [SerializeField]
    private AObjectManager ObjectManager = null;
    [SerializeField]
    private GameObject UICanvas = null;
    [SerializeField]
    private GameObject DialogObject = null;
    [SerializeField]
    private AInteractor Interactor = null;
    [SerializeField]
    private Text NameText = null;
    [SerializeField]
    private Text DescriptonText = null;
    [SerializeField]
    private int TextDelay = 0;
    private bool IsDialogShowing = false;
    private ADialogEvent DialogEvent = null;
    private int DialogIndex = 0;
    private int DescriptionIndex = 0;
    private APlayerController PlayerController = null;

    public void ShowDialog()
    {
        IsDialogShowing = true;
        HideAllWidgetExceptForDialog();
        SetupDialog();
    }

    public void HideDialog()
    {
        IsDialogShowing = false;
        ShowAllWidgetExceptForDialog();
    }

    private void Start()
    {
        PlayerController = Interactor.GetComponent<APlayerController>();
    }

    private void Update()
    {
        if(IsDialogShowing)
        {
            if(Input.GetMouseButtonUp(0))
            {
                UpdateDialog();
            }
        }
    }

    private void SetupDialog()
    {
        AInteractable Interactable = Interactor.GetLastTargetOfRaycast();
        DialogEvent = Interactable.GetComponent<AInteractionEvent>().GetDialogEvent();
        DialogIndex = DialogEvent.IndexBegin;
        DescriptionIndex = 0;
        PlayerController.CaptureCameraTransformProperty();
        UpdateText();
        UpdateCamera();
        UpdateNPC(false);
    }

    private void UpdateDialog()
    {
        DescriptionIndex += 1;
        if(DescriptionIndex >= DialogEvent.Dialogs[DialogIndex].Contexts.Length)
        {
            DialogIndex += 1;
            DescriptionIndex = 0;
        }

        if (IsDialogEnd())
        {
            HideDialog();
            UpdateCamera();
            UpdateNPC(true);
        }
        else
        {
            UpdateText();
            UpdateCamera();
            UpdateNPC(false);
        }
    }

    private void UpdateText()
    {
        NameText.text = GetDialogName();
        UpdateDescriptionWithTextDelay();
    }

    private void UpdateCamera()
    {
        if (IsDialogEnd())
        {
            PlayerController.MoveCameraFrontOf(null);
            PlayerController.ReleaseCameraTransformProperty();
        }
        else
        {
            PlayerController.MoveCameraFrontOf(DialogEvent.CameraTargets[DialogIndex]);
        }
    }

    private void UpdateNPC(bool IsReset)
    {
        string CharacterName = string.Empty;
        if (IsDialogEnd())
        {
            CharacterName = DialogEvent.Dialogs[DialogIndex - 1].Name;
        }
        else
        {
            CharacterName = DialogEvent.Dialogs[DialogIndex].Name;
        }
        int ID = IsReset ? 0 : DialogEvent.Dialogs[DialogIndex].SpriteIDs[DescriptionIndex];
        ObjectManager.UpdateNPC(CharacterName, ID);
    }

    private string GetDialogName()
    {
        return DialogEvent.Dialogs[DialogIndex].Name;
    }

    private string GetDialogDescription()
    {
        return DialogEvent.Dialogs[DialogIndex].Contexts[DescriptionIndex];
    }

    private async void UpdateDescriptionWithTextDelay()
    {
        int IndexValidator01 = DialogIndex;
        int IndexValidator02 = DescriptionIndex;
        DescriptonText.text = "";
        string NewDescription = GetDialogDescription();
        for(int Index = 0; Index < NewDescription.Length; ++Index)
        {
            bool Validation01 = IndexValidator01 == DialogIndex;
            bool Validation02 = IndexValidator02 == DescriptionIndex;
            if (Validation01 && Validation02)
            {
                DescriptonText.text += NewDescription[Index];
                await Task.Delay(TextDelay);
            }
            else
            {
                return;
            }
        }
    }

    private bool IsDialogEnd()
    {
        return DialogIndex >= DialogEvent.IndexEnd;
    }

    private void HideAllWidgetExceptForDialog()
    {
        int NumberOfChildren = UICanvas.transform.childCount;
        for(int Index = 0; Index < NumberOfChildren; ++Index)
        {
            UICanvas.transform.GetChild(Index).gameObject.SetActive(false);
        }
        DialogObject.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void ShowAllWidgetExceptForDialog()
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
