using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HMDInfoManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Is Device active: " + XRSettings.isDeviceActive);
        Debug.Log("Device name: " + XRSettings.loadedDeviceName);

        if(!XRSettings.isDeviceActive)
        {
            Debug.Log("No HMD detected");
        }
        else if(XRSettings.isDeviceActive && XRSettings.loadedDeviceName == "MockHMD" || XRSettings.loadedDeviceName == "MockHMDDisplay")
        {
            Debug.Log("Using Mock HMD");
        }
        else
        {
            Debug.Log("Using " + XRSettings.loadedDeviceName);
        }   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
