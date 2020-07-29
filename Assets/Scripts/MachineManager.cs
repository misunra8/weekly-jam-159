using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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

    private Tilemap tilemap;

    private void Start()
    {
        tilemap = GetComponent<Tilemap>();
    }
}
