using System;
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

    [Tooltip("When the line is full, wait this many seconds before trying to get more customers")]
    public float OverflowLineDelay = 1f;

    private void Start()
    {
        tilemap = GetComponent<Tilemap>();
        // finds all rug cell positions
        foreach (var cell in tilemap.cellBounds.allPositionsWithin)
        {
            
            var tile = tilemap.GetTile(cell);

            if (tile != null)
                waitingSpots.Enqueue(cell);
            
        }

    }

    /// <summary>
    /// Coroutine to run every so often when the line is full.
    /// When the line has empty space again, control is given back to
    /// the customer spawner.
    /// </summary>
    /// <returns></returns>
    public IEnumerator OverflowLine()
    {
        while (IsLineFull())
            yield return new WaitForSeconds(OverflowLineDelay);
    }


    /// <summary>
    /// Gives a free waiting spot location, throws exception
    /// </summary>
    public Vector3 WaitInLine()
    {
        // deny entry if no waiting spots are available
        if (occupancyCount == waitingSpots.Count)
            throw new System.Exception("No more waiting spots");

        var position = waitingSpots.Dequeue();
        occupancyCount++;

        return tilemap.GetCellCenterWorld(position);
    }

    /// <summary>
    /// Free up a spot in the line, throws exception
    /// </summary>
    /// <param name="customer">Customer to leave the line</param>
    public void LeaveLine(Vector3Int machineCell)
    {
        // throw error if no one is in line
        if (occupancyCount == 0) throw new System.Exception("No one is in line");
        occupancyCount--;

        // add the free position
        waitingSpots.Enqueue(machineCell);
    }

    /// <summary>
    /// Whether the line is full
    /// </summary>
    /// <returns></returns>
    public bool IsLineFull() => occupancyCount == waitingSpots.Count;
}
