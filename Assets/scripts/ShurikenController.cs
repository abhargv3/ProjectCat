﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenController : Photon.MonoBehaviour
{

    //private float speed = 4.0f;
    private int rotSpeed = 600;
    private bool isColliderEnabled = false;
    // public Vector3 forwardVec;
    // Use this for initialization
    void Start()
    {
        if (photonView.isMine)
        {
            StartCoroutine(waitAndDestroy());
            StartCoroutine(enableCollision());
        }
    }

    IEnumerator enableCollision()
    {
        yield return new WaitForSeconds(.04f);
        isColliderEnabled = true;
        //Debug.Log("you may start colliding");
    }

    IEnumerator waitAndDestroy()
    {
        yield return new WaitForSeconds(5);
        PhotonNetwork.Destroy(this.gameObject);

    }

    // Update is called once per frame
    void Update()
    {

        transform.Rotate(0, rotSpeed * Time.deltaTime, 0);
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(this.transform.position);
        }
        else
        {
            this.transform.position = (Vector3)stream.ReceiveNext();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (photonView.isMine && isColliderEnabled)
        {
            //Debug.Log("going in");
            GameObject obj = other.gameObject;
            //Debug.Log(obj.tag);
            //Debug.Log(obj.name);
            //Debug.Log(obj);
            if (obj.tag == "avatar")
            {
                //Debug.Log("shuriken hit player");
                PlayerNetworkController parent = obj.GetComponentInParent<PlayerNetworkController>();
                if (parent != null)
                {
                    parent.dealDamage(5);
                }
                else
                {
                    Debug.Log("ERROR playernetworkcontroller not found");
                }
            }
            else if (obj.tag == "shield")
            {
                //Debug.Log ("shuriken hit shield");
                obj.SetActive(false);
            }
            PhotonNetwork.Destroy(this.gameObject);
            //Debug.Log("shuriken destroyed");
        }
    }
}
