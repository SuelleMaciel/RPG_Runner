using UnityEngine;

// Esse script vai no prefab do projétil
public class Projetilmago : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);   
        }
        else if (!other.CompareTag("Player"))
        {
            Destroy(gameObject); 
        }
    }
}