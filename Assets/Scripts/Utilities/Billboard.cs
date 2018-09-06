using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour 
{
    private Transform target;

	void Start () 
    {
        target = Camera.main.transform;
	}
	
	void Update () 
    {
		transform.rotation = Quaternion.LookRotation(transform.position - target.position);
	}
}
