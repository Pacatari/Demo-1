using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // Personaje a seguir
    public float smoothSpeed = 0.1f;  // Suavidad del movimiento
    public Vector2 offset;  // Desplazamiento en los ejes x e y

    private void LateUpdate()
    {
        if (target != null)
        {
            // Calcula la posición deseada de la cámara
            Vector3 targetPosition = new Vector3(
                target.position.x + offset.x,
                target.position.y + offset.y,
                transform.position.z
            );

            // Movimiento suave de la cámara
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        }
    }
}