using UnityEngine;
using UnityEngine.InputSystem;

public class MovimentoPlayer : MonoBehaviour
{
    [Header("Movimento Lateral")]
    [SerializeField] private float distanciaPorPasso = 1.5f;
    [SerializeField] private float velocidadeMovimento = 10f;
    [SerializeField] private int maxPassos = 5;
    [SerializeField] private float intervaloEntrePassos = 0.2f;

    [Header("Física do Pulo")]
    [Tooltip("Força inicial que joga o personagem para cima")]
    [SerializeField] private float forcaDoPulo = 12f; 
    [Tooltip("Força constante puxando o personagem para baixo")]
    [SerializeField] private float gravidade = -35f;  

    [Header("Inputs")]
    [SerializeField] private InputAction moverEsquerda;
    [SerializeField] private InputAction moverDireita;
    [SerializeField] private InputAction pular;

    private int passoAtual = 0;
    private float posicaoZAlvo;
    private bool movendo = false;
    private float posicaoZInicial;
    private float timerPasso = 0f;

    private float posicaoYInicial;
    private bool pulando = false;
    private float posicaoYAtual;
    private float velocidadeY = 0f;

    private Animator animator;

    private void OnEnable()
    {
        moverEsquerda.Enable();
        moverDireita.Enable();
        pular.Enable();
    }

    private void OnDisable()
    {
        moverEsquerda.Disable();
        moverDireita.Disable();
        pular.Disable();
    }

    void Start()
    {
        posicaoZInicial = transform.position.z;
        posicaoZAlvo = posicaoZInicial;
        
        posicaoYInicial = transform.position.y;
        posicaoYAtual = posicaoYInicial;

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        ProcessarInput();
        ProcessarMovimento();
        ProcessarPulo();
    }

    private void ProcessarInput()
    {
        timerPasso -= Time.deltaTime;

        if (moverEsquerda.IsPressed() && passoAtual > -maxPassos && timerPasso <= 0f)
        {
            passoAtual--;
            posicaoZAlvo = posicaoZInicial + (passoAtual * distanciaPorPasso);
            movendo = true;
            timerPasso = intervaloEntrePassos;
        }
        else if (moverDireita.IsPressed() && passoAtual < maxPassos && timerPasso <= 0f)
        {
            passoAtual++;
            posicaoZAlvo = posicaoZInicial + (passoAtual * distanciaPorPasso);
            movendo = true;
            timerPasso = intervaloEntrePassos;
        }

        if (pular.WasPressedThisFrame() && !pulando)
        {
            pulando = true;
            velocidadeY = forcaDoPulo; 
            if (animator != null) animator.SetBool("pulando", true);
        }
    }

    private void ProcessarMovimento()
    {
        if (!movendo) return;

        float novoPosZ = Mathf.MoveTowards(transform.position.z, posicaoZAlvo, velocidadeMovimento * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, novoPosZ);

        if (Mathf.Abs(transform.position.z - posicaoZAlvo) < 0.01f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, posicaoZAlvo);
            movendo = false;
        }
    }

    private void ProcessarPulo()
    {
        if (!pulando) return;

        velocidadeY += gravidade * Time.deltaTime;

        posicaoYAtual += velocidadeY * Time.deltaTime;

        if (posicaoYAtual <= posicaoYInicial)
        {
            posicaoYAtual = posicaoYInicial; 
            velocidadeY = 0f;                
            pulando = false;                 
            if (animator != null) animator.SetBool("pulando", false);
        }

        transform.position = new Vector3(transform.position.x, posicaoYAtual, transform.position.z);
    }
}
