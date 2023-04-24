using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderPrize : MonoBehaviour
{
    public bool isOut;
    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "prizes")
        {
            isOut = true;
        }

    }
}
