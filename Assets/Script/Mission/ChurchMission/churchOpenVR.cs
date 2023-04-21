using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class churchOpenVR : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }
    public void OpenDoorAnimation()
    {
        anim.SetBool("IsOpen", true);
    }
}
