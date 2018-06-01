using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class RedLineScript : Photon.MonoBehaviour
{
    public Rigidbody2D sender;
    public Rigidbody2D receiver;
    Vector2 centerpoint;
    public LineRenderer MyLine;
    public float speed = 2;
    public float damage;
    public float maxtime = 2;
    float timepsd = 0;
    public bool Idrag = false;
    bool pointalive = false;

    // Use this for initialization
    void Start()
    {
        timepsd = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (timepsd >= maxtime || sender == null)
            gameObject.GetComponent<DestroyScript>().Destroyself();
        if (Idrag && receiver == null)
            gameObject.GetComponent<DestroyScript>().Destroyself();
        drawmyline(sender.position, centerpoint);
    }

    void FixedUpdate()
    {
        timepsd += Time.fixedDeltaTime;
        if (Idrag)
        {
            receiver.GetComponent<HPScript>().GetHurt(damage * Time.fixedDeltaTime);
        }
        if (pointalive)
            centerpoint = receiver.position;
        else
        {
            Vector2 RayV2 = sender.position - centerpoint;
            RaycastHit2D[] Allhit = Physics2D.RaycastAll(centerpoint, RayV2);
            foreach (RaycastHit2D hit in Allhit)
            {
                if (!hit.collider.gameObject.GetPhotonView().isMine || hit.collider.gameObject == sender.gameObject || hit.collider.GetComponent<HPScript>() == null)
                    continue;
                hit.collider.GetComponent<HPScript>().GetHurt(damage * Time.fixedDeltaTime);
                return;
            }
        }
    }

    public void DoMyJob(int idv, int ids, Vector2 place, float sp, float dm, float mt)
    {
        photonView.RPC("RedLineWorking", PhotonTargets.All, idv, ids, place, sp, dm, mt);
    }

    [PunRPC]
    void RedLineWorking(int victimId, int senderId, Vector2 missedplace, float spd, float dmg, float maxT)
    {
        sender = PhotonView.Find(senderId).GetComponent<Rigidbody2D>();
        damage = dmg;
        speed = spd;
        maxtime = maxT;
        if (victimId != 0)
        {
            receiver = PhotonView.Find(victimId).GetComponent<Rigidbody2D>();
            sender.GetComponent<MoveScript>().cook += AddConstentCentrallyVelocity;
            pointalive = true;
            centerpoint = receiver.position;
            if (receiver.gameObject.GetPhotonView().isMine)
            {
                Idrag = true;
                receiver.GetComponent<DoSkill>().ClearDebuff += gameObject.GetComponent<DestroyScript>().Destroyself;
            }
        }
        else
            centerpoint = missedplace;
        enabled = true;
    }

    public void AddConstentCentrallyVelocity(Rigidbody2D victim, MoveScript worker)
    {
        Vector2 distance = receiver.position - victim.position;
        if (distance.sqrMagnitude > 1.1)
            worker.VelotoAdd += distance.normalized * speed;
    }

    void OnDestroy()
    {
        if (sender != null)
            sender.GetComponent<MoveScript>().cook -= AddConstentCentrallyVelocity;
        if (receiver != null)
            receiver.GetComponent<DoSkill>().ClearDebuff -= gameObject.GetComponent<DestroyScript>().Destroyself;
    }

    void drawmyline(Vector2 v21, Vector2 v22)
    {
        MyLine.SetPosition(0, v21);
        MyLine.SetPosition(1, v22);
    }
}
