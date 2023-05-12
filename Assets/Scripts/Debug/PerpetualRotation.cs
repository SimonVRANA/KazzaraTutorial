using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A small debugging script that rotates an object (used to test Time.timeScale).
/// </summary>
public class PerpetualRotation : MonoBehaviour
{
    public enum RotationAxis{X, Y, Z}

    /// <summary>
    /// Rotation speed in degres
    /// </summary>
    public float rotationSpeed = 2;

    /// <summary>
    /// The axis around wich rotation is done
    /// </summary>
    public RotationAxis rotationAxis = RotationAxis.Y;

    // Update is called once per frame
    void Update()
    {
        Vector3 lRotationAxis;

        switch(rotationAxis)
        {
            case RotationAxis.X:
                lRotationAxis = Vector3.right;
                break;
            case RotationAxis.Z:
                lRotationAxis = Vector3.forward;
                break;
            default:
                lRotationAxis = Vector3.up;
                break;
        }

        float lAjustedSpeed = rotationSpeed * Time.deltaTime;
        transform.Rotate(lRotationAxis, lAjustedSpeed);
    }
}
