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

    private HabilidadeLutador habilidadeEscudo;

    void Start()
    {
        vidasAtuais = vidasMaximas;
        
        habilidadeEscudo = GetComponent<HabilidadeLutador>();
        
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
        if (other.CompareTag("obstacle") || other.CompareTag("enemy"))
        {
            if (habilidadeEscudo != null && habilidadeEscudo.EscudoAtivo)
            {
                return;
            }

            if (!invulneravel)
            {
                ReceberDano();
            }
        }
    }

    private void ReceberDano()
    {
        vidasAtuais--;
        invulneravel = true;
        timerInvulneravel = tempoInvulnerabilidade;

        AtualizarHUD();

        Debug.Log($"Dano Recebido. Vidas restantes: {vidasAtuais}");

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