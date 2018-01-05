using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teams : MonoBehaviour {
    public GameObject blankTeam;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TeamCreation()
    {
        string teamName = GameObject.Find("TeamName").GetComponent<InputField>().text;
        string password = GameObject.Find("TeamPassword").GetComponent<InputField>().text;
    }
}
[System.Serializable]
public class TeamInfo
{
    public int teamNum;
    public string teamName;
    public List<string> teamMates;
    public List<ServerClient> teamMembers;
    public int[] points;
    public string pointString;
    public List<GameObject> objectives;
    public string password;

    public TeamInfo(string teamNameTemp, string passwordTemp)
    {
        teamName = teamNameTemp;
        //Debug.Log(teamName);
        password = passwordTemp;
        teamNum = -1;
        points = new int[100];
        pointString = "";
        teamMembers = new List<ServerClient>();
        objectives = new List<GameObject>();

    }
}
