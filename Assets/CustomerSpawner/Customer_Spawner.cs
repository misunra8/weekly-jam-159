using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer_Spawner : MonoBehaviour {

	[SerializeField]
	float[] spawnTime;

	[SerializeField]
	GameObject customer;

	void Start()
	{
		StartCoroutine(SpawnCycle());
	}

	IEnumerator SpawnCycle()
	{
		//All of this could be removed
		int numSpawned = 0;
		//

		foreach (float time in spawnTime)
		{
			yield return new WaitForSeconds(time);
			Instantiate(customer, transform.position, Quaternion.identity);

			//
			numSpawned++;
			print("Customer " + numSpawned + " at " + time + " second(s) delay");
			//
		}

		//
		print("Spawn cycle finished");
		//
	}
}
