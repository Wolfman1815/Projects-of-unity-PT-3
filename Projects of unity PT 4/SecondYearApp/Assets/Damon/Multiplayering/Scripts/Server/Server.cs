using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System;
using System.Net;
using System.IO;

public class Server : MonoBehaviour {
    private List<ServerClient> clients;
    private List<ServerClient> dissconnectList;

    private List<TeamInfo> teams;
    private List<string> teamNames;

    public int port = 6321;
    private TcpListener server;
    private bool serverStarted;

    private void Start()
    {
        teamNames = new List<string>();
        clients = new List<ServerClient>();
        dissconnectList = new List<ServerClient>();
        teams = new List<TeamInfo>();
        try
        {
            server = new TcpListener(IPAddress.Any, port);
            server.Start();

            StartListening();
            serverStarted = true;

            Debug.Log("Server has been started on port " + port.ToString());
        }
        catch (Exception e)
        {
            Debug.Log("Socket error: " + e.Message);
        }
    }

    private void Update()
    {
        if (!serverStarted)
        {
            return;
        }
        foreach(ServerClient c in clients)
        {
            // is the client still connected 
            if (!IsConnected(c.tcp))
            {
                c.tcp.Close();
                dissconnectList.Add(c);
                continue;
            }
            //check for message from client 
            else
            {
                NetworkStream s = c.tcp.GetStream();
                if (s.DataAvailable)
                {
                    StreamReader reader = new StreamReader(s, true);
                    string data = reader.ReadLine();
                    
                    if (data != null)
                    {
                        OnIncomingData(c, data);
                    }
                }
            }
        }

        for(int i = 0; i < dissconnectList.Count - 1; i++)
        {
            Broadcast(dissconnectList[i] + " : Has dissconnected", clients);

            clients.Remove(dissconnectList[i]);
            dissconnectList.RemoveAt(i);
        }
    }

    private void StartListening()
    {
        server.BeginAcceptTcpClient(AcceptTcpClient, server);
    }
    private bool IsConnected(TcpClient c)
    {
        try
        {
            if (c != null && c.Client != null && c.Client.Connected)
            {
                if (c.Client.Poll(0, SelectMode.SelectRead))
                {
                    return !(c.Client.Receive(new byte[1], SocketFlags.Peek) == 0);
                }

                return true;
            } else
            {
                return false; 
            }
        }
        catch
        {
            return false;
        }
    }
    private void AcceptTcpClient(IAsyncResult ar)
    {
        TcpListener listener = (TcpListener)ar.AsyncState;

        clients.Add(new ServerClient(listener.EndAcceptTcpClient(ar)));
        StartListening();

        //Send a message to everyone that someone has connected
        Broadcast("%NAME",new List<ServerClient>() { clients[clients.Count - 1] });
    }

    private void OnIncomingData(ServerClient c, TeamInfo data)
    {
        //if we already have the team registered and the team already contains the person sending us data
        //this is where most of our code will go

        if (!teamNames.Contains(data.teamName))
        {
            Debug.Log("Adding to teams list");
            teamNames.Add(data.teamName);
            int temp = teams.Count;
            data.teamNum = temp;
            data.teamMembers.Add(c);
            teams.Add(data);
            Broadcast(Compress(data), c);
            return;
            //Debug.Log(data.teamNum + " Success!");

        }
        /*else if (teamNames.Contains(data.teamName) && data.teamNum == -1)
        {
            Broadcast("This name already exists", c);
            return;
        }*/
        //if we already have the team registered, but the team is full
        int index = teamNames.IndexOf(data.teamName);
        TeamInfo realTeamInfo = teams[index];
        if (teamNames.Contains(data.teamName) && !realTeamInfo.teamMembers.Contains(c) && realTeamInfo.teamMembers.Count >= 2)
        {
            Broadcast("This team is full!", c);
        }//if we already have the team registered but we're trying to add a new member
        else if (teamNames.Contains(data.teamName) && !realTeamInfo.teamMembers.Contains(c) && realTeamInfo.teamMembers.Count < 2)
        {
            realTeamInfo.teamMembers.Add(c);
            teams[index] = realTeamInfo;
            Broadcast(Compress(realTeamInfo), c);
            //Broadcast("You've been added to the team!", c);
            //Broadcast(Compress(data), c);

        }
        else if (teamNames.Contains(data.teamName) && data.teamMembers.Contains(c))
        {
            Debug.Log("Performing normal team operations");
            TeamInfo serverData = teams[data.teamNum];
            if(serverData.points == data.points)
            {
                return;
            }
            serverData.points = data.points;
            teams[data.teamNum] = serverData;
            Broadcast(serverData, serverData.teamMembers);

        }
        
        
    }

    private void OnIncomingData (ServerClient c, string data)
    {
        
        if (data.Contains("&NAME"))
        {
            c.clientName = data.Split('|')[1];
            Broadcast(c.clientName + " has connected!", clients);
        }
        else
        {
            TeamInfo info = Decompress(data);
            OnIncomingData(c, info);
            //Broadcast(c.clientName + " : " + data, clients);
        }
    }

    //broadcast to a list of people, eventually the team list
    private void Broadcast(string data, List<ServerClient> cl)
    {
        foreach(ServerClient c in cl)
        {
            try
            {
                StreamWriter writer = new StreamWriter(c.tcp.GetStream());
                writer.WriteLine(data);
                writer.Flush();
            }
            catch (Exception e)
            {
                Debug.Log("Write error : " + e.Message + " to client " + c.clientName);
            }
        }
    }
    //broadcast the team info back to the team
    private void Broadcast(TeamInfo data, List<ServerClient> cl)
    {
        foreach (ServerClient c in cl)
        {
            try
            {
                StreamWriter writer = new StreamWriter(c.tcp.GetStream());
                writer.WriteLine(data);
                writer.Flush();
            }
            catch (Exception e)
            {
                Debug.Log("Write error : " + e.Message + " to client " + c.clientName);
            }
        }
    }

    //broadcast to a single individual
    private void Broadcast(string data, ServerClient c)
    {
        try
        {
            StreamWriter writer = new StreamWriter(c.tcp.GetStream());
            writer.WriteLine(data);
            writer.Flush();
        }
        catch (Exception e)
        {
            Debug.Log("Write error : " + e.Message + " to client " + c.clientName);
        }
        
    }

    private TeamInfo Decompress(string teamData)
    {
        string[] array = teamData.Split('|');
        string myname = array[1].Split(' ')[0];
        string mynumber = array[2].Split(' ')[0];
        int teamnum;
        //Debug.Log(myname);
        Int32.TryParse(mynumber, out teamnum);
        string mypass = array[3].Split(' ')[0];
        string mypoints = array[4].Split(' ')[0];

        TeamInfo teamInfo = new TeamInfo(name, mypass);
        teamInfo.pointString = mypoints;
        teamInfo.teamName = myname;
        teamInfo.teamNum = teamnum;
        //if (mypoints != teams[teamnum].pointString)
        //{

            string[] points = mypoints.Split(',');
            int[] realPoints = new int[points.Length];
            for (int i = 0; i < points.Length; i++)
            {
            int placeholder = 0; ;
            Int32.TryParse(points[i], out placeholder);
            realPoints[i] = placeholder;
            }
            teamInfo.points = realPoints;
        //}
        
        return teamInfo;
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
        foreach (int i in teamData.points)
        {
            data += "" + i + ",";
        }
        return data;
    }

}

public class ServerClient
{
    public TcpClient tcp;
    public string clientName;

    public ServerClient(TcpClient clientSocket)
    {
        clientName = "Guest";
        tcp = clientSocket;
    }
}
