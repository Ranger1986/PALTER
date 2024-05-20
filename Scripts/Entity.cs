using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class Entity : MonoBehaviour
{
    [SerializeField] public int vieMax;
    public int vie;
    protected Rigidbody body;
    protected bool recovering;

    [SerializeField] protected float recoverDuration;
    protected float recoverBeginTime;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        vie=vieMax;
        recoverBeginTime=-99f;
        body=GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (vie<=0)
        {
            Destroy(gameObject);
            if (gameObject.CompareTag("Player") ) {
                SceneManager.LoadScene("GameOver");
            }
        }
    }
    public virtual void Hit(int damage, Vector3 position)
    {
        vie -= damage;
        
        if (gameObject.CompareTag("Player"))
        {
            //Active le sound effect quand le joueur prend des d�gats
            AudioClip Damagesound = Resources.Load<AudioClip>("MinecraftDamage");
            if (Damagesound != null)
            {
                AudioManager.instance.PlaySound(Damagesound);
            }
            else
            {
                Debug.LogError("Le son n'a pas �t� trouv� dans le dossier Resources.");
            }
        }

        if (gameObject.CompareTag("Ennemy"))
        {
            Araignee araignee = GetComponent<Araignee>();
            Mole mole = GetComponent<Mole>();
            Thrower thrower = GetComponent<Thrower>();

            AudioClip Damagesound = null;

            //Active le sound effect quand l'araign�e prend des d�gats
            if (araignee != null)
            {
                int random = Random.Range(0, 4);

                switch (random)
                {
                    case 0:
                        Damagesound = Resources.Load<AudioClip>("Spider_idle1");
                        break;

                    case 1:
                        Damagesound = Resources.Load<AudioClip>("Spider_idle2");
                        break;

                    case 2:
                        Damagesound = Resources.Load<AudioClip>("Spider_idle3");
                        break;

                    case 3:
                        Damagesound = Resources.Load<AudioClip>("Spider_idle4");
                        break;

                    default:
                        Damagesound = Resources.Load<AudioClip>("MinecraftDamage");
                        break;
                }
            } 
            else if (mole != null)
            {
                int random = Random.Range(0, 3);

                switch (random)
                {
                    case 0:
                        Damagesound = Resources.Load<AudioClip>("Phantom_hurt1");
                        break;

                    case 1:
                        Damagesound = Resources.Load<AudioClip>("Phantom_hurt2");
                        break;

                    case 2:
                        Damagesound = Resources.Load<AudioClip>("Phantom_hurt3");
                        break;

                    default:
                        Damagesound = Resources.Load<AudioClip>("MinecraftDamage");
                        break;
                }
            }
            else if (thrower != null)
            {
                int random = Random.Range(0, 4);

                switch (random)
                {
                    case 0:
                        Damagesound = Resources.Load<AudioClip>("Ravager_hurt1");
                        break;

                    case 1:
                        Damagesound = Resources.Load<AudioClip>("Ravager_hurt2");
                        break;

                    case 2:
                        Damagesound = Resources.Load<AudioClip>("Ravager_hurt3");
                        break;

                    case 3:
                        Damagesound = Resources.Load<AudioClip>("Ravager_hurt4");
                        break;

                    default:
                        Damagesound = Resources.Load<AudioClip>("MinecraftDamage");
                        break;
                }
            }

            if (Damagesound != null)
            {
                AudioManager.instance.PlaySound(Damagesound);
            }
            else
            {
                Debug.LogError("Le son n'a pas �t� trouv� dans le dossier Resources.");
            }
        }


        if (body)
        {
            Vector3 directionRecul = transform.position-position;
            directionRecul.Normalize();
            directionRecul.y=2f;
            body.velocity= directionRecul;
            recovering=true;
            recoverBeginTime=Time.time;
        }
    }
}
