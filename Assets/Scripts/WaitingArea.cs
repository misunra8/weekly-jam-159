using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the allocation of waiting spots for customers
/// </summary>
public class WaitingArea : MonoBehaviour
{
    /// <summary>
    /// Queue of waiting customers
    /// </summary>
    /// <typeparam name="GameObject">Reference to customer game object</typeparam>
    /// <returns></returns>
    private Queue<GameObject> customers = new Queue<GameObject>();

    /// <summary>
    /// Queue of waiting locations on the tilemap
    /// </summary>
    /// <typeparam name="Vector3Int">Tilemap cell position</typeparam>
    /// <returns></returns>
    private Queue<Vector3Int> waitingSpots = new Queue<Vector3Int>();

    /// <summary>
    /// Whether there is a waiting line or not
    /// </summary>
    /// <returns></returns>
    public bool HasALine() => customers.Count == 0;
}
