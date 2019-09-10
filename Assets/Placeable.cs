using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Placeable : MonoBehaviour {

    public SpriteRenderer sr;
    private Vector3 offset;

    void OnMouseDown() {
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
    }

    void OnMouseDrag() {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;
    }

    private void OnMouseOver() {
        if (Input.GetMouseButton(1)) Destroy(gameObject);
    }

    public string JSON() {
        string s = "{";
        s += $"{{\"name\": \"{sr.sprite.name}\", ";
        s += $"\"time\": {GetTime(transform.position.x)}, ";
        s += $"\"yScale\": {GetYScale(transform.position.y)}}}";
        return s;
    }

    float x0 = 16;
    float GetTime(float x) {
        return (x - x0) / MoveCamera.speed;
    }

    float GetYScale(float y) {
        return y / 9f;
    }
}
