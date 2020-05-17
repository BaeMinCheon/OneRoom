using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ADialogTriggerWithParticle : MonoBehaviour
{
    [SerializeField]
    private UnityEvent Callback;

    private void OnParticleSystemStopped()
    {
        Callback.Invoke();
        gameObject.SetActive(false);
    }
}
