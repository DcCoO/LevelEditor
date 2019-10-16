using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRect : MonoBehaviour {

    public static MouseRect instance = null;
    public Transform placeable;
    public bool canDrag = true;
    public Texture selectTexture;
    public List<Transform> list = new List<Transform>();

    Vector2 orgBoxPos = Vector2.zero, endBoxPos = Vector2.zero;
    Camera cam;
    private void Awake()
    {
        instance = this;
    }


    void Start () {
        cam = Camera.main;
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.R)) Clear();
        if (Input.GetKeyDown(KeyCode.Delete) || Input.GetKeyDown(KeyCode.Backspace)) DeleteSelection();
        bool b = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftCommand) || Input.GetKey(KeyCode.RightCommand);

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.C)) { Copy(); }
        else if (Input.GetKeyDown(KeyCode.V)) { Paste(); }
        else if (Input.GetKeyDown(KeyCode.X)) { SelectAll(); }
#else

        if (Input.GetKeyDown(KeyCode.C) && b) { print("COPY! " + b); }
        else if (Input.GetKeyDown(KeyCode.V) && b) { Paste(); print("PASTE!"); }
        else if (Input.GetKeyDown(KeyCode.A) && b) { SelectAll(); }
#endif



        if (!canDrag) return;

        if(Input.GetMouseButtonDown(0)){
            if(!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift)) Clear();
            orgBoxPos = Input.mousePosition;

        }
        else if(Input.GetMouseButton(0)){
            endBoxPos = Input.mousePosition;
        }
        else if(Input.GetMouseButtonUp(0)){
            Vector2 orig = cam.ScreenToWorldPoint(orgBoxPos);
            Vector2 dest = cam.ScreenToWorldPoint(endBoxPos);
            float left = Mathf.Min(orig.x, dest.x), right = Mathf.Max(orig.x, dest.x);
            float top = Mathf.Max(orig.y, dest.y), bottom = Mathf.Min(orig.y, dest.y);
            StartCoroutine(Select(top, bottom, left, right));

            orgBoxPos = Vector2.zero;
            endBoxPos = Vector2.zero;
            //canDrag = true;
        }
	}

    List<Transform> copyList = new List<Transform>();

    public void SelectAll() {
        list.Clear();
        for (int i = 0, len = placeable.childCount; i < len; i++) {
            list.Add(placeable.GetChild(i));
            placeable.GetChild(i).GetComponent<SpriteRenderer>().color = Color.blue;
        }
    }

    public void DeleteSelection() {
        for (int i = 0; i < list.Count; i++) {
            Destroy(list[i].gameObject);
        }
        list.Clear();
    }

    public void Copy() {
        if (list.Count == 0) return;
        copyList.Clear();

        for(int i = 0; i < list.Count; i++) {
            copyList.Add(list[i]);
        }

        print("Copied " + list.Count + " objects");
    }

    public void Paste() {
        Clear();
        for (int i = 0; i < copyList.Count; i++) {
            GameObject g = Instantiate(copyList[i].gameObject, copyList[i].position, Quaternion.identity, placeable);

            list.Add(g.transform);
            g.GetComponent<SpriteRenderer>().color = Color.blue;
        }

        print("Copied " + copyList.Count + " objects");
    }

   

    IEnumerator Select(float top, float bottom, float left, float right){
        Vector2 pos;
        //Clear();
        print($"left: {left}, right: {right}, bottom: {bottom}, top: {top}");
        for (int i = 0, len = placeable.childCount; i < len; i++){
            pos = placeable.GetChild(i).position;

            if (left <= pos.x && pos.x <= right && bottom <= pos.y && pos.y <= top) {
                print("POS = " + pos);
                list.Add(placeable.GetChild(i));
                placeable.GetChild(i).GetComponent<SpriteRenderer>().color = Color.blue;
            };

        }
        yield return null;
    }

    public void MoveSelected(Vector3 delta){
        for (int i = 0; i < list.Count; i++){
            list[i].position += delta;
        }
    }

    public void Clear(){
        for (int i = 0; i < list.Count; i++){
            list[i].GetComponent<SpriteRenderer>().color = Color.white;
        }
        list = new List<Transform>();
    }


    void OnGUI()
    {
        if (!canDrag) return;
        if (orgBoxPos != Vector2.zero && endBoxPos != Vector2.zero)
        {
            GUI.DrawTexture(new Rect(orgBoxPos.x, Screen.height - orgBoxPos.y, endBoxPos.x - orgBoxPos.x, -1 * ((Screen.height - orgBoxPos.y) - (Screen.height - endBoxPos.y))), selectTexture); // -
        }
    }
}
