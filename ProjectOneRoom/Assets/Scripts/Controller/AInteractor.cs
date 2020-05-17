using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AInteractor : MonoBehaviour
{
    [SerializeField]
    private AObjectManager ObjectManager;

    bool IsReadyToInteract;
    RaycastHit LastHitResult;

    public Vector3 GetLastTargetPositionOfInteraction()
    {
        return LastHitResult.collider.transform.position;
    }

    private void Update()
    {
        TryToInteract();
    }

    private void TryToInteract()
    {
        bool IsFailedToFindInteractable = true;
        Vector3 MousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f);
        if (Physics.Raycast(Camera.main.ScreenPointToRay(MousePosition), out LastHitResult))
        {
            if (LastHitResult.transform.CompareTag("Interactable"))
            {
                BeReadyToInteract();
                IsFailedToFindInteractable = false;
            }
        }
        if (IsFailedToFindInteractable)
        {
            LoseInteractable();
        }
        else
        {
            if (Input.GetMouseButtonUp(0))
            {
                Interact();
            }
        }
    }

    private void BeReadyToInteract()
    {
        if(IsReadyToInteract == false)
        {
            IsReadyToInteract = true;
            ObjectManager.UpdateCrosshairs(true);
        }
    }

    private void LoseInteractable()
    {
        if (IsReadyToInteract)
        {
            IsReadyToInteract = false;
            ObjectManager.UpdateCrosshairs(false);
        }
    }

    private void Interact()
    {
        ObjectManager.SetLastInteractor(this);
        ObjectManager.PlayParticle("Question");
    }
}
