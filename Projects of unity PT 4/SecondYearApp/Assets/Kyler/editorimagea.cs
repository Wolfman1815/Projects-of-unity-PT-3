using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class editorimagea : MonoBehaviour {

	// Use this for initialization
	public Sprite img;
	string url;

	public float lat;
	public float lon;

	LocationInfo  li;

	public int zoom = 14;
	public int mapWidth = 640;
	public int mapHeight = 640;

	public enum maptype {roadmap,satellite,hybrid,terrain}
	public maptype mapSelected;
	public int scale;


	IEnumerator Map(){
		url = "https://maps.googleapis.com/maps/api/staticmap?center=" + lat + "," + lon +
	"&zoom=" + zoom + "&size=" + mapWidth + "x" + mapHeight + "&scale=" + scale 
			+"&maptype=" + mapSelected + "&markers=color:blue%7Clabel:S%7C40.702147,-74.015794&markers=color:green%7Clabel:G%7C46.273846,-119.270735&markers=color:red%7Clabel:C%7C40.718217,-73.998284"
            + "&key=AIzaSyAFff0LBWpI4Afp8Fpz-ipNOORdoCd4lho";
		WWW www = new WWW (url);
		yield return www;
		img = Sprite.Create(www.texture,new Rect(0,0, www.texture.width, www.texture.height),new Vector2(0.5f,0.5f));
		//img.SetNativeSize ();
	}

	void Start (){
		img = gameObject.GetComponent<Sprite> ();
		StartCoroutine (Map());
	}
	// Update is called once per frame
	void Update () {
		
	}
    public void ZoomIn()
    {
        if (!(zoom > 18)){
            zoom++;
            img = gameObject.GetComponent<Sprite>();
            StartCoroutine(Map());
        }
    }
    public void ZoomOut()
    {
        if (!(zoom <= 1))
        {
            zoom--;
            img = gameObject.GetComponent<Sprite>();
            StartCoroutine(Map());
        }
    }
    public void MapMake()
    {
        img = gameObject.GetComponent<Sprite>();
        StartCoroutine(Map());
    }
}
