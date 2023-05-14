using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class moves the GameObject by using Horizontal and Vertical from the InputManager.
/// It simply translate the object, no acceleration or other fancy things.
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// The speed of the object.
    /// </summary>
    public float speed = 8;

    // Update is called once per frame
    void Update()
    {
        Vector3 lDirection = CalculateDirection();

        transform.position += lDirection * speed * Time.deltaTime;
    }

    private Vector3 CalculateDirection()
    {
        return transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
    }
}