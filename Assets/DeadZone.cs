using UnityEngine;

public class DeadZone : MonoBehaviour
{
    public PlayerRespawn playerRespawn; // Referencia al sistema de respawn del jugador

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si el objeto que entra es el jugador
        if (other.CompareTag("Player"))
        {
            Debug.Log("Jugador entró en la dead zone. Respawn activado.");

            // Llamar al método de respawn del jugador
            if (playerRespawn != null)
            {
                playerRespawn.Respawn();
            }
            else
            {
                Debug.LogError("No se ha asignado el sistema de respawn al jugador.");
            }
        }
    }
}