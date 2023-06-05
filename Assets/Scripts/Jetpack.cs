using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem.UI;
using TMPro;

public class Jetpack : MonoBehaviour
{
    public ActionBasedController rightController;
    public ActionBasedController leftController;
    public TMP_Text fuelIndicator;
    public float startingFuel;
    public float acceleration;
    public float maxSpeed;

    private Rigidbody body;
    private float fuel = 0;
    private bool isPlaying = false;

    Vector3 getForce() {
        int debugJetpack = -1;
        //Set to 1 to reverse direction to make navigation within unity easier
        debugJetpack = 1;
        Vector3 dir = new Vector3(0,0,0);
        bool isMoving = false;
        if (rightController.activateAction.action.ReadValue<float>() == 1) {
            updateFuel(-1 * Time.deltaTime);
            isMoving = true;
            dir += Vector3.Normalize(debugJetpack * getControllerDirection(rightController));
        }
        if (leftController.activateAction.action.ReadValue<float>() == 1) {
            updateFuel(-1 * Time.deltaTime);
            isMoving = true;
            dir += Vector3.Normalize(debugJetpack * getControllerDirection(leftController));
        }
        if ((!isMoving || fuel <= 0.01) && isPlaying) {
            GetComponent<AudioSource>().Stop();
            isPlaying = false;
        }
        return dir * acceleration * 100 * Time.deltaTime;
    }
    Vector3 getControllerDirection(ActionBasedController controller)
    {
        return controller.GetComponent<TrackedDeviceRaycaster>().transform.forward;

    }

    void applyForce() {
        Vector3 force = getForce();
        if (fuel > 0) {
            body.AddForce(force, ForceMode.Acceleration);
            float newSpeed = Vector3.Magnitude(body.velocity + force);
            if(newSpeed > maxSpeed) {
                Vector3 diff = Vector3.Normalize(body.velocity + force) * (newSpeed - maxSpeed);
                body.AddForce(-diff, ForceMode.Acceleration);
            }
        }
    }

    public void applyExternalForce(Vector3 force) {
        body.AddForce(force);
    }

    void updateFuel(float amount) {
        if (fuel > 0 && !isPlaying) {
            GetComponent<AudioSource>().Play();
            isPlaying = true;
        }

        if(fuel > 0 || amount > 0) {
            fuel += amount;
        }
        fuelIndicator.SetText(String.Format("Fuel: {0}", fuel.ToString("F2")));
    }

    public void resetMomentum() {
        fuel = 0;
        updateFuel(startingFuel);
        body.velocity = new Vector3(0,0,0);
    }

    // Start is called before the first frame update
    void Start()
    {
        updateFuel(startingFuel);
        body = transform.GetComponentInParent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        applyForce();
    }
}
