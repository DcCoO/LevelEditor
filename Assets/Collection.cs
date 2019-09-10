using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collection : MonoBehaviour {

    public static Collection instance = null;

    public GameObject collectionPanel;
    public GameObject placeablePrefab, placeableParent;
    
    private void Awake() {
        instance = this;
    }

    // Update is called once per frame
    void Update () {
        
        if(Input.GetKeyDown(KeyCode.A)) collectionPanel.SetActive(!collectionPanel.activeSelf);

	}

    public void SpawnPlaceable(Sprite sprite) {
        GameObject g = Instantiate(placeablePrefab, (Vector2) Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0)), Quaternion.identity);
        //g.transform.position = new
        g.transform.parent = placeableParent.transform;
        g.GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public void SpawnPlaceable(Sprite sprite, Vector2 position) {
        GameObject g = Instantiate(placeablePrefab, position, Quaternion.identity);
        //g.transform.position = new
        g.transform.parent = placeableParent.transform;
        g.GetComponent<SpriteRenderer>().sprite = sprite;
    }

}
