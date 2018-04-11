using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class BlueLineScript : Photon.MonoBehaviour
{
    public Rigidbody2D sender;
    public GameObject receiver;
    public LineRenderer MyLine;
    public float speed;
    public float damage;
    public float maxtime = 2;
    float timepsd = 0;
    public bool missed = false;

	// Use this for initialization
	void Start () {
        timepsd = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (!missed)
            Paint();
    }

    void FixedUpdate()
    {
        if (missed)
            return;
        timepsd += Time.fixedDeltaTime;
        if (timepsd >= maxtime || receiver == null || sender == null)
            Destroyself();
        receiver.GetComponent<HPScript>().GetHurt(damage * Time.fixedDeltaTime);
    }

    public void AddConstentCentrallyVelocity(Rigidbody2D victim, MoveScript worker)
    {
        //Vector2 vector = sender.position - victim.position;
        worker.VelotoAdd += (sender.position - victim.position).normalized * speed;
    }

    void OnDestroy()
    {
        //photonView.RPC("drawmyline", PhotonTargets.All, Vector3.zero, Vector3.zero);
        if (receiver != null)
            photonView.RPC("YouCanGo", PhotonTargets.All);
    }

    public void IHit(GameObject target)
    {
        missed = false;
        photonView.RPC("HitYou", PhotonTargets.All);
    }

    [PunRPC]
    public void HitYou()
    {
        receiver.GetComponent<MoveScript>().cook += AddConstentCentrallyVelocity;
    }

    [PunRPC]
    public void YouCanGo()
    {
        receiver.GetComponent<MoveScript>().cook -= AddConstentCentrallyVelocity;
    }

    public void IMissed(Vector2 v2)
    {
        missed = true;
        //MyLine.SetPosition(0, gameObject.transform.position);
        //MyLine.SetPosition(1, v2);
        photonView.RPC("drawmyline", PhotonTargets.All, sender.position, v2);
        StartCoroutine(EraseLine());
    }

    void Paint()
    {
        //MyLine.SetPosition(0, sender.position);
        //MyLine.SetPosition(1, receiver.GetComponent<Rigidbody2D>().position);
        photonView.RPC("drawmyline", PhotonTargets.All, sender.position, receiver.GetComponent<Rigidbody2D>().position);
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
        Destroyself();
    }

    void Destroyself()
    {
        photonView.RPC("SDestroy", PhotonTargets.All);
    }

    [PunRPC]
    void SDestroy()
    {
        GameObject.Destroy(this.gameObject);
    }
}
