using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinZone : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        Character character = other.gameObject.GetComponentInParent<Character>();
        if(character != null) {
            character.hasWon();
        }
        
        SwingingCharacter swingingCharacter = other.gameObject.GetComponentInParent<SwingingCharacter>();
        if(swingingCharacter != null) {
            swingingCharacter.hasWon();
        }
    }
}
