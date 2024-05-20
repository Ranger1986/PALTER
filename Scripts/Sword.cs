using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] Player wielder;
    AudioClip generalSound = null;
    AudioClip enemyHitSound = null;
    bool isSwordActive = false;

    // Start is called before the first frame update
    void Start()
    {
        generalSound = Resources.Load<AudioClip>("punch_long_whoosh_21");
        enemyHitSound = Resources.Load<AudioClip>("blade_hit_07");
    }

    // Update is called once per frame
    void Update()
    {
        if (isSwordActive)
        {
            PlayRandomSound();
            isSwordActive = false; // Reset the flag after playing the sound
        }
    }

    void OnTriggerEnter(Collider other)
    {
        isSwordActive = false;

        GameObject entity = other.gameObject;
        if (entity.tag == "Ennemy")
        {
            Ennemy ennemy = entity.GetComponent<Ennemy>();
            if (ennemy != null)
            {
                ennemy.Hit(wielder.force, wielder.transform.position);
                PlayEnemyHitSound();
            }
        }
    }

    public void ActivateSword()
    {
        isSwordActive = true;
    }

    public void DesactivateSword()
    {
        isSwordActive = false;
    }

    void PlayRandomSound()
    {
        int random = Random.Range(0, 5);

        switch (random)
        {
            case 0:
                generalSound = Resources.Load<AudioClip>("punch_long_whoosh_21");
                break;

            case 1:
                generalSound = Resources.Load<AudioClip>("punch_long_whoosh_30");
                break;

            case 2:
                generalSound = Resources.Load<AudioClip>("somersalt_01");
                break;

            case 3:
                generalSound = Resources.Load<AudioClip>("somersalt_10");
                break;

            case 4:
                generalSound = Resources.Load<AudioClip>("kick_long_whoosh_19");
                break;

            default:
                generalSound = Resources.Load<AudioClip>("MinecraftDamage");
                break;
        }

        if (generalSound != null)
        {
            AudioManager.instance.PlaySound(generalSound);
        }
        else
        {
            Debug.LogError("Le son n'a pas été trouvé dans le dossier Resources.");
        }
    }
    void PlayEnemyHitSound()
    {
        int random = Random.Range(0, 3);

        switch (random)
        {
            case 0:
                enemyHitSound = Resources.Load<AudioClip>("blade_hit_07");
                break;

            case 1:
                enemyHitSound = Resources.Load<AudioClip>("blade_hit_08");
                break;

            case 2:
                enemyHitSound = Resources.Load<AudioClip>("metal_punch_finisher_07");
                break;

            default:
                enemyHitSound = Resources.Load<AudioClip>("MinecraftDamage");
                break;
        }

        if (enemyHitSound != null)
        {
            AudioManager.instance.PlaySound(enemyHitSound);
        }
        else
        {
            Debug.LogError("Le son de coup sur l'ennemi n'a pas été trouvé dans le dossier Resources.");
        }
    }
}
