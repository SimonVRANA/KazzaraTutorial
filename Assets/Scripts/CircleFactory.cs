using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is a factory for the circles.
/// A generic GameObject factory would have been better for later uses but I didn't had time to think about hhow to handle initialization and "deletion" of objects.
/// I know Unity added a class for elements that doesn't need the Unity life cycle (Awake, Start, Update,...) but I'm not familiar with it so I used a MonoBehaviour.
/// </summary>
public class CircleFactory : MonoBehaviour
{
    /// <summary>
    /// The prefab for circles
    /// </summary>
    [SerializeField]
    private GameObject circlePrefab;

    /// <summary>
    /// Lis of all the instances (deactivated an ready to be reused).
    /// </summary>
    private Queue<GameObject> instances;

    private void Start()
    {
        instances = new Queue<GameObject>();
    }

    /// <summary>
    /// Returns an instance of circle.
    /// Could come from instantiation or recycling an old one.
    /// In any case you should initialize it.
    /// </summary>
    /// <returns>An instance of circle.</returns>
    public GameObject GetOrCreateCircle()
    {
        if (instances.Count > 0)
        {
            GameObject lInstance= instances.Dequeue();
            lInstance.SetActive(true);
            return lInstance;
        }
        else
        {
            return Instantiate(circlePrefab);
        }
    }
    
    /// <summary>
    /// Stores the Circle for later use.
    /// Will also deactivate it.
    /// </summary>
    /// <param name="pCircle"></param>
    public void StoreCircle(GameObject pCircle)
    {
        pCircle.SetActive(false);
        instances.Enqueue(pCircle);
    }
}
