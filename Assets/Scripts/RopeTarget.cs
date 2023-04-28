using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeTarget : MonoBehaviour
{
    public GameObject player;
    GameObject line;
    LineRenderer lineRenderer;

    public void Start() {
        line = new GameObject("Line");
        lineRenderer = line.AddComponent<LineRenderer>();
        line.SetActive(false);
    }
    public void isSelected() {
        if(line.activeSelf) {
            line.SetActive(false);
        } else {
            lineRenderer.SetPosition(0, transform.position); //x,y and z position of the starting point of the line
            lineRenderer.SetPosition(1, player.transform.position);
            line.SetActive(true);
        }
    }
}
