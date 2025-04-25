using UnityEngine;
using TMPro; // Importa el namespace de TextMeshPro

public class WinCondition : MonoBehaviour
{
    public TextMeshProUGUI WINTEXT; // Referencia al componente TextMeshProUGUI

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica si el objeto con el que colisiona tiene la etiqueta "Win"
        if (collision.gameObject.CompareTag("Win"))
        {
            // Activa el texto de victoria
            if (WINTEXT != null)
            {
                WINTEXT.gameObject.SetActive(true);
            }
            else
            {
                Debug.LogError("WINTEXT no está asignado. Asegúrate de asignar el objeto TextMeshPro en el Inspector.");
            }
        }
    }
}