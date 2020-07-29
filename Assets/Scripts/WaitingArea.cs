using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Controls the allocation of waiting spots for customers
/// </summary>
public class WaitingArea : MonoBehaviour
{

    /// <summary>
    /// Reference to the tilemap
    /// </summary>
    private Tilemap tilemap;

    /// <summary>
    /// Queue of free waiting spots with a rug
    /// </summary>
    /// <typeparam name="Vector3Int"></typeparam>
    /// <returns></returns>
    private Queue<Vector3Int> waitingSpots = new Queue<Vector3Int>();

    /// <summary>
    /// How many waiting rug tiles are occupied
    /// </summary>
    private int occupancyCount = 0;

    private void Start()
    {
        tilemap = GetComponent<Tilemap>();
        // finds all rug cell positions
        // rugs must be in a rectangular formation
        foreach (var item in tilemap.cellBounds.allPositionsWithin)
        {
            waitingSpots.Enqueue(item);
           
        }

    }


    /// <summary>
    /// Gives a free waiting spot location
    /// </summary>
    /// <param name="customer">Customer game object</param>
    public Vector3 WaitInLine(GameObject customer)
    {
        // deny entry if no waiting spots are available
        if (occupancyCount == waitingSpots.Count)
            throw new System.Exception("No more waiting spots");

        var position = waitingSpots.Dequeue();
        occupancyCount++;

        return tilemap.CellToWorld(position);
    }

    /// <summary>
    /// Free up a spot in the line
    /// </summary>
    /// <param name="customer">Customer to leave the line</param>
    public void LeaveLine(GameObject customer)
    {
        // throw error if no one is in line
        if (occupancyCount == 0) throw new System.Exception("No one is in line");
        occupancyCount--;

        // add the free position
        var position = tilemap.WorldToCell(customer.transform.position);
        waitingSpots.Enqueue(position);
    }
}
