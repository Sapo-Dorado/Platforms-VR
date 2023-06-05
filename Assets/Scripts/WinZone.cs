using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinZone : MonoBehaviour
{
    private bool hasTriggered = false;
    void OnTriggerEnter(Collider other) {
        Character character = other.gameObject.GetComponentInParent<Character>();
        if(character != null) {
            if(!hasTriggered) {
                character.hasWon();
                GetComponent<AudioSource>().Play();
                hasTriggered = true;
            }
        }
        
        SwingingCharacter swingingCharacter = other.gameObject.GetComponentInParent<SwingingCharacter>();
        if(swingingCharacter != null) {
            if(!hasTriggered) {
                swingingCharacter.hasWon();
                GetComponent<AudioSource>().Play();
                hasTriggered = true;
            }
        }
    }
}
