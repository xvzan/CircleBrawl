using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class BlueLineScript : Photon.MonoBehaviour
{
    public Rigidbody2D sender;
    public Rigidbody2D receiver;
    public LineRenderer MyLine;
    public float speed = 2;
    public float damage;
    public float maxtime = 2;
    float timepsd = 0;
    //public bool missed;
    public bool Idrag = false;

	// Use this for initialization
	void Start () {
        timepsd = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (timepsd >= maxtime || sender == null)
            gameObject.GetComponent<DestroyScript>().Destroyself();
        if (Idrag && receiver == null)
            gameObject.GetComponent<DestroyScript>().Destroyself();
        drawmyline(sender.position, receiver.position);
    }

    void FixedUpdate()
    {
        if (Idrag)
        {
            timepsd += Time.fixedDeltaTime;
            receiver.GetComponent<HPScript>().GetHurt(damage * Time.fixedDeltaTime);
        }
    }

    public void DoMyJob(int idv,int ids,Vector2 place,float sp,float dm,float mt)
    {
        photonView.RPC("BlueLineWorking", PhotonTargets.All, idv, ids, place, sp, dm, mt);
    }

    [PunRPC]
    void BlueLineWorking(int victimId, int senderId, Vector2 missedplace, float spd, float dmg, float maxT)
    {
        if (victimId != 0)
        {
            receiver = PhotonView.Find(victimId).GetComponent<Rigidbody2D>();
            sender = PhotonView.Find(senderId).GetComponent<Rigidbody2D>();
            damage = dmg;
            speed = spd;
            receiver.GetComponent<MoveScript>().cook += AddConstentCentrallyVelocity;
            maxtime = maxT;
            if (receiver.gameObject.GetPhotonView().isMine)
                Idrag = true;
            receiver.GetComponent<DoSkill>().ClearDebuff += gameObject.GetComponent<DestroyScript>().Destroyself;
        }
        else
        {
            sender = PhotonView.Find(senderId).GetComponent<Rigidbody2D>();
            drawmyline(sender.position, missedplace);
            StartCoroutine(EraseLine());
        }
        enabled = true;
    }

    public void AddConstentCentrallyVelocity(Rigidbody2D victim, MoveScript worker)
    {
        worker.VelotoAdd += (sender.position - victim.position).normalized * speed;
    }

    void OnDestroy()
    {
        if (receiver != null)
        {
            receiver.GetComponent<MoveScript>().cook -= AddConstentCentrallyVelocity;
            receiver.GetComponent<DoSkill>().ClearDebuff -= gameObject.GetComponent<DestroyScript>().Destroyself;
        }
    }
            
    void drawmyline(Vector2 v21,Vector2 v22)
    {
        MyLine.SetPosition(0, v21);
        MyLine.SetPosition(1, v22);
    }

    IEnumerator EraseLine()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<DestroyScript>().SDestroy();
    }
    
}
