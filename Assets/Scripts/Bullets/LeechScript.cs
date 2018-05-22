using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class LeechScript : Photon.MonoBehaviour
{
    private float pasttime;
    public float maxtime;
    public float leechdamage;
    //private Rigidbody2D selfrb;
    public GameObject sender;
    //private bool selfprotect;
    public float turntime;
    public float speed;
    bool unturned = true;

    // Use this for initialization
    void Start()
    {
        //selfrb = GetComponent<Rigidbody2D>();
        //selfprotect = true;
        pasttime = 0;
        maxtime = 2;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        pasttime += Time.fixedDeltaTime;
        if (unturned && pasttime >= turntime)
        {
            unturned = false;
            Vector2 nextvec2 = FindClosestVector2n() * speed;
            SelfTurn(nextvec2);
        }
        if (pasttime >= maxtime)
        {
            gameObject.GetComponent<DestroyScript>().Destroyself();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (!PhotonNetwork.isMasterClient)
            //return;
        if (collision.gameObject == sender && gameObject.GetComponent<DestroyScript>().selfprotect)
            return;
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        HPScript hp = collision.gameObject.GetComponent<HPScript>();
        if (hp != null && rb != null)
        {
            hp.GetHurt(leechdamage);
            sender.GetComponent<HPScript>().GetHurt(-leechdamage);
        }
        gameObject.GetComponent<DestroyScript>().Destroyself();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //if (!PhotonNetwork.isMasterClient)
        //return;
        gameObject.GetComponent<DestroyScript>().selfprotect = false;
    }

    GameObject FindClosestEnemy()
    {
        GameObject closest = null;  // GameObject.FindWithTag("Player");
        GameObject[] Allthem = GameObject.FindGameObjectsWithTag("Player");
        float sqrdis = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject Him in Allthem)
        {
            //if (Him == sender) continue;//跳过施法者
            Vector2 diff = (Him.transform.position - position); //距离向量
            float curDistance = diff.sqrMagnitude; //距离平方
            if (curDistance < sqrdis)
            {
                closest = Him; //更新最近距离敌人
                sqrdis = curDistance; //更新最近距离
            }
        }
        return closest;
    }

    Vector2 FindClosestVector2n()
    {
        GameObject[] Allthem = GameObject.FindGameObjectsWithTag("Player");
        float sqrdis = Mathf.Infinity;
        Vector3 position = transform.position;
        Vector2 vector = gameObject.GetComponent<Rigidbody2D>().velocity;
        foreach (GameObject Him in Allthem)
        {
            if (Him == sender) continue;//跳过施法者
            Vector2 diff = (Him.transform.position - position);//向量距离
            float curDistance = diff.sqrMagnitude; //向量距离平方
            if (curDistance < sqrdis)
            {
                sqrdis = curDistance;//更新最近距离
                vector = diff;//更新向量
            }
        }
        return vector.normalized;
    }
    
    void SelfTurn(Vector2 vector)
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = vector;
    }
}
