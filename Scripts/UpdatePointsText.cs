using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdatePointsText : MonoBehaviour
{
    // Référence au composant Text
    private TMP_Text textComponent;

    // Méthode d'initialisation
    private void Start()
    {
        // Récupérer le composant Text attaché à cet objet
        textComponent = GetComponent<TMP_Text>();

        // Vérifier si le composant Text existe
        if (textComponent == null)
        {
            Debug.LogError("Le composant Text est manquant sur cet objet.");
        }
        else
        {
            // Mettre à jour le texte
            textComponent.text = GlobalVariables.playerScore + " POINTS";
        }
    }
}
