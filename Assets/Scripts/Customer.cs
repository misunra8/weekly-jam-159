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
    [NonSerialized]
    public bool CanWalk = true;

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
    /// Cursor selected the customer
    /// </summary>
    /// <param name="selectedMaterial">Selection material</param>
    public override Person Select(Material selectedMaterial)
    {
        // change material
        base.Select(selectedMaterial);

        return this;
    }

    /// <summary>
    /// Tell the customer to move to a place
    /// </summary>
    /// <param name="destination"></param>
    public override void MoveTo(Vector3 destination)
    {
        // only moves when the customer is allowed to
        if (CanWalk)
            SetPathDestination(destination);
    }

    /// <summary>
    /// Deselect the customer
    /// </summary>
    /// <param name="deselectedMaterial">Deselection material</param>
    /// <returns></returns>
    public override Person Deselect(Material deselectedMaterial)
    {
        // change material
        base.Deselect(deselectedMaterial);

        return this;
    }
}
