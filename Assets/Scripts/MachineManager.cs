using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Script managing the usage of machines
/// </summary>
public class MachineManager : MonoBehaviour
{
    /// <summary>
    /// Machine status at the counter
    /// </summary>
    private enum Status {
        Free,
        CustomerIdle,
        EmployeeIdle,
        Ordering,
        Cooking
    }

    /// <summary>
    /// Availability of different machines
    /// </summary>
    /// <typeparam name="Vector3Int">Cell coordinate of the machine</typeparam>
    /// <typeparam name="Status">Status of the machine</typeparam>
    /// <returns></returns>
    private Dictionary<Vector3Int, Status> availabilities = new Dictionary<Vector3Int, Status>();

    /// <summary>
    /// Machines tilemap
    /// </summary>
    private Tilemap tilemap;

    private void Start()
    {
        tilemap = GetComponent<Tilemap>();

        // populate the dictionary
        foreach (var cell in tilemap.cellBounds.allPositionsWithin)
        {
            
            var tile = tilemap.GetTile(cell);

            if (tile != null)
                availabilities.Add(cell, Status.Free);
            
        }
    }

    /// <summary>
    /// Customer is accepted at the machine counter
    /// </summary>
    /// <param name="cell">Cell position of the machine</param>

    public void CustomerCollision(Vector3Int cell)
    {
        

    }

    
    /// <summary>
    /// Determines if the machine at cell is availabe for the customer
    /// </summary>
    /// <param name="cell"></param>
    /// <returns>Whether they are allowed to be served, one customer per machine</returns>
    public bool IsAvailableForCustomer(Vector3Int cell)
    {
        switch (availabilities[cell])
        {
            case Status.Free:
            case Status.EmployeeIdle:
                return true;

            default:
                return false;
        }
    }


}
