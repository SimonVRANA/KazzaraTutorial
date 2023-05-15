using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBehaviour : MonoBehaviour
{
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
    /// This circle is following the player.
    /// </summary>
    private bool isFollowingPlayer = true;

    /// <summary>
    /// This circle is waiting to be hit by the ray.
    /// </summary>
    private bool isWaitingForRay = false;

    /// <summary>
    /// Time before the circle Drops to the ground (stops following the player).
    /// </summary>
    private float timeBeforeDropToGround = 4;

    /// <summary>
    /// Time before the circle de-spawns after the ray hits it.
    /// </summary>
    private float timeBeforeDespawn = 3;

    /// <summary>
    /// The coroutine to handle the drop (stops following the player) of the circle.
    /// </summary>
    private IEnumerator waitBeforeDropCoroutine;

    /// <summary>
    /// The coroutine to handle the despawn of the circle.
    /// </summary>
    private IEnumerator waitBeforeDestroyCoroutine;

    void Start()
    {
        waitBeforeDropCoroutine = WaitBeforeDrop();
        waitBeforeDestroyCoroutine = WaitBeforeDespawn();

        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if(isFollowingPlayer)
        {
            transform.position = new Vector3(player.transform.position.x, 0.001f, player.transform.position.z);
        }
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

        StopCoroutine(waitBeforeDropCoroutine);
        StopCoroutine(waitBeforeDestroyCoroutine);

        StartCoroutine(waitBeforeDropCoroutine);
    }

    private IEnumerator WaitBeforeDrop()
    {
        yield return new WaitForSeconds(timeBeforeDropToGround);
        isFollowingPlayer=false;
        isWaitingForRay=true;

        Material[] lMaterials = mesh.materials;
        lMaterials[0] = waitForRayMaterial;
        mesh.materials = lMaterials;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("Ray"))
        {
            isWaitingForRay= false;

            Material[] lMaterials = mesh.materials;
            lMaterials[0] = resolvedMaterial;
            mesh.materials = lMaterials;

            StartCoroutine(waitBeforeDestroyCoroutine);
        }
    }

        private IEnumerator WaitBeforeDespawn()
    {
        yield return new WaitForSeconds(timeBeforeDespawn);
        //TODO: Call the factory to be stored.
    }
}
