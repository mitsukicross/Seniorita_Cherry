using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarEscena : MonoBehaviour
{
    public string nombreEscenaDestino; // Cambié el nombre para que sea más claro

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            // AVISAMOS que la siguiente escena debe usar el punto de spawn
            SpawnPlayer.vieneDeTransicion = true;
            
            SceneManager.LoadScene(nombreEscenaDestino);
        }
    }
}
