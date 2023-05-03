using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Jetpack : MonoBehaviour
{
    public ActionBasedController rightController;
    public ActionBasedController leftController;
    public float acceleration;
    public float maxSpeed;

    private Rigidbody body;

    Vector3 getForce() {
        Vector3 dir = new Vector3(0,0,0);
        if (rightController.activateAction.action.ReadValue<float>() == 1) {
            dir += Vector3.Normalize(-rightController.transform.forward);
        }
        if (leftController.activateAction.action.ReadValue<float>() == 1) {
            dir += Vector3.Normalize(-leftController.transform.forward);
        }
        return dir * acceleration * 100 * Time.deltaTime;
    }

    void applyForce() {
        Vector3 force = getForce();
        body.AddForce(force, ForceMode.Acceleration);
        float newSpeed = Vector3.Magnitude(body.velocity + force);
        if(newSpeed > maxSpeed) {
            Vector3 diff = Vector3.Normalize(body.velocity + force) * (newSpeed - maxSpeed);
            body.AddForce(-diff, ForceMode.Acceleration);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        body = transform.GetComponentInParent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        applyForce();
    }
}
