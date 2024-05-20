using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public void PlayGame()
    {
        GlobalVariables.playerHP = 10;
        UIManagerScript.Instance.UpdateHP();
        GlobalVariables.playerScore = 0;
        UIManagerScript.Instance.UpdateScore();
        GlobalVariables.shieldHP = 3;
        UIManagerScript.Instance.UpdateShield();
        Time.timeScale = 1f;
        PauseMenu.isPaused = false;
        SceneManager.LoadScene("Entree");
    }
}
