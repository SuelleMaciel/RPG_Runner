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
    [SerializeField] private float distanciaSpawnAFrente = 180f;

    private List<GameObject> blocosAtuais = new List<GameObject>();
    private Transform player;

    void Awake()
    {
        if (instancia == null) instancia = this;
    }

    void Start()
    {
        if (prefabsChunks == null || prefabsChunks.Count == 0) return;

        for (int i = 0; i < blocosNaTela; i++)
            SpawnBlocoInicial(Random.Range(0, prefabsChunks.Count), i);

        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
        else
            Debug.LogWarning("nao achou com obj com tag player");
    }

    void Update()
    {
        if (player == null) //tentando achar o player de novo se n for da primeira vez
        {
            GameObject playerObj = GameObject.FindWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
            return;
        }

        if (blocosAtuais.Count > 0)
        {
            GameObject ultimoBloco = blocosAtuais[blocosAtuais.Count - 1];
            float distanciaAFrente = player.position.x - ultimoBloco.transform.position.x;

            if (distanciaAFrente < distanciaSpawnAFrente)
                SpawnBlocoAleat();
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

    private void SpawnBlocoInicial(int indice, int ordem)
    {
        Vector3 posicaoSpawn = new Vector3(
            pontoInicial.position.x - (ordem * tamanhoDoBloco),
            pontoInicial.position.y,
            pontoInicial.position.z
        );

        GameObject novoBloco = Instantiate(prefabsChunks[indice], posicaoSpawn, Quaternion.identity);
        blocosAtuais.Add(novoBloco);
    }

    public void SpawnBlocoAleat()
    {
        if (prefabsChunks.Count == 0 || blocosAtuais.Count == 0) return;

        int numAleat = Random.Range(0, prefabsChunks.Count);
        GameObject ultimoBloco = blocosAtuais[blocosAtuais.Count - 1];

        Vector3 novaPosicao = new Vector3(
            ultimoBloco.transform.position.x - tamanhoDoBloco,
            pontoInicial.position.y,
            pontoInicial.position.z
        );

        GameObject novoBloco = Instantiate(prefabsChunks[numAleat], novaPosicao, Quaternion.identity);
        blocosAtuais.Add(novoBloco);
    }
}