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
    /// <summary>
    /// Flag telling if the customer can walk anywhere
    /// </summary>
    [NonSerialized]
    public bool CanWalk = true;

    /// <summary>
    /// For audio
    /// </summary>
    private uint walkEvent;
    
    /// <summary>
    /// Reference to the waiting area script
    /// </summary>
    [NonSerialized]
    public WaitingArea WaitingArea;
    
    /// <summary>
    /// Location where the customer waits in line
    /// </summary>
    [NonSerialized]
    public Vector3Int WaitingSpot;

    private void OnMouseDown() {
        CanWalk = true;
        AkSoundEngine.PostEvent("CoinPay",gameObject);
        AkSoundEngine.SetRTPCValue("Theme_RTPC", 20);
    }

    /// <summary>
    /// Sits at the table, influences the behavior machine
    /// </summary>
    /// <param name="cell"></param>
    protected override void ActOnTableCollision(Vector3Int cell)
    {
        AkSoundEngine.StopPlayingID(walkEvent);
        CanWalk = false;

    }

    /// <summary>
    /// Requests service at the counter, leave the line
    /// </summary>
    /// <param name="cell">Cell of the collided counter</param>
    protected override void ActOnMachineCollision(Vector3Int cell)
    {
        // free up the line, change behavior state
        behavior.SetTrigger("Leave line");
        WaitingArea.LeaveLine(WaitingSpot);
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
            AkSoundEngine.StopPlayingID(walkEvent);
            walkEvent = AkSoundEngine.PostEvent("Walk", gameObject);
            SetPathDestination(destination);
            //walk.Stop(gameObject);
            
        }
    }

    public override void StoppedMoving() {
        AkSoundEngine.StopPlayingID(walkEvent);
    }

}
