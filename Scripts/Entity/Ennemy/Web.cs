using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Web : MonoBehaviour
{
    private float TTL;
    // Start is called before the first frame update
    void Start()
    {
        TTL=Time.time+2;
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other){
        GameObject entity = other.gameObject;
        if (entity.tag == "Player")
        {
            Player player = entity.GetComponent<Player>();
            player.slow.Apply();
        } 
        Destroy(gameObject);
    }
}
