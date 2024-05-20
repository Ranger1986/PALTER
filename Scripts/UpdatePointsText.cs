using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdatePointsText : MonoBehaviour
{
    // R�f�rence au composant Text
    private TMP_Text textComponent;

    // M�thode d'initialisation
    private void Start()
    {
        // R�cup�rer le composant Text attach� � cet objet
        textComponent = GetComponent<TMP_Text>();

        // V�rifier si le composant Text existe
        if (textComponent == null)
        {
            Debug.LogError("Le composant Text est manquant sur cet objet.");
        }
        else
        {
            // Mettre � jour le texte
            textComponent.text = GlobalVariables.playerScore + " POINTS";
        }
    }
}
