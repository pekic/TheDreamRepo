using UnityEngine;
using System.Collections;

public class RPCHelper : MonoBehaviour {

    public static RPCHelper helper;

    public ServerScript server;
    public ClientScript client;


	// Use this for initialization
	void Start () {
        RPCHelper.helper = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    [RPC]
    public void SendInput(string input, float rotation, NetworkMessageInfo NMI = new NetworkMessageInfo())
    {
        if(Network.isClient)
        {
            networkView.RPC( "SendInput", RPCMode.Server, input, rotation);
        }
        else if (Network.isServer)
        {
            foreach (var character in server.characters) 
            {
                if (character.networkPlay == NMI.sender) 
                {
                    if (input.Contains("W")) 
                    {
                        character.charControl.MoveForward();
                    }
                    else
                    {
                        character.charControl.StopForward();
                    }

                    if (input.Contains("A")) 
                    {
                        character.charControl.MoveLeft();
                    }
                    else
                    {
                        character.charControl.StopLeft();
                    }

                    if (input.Contains("D")) 
                    {
                        character.charControl.MoveRight();
                    }
                    else
                    {
                        character.charControl.StopRight();
                    }

                    if (input.Contains("S")) 
                    {
                        character.charControl.MoveBackward();
                    }
                    else
                    {
                        character.charControl.StopBackward();
                    }

                    character.charControl.RotateTurn(rotation);


                }
            }
        }         
    }

    [RPC]
    public void InitializeCharacter(NetworkViewID networkViewId, NetworkPlayer np)
    {
        if (Network.isServer)
        {
            networkView.RPC( "InitializeCharacter", np, networkViewId, np);
        }
        else if (Network.isClient)
        {
            StartCoroutine(client.SetupCharacter(networkViewId));

        }
    }

    [RPC]
    public void SyncObject(NetworkViewID id, string state)
    {
        if (Network.isServer)
        {
            networkView.RPC( "SyncObject", RPCMode.Others, id, state);
        }
        else if (Network.isClient)
        {
            client.SyncObject(id, state);
        }
    }



}
