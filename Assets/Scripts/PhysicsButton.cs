using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhysicsButton : MonoBehaviour
{
    public Transform buttonTop;//saves the top of the button (the part of the button that moves within the base)
    public Transform buttonLowerLimit;//Transform that is used to track the upper limit that we want the top of the button to be contained in
    public Transform buttonUpperLimit;//Transform that is used to track the lower limit that we want the top of the button to be contained in
    public float threshHold;//value from 0 to 1 that is use to calculate whether or not the button should be trigger. 
                            //For example, if thresHold = 0.5, halfway between the lower and upper limit the button will be pressed.
    public float force = 10;//Basically, Provides a spring force on the op of the button in order to make it return to the top position like a button would
    private float upperLowerDiff;//used with thresHold value to calculate whether or not the bool isPressed should be on or off
    public bool isPressed;
    private bool prevPressedState;//used that the onPressed and onReleased unity events are only triggered once
    public AudioSource pressedSound;//audio source that is trigger when the button is pressed
    public AudioSource releasedSound;//audio source that is trigger when the button is released
    public UnityEvent onPressed;
    public UnityEvent onReleased;
    //public Collider[] CollidersToIgnore;//Used to ignore collisions 

    // Start is called before the first frame update
    void Start()
    {
        Collider localCollider = GetComponent<Collider>();
        //if (localCollider != null)
        //{
        //Physics.IgnoreCollision(localCollider, buttonTop.GetComponentInChildren<Collider>());

        //foreach (Collider singleCollider in CollidersToIgnore)
        //{
        //Physics.IgnoreCollision(localCollider, singleCollider);
        //}
        //}

        //At the start we want to ignore collisions between the top of the button and the base of the button so they can clip through each other and they are not going to cause any problems  
        Physics.IgnoreCollision(localCollider, buttonTop.GetComponent<Collider>());
        if (transform.eulerAngles != Vector3.zero)//If the base of the button does not have a angle of zero (the button base is not upright)
        {
            Vector3 savedAngle = transform.eulerAngles;
            transform.eulerAngles = Vector3.zero;
            upperLowerDiff = buttonUpperLimit.position.y - buttonLowerLimit.position.y;
            transform.eulerAngles = savedAngle;
        }
        else
            upperLowerDiff = buttonUpperLimit.position.y - buttonLowerLimit.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        buttonTop.transform.localPosition = new Vector3(0, buttonTop.transform.localPosition.y, 0);//Setting the local position of the top of the button to have a clamped x and z position so the button is forced to only go up and down
        buttonTop.transform.localEulerAngles = new Vector3(0, 0, 0);//Setting the local angle of the top button to be at the zero position since the top of the button will always have the same rotation as the base


        if (buttonTop.localPosition.y >= 0)//Checking if the top of the button local y position is >= 0. 
                                           //Since the top of the button is parented to the upper limit, 
                                           //if the local position of the top of the button is 0
                                           //then it is going to be at the upper limit. 
                                           //Hence, we set the top of the button transform postion equal to the upper limit position 
                                           //so that way if it exceeds the upper limit, it will just be set to the upper limit. 
            buttonTop.transform.position = new Vector3(buttonUpperLimit.position.x, buttonUpperLimit.position.y, buttonUpperLimit.position.z);
        else//if the top of the button local y position is not at the upper limit then we add the spring force
            buttonTop.GetComponent<Rigidbody>().AddForce(buttonTop.transform.up * force * Time.fixedDeltaTime);


        if (buttonTop.localPosition.y <= buttonLowerLimit.localPosition.y)//Checking if the top of the button local y position is below the lower limit.
                                                                          //if it is or equal to it then we set it to the lower limit.
            buttonTop.transform.position = new Vector3(buttonLowerLimit.position.x, buttonLowerLimit.position.y, buttonLowerLimit.position.z);


        if (Vector3.Distance(buttonTop.position, buttonLowerLimit.position) < upperLowerDiff * threshHold)
            isPressed = true;
        else
            isPressed = false;

        if ((isPressed) && (prevPressedState != isPressed))
            Pressed();
        if ((!isPressed) && (prevPressedState != isPressed))
            Released();
    }

    // void FixedUpdate(){
    //     Vector3 localVelocity = transform.InverseTransformDirection(buttonTop.GetComponent<Rigidbody>().velocity);
    //     Rigidbody rb = buttonTop.GetComponent<Rigidbody>();
    //     localVelocity.x = 0;
    //     localVelocity.z = 0;
    //     rb.velocity = transform.TransformDirection(localVelocity);
    // }

    void Pressed()
    {
        prevPressedState = isPressed;
        pressedSound.pitch = 1;
        pressedSound.Play();
        onPressed.Invoke();
    }

    void Released()
    {
        prevPressedState = isPressed;
        releasedSound.pitch = Random.Range(1.1f, 1.2f);
        releasedSound.Play();
        onReleased.Invoke();
    }
}