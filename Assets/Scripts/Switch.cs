using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour 
{
    public Animator bridge;

    public void OpenBridge()
    {
        bridge.Play("Bridge Open");
    }
}
