using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia;

    [SerializeField] private float velocidadeInicial = 10f;
    [SerializeField] private float velocidadeMaxima = 40f;
    [SerializeField] private float incrementoVelocidade = 1f;
    [SerializeField] private float intervaloAumento = 7f;

    [SerializeField] private float chanceSpawnInicial = 0.3f;  
    [SerializeField] private float chanceSpawnMaxima = 0.9f; 
    [SerializeField] private float incrementoChance = 0.05f;  

    public float VelocidadeAtual { get; private set; }
    public float ChanceSpawnAtual { get; private set; }

    private float timerAumento = 0f;

    void Awake()
    {
        if (instancia == null) instancia = this;
        VelocidadeAtual = velocidadeInicial;
        ChanceSpawnAtual = chanceSpawnInicial;
    }

    void Update()
    {
        timerAumento += Time.deltaTime;

        if (timerAumento >= intervaloAumento)
        {
            timerAumento = 0f;
            VelocidadeAtual = Mathf.Min(VelocidadeAtual + incrementoVelocidade, velocidadeMaxima);
            ChanceSpawnAtual = Mathf.Min(ChanceSpawnAtual + incrementoChance, chanceSpawnMaxima);
        }
    }

    public void PararJogo()
    {
        VelocidadeAtual = 0f;
    }
}