﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

public class Client : MonoBehaviour {

    public GameObject chatContainer;
    public GameObject messagePrefab;
    public string clientName;

    private bool socketReady;
    private TcpClient socket;
    private NetworkStream stream;
    private StreamWriter writer;
    private StreamReader reader;

    public void ConnectToServer()
    {
        //If already Connected, ignore this function
        if (socketReady)
        {
            return;
        }
        //Default host /port values 
        string host = "127.0.0.1";
        int port = 6321;
        
        //Overwrite default host / port values, if there is something in those boxes 
        string h;
        int p;
        h = GameObject.Find("HostInput").GetComponent<InputField>().text;
        if (h != "")
        {
            host = h;
        }
        int.TryParse(GameObject.Find("PortInput").GetComponent<InputField>().text, out p);
        if (p != 0)
        {
            port = p;
        }
        //Create the socket 
        try
        {
            socket = new TcpClient(host, port);
            stream = socket.GetStream();
            writer = new StreamWriter(stream);
            reader = new StreamReader(stream);
            socketReady = true;
        }
        catch (Exception e)
        {
            Debug.Log("Socket error : " + e.Message);
        }
    }

    private void Update()
    {
        if (socketReady)
        {
            if (stream.DataAvailable)
            {
                string data = reader.ReadLine();
                if (data != null)
                {
                    OnIncomingData(data);
                }
            }
        }
    }

    private void OnIncomingData(string data)
    {
        if (data == "%NAME")
        {
            Send("&NAME|" + clientName);
            return;
        }

        GameObject go= Instantiate(messagePrefab, chatContainer.transform) as GameObject;
        go.GetComponentInChildren<Text>().text = data;
    }

    private void Send(string data)
    {
        if (!socketReady)
        {
            return;
        }
        
        writer.WriteLine(data);
        writer.Flush();
    }
    private string Compress(TeamInfo teamData)
    {
        string data = "%NAME|" + teamData.teamName + " ";
        data += "%NUMBER|" + teamData.teamNum + " ";
        data += "%PASSWORD|" + teamData.password + " ";
        /*data += "%MATES|";
        foreach (string s in teamData.teamMates)
        {
            data += s + ",";
        }*/
        data += " ";
        data += "%POINTS|";
        foreach(int i in teamData.points)
        {
            data += "" + i + ",";
        }
        return data;
    }
    private TeamInfo Decompress(string teamData)
    {
        string[] array = teamData.Split('|');
        string myname = array[1].Split(' ')[0];
        string mynumber = array[2].Split(' ')[0];
        string mypass = array[3].Split(' ')[0];
        string mypoints = array[4].Split(' ')[0];
        string[] points = mypoints.Split(',');
        TeamInfo teamInfo = new TeamInfo(name, mypass);
        //Debug.Log("Name " + myname);
        //Debug.Log("Number " + mynumber);
        //Debug.Log("Pass " + mypass);
        //Debug.Log("Points " + points);
        return teamInfo;
    }
    private void Send(TeamInfo teamData)
    {
        string foo = Compress(teamData);
        TeamInfo bar = Decompress(foo);
        if (!socketReady)
        {
            return;
        }
        writer.WriteLine(foo);
        writer.Flush();
    }

    public void OnSendButton()
    {
        string message = GameObject.Find("SendInput").GetComponent<InputField>().text;
        Send(message);
    }

    public void CreateTeam()
    {
        TeamInfo data = new TeamInfo(GameObject.Find("NameInput").GetComponent<InputField>().text, 
            GameObject.Find("PasswordInput").GetComponent<InputField>().text);
        //Debug.Log("Name " + data.teamName);
        Send(data);
    }

    public void JoinTeam()
    {
        TeamInfo data = new TeamInfo(GameObject.Find("NameInput").GetComponent<InputField>().text,
            GameObject.Find("PasswordInput").GetComponent<InputField>().text);
        //Debug.Log(data.teamName);
        Send(data);
    }

    private void CloseSocket()
    {
        if (!socketReady)
        {
            return;
        }

        writer.Close();
        reader.Close();
        socket.Close();
        socketReady = false;
    }

    private void OnApplicationQuit()
    {
        CloseSocket();
    }

    private void OnDisable()
    {
        CloseSocket();
    }
}
