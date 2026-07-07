using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia;

    [Header("Velocidade")]
    [SerializeField] private float velocidadeInicial = 10f;
    [SerializeField] private float velocidadeMaxima = 30f;
    [SerializeField] private float incrementoVelocidade = 1f;  
    [SerializeField] private float intervaloAumento = 7f;

    public float VelocidadeAtual { get; private set; }

    private float timerAumento = 0f;

    void Awake()
    {
        if (instancia == null) instancia = this;
        VelocidadeAtual = velocidadeInicial;
    }

    void Update()
    {
        timerAumento += Time.deltaTime;

        if (timerAumento >= intervaloAumento)
        {
            timerAumento = 0f;
            VelocidadeAtual = Mathf.Min(VelocidadeAtual + incrementoVelocidade, velocidadeMaxima);
        }
    }
    public void PararJogo()
    {
        VelocidadeAtual = 0f;
    }
}