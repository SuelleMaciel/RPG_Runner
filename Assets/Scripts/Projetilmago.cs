using UnityEngine;

public class ProjetilMago : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
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