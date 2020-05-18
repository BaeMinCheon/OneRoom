using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AThrowableParticle : MonoBehaviour
{
    [SerializeField]
    private AObjectManager ObjectManager = null;
    [SerializeField]
    private ParticleSystem HitEffectParticle = null;
    [SerializeField]
    private float MoveSpeed = float.NaN;
    private Vector3 TargetPosition = Vector3.zero;

    public void SetTargetPosition(Vector3 NewTargetPosition)
    {
        TargetPosition = NewTargetPosition;
    }

    private void OnEnable()
    {
        TargetPosition = ObjectManager.RequestLastTargetPositionOfRaycast();
        gameObject.transform.position = Camera.main.transform.position;
    }

    private void Update()
    {
        Vector3 RemainingDistance = transform.position - TargetPosition;
        float TinyDistance = 0.1f;
        if(RemainingDistance.sqrMagnitude >= TinyDistance)
        {
            transform.position = Vector3.Lerp(transform.position, TargetPosition, MoveSpeed);
        }
        else
        {
            HitEffectParticle.gameObject.SetActive(true);
            HitEffectParticle.transform.position = transform.position;
            HitEffectParticle.Play();
            gameObject.SetActive(false);
        }
    }
}
