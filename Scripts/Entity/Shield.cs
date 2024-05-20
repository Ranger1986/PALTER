using UnityEngine;
using UnityEngine.SceneManagement;

public class Shield : Entity
{
    [SerializeField] Player wielder;
    [SerializeField] float tempsRegen;
    AudioClip enemyHitShieldSound = null;

    bool inRegen;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        inRegen=false;
        enemyHitShieldSound = Resources.Load<AudioClip>("Shield_block1");

        Scene scene = gameObject.scene;
        GameObject[] listeObjects = scene.GetRootGameObjects();
        foreach (GameObject item in listeObjects)
        {
            Collider itemCollider = item.GetComponent<Collider>();
            if (itemCollider && item.tag != "Ennemy")
            {
                Physics.IgnoreCollision(itemCollider, GetComponent<Collider>());
            }
        }
    }

    protected override void Update()
    {
        //base.Update();
        if (vie <= 0)
        {
            gameObject.SetActive(false);
        }
        if (vie < vieMax && !inRegen)
        {
            Invoke("Regen", tempsRegen);
            inRegen=!inRegen;
        }
        UIManagerScript.Instance.UpdateShield();
    }

    void Regen()
    {
        if (this != null) 
        {
            vie++;
            GlobalVariables.shieldHP = vie;
            if (vie < vieMax)
            {
                Invoke("Regen", tempsRegen);
            }
            else
            {
                inRegen = false;
            }
        }

    }
    public override void Hit(int damage, Vector3 position) 
    {
        vie -= damage;
        GlobalVariables.shieldHP = vie;
        AudioManager.instance.PlaySound(enemyHitShieldSound);
    }
}