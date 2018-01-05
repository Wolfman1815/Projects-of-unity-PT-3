using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeName : MonoBehaviour {
    public GameObject popup;
    public string theChange;
    public bool onlyOnce = false;
    public GameObject popup2;
	// Use this for initialization
	void Start () {
        PlayerPrefs.SetString("Placeholder", "Team One");
        //Debug.Log(PlayerPrefs.GetString("Placeholder"));
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AreYouSure()
    {
        if (!onlyOnce)
        {
            popup.SetActive(true);
            theChange = gameObject.GetComponent<InputField>().text;
            PlayerPrefs.SetString("Changing", theChange);
            //Debug.Log("Loading");
            onlyOnce = true;
        } else
        {
			popup.SetActive (false);
            popup2.SetActive(true);
        }
    }
    public void NameChange()
    {
        PlayerPrefs.SetString("Placeholder", PlayerPrefs.GetString("Changing"));
        PlayerPrefs.SetString("Changing", "");
        popup.SetActive(false);
        //Debug.Log("Changing");
        Debug.Log(PlayerPrefs.GetString("Placeholder"));
    }
    public void NoNameChange()
    {
        popup.SetActive(false);
        //Debug.Log("Cancel");
    }
}
