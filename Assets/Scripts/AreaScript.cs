using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AreaScript : MonoBehaviour {

    // Use this for initialization
    float waittime =5f;
    float diameter;
    float speed = 1f;

    // Use this for initialization
    void Start()
    {
        diameter = transform.lossyScale.x;
        StartCoroutine(Changeradius());
    }
    IEnumerator Changeradius()
    {
        while (diameter > 1.5 * speed)
        {
            diameter -= speed;
            Vector3 nextscale = new Vector3(diameter, diameter, 1);
            yield return new WaitForSeconds(waittime);
            transform.localScale = nextscale;
        }
    }
}
