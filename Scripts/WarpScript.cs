using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;


public class WarpScript : MonoBehaviour
{
    bool AreEnemiesAlive()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Ennemy");
        foreach (GameObject enemy in enemies)
        {
            if (enemy.activeSelf)
            {
                // Il y a encore un ennemi actif dans la scène
                return true;
            }
        }
        // Aucun ennemi actif trouvé dans la scène
        return false;
    }

    void OnTriggerEnter(Collider other)
    {
        //Vérifie que c'est le joueur qui touche la porte
        if (other.CompareTag("Player"))
        {
            if (!AreEnemiesAlive())
            {
                SceneManager.LoadScene("Scenes/Lambda");

                if (GlobalVariables.indexScene == 0)
                {
                    GlobalVariables.indexScene = 1;
                }
                else
                {
                    GlobalVariables.indexScene++;
                    Debug.Log(GlobalVariables.indexScene);
                }
            }
            else
            {
                // S'il y a des ennemis en vie, jouez un son
                AudioClip sonATrouver = Resources.Load<AudioClip>("DoorIfEnemiesAlive"); // Assurez-vous que le son est dans le dossier Resources
                if (sonATrouver != null)
                {
                    AudioManager.instance.PlaySound(sonATrouver);
                }
                else
                {
                    Debug.LogError("Le son n'a pas été trouvé dans le dossier Resources.");
                }
            }
        }        
    }
}