using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class rotates the gameobject around the "up" axis using the mouse x input.
/// </summary>
public class RotateByMouse : MonoBehaviour
{
    /// <summary>
    /// The sensibility (speed) of the rotation.
    /// </summary>
    public float sensibility = 1000;

    void Update()
    {
        if(Input.GetMouseButton(0)
           || Input.GetMouseButton(1))
        {
            transform.Rotate(transform.up, -Input.GetAxis("Mouse X") * sensibility * Time.deltaTime);
        }
    }
}
