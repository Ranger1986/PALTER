using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Ennemy : Entity
{
    protected GameObject cible;
    protected Collider hitbox;
    [SerializeField] CollectibleScript collectible;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        hitbox = GetComponent<Collider>();
        cible = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (vie<=0 && collectible != null)
        {
            Instantiate(collectible, transform.position, Quaternion.identity);
        }
    }
}
