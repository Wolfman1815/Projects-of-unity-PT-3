using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class latlon2xy : MonoBehaviour {


    // Use this for initialization
    Vector3 startPos;
    public Text text;
    string words;
    public float startLat;
    public float startLong;
    public Image canvasImage;
    void Start () {
        startPos = transform.position;
        //startLat = canvasImage.GetComponent<editorimage>().lat;
        //startLong = canvasImage.GetComponent<editorimage>().lon;

    }
	
	// Update is called once per frame
	void Update () {
        words = "Lat: " + Input.location.lastData.latitude + " Long: " + Input.location.lastData.longitude;
        float y = latToY(Input.location.lastData.latitude);
        float x = lonToX(Input.location.lastData.longitude);
        words += " y: " + y;
        words += " X: " + x;
        text.GetComponent<Text>().text = words;
        this.transform.position = new Vector3(x, y, 0f);
    }
    void RetrieveGPSData()
    {
      

        float y = latToY(Input.location.lastData.latitude);
        float x = lonToX(Input.location.lastData.longitude);
        
        text.GetComponent<Text>().text = words;
        this.transform.position = new Vector3(x, y, 0f);

    }

    float latToY(double lat)
    {
        // lat = 46.2732197;
        lat = (lat - startLat) / (0.000004507291667);//(0.00000143020833);
        double y = lat;

        return (float)y;
    }

    float lonToX(double lon)
    {
       // lon = -119.276123;
        lon = (lon - startLong) / (0.000003550625);/// 0.00001 * ;
        //Debug.Log(lon);
        double x = lon;

        return (float)x;
    }
}
