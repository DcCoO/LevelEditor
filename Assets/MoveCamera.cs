using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour {
    public bool x1;
    public bool x2;
    public bool x4;

    int curr = 1;


    public static float speed = 0.04f;
	
	// Update is called once per frame
	void Update () {
        if (curr == 1) {
            if (x2) {
                x1 = false;
                x4 = false;
                curr = 2;
            }
            else if (x4) {
                x1 = false;
                x2 = false;
                curr = 4;
            }
        }
        else if (curr == 2) {
            if (x1) {
                x2 = false;
                x4 = false;
                curr = 1;
            }
            if (x4) {
                x1 = false;
                x2 = false;
                curr = 4;
            }
        }
        else if (curr == 4) {
            if (x1) {
                x2 = false;
                x4 = false;
                curr = 1;
            }
            if (x2) {
                x1 = false;
                x4 = false;
                curr = 2;
            }
        }
        transform.Translate((float) curr * speed, 0, 0);
	}
}
