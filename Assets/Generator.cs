using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class Generator : MonoBehaviour {

    private void Update() {
        if (Input.GetKeyDown(KeyCode.A)) {
            GenerateJson();
        }
    }

    public void GenerateJson() {
        List<GameObject> list = new List<GameObject>();
        Transform t = transform;
        int childCount = t.childCount;
        print(childCount + " children");
        for(int i = 0; i < childCount; i++) {
            list.Add(t.GetChild(i).gameObject);
        }

        list = list.OrderBy(x => x.transform.position.x).ToList();
        print("list has " + list.Count + " elements");
        string s = "{\n";

        for(int i = 0; i < childCount; i++) {
            GameObject g = list[i];
            s += "\t";
            s += $"{{\"name\": \"{g.GetComponent<SpriteRenderer>().sprite.name}\", ";
            s += $"\"time\": {GetTime(g.transform.position.x)}, ";
            s += $"\"yScale\": {GetYScale(g.transform.position.y)}}}";
            if (i == childCount - 1) s += "\n";
            else s += ",\n";
        }
        s += "}";
        Debug.Log(s);
    }

    float x0 = 16;
    float GetTime(float x) {
        return (x - x0) / MoveCamera.speed; 
    }

    float GetYScale(float y) {
        return y / 9f;
    }

}

[CustomEditor(typeof(Generator))]
public class GeneratorEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        if(GUILayout.Button("Generate JSON")) {
            Generator g = (Generator) target;
            g.GenerateJson();
        }
    }
}