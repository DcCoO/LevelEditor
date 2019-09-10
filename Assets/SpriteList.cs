using UnityEngine;

[CreateAssetMenu(fileName = "Sprite List", menuName = "Sprite List", order = 52)] // 1
public class SpriteList : ScriptableObject {

    public Sprite[] list;

    public Sprite GetSprite(string name) {
        Sprite s = null;
        for(int i = 0; i < list.Length; i++) {
            if (list[i].name == name) s = list[i];
        }
        return s;
    }

}