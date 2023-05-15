using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class manages the behavior of the ray.
/// </summary>
public class RayBehavior : MonoBehaviour
{
    /// <summary>
    /// Link to the player game object.
    /// </summary>
    public GameObject player;

    /// <summary>
    /// Movement speed of the ray.
    /// </summary>
    public float speed = 3;

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(player.transform.position, transform.position) < speed)
        {
            transform.position = player.transform.position;
        }
        else
        {
            Vector3 lTargetPosition = new Vector3(player.transform.position.x, 10, player.transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, lTargetPosition, speed * Time.deltaTime);

        }
    }
}
