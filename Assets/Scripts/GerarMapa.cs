using System.Collections.Generic;
using UnityEngine;

public class GerarMapa : MonoBehaviour
{
    public static GerarMapa instancia;

    [SerializeField] private List<GameObject> prefabsChunks;
    [SerializeField] private int blocosNaTela = 6;
    [SerializeField] private float tamanhoDoBloco = 62f;
    [SerializeField] private Transform pontoInicial;
    [SerializeField] private float limiteDestruicao = 100f;

    private List<GameObject> blocosAtuais = new List<GameObject>();

    [SerializeField] private Transform player;
    [SerializeField] private float distanciaSpawnAFrente = 180f; // 3 chunks de 60

    void Update()
    {
        if (blocosAtuais.Count > 0)
        {
            GameObject ultimoBloco = blocosAtuais[blocosAtuais.Count - 1];
            float distanciaAFrente = player.position.x - ultimoBloco.transform.position.x;
            
            if (distanciaAFrente < distanciaSpawnAFrente)
            {
                SpawnBlocoAleat();
            }
        }

        if (blocosAtuais.Count > 0 && blocosAtuais[0] != null)
        {
            if (blocosAtuais[0].transform.position.x > limiteDestruicao)
            {
                Destroy(blocosAtuais[0]);
                blocosAtuais.RemoveAt(0);
            }
        }
    }
    void Awake()
    {
        if (instancia == null) instancia = this;
    }

    void Start()
    {
        if (prefabsChunks == null || prefabsChunks.Count == 0) return;

        for (int i = 0; i < blocosNaTela; i++)
        {
            SpawnBlocoInicial(Random.Range(0, prefabsChunks.Count), i);
        }
    }

    private void SpawnBlocoInicial(int indice, int ordem)
    {
        Vector3 posicaoSpawn = new Vector3(
            pontoInicial.position.x - (ordem * tamanhoDoBloco), pontoInicial.position.y, pontoInicial.position.z
        );

        GameObject novoBloco = Instantiate(prefabsChunks[indice], posicaoSpawn, Quaternion.identity);
        blocosAtuais.Add(novoBloco);
    }

    public void SpawnBlocoAleat()
    {
        if (prefabsChunks.Count == 0 || blocosAtuais.Count == 0) {
            return;
        }

        int numAleat = Random.Range(0, prefabsChunks.Count); //indice aleatorio
        GameObject ultimoBloco = blocosAtuais[blocosAtuais.Count - 1];

        Vector3 novaPosicao = new Vector3(
            ultimoBloco.transform.position.x - tamanhoDoBloco, pontoInicial.position.y, pontoInicial.position.z
        );

        GameObject novoBloco = Instantiate(prefabsChunks[numAleat], novaPosicao, Quaternion.identity);
        blocosAtuais.Add(novoBloco);
    }
}