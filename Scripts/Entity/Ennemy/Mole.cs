using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : Ennemy
{
    [SerializeField] float speed;

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if(cible&& !recovering)
        {
            Vector3 vitesse = (cible.transform.position - transform.position).normalized*speed;
            body.velocity = new Vector3(vitesse.x,body.velocity.y,vitesse.z);
        }
        else if (recovering && recoverBeginTime+recoverDuration < Time.time){
            recovering = false;
        }
    }
    void OnCollisionEnter(Collision collision){
        Player player = collision.contacts[0].otherCollider.gameObject.GetComponent<Player>();
        if (player)
        {
            player.Hit(1, collision.transform.position);
            return;
        }
        Shield shield = collision.contacts[0].otherCollider.gameObject.GetComponent<Shield>();
        if (shield)
        {
            shield.vie--;
            return;
        }
    }
}
