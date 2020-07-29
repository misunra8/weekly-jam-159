using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

/// <summary>
/// Script dictating how a customer interacts
/// </summary>
public class Customer : Person
{

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

    protected override void ActOnMachineCollision(Vector3Int cell)
    {
        
    }

    /// <summary>
    /// The customer will move to the target waiting spot
    /// </summary>
    /// <param name="waitingSpot">Location of a free spot to wait</param>
    public void WaitInLine(Vector3 waitingSpot)
    {
        SetPathDestination(waitingSpot);
    }

    /// <summary>
    /// Cursor clicked the customer
    /// </summary>
    /// <param name="selectedMaterial">Selection material</param>
    public override void OnClick(Material selectedMaterial)
    {
        Debug.Log("clicked this");
        GetComponentInChildren<SpriteRenderer>().material = selectedMaterial;
    }
}
