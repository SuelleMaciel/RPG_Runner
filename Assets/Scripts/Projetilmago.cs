using UnityEngine;

// Esse script vai no prefab do projétil
public class Projetilmago : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Inimigo"))
        {
            Destroy(other.gameObject); // mata o inimigo
            Destroy(gameObject);       // destroi o projétil
        }
        else if (!other.CompareTag("Player"))
        {
            Destroy(gameObject); // destroi ao bater em qualquer outra coisa
        }
    }
}