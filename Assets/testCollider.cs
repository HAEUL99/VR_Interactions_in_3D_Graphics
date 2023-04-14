using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class testCollider : XRGrabInteractable
{
    protected override void Grab()
        {
            base.Grab();
            var characterController = FindObjectOfType<CharacterController>();
            if (characterController != null)
            {
                foreach (var collider in colliders)
                {
                    Physics.IgnoreCollision(collider, characterController, true);
                }
            }

        }

   protected override void Detach()
        {
            base.Detach();
            if (throwOnDetach)
            {
                

                var characterController = FindObjectOfType<CharacterController>();
                if (characterController != null)
                {
                    foreach (var collider in colliders)
                    {
                        Physics.IgnoreCollision(collider, characterController, true);
                    }
                }
            }
        }
}
