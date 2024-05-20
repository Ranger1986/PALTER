using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMainMenu : MonoBehaviour
{
    // Méthode appelée lors du clic sur le bouton
    public void LoadMainMenu()
    {
        GlobalVariables.playerHP = 10;
        UIManagerScript.Instance.UpdateHP();
        GlobalVariables.playerScore = 0;
        UIManagerScript.Instance.UpdateScore();
        GlobalVariables.shieldHP = 3;
        UIManagerScript.Instance.UpdateShield();
        // Charger la scène du menu principal
        SceneManager.LoadScene("Menu");
    }
}
