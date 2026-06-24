using UnityEngine;

public class SpawnPersonagem : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabsPersonagens;
    [SerializeField] private Transform pontoSpawn;

    void Start()
    {
        int indice = PlayerPrefs.GetInt("PersonagemEscolhido", 0);
        Instantiate(prefabsPersonagens[indice], pontoSpawn.position, pontoSpawn.rotation);
    }
}