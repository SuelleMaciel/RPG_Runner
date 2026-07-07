using UnityEngine;

public class ProjetilMago : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Projetil colidiu com: " + other.gameObject.name + " | Tag: " + other.gameObject.tag);
        
        if (other.CompareTag("enemy"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            return;
        }
        if (other.CompareTag("Untagged")) return;

        if (!other.CompareTag("Player"))
            Destroy(gameObject);
    }
}