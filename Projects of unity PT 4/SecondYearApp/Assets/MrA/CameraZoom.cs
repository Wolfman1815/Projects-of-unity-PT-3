using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour {

	public void ZoomIn()
    {
        var foo = Camera.main.GetComponent<Camera>().orthographicSize;
        foo += 3;
        Camera.main.GetComponent<Camera>().orthographicSize = foo;
    }
}
