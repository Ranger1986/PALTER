using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleScript : MonoBehaviour
{
    public enum CollectibleType { Gold, Ruby, HealthPotion} 
    public CollectibleType type;

    public void Pickup()
    {
        Destroy(gameObject);
        AudioClip goldAndRubySound = Resources.Load<AudioClip>("Amethyst_break1");

        int random = Random.Range(0, 4);

        switch (random)
        {
            case 0:
                goldAndRubySound = Resources.Load<AudioClip>("Amethyst_break1");
                break;

            case 1:
                goldAndRubySound = Resources.Load<AudioClip>("Amethyst_break2");
                break;

            case 2:
                goldAndRubySound = Resources.Load<AudioClip>("Amethyst_break3");
                break;

            case 3:
                goldAndRubySound = Resources.Load<AudioClip>("Amethyst_break4");
                break;

            default:
                goldAndRubySound = Resources.Load<AudioClip>("Amethyst_break1");
                break;
        }

        if (goldAndRubySound != null)
        {
            AudioManager.instance.PlaySound(goldAndRubySound);
        }
        else
        {
            Debug.LogError("Le son n'a pas été trouvé dans le dossier Resources.");
        }
    }
}
