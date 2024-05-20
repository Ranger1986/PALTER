using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrower : Ennemy
{
    [SerializeField] float speed;
    [SerializeField] float throwSpeed;
    [SerializeField] GameObject throwable;
    [SerializeField] float fireRate;
    [SerializeField] float fireRange;
    [SerializeField] float timeToFire;
    [SerializeField] float flashInterval;
    [SerializeField] Material flashMat;
    private float nextFire = 0.0f;

    MeshRenderer meshRenderer;
    protected override void Start()
    {
        base.Start();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (cible && !recovering)
        {
            Vector3 supposedForward = cible.transform.position - transform.position;
            transform.forward = new Vector3(supposedForward.x, 0, supposedForward.z).normalized;
            if (Time.time > nextFire && Vector3.Distance(cible.transform.position,transform.position)<fireRange)
            {
                //StartCoroutine(ChangeMatAndFire());
                Fire();
                nextFire = Time.time + fireRate;
            }
            else if (Vector3.Distance(cible.transform.position,transform.position)>fireRange)
            {
                Vector3 vitesse = (cible.transform.position - transform.position).normalized*speed;
                body.velocity = new Vector3(vitesse.x,body.velocity.y,vitesse.z);
            }
            else
            {
                body.velocity = body.velocity/2;
            }
        }
        else if (recovering && recoverBeginTime+recoverDuration < Time.time){
            recovering = false;
        }
    }
/*
    protected IEnumerator ChangeMatAndFire()
    {
        float currentTime = Time.time+timeToFire;
        while (Time.time <currentTime)
        {
            Material current = meshRenderer.sharedMaterial;
            meshRenderer.material = flashMat; // Change the material to flashMat

            // Wait for a short duration to allow the player to see the change
            yield return new WaitForSeconds(flashInterval);

            // Change back to the original material
            meshRenderer.material = current;
            yield return new WaitForSeconds(flashInterval);
        }
        // Fire after the material has been changed
        Fire();
    }
*/
    void Fire()
    {
        GameObject newThrowable = Instantiate(throwable);
        newThrowable.transform.position = transform.position + (throwable.transform.position.y * Vector3.up) + transform.forward;
        newThrowable.GetComponent<Rigidbody>().velocity = (cible.transform.position - newThrowable.transform.position).normalized * throwSpeed;
    }
}
