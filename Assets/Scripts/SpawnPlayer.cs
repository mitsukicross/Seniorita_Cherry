using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public Transform player;
    // Esta variable guarda si venimos de una transición o no
    public static bool vieneDeTransicion = false;

    void Start()
    {
        // SOLO mueve al jugador si venimos de un cambio de escena
        if (vieneDeTransicion == true)
        {
            player.position = transform.position;
            // Después de moverlo, lo apagamos para que no afecte el inicio normal
            vieneDeTransicion = false; 
        }
    }
}
