using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script dictating how a customer interacts
/// </summary>
public class Customer : Person
{

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

    [Tooltip("Sprite of a cash register in a bubble")]
    public Sprite cashRegisterBubble;

    [Tooltip("Sprite of a cash symbol in a bubble")]
    public Sprite cashCollectionBubble;

    [Tooltip("Sprite of a table in a bubble")]
    public Sprite tableBubble;

    [Tooltip("Sprite renderer of the bubble")]
    public SpriteRenderer Bubble;

    /// <summary>
    /// Used to decide which sprite to display as a bubble
    /// </summary>
    public enum BubbleSprite {
        None,
        Machine,
        Table,
        Cash
    }

    private void OnMouseDown() {
        //AkSoundEngine.PostEvent("CoinPay",gameObject);
        AkSoundEngine.SetRTPCValue("Theme_RTPC", 20);
    }

    /// <summary>
    /// Sits at the table, influences the behavior machine
    /// </summary>
    /// <param name="cell"></param>
    protected override void ActOnTableCollision(Vector3Int cell)
    {
        AkSoundEngine.StopPlayingID(walkEvent);

    }

    /// <summary>
    /// Requests service at the counter, leave the line
    /// </summary>
    /// <param name="cell">Cell of the collided counter</param>
    /// <param name="machine">Script of the machine counter</param>
    protected override void ActOnMachineCollision(Vector3Int cell, MachineManager machine)
    {
        // check for machine availability
        if (!machine.IsAvailableForCustomer(cell)) return;

        // free up the line, change behavior state
        AkSoundEngine.StopPlayingID(walkEvent);
        AkSoundEngine.PostEvent("Bell", gameObject);
        behavior.SetTrigger("Leave line");
        WaitingArea.LeaveLine(WaitingSpot);

        // interact with machine
        machine.CustomerCollision(cell, this);
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

    /// <summary>
    /// When the customer stops moving
    /// </summary>
    public override void StoppedMoving() {
        AkSoundEngine.StopPlayingID(walkEvent);
    }

    /// <summary>
    /// Placed an order
    /// </summary>
    public void PlacedOrder()
    {
        // now can walk to table
        behavior.SetTrigger("Walk to table");
    }

    /// <summary>
    /// Display a bubble sprite next to the customer
    /// </summary>
    /// <param name="bubble"></param>
    public void DisplayBubble(BubbleSprite bubble)
    {
        switch (bubble)
        {
            case BubbleSprite.None:
                
                break;
            
            default:
                break;
        }
    }
}
