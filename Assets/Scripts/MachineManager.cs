using System;
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
        Cooking,
        CoffeeReady
    }

    /// <summary>
    /// Availability of different machines
    /// </summary>
    /// <typeparam name="Vector3Int">Cell coordinate of the machine</typeparam>
    /// <typeparam name="Status">Status of the machine</typeparam>
    /// <returns></returns>
    private Dictionary<Vector3Int, Status> availabilities = new Dictionary<Vector3Int, Status>();

    /// <summary>
    /// Who are located at the machine counter
    /// </summary>
    /// <typeparam name="Vector3Int">Cell position of the machine</typeparam>
    /// <typeparam name="customer">Reference to customer at this location, if any</typeparam>
    /// <typeparam name="employee">Reference to employee at this location, if any</typeparam>
    /// <returns></returns>
    private Dictionary<Vector3Int, (Customer customer, Employee employee)> users = new Dictionary<Vector3Int, (Customer customer, Employee employee)>();

    /// <summary>
    /// Machines tilemap
    /// </summary>
    private Tilemap tilemap;
    
    /// <summary>
    /// Time to place an order
    /// </summary>
    [Tooltip("Time needed to place an order")]
    public float OrderTime = 1f;

    private void Start()
    {
        tilemap = GetComponent<Tilemap>();

        // populate the dictionary
        foreach (var cell in tilemap.cellBounds.allPositionsWithin)
        {
            
            var tile = tilemap.GetTile(cell);

            if (tile != null)
            {
                availabilities.Add(cell, Status.Free);
                users.Add(cell, (null, null));
            }
        }
    }

    /// <summary>
    /// Customer is accepted at the machine counter
    /// </summary>
    /// <param name="cell">Cell position of the machine</param>

    public void CustomerCollision(Vector3Int cell, Customer customer)
    {
        (Customer customer, Employee employee) people = (null, null);
        switch (availabilities[cell])
        {
            case Status.Free:
                // attach the customer to this cell
                availabilities[cell] = Status.CustomerIdle;
                people = users[cell];
                people.customer = customer;
                users[cell] = people;
                break;

            case Status.EmployeeIdle:
                // attach the customer to this cell
                availabilities[cell] = Status.Ordering;
                people = users[cell];
                people.customer = customer;
                users[cell] = people;
                // make the order go through when both people are present
                ProcessOrder(cell);
                break;

            default:
                throw new System.Exception($"Inappriopriate serving machine state: {availabilities[cell]}");
        }

    }

    /// <summary>
    /// Employee at the machine takes order from customer
    /// </summary>
    /// <param name="cell">Machine position</param>
    private void ProcessOrder(Vector3Int cell)
    {
        var (customer, employee) = users[cell];

        if (customer == null || employee == null) 
            throw new Exception($"Not enough people present at machine {cell}");

        IEnumerator Ordering(){
            yield return new WaitForSeconds(this.OrderTime);
            customer.PlacedOrder(); 
            employee.TookOrder();
            FinishOrdering(cell);
        }
        StartCoroutine(Ordering());
        
        availabilities[cell] = Status.Ordering;
    }

    /// <summary>
    /// When the ordering finishes, clean up the cell data
    /// </summary>
    /// <param name="cell"></param>
    public void FinishOrdering(Vector3Int cell)
    {
        availabilities[cell] = Status.Free;
        users[cell] = (null, null);
    }

    /// <summary>
    /// Determines if the machine at cell is availabe for a customer
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

    /// <summary>
    /// Determines if the machine at cell is available for an employee
    /// </summary>
    /// <param name="cell"></param>
    /// <returns>Whether the machine is available, one employee per machine</returns>
    public bool IsAvailableForEmployee(Vector3Int cell)
    {
        switch (availabilities[cell])
        {
            case Status.Free:
            case Status.CustomerIdle:
                return true;

            default:
                return false;
        }
    }

    /// <summary>
    /// Employee is accepted at the collision site
    /// </summary>
    /// <param name="cell">Cell coord</param>
    /// <param name="employee"></param>
    public void EmployeeCollision(Vector3Int cell, Employee employee)
    {
        (Customer customer, Employee employee) people = (null, null);
        switch (availabilities[cell])
        {
            case Status.Free:
                availabilities[cell] = Status.EmployeeIdle;
                people = users[cell];
                people.employee = employee;
                users[cell] = people;
                break;

            case Status.CustomerIdle:
                availabilities[cell] = Status.Ordering;
                people = users[cell];
                people.employee = employee;
                users[cell] = people;
                // make the order go through when both people are present
                ProcessOrder(cell);
                break;

            default:
                throw new System.Exception($"Inappriopriate serving machine state: {availabilities[cell]}");
        }
    }
}
