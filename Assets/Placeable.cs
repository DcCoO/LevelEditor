using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Placeable : MonoBehaviour {

    public SpriteRenderer sr;
    private Vector3 offset;

    Vector3 lastPos;

    void OnMouseDown(){

        MouseRect.instance.canDrag = false;


        if(MouseRect.instance.list.Count == 0) {
            MouseRect.instance.Clear();
            MouseRect.instance.list.Add(transform);
        }
        else {
            if (!MouseRect.instance.list.Contains(transform)) {
                MouseRect.instance.Clear();
                MouseRect.instance.list.Add(transform);
            }
        }

        /*
        if (sr.color.g > 0.8f){   //se ele nao faz parte de uma selecao, ele esta branco, logo g = 1
            MouseRect.instance.Clear();
            MouseRect.instance.list.Add(transform);
            print("ENTROU AQUI2");
        }*/
        sr.color = Color.blue;

        lastPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
    }


    void OnMouseDrag() {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        Vector3 currPos = Camera.main.ScreenToWorldPoint(curScreenPoint);
        Vector3 delta = currPos - lastPos;
        //transform.position += delta;
        MouseRect.instance.MoveSelected(delta);
        lastPos = currPos;
    }

    private void OnMouseUp()
    {
        MouseRect.instance.canDrag = true;
    }


    private void OnMouseOver() {
        if (Input.GetMouseButton(1)) {
            MouseRect.instance.list.Remove(transform);
            Destroy(gameObject);
        }
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
