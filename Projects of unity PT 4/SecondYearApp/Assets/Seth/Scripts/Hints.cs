using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hints : MonoBehaviour {

    // Use this for initialization

    public GameObject textObject;

    public bool withinRange;

    public bool vacinity;

    public GameObject Player;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
       //trying to make it where the player is within vacinity, turn bool true and display hint button. 
       //Then, if vacinity is out of range, turn bool false.
        if(gameObject.tag == "Player" && vacinity == true)
        {

            withinRange = true;
            gameObject.GetComponentInParent<Button>().enabled = true;


        }
        else
        {

            if(vacinity == false)
            {

                withinRange = false;
                gameObject.GetComponentInParent<Button>().enabled = false;

            }

        }


	}


    public void ButtonPress()
    {


        if(textObject.active == true)
        {

            textObject.SetActive(false);

        }
        else
        {

            textObject.SetActive(true);

        }


    }

}
