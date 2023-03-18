using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScript : MonoBehaviour
{
    private Light myLight;

    // Start is called before the first frame update
    void Start()
    {
        myLight = gameObject.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            LightControlFunc();
        }
    }

    void LightControlFunc()
    {
        myLight.enabled = !myLight.enabled;
    }
}