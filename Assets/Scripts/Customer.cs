using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Script controlling how the customer interacts with the environment
/// </summary>
public class Customer : MonoBehaviour
{
    /// <summary>
    /// Used in finding the tile that the customer collides with
    /// </summary>
    [Tooltip("Used to find which tile is colliding with, usually the value is 1/2 the size of a tile")]
    public float CollisionStep = 0.5f;
    
    /// <summary>
    /// For testing purposes
    /// </summary>
    public TileBase CashRegister;
    
    /// <summary>
    /// Standard Unity collision listener method
    /// </summary>
    /// <param name="other"></param>
    private void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.gameObject.name)
        {
            // colliding with something in the tables tilemap
            case "Tables":
                CollideTable(other);
                break;
        }
    }

    /// <summary>
    /// Triggers an interaction between the customer and the table
    /// </summary>
    /// <param name="other">Unity's detected collision</param>
    private void CollideTable(Collision2D other)
    {   
        var tablesTilemap = other.gameObject.GetComponent<Tilemap>();
        var contact = other.GetContact(0); // first contact point
        
        // To get the tile, we need to get a world position inside a cell.
        // But the collision position is not inside the cell all the time.
        // So we'll lengthen the path taken by the customer by a step to get a
        // position inside the cell.
        
        // Find the position that is slightly ahead of the customer
        // Use the opposite direction of the normal of the surface of collision 
        var oppositeNormal = contact.normal.normalized * -1;

        var simulatedPosition = oppositeNormal * CollisionStep + contact.point;
        var cell = tablesTilemap.WorldToCell(simulatedPosition);

        // Change the touched tile to another tile
        tablesTilemap.SetTile(cell, CashRegister);
    }
}
