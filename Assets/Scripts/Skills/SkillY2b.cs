using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class SkillY2b : Photon.MonoBehaviour
{

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update ()
    {

    }

    void testrotate(Vector2 vec2)
    {
        vec2 = Quaternion.AngleAxis(30, Vector3.forward) * vec2;
    }
}
