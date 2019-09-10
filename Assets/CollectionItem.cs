using UnityEngine.UI;
using UnityEngine;

public class CollectionItem : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(SpawnPlaceable);
    }
	
	// Update is called once per frame
	void SpawnPlaceable() {
        Collection.instance.SpawnPlaceable(GetComponent<Image>().sprite);
    }
}
