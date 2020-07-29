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

    private uint walkEvent;

    private void OnMouseDown() {
        AkSoundEngine.PostEvent("CoinPay",gameObject);
        AkSoundEngine.SetRTPCValue("Theme_RTPC", 20);
        AkSoundEngine.StopPlayingID(walkEvent);
    }

    /// <summary>
    /// Sits at the table, influences the behavior machine
    /// </summary>
    /// <param name="cell"></param>
    protected override void ActOnTableCollision(Vector3Int cell)
    {
        
    }

    /// <summary>
    /// Requests service at the counter, leave the line
    /// </summary>
    /// <param name="cell">Cell of the collided counter</param>
    protected override void ActOnMachineCollision(Vector3Int cell)
    {
        behavior.SetTrigger("Leave line");
        
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
    /// Tell the customer to move to a place
    /// </summary>
    /// <param name="destination"></param>
    public override void MoveTo(Vector3 destination)
    {
        // only moves when the customer is allowed to
        if (CanWalk) {
            Debug.Log("walking");
            walkEvent = AkSoundEngine.PostEvent("Walk", gameObject);
            Debug.Log(walkEvent);
            SetPathDestination(destination);
            //walk.Stop(gameObject);
            
        }
            

        Debug.Log("path finished");
    }
}
