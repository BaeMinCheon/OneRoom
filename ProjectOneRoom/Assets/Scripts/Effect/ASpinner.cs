using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASpinner : MonoBehaviour
{
    [SerializeField]
    private Vector3 RotateVector = Vector3.zero;
    [SerializeField]
    private float SpinSpeed = float.NaN;

    private void Update()
    {
        transform.Rotate(RotateVector * SpinSpeed * Time.deltaTime);
    }
}
