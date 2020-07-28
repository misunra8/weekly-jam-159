using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script dictating how a customer interacts
/// </summary>
public class Customer : Person
{
    
    /// <summary>
    /// Sits at the table, influences the behavior machine
    /// </summary>
    /// <param name="cell"></param>
    protected override void ActOnTableCollision(Vector3Int cell)
    {
        
    }
}
