using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundmanagerscript : MonoBehaviour {
   
    public static AudioClip exit, Select, name1;

    
    static AudioSource audioSrc;

	
	void Start () {
   

        exit = Resources.Load<AudioClip>("exit");
        Select = Resources.Load<AudioClip>("Select");
        name1 = Resources.Load<AudioClip>("name1");


        audioSrc = GetComponent<AudioSource>();

	}
	
	
	void Update () {
		
	}


   
 

    public static void PlaySound (string clip)
    {
        switch (clip)
        {
            case "exit":
                audioSrc.PlayOneShot(exit);
                break;
        }
        switch (clip)
        {
            case "Select":
                audioSrc.PlayOneShot(Select);
                break;
        }
        switch (clip)
        {
            case "name1":
                audioSrc.PlayOneShot(name1);
                break;
        }

    }


}
//Use this in another script to have your sound play after something, put it after you jump or when you get hit stuff like that go nuts.
//Soundmanagerscript.PlaySound("shootp");