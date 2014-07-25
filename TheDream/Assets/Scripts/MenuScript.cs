using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour
{

    public Transform client, server;

    public RPCHelper rpcHelper;

    // Use this for initialization
    void Start()
    {
	
    }
	
    // Update is called once per frame
    void Update()
    {
	
    }

    void OnGUI()
    {
        if (Network.isClient || Network.isServer)
        {
            return;
        }

        if (GUI.Button(new Rect(0, 0, 110, 30), "Start Server"))
        {
            var go = (Transform)Instantiate(server);
            rpcHelper.server = go.GetComponent<ServerScript>();
        }

        if (GUI.Button(new Rect(0, 30, 110, 30), "Start Client"))
        {
            var go = (Transform)Instantiate(client);
            rpcHelper.client = go.GetComponent<ClientScript>();
        }
    }

}
