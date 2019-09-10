using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour {
    public bool playing;

    [Range(1, 4)] public int speedMultiplier = 1;

    public static float speed = 0.04f;

    Transform t;

    private void Start() {
        t = transform;
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown(KeyCode.Q)) speedMultiplier--;
        if (Input.GetKeyDown(KeyCode.E)) speedMultiplier++;
        speedMultiplier = Mathf.Clamp(speedMultiplier, 1, 4);
        if (Input.GetKeyDown(KeyCode.W)) playing = !playing;
        if (Input.GetKeyDown(KeyCode.R)) t.position = new Vector3(8, t.position.y, t.position.z);

        if (playing) t.Translate((float)speedMultiplier * speed, 0, 0);
        t.Translate((float)speedMultiplier * speed * Input.GetAxisRaw("Horizontal"), 0, 0);

        if (t.position.x < 8) t.position = new Vector3(8, t.position.y, t.position.z);
    }
}
