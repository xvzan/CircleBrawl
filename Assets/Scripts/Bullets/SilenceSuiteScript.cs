using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class SilenceSuiteScript : Photon.MonoBehaviour
{
    public float speed = 7;
    public GameObject cA;
    public GameObject cB;
    public LineRenderer LRR;
    public float turnangle = 15;
    public float maxtime = 1;
    Rigidbody2D rA;
    Rigidbody2D rB;

	// Use this for initialization
	void Start ()
    {
        rA = cA.GetComponent<Rigidbody2D>();
        rB = cB.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void work(Vector2 workdir)
    {
        Vector2 sA = Quaternion.AngleAxis(turnangle, Vector3.forward) * workdir;
        Vector2 sB = Quaternion.AngleAxis(turnangle, Vector3.back) * workdir;
        maxtime = workdir.magnitude * 2 / (sA + sB).magnitude;
        rA.velocity = sA;
        rB.velocity = sB;
    }
}
