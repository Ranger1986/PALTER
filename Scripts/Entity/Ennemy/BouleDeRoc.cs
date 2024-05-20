using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouleDeRoc : MonoBehaviour
{
    [SerializeField] protected int damage;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
    }

    protected virtual void OnTriggerEnter(Collider other){
        GameObject obj = other.gameObject;
        Entity entity = obj.GetComponent<Entity>();
        if (entity)
        {
            entity.Hit(1, other.transform.position);
        }
        Destroy(gameObject);
    }
}
