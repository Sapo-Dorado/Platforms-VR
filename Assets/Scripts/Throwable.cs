using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Throwable : XRGrabInteractable
{
    public GameObject character;
    private void Start() {
        Physics.IgnoreCollision(character.GetComponent<Collider>(), GetComponent<Collider>());
    }
    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        // Vector3 force = applySpeed(args.interactorObject.transform.gameObject.GetComponent<ActionBasedController>().transform.forward, 20);
        // transform.GetComponentInParent<Rigidbody>().AddForce(force);
    }

    private Vector3 applySpeed(Vector3 dir, int speed)
    {
        return Vector3.Normalize(dir) * speed;
    }
}
