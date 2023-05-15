using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class manages the behavior of the circle through it different states.
/// </summary>
public class CircleBehaviour : MonoBehaviour
{
    /// <summary>
    /// Link to the GameManager
    /// </summary>
    [HideInInspector]
    public GameManager gameManager;

    /// <summary>
    /// Link to the CircleFactory
    /// </summary>
    [HideInInspector]
    public CircleFactory circleFactory;


    /// <summary>
    /// Link to the player game object.
    /// </summary>
    public GameObject player;

    /// <summary>
    /// Link to the mesh renderer.
    /// </summary>
    public MeshRenderer mesh;

    /// <summary>
    /// Material to apply when following player.
    /// </summary>
    /// 
    public Material followPlayerMaterial;
    /// <summary>
    /// Material to apply when waiting to be hit by the ray.
    /// </summary>
    public Material waitForRayMaterial;

    /// <summary>
    /// Material to apply once the mechanic is resolved.
    /// </summary>
    public Material resolvedMaterial;

    /// <summary>
    /// Time before the circle Drops to the ground (stops following the player).
    /// </summary>
    public float timeBeforeDropToGround = 4;

    /// <summary>
    /// Time before the circle de-spawns after the ray hits it.
    /// </summary>
    public float timeBeforeDespawn = 3;


    /// <summary>
    /// This circle is following the player.
    /// </summary>
    private bool isFollowingPlayer = true;

    /// <summary>
    /// This circle is waiting to be hit by the ray.
    /// </summary>
    private bool isWaitingForRay = false;

    // Update is called once per frame
    void Update()
    {
        if(isFollowingPlayer)
        {
            transform.position = new Vector3(player.transform.position.x, 0.001f, player.transform.position.z);
        }
    }

    /// <summary>
    /// The circle is following the player.
    /// </summary>
    /// <returns>The circle is following the player.</returns>
    public bool IsFollowingPlayer()
    {
        return isFollowingPlayer;
    }

    /// <summary>
    /// Initialize the Circle, and begins it's behavior.
    /// </summary>
    public void Initialize()
    {
        isFollowingPlayer = true;
        isWaitingForRay=false;

        Material[] lMaterials = mesh.materials;
        lMaterials[0] = followPlayerMaterial;
        mesh.materials = lMaterials;

        StopAllCoroutines();

        StartCoroutine(WaitBeforeDrop());
    }

    /// <summary>
    /// Waits before dropping (stops following the player).
    /// </summary>
    private IEnumerator WaitBeforeDrop()
    {
        yield return new WaitForSeconds(timeBeforeDropToGround);
        isFollowingPlayer=false;
        isWaitingForRay=true;

        Material[] lMaterials = mesh.materials;
        lMaterials[0] = waitForRayMaterial;
        mesh.materials = lMaterials;

        gameManager.OnCirclePlaced();
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Equals("Ray")
           && isWaitingForRay)
        {
            isWaitingForRay= false;

            Material[] lMaterials = mesh.materials;
            lMaterials[0] = resolvedMaterial;
            mesh.materials = lMaterials;

            gameManager.OnCircleResolved();

            StartCoroutine(WaitBeforeDespawn());
        }
    }

    /// <summary>
    /// Wait before despawning.
    /// </summary>
    private IEnumerator WaitBeforeDespawn()
    {
        yield return new WaitForSeconds(timeBeforeDespawn);
        Store();
    }

    /// <summary>
    /// Stores itself in the factory.
    /// </summary>
    public void Store()
    {
        circleFactory.StoreCircle(this.gameObject);
    }
}
