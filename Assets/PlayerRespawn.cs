using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public Transform respawnPoint; // Punto de respawn (posición fija o zona)
    private Vector3 initialPosition; // Posición inicial del jugador

    void Start()
    {
        // Guardar la posición inicial del jugador
        if (respawnPoint != null)
        {
            initialPosition = respawnPoint.position;
        }
        else
        {
            Debug.LogWarning("No se ha asignado un punto de respawn.");
        }
    }

    void Update()
    {
        // Detectar si el jugador cae al vacío (opcional)
        if (transform.position.y < -10f) // Umbral para caídas al vacío
        {
            Respawn();
        }
    }

    public void Respawn()
    {
        // Reaparecer en el punto de respawn
        if (respawnPoint != null)
        {
            transform.position = respawnPoint.position;
        }
        else
        {
            Debug.LogError("El punto de respawn no está asignado.");
        }
    }

    public void SetRespawnPoint(Transform newRespawnPoint)
    {
        // Actualizar el punto de respawn
        respawnPoint = newRespawnPoint;
    }
}