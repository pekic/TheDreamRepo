using UnityEngine;
using System.Collections;

public class ControlableCharacter {

    public CharacterController charControl;
    public NetworkPlayer networkPlay;

    public ControlableCharacter(CharacterController cc, NetworkPlayer np)
    {
        charControl = cc;
        networkPlay = np;
    }
}
