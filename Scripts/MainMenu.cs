using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        GlobalVariables.playerHP = 10;
        //UIManagerScript.Instance.UpdateHP();
        GlobalVariables.playerScore = 0;
        //UIManagerScript.Instance.UpdateScore();
        GlobalVariables.shieldHP = 3;
        //UIManagerScript.Instance.UpdateShield();
        //Charge la scène avec l'index suivant le menu (IndexMenu = 0)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT !");
        Application.Quit();
    }
}
