using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerControl : MonoBehaviour
{

    ActionBasedContinuousMoveProvider continuousMoveProvider;
    ActionBasedContinuousTurnProvider continuousTurnProvider;

    // Start is called before the first frame update
    void Start()
    {
        continuousMoveProvider = gameObject.GetComponent<ActionBasedContinuousMoveProvider>();
        continuousTurnProvider = gameObject.GetComponent<ActionBasedContinuousTurnProvider>();

    }

    public void EnableMoveandTurn()
    {
        continuousMoveProvider.enabled = true;
        continuousTurnProvider.enabled = true;
    }

    public void DisableMoveandTurn()
    {
        continuousMoveProvider.enabled = false;
        continuousTurnProvider.enabled = false;
    }
}
