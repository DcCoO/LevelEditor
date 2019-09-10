using System.Text.RegularExpressions;
using UnityEngine;

public class Memory : MonoBehaviour {

    public GameObject memoryPanel;
    public SpriteList spriteList;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.S)) memoryPanel.SetActive(!memoryPanel.activeSelf);
    }


    public void Clear() {
        Placeable[] gs = FindObjectsOfType<Placeable>();
        for (int i = gs.Length - 1; i >= 0; i--) Destroy(gs[i].gameObject);
    }

    public void Load() {
        string level = PlayerPrefs.GetString(Level.chosenLevel.ToString(), string.Empty);
        if (level == string.Empty) return;

        Clear();

        string[] res = Regex.Split(level, " ");

        print("TAMANHO " + res.Length + ": " + res[0] );

        for(int i = 0; i < res.Length; i += 3) {
            Collection.instance.SpawnPlaceable(spriteList.GetSprite(res[i]), new Vector2(float.Parse(res[i + 1]), float.Parse(res[i + 2])));
        }
    }

    public void Save() {
        //nome x y nome x y nome x y

        string level = "";
        Placeable[] gs = FindObjectsOfType<Placeable>();
        for(int i = 0; i < gs.Length; i++) {
            level += gs[i].sr.sprite.name + " ";
            level += gs[i].transform.position.x + " " + gs[i].transform.position.y;
            if (i != gs.Length - 1) level += " ";
        }

        PlayerPrefs.SetString(Level.chosenLevel.ToString(), level);
    }


}
