using UnityEngine;

public class BouleDeLave : BouleDeRoc
{
    [SerializeField] protected GameObject lave;
    // Start is called before the first frame update
    protected override void Start()
    {
    }

    // Update is called once per frame
    protected override void Update()
    {
        GameObject lava=Instantiate(lave);
        lava.transform.position=new Vector3(transform.position.x,0.05f,transform.position.z);
    }
    protected override void OnTriggerEnter(Collider other){
        base.OnTriggerEnter(other);
    }
}
