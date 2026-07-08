using UnityEngine;
using UnityEngine.SceneManagement;

public class VidaPlayer : MonoBehaviour
{
    [Header("Configurações de Vida")]
    public int vidasMaximas = 3;
    private int vidasAtuais;

    [Header("Colisão e Invulnerabilidade")]
    [SerializeField] private float tempoInvulnerabilidade = 1.5f;
    private float timerInvulneravel = 0f;
    private bool invulneravel = false;

    [Header("Morte")]
    [SerializeField] private float tempoAteMorte = 2.2f;
    private bool morreu = false;

    private HabilidadeLutador habilidadeEscudo;
    private HabilidadeBarbaro habilidadeBarbaro;
    private Animator animator;

    void Start()
    {
        vidasAtuais = vidasMaximas + PlayerPrefs.GetInt("VidaExtra", 0);
        habilidadeEscudo = GetComponent<HabilidadeLutador>();
        habilidadeBarbaro = GetComponent<HabilidadeBarbaro>();
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
        if (morreu) return;

        if (other.CompareTag("obstacle") || other.CompareTag("enemy"))
        {
            if (habilidadeEscudo != null && habilidadeEscudo.EscudoAtivo) return;
            if (habilidadeBarbaro != null && habilidadeBarbaro.habilidadeAtiva) return;
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
        morreu = true;
        MovimentoPlayer movimento = GetComponent<MovimentoPlayer>();
        if (movimento != null)
        {
          movimento.enabled = false;
        }

        if (GerarMapa.instancia != null)
            GerarMapa.instancia.PararGeracao();
        if (GameManager.instancia != null)
            GameManager.instancia.PararJogo();

        if (animator != null)
            animator.SetTrigger("morreu");

        Invoke(nameof(IrParaMorte), tempoAteMorte);
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
