using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Employee : Person
{

    private uint walkEvent;
    // Start is called before the first frame update
    void Start()
    {
        float themeRTPC = 90;
        AkSoundEngine.SetRTPCValue("Theme_RTPC", themeRTPC);
        AkSoundEngine.PostEvent("ThemePlay",gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown() {
        AkSoundEngine.PostEvent("DialogueRegister", gameObject);
        AkSoundEngine.SetRTPCValue("Theme_RTPC", 40);
    }

    protected override void ActOnMachineCollision(Vector3Int cell, MachineManager machine)
    {
        throw new System.NotImplementedException();
    }

    protected override void ActOnTableCollision(Vector3Int cell)
    {
        throw new System.NotImplementedException();
    }

    public override void MoveTo(Vector3 destination)
    {
        throw new System.NotImplementedException();
        //AkSoundEngine.StopPlayingID(walkEvent);
        //walkEvent = AkSoundEngine.PostEvent("Walk", gameObject);
    }

    public override void StoppedMoving() {

        throw new System.NotImplementedException();
        //AkSoundEngine.StopPlayingID(walkEvent);
    }
}
