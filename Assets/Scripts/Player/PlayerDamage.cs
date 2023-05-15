using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class manages the "damage" taken by the player.
/// currently it only changes the material of the player.
/// </summary>
public class PlayerDamage : MonoBehaviour
{
    /// <summary>
    /// Link to the mesh renderer.
    /// </summary>
    public MeshRenderer mesh;

    /// <summary>
    /// Material to apply when not damaged.
    /// </summary>
    /// 
    public Material notDamagedMaterial;
    /// <summary>
    /// Material to apply when damaged.
    /// </summary>
    public Material damagedMaterial;

    private void OnCollisionEnter(Collision collision)
    {
        CircleBehaviour lCircleBehaviour= collision.gameObject.GetComponent<CircleBehaviour>();
        if(lCircleBehaviour == null
           || !lCircleBehaviour.IsFollowingPlayer())
        {
            Material[] lMaterials = mesh.materials;
            lMaterials[0] = damagedMaterial;
            mesh.materials = lMaterials;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        Material[] lMaterials = mesh.materials;
        lMaterials[0] = notDamagedMaterial;
        mesh.materials = lMaterials;
    }
}
