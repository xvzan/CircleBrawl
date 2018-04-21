using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class SkillC3 : Photon.MonoBehaviour
{
    public GameObject ShadowCircle;
    GameObject MyShadow;
    private float currentcooldown;
    public float cooldowntime = 3;
    public float maxshadowtime = 2.5f;
    //float shadowtime;
    public bool skillavaliable;
    bool HaveShadow = false;
    bool canctrl;
    Vector2 flyspeed;
    float remaintime;

    // Use this for initialization
    void Start ()
    {
        currentcooldown = cooldowntime;
        //ShadowCircle.GetComponent<CircleCollider2D>().enabled = false;
        //ShadowCircle.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 127);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("FireC"))
        {
            if (HaveShadow)
            {
                BackToShadow();
            }
            else if(skillavaliable)
            {
                DoSkill.singing = 0;
                Skill();
            }
        }
    }

    private void FixedUpdate()
    {
        if (!skillavaliable)
        {
            currentcooldown += Time.fixedDeltaTime;
            if (HaveShadow && currentcooldown >= maxshadowtime)
                BackToShadow();
            if (currentcooldown >= cooldowntime)
            {
                skillavaliable = true;
            }
        }
    }

    void Skill()
    {
        //shadowtime = 0;
        gameObject.GetComponent<MoveScript>().stopwalking(); //停止走动
        MyShadow = Instantiate(ShadowCircle, transform.position, Quaternion.identity);
        canctrl = gameObject.GetComponent<MoveScript>().controllable;
        if (!canctrl)
        {
            flyspeed = gameObject.GetComponent<MoveScript>().Givenvelocity;
            remaintime = gameObject.GetComponent<RBScript>().GetRemainTime();
        }
        currentcooldown = 0;
        skillavaliable = false;
        HaveShadow = true;
    }

    void BackToShadow()
    {
        gameObject.GetComponent<MoveScript>().stopwalking(); //停止走动
        transform.position = MyShadow.transform.position;
        gameObject.GetComponent<MoveScript>().controllable = canctrl;
        if (!canctrl)
        {
            gameObject.GetComponent<RBScript>().GetPushed(flyspeed, remaintime);
        }
        HaveShadow = false;
        GameObject.Destroy(MyShadow);
        gameObject.GetComponent<DoSkill>().DoClearJob();
    }
}
