using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class SkillC4 : Photon.MonoBehaviour
{
    public CooldownImage MyImageScript;
    public GameObject Faker;
    private float currentcooldown;
    public float cooldowntime = 5;
    public bool skillavaliable;
    public float maxfaketime = 3;
    float currentfaketime = 0;
    public Rigidbody2D selfRB;
    bool faking = false;

    // Use this for initialization
    void Start()
    {
        currentcooldown = cooldowntime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("FireC") && skillavaliable)
        {
            DoSkill.singing = 0;
            Skill();
        }
        if (faking && Input.GetMouseButtonDown(1))
        {
            Vector2 rightclickplace = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            DoFake(rightclickplace);
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
        if (faking)
        {
            currentfaketime += Time.fixedDeltaTime;
            if (currentfaketime >= maxfaketime)
            {
                faking = false;
            }
        }
    }

    void DoFake(Vector2 Worldv2)
    {
        faking = false;
        Vector2 Realv2 = (Worldv2 - selfRB.position).normalized * 2;
        Vector2 center = selfRB.position;
        gameObject.GetComponent<DoSkill>().DoClearJob();
        selfRB.position += Realv2;
        for (int i = 0; i < 2; i++)
        {
            Realv2 = Quaternion.AngleAxis(120, Vector3.forward) * Realv2;
            GameObject nm = PhotonNetwork.Instantiate(Faker.name, center + Realv2, Quaternion.identity, 0);
            nm.GetComponent<FakeCircleScript>().Beauty = selfRB;
            nm.GetComponent<FakeCircleScript>().maxtime = maxfaketime-currentfaketime;
        }
        GetComponent<MoveScript>().stopwalking();
    }

    void Skill()
    {
        GetComponent<DoSkill>().BeforeSkill();
        currentcooldown = 0;
        skillavaliable = false;
        currentfaketime = 0;
        faking = true;
    }
}
