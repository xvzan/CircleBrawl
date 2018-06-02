using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public delegate void CookVelo(Rigidbody2D victim,MoveScript worker);

public class MoveScript : Photon.MonoBehaviour {

    public bool controllable;
    //public bool cooking;
    public float movespeed;
    public Vector2 movedirection = Vector2.zero;
    public GameObject targetshadow;
    public Vector2 Givenvelocity;
    public Vector2 VelotoAdd = Vector2.zero;
    private Vector2 rightclickplace;
    private Vector2 movetarget;
    public Vector2 selfvelocity;
    public Rigidbody2D PlayerRb2d;
    private GameObject maincam;
    public GameObject targeticon;
    private Vector3 followplace;
    public CookVelo cook;

    void cookstart(Rigidbody2D victim, MoveScript worker)
    {
        worker.VelotoAdd = Vector2.zero;
    }

	// Use this for initialization
	void Start ()
    {
        //PlayerRb2d = GetComponent<Rigidbody2D>();
        maincam = GameObject.Find("Main Camera");
        controllable = true;
        cook = cookstart;
    }

    void Setselfvelocity()
    {
        if (targeticon != null)
        {
            movetarget = targeticon.transform.position;
            movedirection = movetarget - PlayerRb2d.position;
            if (movedirection.sqrMagnitude < 0.1)
            {
                GameObject.Destroy(targeticon);
            }
            selfvelocity = movedirection.normalized * (movespeed + StealthScript.Speed);
        }
        else
        {
            selfvelocity = Vector2.zero;
        }
    }
    
    // Update is called once per frame
    void Update ()
    {
        if (Input.GetMouseButtonDown(1))
        {
            rightclickplace = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GameObject.Destroy(targeticon);
            targeticon = Instantiate(targetshadow, rightclickplace, Quaternion.identity);
            DoSkill.singing = 0;
            GetComponent<SkillR2b>().IdoDSWL();
        }
	}

    void FixedUpdate()
    {
        Setselfvelocity();
        if (controllable)
        {
            cook(PlayerRb2d, this);
            PlayerRb2d.velocity = selfvelocity + VelotoAdd;
        }
        else
        {
            PlayerRb2d.velocity = Givenvelocity;
        }
        //VelotoAdd = Vector2.zero;
    }
    private void LateUpdate()
    {
        if (Input.GetButtonDown("FollowCam"))
        {
            followplace = new Vector3(PlayerRb2d.position.x, PlayerRb2d.position.y, maincam.transform.position.z);
            maincam.transform.position = followplace;
        }
    }

    public void stopwalking()
    {
        GameObject.Destroy(targeticon);
        GetComponent<SkillR2b>().IdoDSWL();
    }
    
    /*
    public Vector2 AddConstentCentrallyVelocity(Vector2 point, float speed, Vector2 orivelo)
    {
        Vector2 aftervelo = AddConstentVelocity((point - PlayerRb2d.position).normalized * speed, orivelo);
        return aftervelo;
    }

    public Vector2 AddPropCentrallyVelocity(Vector2 point, float speed, Vector2 orivelo)
    {
        Vector2 aftervelo = AddConstentVelocity((point - PlayerRb2d.position) * speed, orivelo);
        return aftervelo;
    }

    public Vector2 AddInveCentrallyVelocity(Vector2 point, float speed, Vector2 orivelo)
    {
        Vector2 distance = point - PlayerRb2d.position;
        Vector2 aftervelo = AddConstentVelocity(distance.normalized * speed / distance.magnitude, orivelo);
        return aftervelo;
    }

    public Vector2 AddConstentVelocity(Vector2 Velotoadd, Vector2 orivelo)
    {
        Vector2 aftervelo = orivelo + Velotoadd;
        return aftervelo;
    }
    */

}
