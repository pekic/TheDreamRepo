using UnityEngine;
using System.Collections;

public class ClientScript : MonoBehaviour
{
    string oldInput = "";
    string logDebug = "";

    public CharacterController character;
    public Transform mainCamera;

    // Use this for initialization
    void Start()
    {
        ConnectToServer();
    }
    
    // Update is called once per frame
    void Update()
    {

        string input = "";

        logDebug = Input.inputString;

        if (Input.GetKey(KeyCode.W))
        {
            input += "W";
            character.MoveForward();
        }
        else
        {
            character.StopForward();
        }

        if (Input.GetKey(KeyCode.A))
        {
            input += "A";
            character.MoveLeft();
        }
        else
        {
            character.StopLeft();
        }

        if (Input.GetKey(KeyCode.D))
        {
            input += "D";
            character.MoveRight();
        }
        else
        {
            character.StopRight();
        }

        if (Input.GetKey(KeyCode.S))
        {
            input += "S";
            character.MoveBackward();
        }
        else
        {
            character.StopBackward();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Screen.lockCursor = !Screen.lockCursor;
            Screen.showCursor = !Screen.showCursor;
        }

        float rotation = Time.fixedDeltaTime * Input.GetAxisRaw("Mouse X") * 50;
        character.RotateTurn(rotation);
        Debug.Log(rotation);
        if (oldInput != input || rotation != 0)
        {
            RPCHelper.helper.SendInput(input, rotation);
        }

        oldInput = input;


    }

    public void SyncObject(NetworkViewID IDictionary, string NetworkStateSynchronization)
    {

    }

    void ConnectToServer()
    {
        Network.Connect( "46.240.238.28", 25000);
        Screen.lockCursor = !Screen.lockCursor;
        Screen.showCursor = !Screen.showCursor;
    }

    void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 300, 800), logDebug);
    }

    public IEnumerator SetupCharacter(NetworkViewID networkViewId)
    {
        while (character == null)
        {             
            var chars = GameObject.FindGameObjectsWithTag("Player");
            foreach (var c in chars) 
            {
                if (c.networkView != null && c.networkView.viewID == networkViewId) 
                {
                    character = c.GetComponent<CharacterController>();
                }
            }

            yield return new WaitForSeconds(0.1f);
        }

        character.SetupCharater();



    }

}
