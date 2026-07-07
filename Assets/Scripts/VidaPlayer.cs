using UnityEngine;
using UnityEngine.SceneManagement;

public class VidaPlayer : MonoBehaviour
{
    [Header("Configurações de Vida")]
    [Tooltip("Defina no Inspector: Bárbaro = 3, Guerreiro = 4, Mago = 2")]
    public int vidasMaximas = 3;
    private int vidasAtuais;

    [Header("Colisão e Invulnerabilidade")]
    [SerializeField] private float tempoInvulnerabilidade = 1.5f;
    private float timerInvulneravel = 0f;
    private bool invulneravel = false;

    [Header("Morte")]
    [SerializeField] private float tempoAnimacaoMorte = 2f;

    private HabilidadeLutador habilidadeEscudo;
    private Animator animator;

    void Start()
    {
        vidasAtuais = vidasMaximas;
        habilidadeEscudo = GetComponent<HabilidadeLutador>();
        animator = GetComponent<Animator>();
        AtualizarHUD();
    }

    void Update()
    {
        if (invulneravel)
        {
            timerInvulneravel -= Time.deltaTime;
            if (timerInvulneravel <= 0f)
                invulneravel = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("obstacle") || other.CompareTag("enemy"))
        {
            if (habilidadeEscudo != null && habilidadeEscudo.EscudoAtivo) return;
            if (!invulneravel) TomarDano();
        }
    }

    private void TomarDano()
    {
        vidasAtuais--;
        invulneravel = true;
        timerInvulneravel = tempoInvulnerabilidade;

        AtualizarHUD();
        Debug.Log($"Tomou dano! Vidas restantes: {vidasAtuais}");

        if (vidasAtuais <= 0)
            IniciarMorte();
    }

    private void IniciarMorte()
    {
        // para o mapa 
        if (GerarMapa.instancia != null)
            GerarMapa.instancia.PararGeracao();
        if (GameManager.instancia != null)
            GameManager.instancia.PararJogo();

        if (animator != null)
            animator.SetTrigger("morreu");

        Invoke(nameof(IrParaMorte), tempoAnimacaoMorte);
    }

    private void IrParaMorte()
    {
        SceneManager.LoadScene("Morte");
    }

    private void AtualizarHUD()
    {
        if (HUDgameplay.instancia != null)
        {
            float porcentagem = (float)vidasAtuais / vidasMaximas;
            HUDgameplay.instancia.AtualizarVida(porcentagem);
        }
    }
}