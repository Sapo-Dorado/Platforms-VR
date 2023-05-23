using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem.UI;

public class Throwable : XRGrabInteractable
{
    public GameObject character;
    private Jetpack jetpack;
    private void Start() {
        Physics.IgnoreCollision(character.GetComponent<Collider>(), GetComponent<Collider>());
        jetpack = character.GetComponent<Jetpack>();
    }
    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        Vector3 force = applySpeed(args.interactorObject.transform.gameObject.GetComponent<TrackedDeviceRaycaster>().transform.forward, 10);
        transform.GetComponentInParent<Rigidbody>().AddForce(force);
        jetpack.applyExternalForce(-force * args.interactableObject.transform.gameObject.GetComponent<Rigidbody>().mass * 10);
    }

    private Vector3 applySpeed(Vector3 dir, int speed)
    {
        return Vector3.Normalize(dir) * speed;
    }
}
