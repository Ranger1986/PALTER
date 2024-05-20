using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : Entity
{
    [SerializeField] Shield shield;
    [SerializeField] Sword sword;
    public Slow slow;
    public Burn burn;

    private bool isSwinging;
    private float swordAngle;
    public float speed;
    [SerializeField] public float maxSpeed;
    [SerializeField] public int force;
    [SerializeField] int vitesseSwing;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        vie = GlobalVariables.playerHP;

        gameObject.tag="Player";


        shield.gameObject.SetActive(false);
        sword.gameObject.SetActive(false);

        isSwinging=false;
        swordAngle=0;
        sword.transform.RotateAround(transform.position, Vector3.up, -90);

        speed = maxSpeed;

        slow = new Slow(this);
        burn = new Burn(this);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        //input
        if (!recovering)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            body.velocity = new Vector3( horizontalInput* speed, body.velocity.y,  verticalInput* speed);
            if ((horizontalInput != 0 || verticalInput!= 0) && body.velocity!=Vector3.zero)
            {
                transform.forward = body.velocity;
            }
            if (Input.GetKeyDown("space"))
            {
                RaycastHit slash;
                Vector3 pastPosition = transform.position;
                transform.Translate(0, 0, 1);
                if (Physics.Linecast(pastPosition, transform.position, out slash) && !slash.collider.CompareTag("Ennemy"))
                {
                    transform.Translate(0, 0, -1);
                }
            }
        }
        else if (recovering && recoverBeginTime+recoverDuration < Time.time){
            recovering = false;
        }
        if (Input.GetKeyDown("e"))
        {
            if (shield.vie>0)
            {
                shield.gameObject.SetActive(true);
            }
        }
        if (Input.GetKeyUp("e"))
        {
            shield.gameObject.SetActive(false);
        }
        
        if (Input.GetKeyDown("q") && !isSwinging)
        {
            sword.gameObject.SetActive(true);
            isSwinging=true;
            sword.ActivateSword();
        }
        //swinging sword
        if (isSwinging)
        {
            swordAngle+=vitesseSwing*Time.deltaTime;
            sword.transform.RotateAround(transform.position, Vector3.up, vitesseSwing*Time.deltaTime);
            if (swordAngle>=180)
            {
                sword.gameObject.SetActive(false);
                isSwinging=false;
                sword.transform.RotateAround(transform.position, Vector3.up, -swordAngle);
                swordAngle=0;
            }
        }
        slow.update();
    }
    public override void Hit(int damage, Vector3 position)
    {
     
        base.Hit(damage,position);
        GlobalVariables.playerHP = vie;
        UIManagerScript.Instance.UpdateHP();
        Debug.Log($"The player got damaged: {GlobalVariables.playerHP}");
    }
    void OnTriggerEnter(Collider other)
    {
        CollectibleScript collectible = other.GetComponent<CollectibleScript>();
        if (collectible != null)
        {
            collectible.Pickup();
            HandleCollectible(collectible.type);
        }
    }

    private void HandleCollectible(CollectibleScript.CollectibleType type)
    {
        switch (type)
        {
            case CollectibleScript.CollectibleType.Gold:
                GlobalVariables.playerScore += 100;
                UIManagerScript.Instance.UpdateScore();
                break;
            case CollectibleScript.CollectibleType.Ruby:
                GlobalVariables.playerScore += 500;
                UIManagerScript.Instance.UpdateScore();
                break;
            case CollectibleScript.CollectibleType.HealthPotion:
                GlobalVariables.playerHP = Mathf.Min(GlobalVariables.playerHP + 3, 10);
                UIManagerScript.Instance.UpdateHP();
                Debug.Log($"Potion de sant� ramass�e: {GlobalVariables.playerHP}");
                break;

        }
    }
}
