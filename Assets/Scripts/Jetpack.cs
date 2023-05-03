using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Jetpack : MonoBehaviour
{
    public ActionBasedController rightController;
    public ActionBasedController leftController;
    public float speed;

    private Rigidbody body;

    Vector3 getForce() {
        Vector3 dir = Vector3.Normalize(-rightController.transform.forward) + Vector3.Normalize(-leftController.transform.forward);
        return (dir / 2) * speed * 1000 * Time.deltaTime;
    }

    // Start is called before the first frame update
    void Start()
    {
        body = transform.GetComponentInParent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var forceVector = getForce();

        //reset force to new direction
        body.AddForce(-body.velocity);
        body.AddForce(forceVector);
    }
}
