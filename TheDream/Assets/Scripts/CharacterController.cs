using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {

    public bool forward, left, right, back;
    Animator animator;


	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
        if (forward ^ back)
        {
            var p = transform.position;
            transform.position = p + ((forward ? transform.forward : -transform.forward/2) * Time.fixedDeltaTime * 20);
        }

        if (right ^ left)
        {
            var p = transform.position;
            transform.position = p + ((right ? transform.right : -transform.right) * Time.fixedDeltaTime * 20);
        }
    }

    public void SetupCharater()
    {
        if (Network.isClient)
        {
            ClientScript client = GameObject.FindGameObjectWithTag("Client").GetComponent<ClientScript>();
            client.character = this;
            Transform go = (Transform)Instantiate(client.mainCamera);
            go.parent = transform;
            go.GetComponent<MainCharacterCameraScript>().SetupCamera();
            
        }
    }

    public void MoveForward()
    {
        forward = true;
        animator.SetBool("Forward", true);
    }

    public void StopForward()
    {
        forward = false;
        animator.SetBool("Forward", false);
    }

    public void MoveLeft()
    {
        left = true;
        animator.SetBool("Left", true);
    }

    public void StopLeft()
    {
        left = false;
        animator.SetBool("Left", false);
    }

    public void MoveRight()
    {
        right = true;
        animator.SetBool("Right", true);
    }

    public void StopRight()
    {
        right = false;
        animator.SetBool("Right", false);
    }

    public void MoveBackward()
    {
        back = true;
        animator.SetBool("Backward", true);
    }
    
    public void StopBackward()
    {
        back = false;
        animator.SetBool("Backward", false);
    }
    
    public void RotateTurn(float rotation)
    {
        var r = transform.rotation.eulerAngles;
        r.y += rotation;
        transform.rotation = Quaternion.Euler(r);

    }


}
