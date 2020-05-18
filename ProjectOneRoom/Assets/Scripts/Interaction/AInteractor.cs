using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AInteractor : MonoBehaviour
{
    [SerializeField]
    private AObjectManager ObjectManager = null;

    private bool IsReadyToInteract = false;
    private RaycastHit LastHitResult = new RaycastHit();
    private AInteractable LastTarget = null;

    public AInteractable GetLastTargetOfRaycast()
    {
        return LastTarget;
    }

    public Vector3 GetLastTargetPositionOfRaycast()
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
            // do not allow raycast when there is ui object
            if (EventSystem.current.IsPointerOverGameObject())
            {
                AInteractable Interactable = LastHitResult.transform.GetComponent<AInteractable>();
                if (Interactable != null)
                {
                    LastTarget = Interactable;
                    BeReadyToInteract();
                    IsFailedToFindInteractable = false;
                }
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
            ObjectManager.SetLastInteractorWithRaycast(this);
            ObjectManager.UpdateCrosshairs(true);
            ObjectManager.UpdateToolTip(true);
        }
    }

    private void LoseInteractable()
    {
        if (IsReadyToInteract)
        {
            IsReadyToInteract = false;
            ObjectManager.UpdateCrosshairs(false);
            ObjectManager.UpdateToolTip(false);
        }
    }

    private void Interact()
    {
        ObjectManager.PlayParticle("Question");
    }
}
