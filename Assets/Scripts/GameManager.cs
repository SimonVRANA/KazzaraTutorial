using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class manages the game by spawning and despawning entities at the good timings.
/// Also manages the help text that appears on top of the screen.
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Link to the player game object.
    /// </summary>
    public GameObject player;

    /// <summary>
    /// Link to the circle factory;
    /// </summary>
    public CircleFactory circleFactory;

    /// <summary>
    /// Link to the ray.
    /// </summary>
    public GameObject ray;


    /// <summary>
    /// Text displayed when waiting for the first part of the mechanic.
    /// </summary>
    public GameObject waitForMechanicText;

    /// <summary>
    /// Text displayed when the player have to place the circle.
    /// </summary>
    public GameObject placeCircleText;

    /// <summary>
    /// Text displayed when waiting for the ray.
    /// </summary>
    public GameObject waitRayText;

    /// <summary>
    /// Text displayed when the player needs to place the ray.
    /// </summary>
    public GameObject placeRayText;


    /// <summary>
    /// Time to wait before the next circle appears.
    /// </summary>
    public float timeBeforeCircleSpawn = 5;

    /// <summary>
    /// Time to wait before the ray appears.
    /// </summary>
    public float timeBeforeRaySpawn = 5;

    /// <summary>
    /// Initialize the game and starts the mechanics
    /// </summary>
    public void Initialize()
    {
        player.transform.localPosition = new Vector3(0, 1.01f, 0);
        player.transform.localRotation = Quaternion.identity;

        StoreAllCircles();

        ray.SetActive(false);

        waitForMechanicText.SetActive(false);
        placeCircleText.SetActive(false);
        waitRayText.SetActive(false);
        placeRayText.SetActive(false);

        StopAllCoroutines();

        StartCoroutine(WaitBeforeSpawnCircle());
    }

    /// <summary>
    /// Stores all the circles.
    /// </summary>
    private void StoreAllCircles()
    {
        // FindObjectsByType is not very effective, but this function is only called on initialization so it should be fine.
        CircleBehaviour[] lCircles = FindObjectsByType<CircleBehaviour>(FindObjectsSortMode.None);
        for(int i = 0; i < lCircles.Length; i++)
        {
            lCircles[i].Store();
        }
    }

    /// <summary>
    /// Waits then spawns a circle.
    /// Also updates the texts.
    /// </summary>
    private IEnumerator WaitBeforeSpawnCircle()
    {
        waitForMechanicText.SetActive(true);
        yield return new WaitForSeconds(timeBeforeCircleSpawn);

        GameObject lCircle = circleFactory.GetOrCreateCircle();
        CircleBehaviour lCircleBehaviour= lCircle.GetComponent<CircleBehaviour>();
        if(lCircleBehaviour != null )
        {
            lCircleBehaviour.gameManager = this;
            lCircleBehaviour.circleFactory = circleFactory;
            lCircleBehaviour.player = player;

            lCircleBehaviour.gameObject.SetActive(true);
            lCircleBehaviour.Initialize();

            waitForMechanicText.SetActive(false);
            placeCircleText.SetActive(true);
        }
    }

    /// <summary>
    /// Continues the mechanic once the circle have been placed.
    /// </summary>
    public void OnCirclePlaced()
    {
        placeCircleText.SetActive(false);
        waitRayText.SetActive(true);

        StartCoroutine(WaitBeforeSpawnRay());
    }

    /// <summary>
    /// Waits before spawning the ray.
    /// </summary>
    private IEnumerator WaitBeforeSpawnRay()
    {
        yield return new WaitForSeconds(timeBeforeRaySpawn);

        waitRayText.SetActive(false);
        placeRayText.SetActive(true);

        Vector3 lRaySpawnPosition= player.transform.position;
        lRaySpawnPosition = lRaySpawnPosition + new Vector3(5, 0, 0);
        lRaySpawnPosition= new Vector3(lRaySpawnPosition.x, 10, lRaySpawnPosition.z);

        ray.transform.position = lRaySpawnPosition;
        ray.transform.RotateAround(player.transform.position, Vector3.up, Random.Range(0f, 360f));

        ray.SetActive(true);
    }

    /// <summary>
    /// Cleans the game and starts over.
    /// </summary>
    public void OnCircleResolved()
    {
        placeRayText.SetActive(false);
        ray.SetActive(false);

        StopAllCoroutines();

        StartCoroutine(WaitBeforeSpawnCircle());
    }
}
