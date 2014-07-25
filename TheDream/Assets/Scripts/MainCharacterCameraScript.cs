using UnityEngine;
using System.Collections;

public class MainCharacterCameraScript : MonoBehaviour
{

    public Vector3 position, rotation;

    // Use this for initialization
    void Start()
    {
    
    }
    
    // Update is called once per frame
    void Update()
    {
    
    }

    public void SetupCamera()
    {
        transform.localPosition = position;
        transform.localRotation = Quaternion.Euler(rotation);
    }
}
