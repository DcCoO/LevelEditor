using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour {
    public bool playing;

    [Range(1, 5)] public int speedMultiplier = 1;

    public static float speed = 2.5f;

    Transform t;

    private void Start() {
        t = transform;
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown(KeyCode.Q)) speedMultiplier--;
        if (Input.GetKeyDown(KeyCode.E)) speedMultiplier++;
        speedMultiplier = Mathf.Clamp(speedMultiplier, 1, 5);
        if (Input.GetKeyDown(KeyCode.W)) playing = !playing;
        if (Input.GetKeyDown(KeyCode.R)) t.position = new Vector3(8, t.position.y, t.position.z);

        if (playing) t.Translate((float)speedMultiplier * speed * Time.deltaTime, 0, 0);
        t.Translate((float)speedMultiplier * speed * Time.deltaTime * Input.GetAxisRaw("Horizontal"), 0, 0);

        if (t.position.x < 8) t.position = new Vector3(8, t.position.y, t.position.z);
    }
}
