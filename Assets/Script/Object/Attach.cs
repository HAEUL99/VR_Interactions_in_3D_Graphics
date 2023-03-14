using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attach : MonoBehaviour
{
    public GameObject cube;
    public bool isAttach;
    public bool button;
    // Start is called before the first frame update
    void Start()
    {
        button = false;
        isAttach = false;
    }

    private void Update()
    {
        if (button == true)
        {
            Debug.Log($"?");
            if (isAttach == true)
            {
                gameObject.transform.SetParent(null);
                isAttach = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //player
        Debug.Log($"닿");
        if (other.gameObject.tag == "busstop")
        {
            if (isAttach == false)
            {
                gameObject.transform.SetParent(other.gameObject.transform, true);
                //gameObject.transform.position = other.transform.position;
                Debug.Log($"gameObject.transform.position: {gameObject.transform.position}");
                Debug.Log($"gameObject.transform.localPosition: {gameObject.transform.localPosition}");
                isAttach = true;
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        //player
        Debug.Log($"닿");
        if (other.gameObject.tag == "busstop")
        {
            if (isAttach == true)
            {
                gameObject.transform.SetParent(null);
                gameObject.transform.position = other.gameObject.transform.position + new Vector3(1.0f, 1.0f, 1.0f);
                isAttach = false;
            }
        }

    }


}
