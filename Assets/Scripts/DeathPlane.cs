using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    /// <summary>
    /// Link to the game manager
    /// </summary>
    public GameManager gameManager;

    private void OnCollisionEnter(Collision collision)
    {
        gameManager.Initialize();
    }
}
