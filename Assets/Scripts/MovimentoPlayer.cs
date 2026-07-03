using UnityEngine;
using UnityEngine.InputSystem;

public class MovimentoPlayer : MonoBehaviour
{
    [SerializeField] private float distanciaPorPasso = 1.5f;
    [SerializeField] private float velocidadeMovimento = 10f;
    [SerializeField] private int maxPassos = 5;
    [SerializeField] private float intervaloEntrePassos = 0.2f;

    [SerializeField] private float alturaMaximaPulo = 3f;
    [SerializeField] private float velocidadePulo = 8f;

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
    private bool subindo = false;
    private float posicaoYAtual;

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
            subindo = true;
            animator.SetBool("pulando", true);
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

        float alvoY = subindo ? posicaoYInicial + alturaMaximaPulo : posicaoYInicial;

        posicaoYAtual = Mathf.MoveTowards(posicaoYAtual, alvoY, velocidadePulo * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, posicaoYAtual, transform.position.z);

        if (subindo && Mathf.Abs(posicaoYAtual - alvoY) < 0.01f)
        {
            subindo = false; 
        }

        if (!subindo && Mathf.Abs(posicaoYAtual - posicaoYInicial) < 0.01f)
        {
            transform.position = new Vector3(transform.position.x, posicaoYInicial, transform.position.z);
            pulando = false;
            animator.SetBool("pulando", false);
        }
    }
}