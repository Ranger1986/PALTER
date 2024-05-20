using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManagerScript : MonoBehaviour
{
    public static UIManagerScript Instance;

    [SerializeField] private Image HealthBar;
    [SerializeField] private Image ShieldBar;
    [SerializeField] private TextMeshProUGUI ScoreText;
    [SerializeField] private GameObject UIContainer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

    }

    public void UpdateHP()
    {
        Debug.Log("Interface MAJ HP");
        if (HealthBar != null)
        {

            HealthBar.fillAmount = (float)GlobalVariables.playerHP / 10f;
        }
    }

    public void UpdateShield()
    {
        ShieldBar.fillAmount = GlobalVariables.shieldHP / 3f;
    }


    public void UpdateScore()
    {
        if (ScoreText != null)
        {
            ScoreText.text = GlobalVariables.playerScore.ToString("D8");
        }
    }

}
