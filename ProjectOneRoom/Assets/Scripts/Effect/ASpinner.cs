using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASpinner : MonoBehaviour
{
    [SerializeField]
    private Vector3 RotateVector;
    [SerializeField]
    private float SpinSpeed;

    private void Update()
    {
        transform.Rotate(RotateVector * SpinSpeed * Time.deltaTime);
    }
}
