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

    private HabilidadeLutador habilidadeEscudo;
    private HabilidadeBarbaro habilidadeBarbaro;

    void Start()
    {
        vidasAtuais = vidasMaximas;
        habilidadeEscudo = GetComponent<HabilidadeLutador>();
        habilidadeBarbaro = GetComponent<HabilidadeBarbaro>();
        AtualizarHUD();
    }

    void Update()
    {
        if (invulneravel)
        {
            timerInvulneravel -= Time.deltaTime;
            if (timerInvulneravel <= 0f)
            {
                invulneravel = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Colidiu com: " + other.gameObject.name + " | Tag: " + other.gameObject.tag);

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
        {
            Morrer();
        }
    }

    private void AtualizarHUD()
    {
        if (HUDgameplay.instancia != null)
        {
            float porcentagem = (float)vidasAtuais / vidasMaximas;
            HUDgameplay.instancia.AtualizarVida(porcentagem);
        }
    }

    private void Morrer()
    {
        Debug.Log("Fim de Jogo!");
        
        SceneManager.LoadScene("Morte"); 
    }
}
