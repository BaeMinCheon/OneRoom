using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class ANPC : AInteractable
{
    [SerializeField]
    private float BlinkSpriteSpeed = 1.0f;
    private SpriteRenderer SpriteComponent = null;
    private Sprite DefaultSprite = null;
    private int PreviousID = -1;
    private int BlinkID = 0;

    public override string GetInteractionType()
    {
        return "NPC";
    }

    public void SetSpriteID(int ID)
    {
        if(ID <= 0)
        {
            SpriteComponent.sprite = DefaultSprite;
        }
        else
        {
            Sprite NewSprite = Resources.Load<Sprite>("Characters/" + GetName() + ID.ToString());
            SpriteComponent.sprite = NewSprite;
        }
        if(ID != PreviousID)
        {
            BlinkSprite();
        }
        PreviousID = ID;
    }

    private void Start()
    {
        SpriteComponent = GetComponentInChildren<SpriteRenderer>();
        DefaultSprite = Resources.Load<Sprite>("Characters/" + GetName() + "0");
    }

    private void Update()
    {
        LookAtCamera();
    }

    private void LookAtCamera()
    {
        Transform CameraTransform = Camera.main.transform;
        Vector3 RelativePosition = CameraTransform.position - transform.position;
        Quaternion NewRotation = Quaternion.LookRotation(RelativePosition);
        Vector3 NewEuler = new Vector3(0.0f, NewRotation.eulerAngles.y, 0.0f);
        transform.eulerAngles = NewEuler;
    }

    private async void BlinkSprite()
    {
        BlinkID += 1;
        int CurrentBlinkID = BlinkID;
        Color InitialColor = SpriteComponent.color;
        InitialColor.a = 0.0f;
        SpriteComponent.color = InitialColor;
        while(SpriteComponent.color.a < 1.0f)
        {
            if(BlinkID != CurrentBlinkID)
            {
                break;
            }
            Color NewColor = SpriteComponent.color;
            NewColor.a += BlinkSpriteSpeed;
            SpriteComponent.color = NewColor;
            await Task.Delay((int)(Time.deltaTime * 1000.0f));
        }
    }
}
