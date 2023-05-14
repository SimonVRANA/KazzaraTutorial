using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Quits the game on escape press.
/// </summary>
public class ExitGame : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
