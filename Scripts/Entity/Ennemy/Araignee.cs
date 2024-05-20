using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Araignee : Ennemy
{
    [SerializeField] float speed;
    [SerializeField] GameObject web;
    [SerializeField] float throwSpeed;
    [SerializeField] float fireRate;
    private float nextFire = 0.0f;

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if(cible&& !recovering)
        {
            Vector3 supposedForward = cible.transform.position - transform.position;
            transform.forward=new Vector3(supposedForward.x, 0, supposedForward.z).normalized;
            Vector3 vitesse = supposedForward.normalized*speed;
            body.velocity = new Vector3(vitesse.x,body.velocity.y,vitesse.z);
            if (nextFire<Time.time && Vector3.Distance(cible.transform.position, transform.position)<4)
            {
                GameObject newWeb = Instantiate(web);
                newWeb.transform.forward=-transform.forward;
                newWeb.transform.position=transform.position+(newWeb.transform.position.y*new Vector3(0,1,0))+transform.forward;
                newWeb.GetComponent<Rigidbody>().velocity = (cible.transform.position - newWeb.transform.position).normalized*throwSpeed;
                nextFire = Time.time + fireRate;
            }
        }
        else if (recovering && recoverBeginTime+recoverDuration < Time.time){
            Debug.Log(body.velocity.y);
            recovering = false;
        }
        // if(cible)
        // {
        //     Vector3 supposedForward = cible.transform.position - transform.position;
        //     transform.forward=new Vector3(supposedForward.x, 0, supposedForward.z).normalized;
        //     body.velocity = (cible.transform.position - transform.position).normalized*speed;
        //     if (nextFire<Time.time && Vector3.Distance(cible.transform.position, transform.position)<4)
        //     {
        //         GameObject newWeb = Instantiate(web);
        //         newWeb.transform.forward=-transform.forward;
        //         newWeb.transform.position=transform.position+(newWeb.transform.position.y*new Vector3(0,1,0))+transform.forward;
        //         newWeb.GetComponent<Rigidbody>().velocity = (cible.transform.position - newWeb.transform.position).normalized*throwSpeed;
        //         nextFire = Time.time + fireRate;
        //     }
        // }
        
    }
    void OnCollisionEnter(Collision other){
        Player player = other.gameObject.GetComponent<Player>();
        if (player)
        {
            player.Hit(1, other.transform.position);
        }
    }
}
