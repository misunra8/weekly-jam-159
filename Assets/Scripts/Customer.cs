using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script dictating how a customer interacts
/// </summary>
public class Customer : Person
{
    
    /// <summary>
    /// Reference to the customer behavior state machine
    /// </summary>
    private Animator behavior;
    
    /// <summary>
    /// Assigns references
    /// </summary>
    private void Start()
    {
        behavior = GetComponent<Animator>();
    }

    private void OnMouseDown() {
        AkSoundEngine.PostEvent("CoinPay",gameObject);
        AkSoundEngine.SetRTPCValue("Theme_RTPC", 20);
    }

    /// <summary>
    /// Sits at the table, influences the behavior machine
    /// </summary>
    /// <param name="cell"></param>
    protected override void ActOnTableCollision(Vector3Int cell)
    {
        
    }
}
