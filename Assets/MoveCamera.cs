using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveCamera : MonoBehaviour {
    public bool playing;

    [Range(1, 5)] public int speedMultiplier = 1;

    public static float speed = 1f;

    Transform t;

    private void Start() {
        t = transform;
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        if (Input.GetKeyDown(KeyCode.Q)) speedMultiplier--;
        if (Input.GetKeyDown(KeyCode.E)) speedMultiplier++;
        speedMultiplier = Mathf.Clamp(speedMultiplier, 1, 5);
        if (Input.GetKeyDown(KeyCode.W)) playing = !playing;
        if (Input.GetKeyDown(KeyCode.R)) t.position = new Vector3(8, t.position.y, t.position.z);

        if (playing) t.Translate((float)speedMultiplier * speed * 5f * Time.deltaTime, 0, 0);
        t.Translate((float)speedMultiplier * 5f * speed * Time.deltaTime * Input.GetAxisRaw("Horizontal"), 0, 0);

        if (Input.GetKeyDown(KeyCode.UpArrow)){
            Camera.main.orthographicSize = Mathf.Max(4.5f, Camera.main.orthographicSize - 2);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)){
            Camera.main.orthographicSize = Camera.main.orthographicSize + 2;
        }


        if (t.position.x < 8) t.position = new Vector3(8, t.position.y, t.position.z);
    }
}
