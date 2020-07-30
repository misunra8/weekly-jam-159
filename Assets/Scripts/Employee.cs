using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script controlling how a customer behaves
/// </summary>
public class Employee : Person
{
    [Tooltip("An empty that helps in navigating")]
    public GameObject employeePairedEmpty;

    private uint walkEvent;
    // Start is called before the first frame update
    
    void Start()
    {
        // push up the pairedEmpty attribute
        base.pairedEmpty = this.employeePairedEmpty;

        float themeRTPC = 90;
        AkSoundEngine.SetRTPCValue("Theme_RTPC", themeRTPC);
        AkSoundEngine.PostEvent("ThemePlay", gameObject);
    }

    private void OnMouseDown()
    {
        AkSoundEngine.PostEvent("DialogueRegister", gameObject);
        AkSoundEngine.SetRTPCValue("Theme_RTPC", 40);
    }

    /// <summary>
    /// When the employee collides with a machine counter
    /// </summary>
    /// <param name="cell">Cell position of the collision</param>
    /// <param name="machine"></param>
    protected override void ActOnMachineCollision(Vector3Int cell, MachineManager machine)
    {
        // check for machine availability
        if (!machine.IsAvailableForEmployee(cell)) return;

        // triggers behavior states
        behavior.SetTrigger("Operate cash register");

        // interact with the machine
        machine.EmployeeCollision(cell, this);

    }

    protected override void ActOnTableCollision(Vector3Int cell)
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// Tell the employee to move somewhere
    /// </summary>
    /// <param name="destination"></param>
    public override void MoveTo(Vector3 destination)
    {
        // only walk when allowed to by the state machine
        if (CanWalk)
        {
            AkSoundEngine.StopPlayingID(walkEvent);
            walkEvent = AkSoundEngine.PostEvent("Walk", gameObject);
            SetPathDestination(destination);
        }

    }

    /// <summary>
    /// When the employee stops moving
    /// </summary>
    public override void StoppedMoving()
    {
        AkSoundEngine.StopPlayingID(walkEvent);
    }

    /// <summary>
    /// When the employee takes an order
    /// </summary>
    public void TookOrder()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cell"></param>
    /// <param name="machineManager"></param>
    protected override void ActOnExitMachineCollision(Vector3Int cell, MachineManager machineManager)
    {
        throw new NotImplementedException();
    }

    protected override void ActOnExitTableCollision(Vector3Int cell, MachineManager machineManager)
    {
        throw new NotImplementedException();
    }
}
