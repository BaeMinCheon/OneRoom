using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AInteractor : MonoBehaviour
{
    [SerializeField]
    private AObjectManager ObjectManager;

    bool IsInInteraction;

    private void Update()
    {
        TryToInteract();
    }

    private void TryToInteract()
    {
        Vector3 MousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f);
        RaycastHit HitInformation;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(MousePosition), out HitInformation))
        {
            if(HitInformation.transform.CompareTag("Interactable"))
            {
                BeReadyToInteract();
                return;
            }
        }

        LoseInteractable();
    }

    private void BeReadyToInteract()
    {
        if(IsInInteraction == false)
        {
            IsInInteraction = true;
            ObjectManager.UpdateCrosshairs();
        }
    }

    private void LoseInteractable()
    {
        if (IsInInteraction)
        {
            IsInInteraction = false;
            ObjectManager.UpdateCrosshairs();
        }
    }
}
