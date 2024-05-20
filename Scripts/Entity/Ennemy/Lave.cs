using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lave : MonoBehaviour
{
    private float TTL;
    // Start is called before the first frame update
    void Start()
    {
        TTL=Time.time+2;
    }

    // Update is called once per frame
    void Update()
    {
        if (TTL<Time.time)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter(Collider other){
        Player player = other.gameObject.GetComponent<Player>();
        if (player)
        {
            player.burn.Apply();
        }
    }
}
