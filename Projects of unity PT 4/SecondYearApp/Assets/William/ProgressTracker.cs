using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressTracker : MonoBehaviour {
    public string team = "Game Design";
	// Use this for initialization
	void Start () {
		//public float[]
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ScoreUpdate(int score)
    {
        PlayerPrefs.SetInt(team, PlayerPrefs.GetInt(team) + score);
    }
}
