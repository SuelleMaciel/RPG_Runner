using UnityEngine;

public class SpawnObstaculos : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] private float chanceDeSpawnar = 0.3f;
    [SerializeField] private string tagPontoSpawn = "PontoSpawn";
    [SerializeField] private Transform containerSpawns;

    void Start()
    {
        if (GameManager.instancia != null)
            chanceDeSpawnar = GameManager.instancia.ChanceSpawnAtual;

        foreach (Transform filho in containerSpawns)
        {
            if (!filho.CompareTag(tagPontoSpawn)) continue;

            foreach (Transform opcao in filho)
                opcao.gameObject.SetActive(false);

            if (Random.value > chanceDeSpawnar) continue;

            int totalOpcoes = filho.childCount;
            if (totalOpcoes == 0) continue;

            int indice = Random.Range(0, totalOpcoes);
            filho.GetChild(indice).gameObject.SetActive(true);
        }
    }
}