using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia;

    [Header("Velocidade")]
    [SerializeField] private float velocidadeInicial = 10f;
    [SerializeField] private float velocidadeMaxima = 40f;
    [SerializeField] private float incrementoVelocidade = 1f;
    [SerializeField] private float intervaloAumento = 7f;

    [SerializeField] private float chanceSpawnInicial = 0.3f;  
    [SerializeField] private float chanceSpawnMaxima = 0.9f; 
    [SerializeField] private float incrementoChance = 0.05f;  
 
    private float velocidadeBase;

    public float multiplicadorDash = 1f;
    
    public float VelocidadeAtual => velocidadeBase * multiplicadorDash;
    public float ChanceSpawnAtual { get; private set; }

    private float timerAumento = 0f;

    void Awake()
    {
        if (instancia == null) instancia = this;
        ChanceSpawnAtual = chanceSpawnInicial;
        velocidadeBase = velocidadeInicial;
    }

    void Update()
    {
        timerAumento += Time.deltaTime;

        if (timerAumento >= intervaloAumento)
        {
            timerAumento = 0f;
            velocidadeBase = Mathf.Min(velocidadeBase + incrementoVelocidade, velocidadeMaxima);
            ChanceSpawnAtual = Mathf.Min(ChanceSpawnAtual + incrementoChance, chanceSpawnMaxima);
        }
    }

    public void PararJogo()
    {
        velocidadeBase = 0f;
        multiplicadorDash = 1f;
    }
}
