using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class churchCollider : MonoBehaviour
{
   
    private void OnTriggerEnter(Collider other)
    {
        //player
        if (other.gameObject.tag == "Player")
        {
            
        }

    }
}
