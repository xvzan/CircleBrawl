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
    public bool missed;
    public bool Idrag = true;

	// Use this for initialization
	void Start () {
        timepsd = 0;
        Idrag = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (!missed)
            Paint();
        if (timepsd >= maxtime || receiver == null || sender == null)
            gameObject.GetComponent<DestroyScript>().Destroyself();
        if (Idrag && receiver != null)
            IHit();
    }

    void FixedUpdate()
    {
        if (!missed)
        {
            timepsd += Time.fixedDeltaTime;
            receiver.GetComponent<HPScript>().GetHurt(damage * Time.fixedDeltaTime);
        }
    }

    public void AddConstentCentrallyVelocity(Rigidbody2D victim, MoveScript worker)
    {
        worker.VelotoAdd += (sender.position - victim.position).normalized * speed;
    }

    void OnDestroy()
    {
        if (receiver != null)
            receiver.GetComponent<MoveScript>().cook -= AddConstentCentrallyVelocity;
    }

    public void IHit()
    {
        receiver.GetComponent<MoveScript>().cook += AddConstentCentrallyVelocity;
        Idrag = false;
    }

    public void IMissed(Vector2 v2)
    {
        missed = true;
        photonView.RPC("drawmyline", PhotonTargets.All, sender.position, v2);
        StartCoroutine(EraseLine());
    }

    void Paint()
    {
        photonView.RPC("catchyou", PhotonTargets.All, receiver.position);
        drawmyline(sender.position, receiver.position);
    }
    
    [PunRPC]
    void catchyou(Vector2 catchplace)
    {
        if (receiver == null)
            receiver = Physics2D.OverlapPoint(catchplace).GetComponent<Rigidbody2D>();
    }

    [PunRPC]
    void drawmyline(Vector2 v21,Vector2 v22)
    {
        MyLine.SetPosition(0, v21);
        MyLine.SetPosition(1, v22);
    }

    IEnumerator EraseLine()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<DestroyScript>().Destroyself();
    }
    
    /*void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(speed);
        }
        else
        {
            speed = (float)stream.ReceiveNext();
        }
    }*/

    public void EnableSelf()
    {
        photonView.RPC("SelfEnableBlue", PhotonTargets.All);
    }
    [PunRPC]
    void SelfEnableBlue()
    {
        enabled = true;
    }
}
