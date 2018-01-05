using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerafollw : MonoBehaviour {
    public GameObject objects;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position.Set(objects.gameObject.transform.position.x, objects.gameObject.transform.position.y, -10);
	}
}
