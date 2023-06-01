using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ShootRope : MonoBehaviour
{

    private Transform shooterTip;
    public Rigidbody player;
    //public GameObject webEnd;
    public ActionBasedController controllerLeft;
    public ActionBasedController controllerRight;
    public GameObject leftHand;
    
    private LineRenderer lineRenderer;

    private SpringJoint joint;
    private FixedJoint endJoint;
    private CharacterJoint joint1;

    private bool leftIsShooting;
    private bool rightIsShooting;

    private float maxDistance = 100;
    private Vector3 webPoint;
    float distanceFromPoint;

    private int count = 0;

    //public CreateRope createRope;

    // Start is called before the first frame update
    void Start()
    {
        leftIsShooting = false;
        rightIsShooting = false;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.widthMultiplier = 0.01f;
    }

    // Update is called once per frame
    void Update()
    {
        checkInput();

        Debug.Log(count);

        if(leftIsShooting){
            lineRenderer.enabled = true;
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0,shooterTip.position);
            lineRenderer.SetPosition(1, webPoint);
        }else{
            lineRenderer.enabled = false;
        }

        if(count > 0){
            count --;
        }

    }

    //will check if we need to shoot the rope based on user input
    private void checkInput(){
        float leftClick = controllerLeft.activateAction.action.ReadValue<float>();

        //Checks web for left hand
        if(leftClick > 0 && !leftIsShooting){
            leftIsShooting = true;
            shooterTip = leftHand.transform;
            shootWeb();
            count = 100;
            Debug.Log("Left pressed ");
        }else if(leftClick == 0f && leftIsShooting){ //If its been shot but not clicking anymore
            //leftIsShooting = false;
            //stopWeb();
            Debug.Log("Left UnPressed ");
        }

        if(leftClick > 0 && leftIsShooting && count == 0){
            stopWeb();
            Debug.Log("Stopping Web");
        }


    }

    //will cause the "web" to be shot out of the player's 
    private void shootWeb(){
        RaycastHit hit;
        if(Physics.Raycast(shooterTip.position, shooterTip.forward, out hit, maxDistance)){
            Debug.Log("HIT!");
            webPoint = hit.point;
            Debug.Log("Where it hit: " + hit.point);
            if(hit.rigidbody){
                //webEnd = createRope.makeRope(webPoint, hit.rigidbody, shooterTip);
                //Debug.Log("Where it hit: " + hit.point);
                //webEnd = hit.rigidbody;
            }
            
            joint1 = player.gameObject.AddComponent<CharacterJoint>();
            joint1.autoConfigureConnectedAnchor = false;
            joint1.anchor = webPoint - shooterTip.position;
            joint1.connectedAnchor = webPoint - webPoint;

            Debug.Log("Where shootertip is: " + shooterTip.position);

            if(hit.rigidbody && hit.rigidbody != player.GetComponent<Rigidbody>()){
                joint1.connectedBody = hit.rigidbody;
            }

            //configuring rotation
            joint1.axis = new Vector3(1.0f, 0.0f, 0.0f);
            joint1.swingAxis =  new Vector3(0.0f, 0.0f, 1.0f);

        }
        else{
            Debug.Log("No Hit");
            leftIsShooting = false;
            count = 0;
        }

    }

    private void stopWeb(){
        Destroy(joint1);
        leftIsShooting = false;
        count = 0;

        if(endJoint) Destroy(endJoint);

    }

}