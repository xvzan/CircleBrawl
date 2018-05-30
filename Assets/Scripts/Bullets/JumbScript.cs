using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class JumbScript : Photon.MonoBehaviour
{
    public GameObject reGO;
    private float pasttime;
    public float maxtime;
    public float pushtime = 1;
    public float bombpower;
    public float bombdamage;
    public GameObject sender;
    public bool selfbreak = true;
    bool bonus = false;
    //public bool selfprotect = true;

    // Use this for initialization
    void Start()
    {
        //selfprotect = true;
        pasttime = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        pasttime += Time.fixedDeltaTime;
        if (pasttime >= maxtime)
        {
            gameObject.GetComponent<DestroyScript>().Destroyself();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!photonView.isMine)
            return;
        if (gameObject.GetComponent<DestroyScript>().selfprotect && collision.gameObject == sender)
            return;
        /*if (selfprotect && collision.gameObject.GetComponent<ShieldScript>().sender == sender)
            return;*/
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        HPScript hp = collision.gameObject.GetComponent<HPScript>();
        if (hp != null && rb != null)
        {
            Vector2 explforce;
            Rigidbody2D selfrb = gameObject.GetComponent<Rigidbody2D>();
            explforce = rb.position - selfrb.position;
            collision.gameObject.GetComponent<RBScript>().GetPushed(explforce.normalized * bombpower, pushtime);
            //hp.GetKicked(explforce.normalized * bombpower);
            hp.GetHurt(bombdamage);
            Jumb();
            bonus = true;
        }
        if (selfbreak)
            gameObject.GetComponent<DestroyScript>().Destroyself();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        gameObject.GetComponent<DestroyScript>().selfprotect = false;
    }

    public void Jumb()
    {
        GameObject myrego = PhotonNetwork.Instantiate(reGO.name, transform.position, Quaternion.identity, 0);
        myrego.GetComponent<ReturnScript>().Target = sender.GetComponent<Rigidbody2D>();
        sender.GetComponent<SkillT3b>().damageplus += 0.3f;
    }

    private void OnDestroy()
    {
        if (!bonus)
            sender.GetComponent<SkillT3b>().damageplus = 0;
    }
}
