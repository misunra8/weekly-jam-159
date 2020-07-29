using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Creates customer based on set times
/// </summary>
public class CustomerSpawner : MonoBehaviour {

    [Tooltip("List of customer spawn times in delay seconds")]
	public float[] spawnTime;

    [Tooltip("Customer prefab")]
	public GameObject customerPrefab;

    [Tooltip("Customer sprites for spawning randomly")]
    public Sprite[] customerSprites;
    
    [Tooltip("Tilemap with the waiting area script")]
    public GameObject WaitingAreaTilemap;

    /// <summary>
    /// Script of the waiting area
    /// </summary>
    private WaitingArea waitingArea;

    /// <summary>
    /// Parent of empties for moving people
    /// </summary>
    private GameObject targetEmptiesParent;

    void Start()
	{
        waitingArea = WaitingAreaTilemap.GetComponent<WaitingArea>();

        targetEmptiesParent = new GameObject("Customer target empties");

		StartCoroutine(SpawnCycle());
	}

    /// <summary>
    /// Instantiates a customer in delayed times
    /// </summary>
    /// <returns></returns>
	private IEnumerator SpawnCycle()
	{
		foreach (float time in spawnTime)
        {
            // fixed arrival time
            yield return new WaitForSeconds(time);

            // line overflow waiting loop
            if (waitingArea.IsLineFull())
                yield return waitingArea.OverflowLine();

            NewCustomer();
        }
    }

    /// <summary>
    /// Creates a new customer
    /// </summary>
    private void NewCustomer()
    {
        AkSoundEngine.PostEvent("DoorOpen", gameObject);
        var customer = Instantiate(customerPrefab, transform.position, Quaternion.identity);

        customer.GetComponent<Customer>().WaitingArea = waitingArea;

        // pair an empty
        PairEmpty(customer);

        AssignRandomSprite(customer);
        WaitInLine(customer);
    }

    /// <summary>
    /// Pairs an empty so the AI setter can use it
    /// </summary>
    /// <param name="customer"></param>
    private void PairEmpty(GameObject customer)
    {
        var empty = new GameObject();
        empty.name = "Customer target empty";
        empty.transform.SetParent(this.targetEmptiesParent.transform);
        customer.GetComponent<Customer>().PairEmpty(empty);
    }

    /// <summary>
    /// Sends customer to wait in line, also link the waiting area script
    /// </summary>
    /// <param name="customer"></param>
    private void WaitInLine(GameObject customer)
    {
        var customerScript = customer.GetComponent<Customer>();
        var spot = waitingArea.WaitInLine(customerScript);
        customerScript.WaitInLine(spot);
    }

    /// <summary>
    /// Assigns the sprite of the customer in a random fashion
    /// </summary>
    /// <param name="customer">Recently instantiated customer</param>
    private void AssignRandomSprite(GameObject customer)
    {
        var index = Random.Range(0, customerSprites.Length);
        customer.GetComponentInChildren<SpriteRenderer>().sprite = customerSprites[index];
    }
}
