using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Employee : MonoBehaviour
{
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
        AkSoundEngine.PostEvent("Pouring", gameObject);
        AkSoundEngine.SetRTPCValue("Theme_RTPC", 40);
    }

}
