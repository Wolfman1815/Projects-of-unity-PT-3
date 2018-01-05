using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointmaths : MonoBehaviour {
    GameObject chara;
    public float distancea;
    public GameObject input;
	// Use this for initialization
	void Start () {
        chara = GameObject.Find("Character");
	}
	
	// Update is called once per frame
	void Update () {
        if (new  Vector2(chara.transform.position.x - gameObject.transform.position.x, chara.transform.position.y - gameObject.transform.position.y).magnitude <= distancea)
            input.SetActive(true);
        else
            input.SetActive(false);
    }
}
