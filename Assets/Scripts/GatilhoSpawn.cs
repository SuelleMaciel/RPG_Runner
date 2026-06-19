using UnityEngine;

public class GatilhoSpawn : MonoBehaviour
{
    private bool jaAtivou = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !jaAtivou)
        {
            jaAtivou = true;
            GerarMapa.instancia.SpawnBlocoAleat();
        }
    }
}