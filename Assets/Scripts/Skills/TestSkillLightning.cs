using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class TestSkillLightning : Photon.MonoBehaviour
{
    public CooldownImage MyImageScript;
    public float maxdistance = 10;
    private float currentcooldown;
    public float cooldowntime = 3;
    public bool skillavaliable;
    public float SelfR = 0.51f;
    public LineRenderer line;

    // Use this for initialization
    void Start()
    {
        currentcooldown = cooldowntime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("FireD") && skillavaliable)
        {
            DoSkill.singing = 0;
            gameObject.GetComponent<DoSkill>().Fire = Skill;
        }
    }

    private void FixedUpdate()
    {
        if (skillavaliable)
            return;
        if (currentcooldown >= cooldowntime)
        {
            skillavaliable = true;
        }
        else
        {
            currentcooldown += Time.fixedDeltaTime;
            MyImageScript.IconFillAmount = currentcooldown / cooldowntime;
        }
    }

    public void Skill(Vector2 actionplace)
    {
        Vector2 realplace;
        //DoSkill.singing = 0; //停止吟唱中技能
        gameObject.GetComponent<MoveScript>().stopwalking(); //停止走动
        gameObject.GetComponent<StealthScript>().StealthEnd();
        currentcooldown = 0;
        skillavaliable = false;
        gameObject.GetComponent<DoSkill>().Fire = null;
        Rigidbody2D selfrb2d = gameObject.GetComponent<Rigidbody2D>();
        Vector2 skilldirection = actionplace - selfrb2d.position;
        RaycastHit2D hit2D = Physics2D.Raycast(selfrb2d.position + skilldirection.normalized * SelfR, skilldirection - skilldirection.normalized * SelfR);
        if (hit2D.collider != null && hit2D.distance <= maxdistance - SelfR)
        {
            realplace = hit2D.point;
            Drawline(realplace);
            if (hit2D.collider.GetComponent<DestroyScript>() != null && hit2D.collider.GetComponent<DestroyScript>().breakable == true)
                hit2D.collider.GetComponent<DestroyScript>().Destroyself();
            else if (hit2D.collider.GetComponent<RollScript>() != null)
            {
                if (!hit2D.collider.GetComponent<PhotonView>().isMine)
                    hit2D.collider.GetComponent<DestroyScript>().Destroyself();
                return;
            }
            else if (hit2D.collider.GetComponent<RBScript>() != null)
            {
                Vector2 kickdirection = hit2D.collider.GetComponent<Rigidbody2D>().position - realplace;
                hit2D.collider.GetComponent<SkillE2b>().lighthit();
                hit2D.collider.GetComponent<RBScript>().GetPushed(kickdirection * 10, 2);
                hit2D.collider.GetComponent<HPScript>().GetHurt(10);
            }
        }
        else
        {
            realplace = selfrb2d.position + maxdistance * skilldirection.normalized;
            Drawline(realplace);
        }
    }

    void Drawline(Vector2 destn)
    {
        photonView.RPC("LineDraw", PhotonTargets.All, destn);
    }

    [PunRPC]
    void LineDraw(Vector2 destn)
    {
        line.SetPosition(0, GetComponent<Rigidbody2D>().position);
        line.SetPosition(1, destn);
        StartCoroutine(EraseLine());
    }

    IEnumerator EraseLine()
    {
        yield return new WaitForSeconds(0.1f);
        line.SetPosition(0, Vector3.zero);
        line.SetPosition(1, Vector3.zero);
    }
}
