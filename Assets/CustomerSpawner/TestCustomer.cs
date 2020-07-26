using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCustomer : MonoBehaviour {

	float speed = 2f;

	void Update () 
	{
		transform.Translate(speed * Time.deltaTime, 0, 0);

		Destroy(gameObject, 3f);
	}
}
