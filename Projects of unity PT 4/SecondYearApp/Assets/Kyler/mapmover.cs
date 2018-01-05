using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapmover : MonoBehaviour {
    public int am;
    public int xlimit;
    public int ylimit;
    int limit = 13;
    int ylimitxx = 10;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    }
    public void Out()
    {
        gameObject.GetComponent<Camera>().orthographicSize += 50;
    }
    public void In()
    {
        gameObject.GetComponent<Camera>().orthographicSize -= 50;
    }
    public void Down()
    {
        if (ylimit > -ylimitxx)
        {
            gameObject.GetComponent<Transform>().position = new Vector3(gameObject.GetComponent<Transform>().position.x, gameObject.GetComponent<Transform>().position.y - am, -11);
            ylimit--;
        }
    }
    public void Left()
    {
        if (xlimit > -limit)
        {
            gameObject.GetComponent<Transform>().position = new Vector3(gameObject.GetComponent<Transform>().position.x - am, gameObject.GetComponent<Transform>().position.y, -11);
            xlimit--;
        }

    }
    public void UP()
    {
        if (ylimit < ylimitxx)
        {
            gameObject.GetComponent<Transform>().position = new Vector3(gameObject.GetComponent<Transform>().position.x, gameObject.GetComponent<Transform>().position.y + am, -11);
            ylimit++;
        }
    }
    public void Right()
    {
        if (xlimit < limit)
        {
            gameObject.GetComponent<Transform>().position = new Vector3(gameObject.GetComponent<Transform>().position.x + am, gameObject.GetComponent<Transform>().position.y, -11);
            xlimit++;
        }
    }
}
