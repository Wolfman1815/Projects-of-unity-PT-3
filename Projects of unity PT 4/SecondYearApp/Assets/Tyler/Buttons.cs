using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour {

	public string playerName;

	private Animator registerAnim;
	private Animator findTeamAnim;
	private Animator teamAnim;
	private Animator inventoryAnim;

	public GameObject register;
	public GameObject findTeam;
	public GameObject team;
	public GameObject inventory;

	// Use this for initialization
	void Start () {

		registerAnim = register.GetComponent <Animator> ();
		findTeamAnim = findTeam.GetComponent <Animator> ();
		teamAnim = team.GetComponent <Animator> ();
		inventoryAnim = inventory.GetComponent <Animator> ();

		registerAnim.enabled = false;
		findTeamAnim.enabled = false;
		teamAnim.enabled = false;
		inventoryAnim.enabled = false;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Play () 
	{
		registerAnim.enabled = true;
		registerAnim.Play ("RegisterIn");
	}

	public void Quit () 
	{
		Application.Quit ();
	}

	public void PlayerName () 
	{
		PlayerPrefs.SetString ("PlayerName", playerName);
       
    }

	public void Register () 
	{
		findTeamAnim.enabled = true;
		findTeamAnim.Play ("FindTeamIn");
	}

	public void Okay ()
	{
		SceneManager.LoadScene ("Game");
	}

	public void TeamIn ()
	{
		Debug.Log (gameObject.name);
        teamAnim.enabled = true;
		teamAnim.Play ("TeamSlideIn");
        Soundmanagerscript.PlaySound("Select");
    }

	public void TeamOut ()
	{
		teamAnim.Play ("TeamSlideOut");
        Soundmanagerscript.PlaySound("exit");
    }

	public void InventoryIn ()
	{
		inventoryAnim.enabled = true;
		inventoryAnim.Play ("InventorySlideIn");
        Soundmanagerscript.PlaySound("Select");
    }

	public void InventoryOut ()
	{
		inventoryAnim.Play ("InventorySlideOut");
        Soundmanagerscript.PlaySound("exit");
    }


}
