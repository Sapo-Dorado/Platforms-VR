using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressing : MonoBehaviour
{
    private bool isPressed = false;
    public Character character;
    void OnTriggerEnter(Collider other) {
        if(!isPressed) {
            transform.position -= Vector3.Normalize(transform.forward) / 6;
            isPressed = true;
            GetComponent<AudioSource>().Play();
            character.addGravity();
        }
    }
}
