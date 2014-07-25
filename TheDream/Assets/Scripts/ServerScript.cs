using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ServerScript : MonoBehaviour
{
    public Transform playerGameObject;
    public static string logText;
    public int maxConnections, listenedPort;
    public ICollection<ControlableCharacter> characters;


    // Use this for initialization
    void Start()
    {
        characters = new List<ControlableCharacter>();
        InitializeServer();
    }
    
    // Update is called once per frame
    void Update()
    {
    
    }

    void InitializeServer()
    {
        NetworkConnectionError nce = Network.InitializeServer(maxConnections, listenedPort, true);

        if (nce == null)
        {
            logText += "\nError initializing server: " + nce.ToString();
            return;
        }
                
        logText += "\nServerInitialized";
    }

    void OnGUI()
    {
        GUI.BeginGroup( new Rect(0, 0, 450, 800));
        GUI.Label( new Rect(10, 0, 400, 800), logText);  
        GUI.EndGroup();
    }

    public void OnPlayerConnected(NetworkPlayer np)
    {       

        Transform go =  (Transform)Network.Instantiate( playerGameObject, Vector3.zero, new Quaternion(), 0);
        characters.Add(new ControlableCharacter(go.GetComponent<CharacterController>(), np));
        RPCHelper.helper.InitializeCharacter(go.networkView.viewID, np);
        logText += "\nPlayer Connected";
    }

    public void OnPlayerDisconnected(NetworkPlayer np)
    {

    }

}
