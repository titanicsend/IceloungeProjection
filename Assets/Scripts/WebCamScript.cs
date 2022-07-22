using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebCamScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        WebCamDevice[] devices = WebCamTexture.devices;
		Debug.Log("Number of web cams connected: " + devices.Length);

        for (int i = 0; i < devices.Length; i++)
        {
            Debug.Log(i + " " + devices[i].name);
        }
		Renderer rend = this.GetComponentInChildren<Renderer>();

		WebCamTexture mycam = new WebCamTexture();
        // Set device number to choose webcam
		//  string camName = devices[0].name;
        string camName = "RICOH THETA V";
		Debug.Log("The webcam name is " + camName);
		mycam.deviceName = camName;
		rend.material.mainTexture = mycam;

		mycam.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
