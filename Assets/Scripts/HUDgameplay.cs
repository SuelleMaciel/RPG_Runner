using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;

public class HUDgameplay : MonoBehaviour
{
    public static HUDgameplay instancia;
    [SerializeField] private UnityEngine.UI.Image avatar;
    [SerializeField] private TextMeshProUGUI textoClasse;
    [SerializeField] private UnityEngine.UI.Slider barraVida;
    [SerializeField] private UnityEngine.UI.Slider barraXP;
    [SerializeField] private TextMeshProUGUI textoTempo;
    [SerializeField] private TextMeshProUGUI textoMoedas;
    [SerializeField] private DadosPersonagem[] personagens;
    private float tempoSobrevivido = 0f;
    private int moedas = 0;
    private bool pausado = false;
    [SerializeField] private Sprite spritePlay;
    [SerializeField] private Sprite spritePause;
    [SerializeField] private UnityEngine.UI.Button botaoPause;
    [SerializeField] private TextMeshProUGUI textoCooldown;

    private HabilidadeBase habilidade;

    public void AlternarPause()
    {
        pausado = !pausado;

        if (pausado)
        {
            Time.timeScale = 0f;
            botaoPause.image.sprite = spritePlay;
        }
        else
        {
            Time.timeScale = 1f;
            botaoPause.image.sprite = spritePause;
        }
    }

    [System.Serializable]
    public class DadosPersonagem
    {
        public string nomeClasse;
        public Sprite icon;
    }
    
    void Awake()
    {
        if (instancia == null) instancia = this;
    }

    void Start()
    {
        int indice = PlayerPrefs.GetInt("PersonagemEscolhido", 0);
        ConfigurarPerso(indice);
        AtualizarVida(1f);
        AtualizarXP(0f);
        AtualizarMoedas(0);
    }
    public void ConfigurarPerso(int indice)
    {
        if (personagens == null || indice < 0 || indice >= personagens.Length) return;
        DadosPersonagem dados = personagens[indice];
        avatar.sprite = dados.icon;
        textoClasse.text = dados.nomeClasse;
    }

    // Update is called once per frame
    void Update()
    {
        tempoSobrevivido += Time.deltaTime;
        AtualizarTempo();
        AtualizarCooldown();
    }
    public void AtualizarVida(float porcentagem)
    {
        barraVida.value = Mathf.Clamp01(porcentagem);
    }
    public void AtualizarXP(float porcentagem)
    {
        barraXP.value = Mathf.Clamp01(porcentagem); //clamp01 é pra normalizar o valor (pode ser so clamp tambem)
    }
    private void AtualizarTempo()
    {
        int minutos = Mathf.FloorToInt(tempoSobrevivido / 60f);
        int segundos = Mathf.FloorToInt(tempoSobrevivido % 60);
        textoTempo.text = string.Format("Tempo sobrevivido: {0:00}:{1:00}", minutos, segundos);
    }
    public void AdicionarMoedas(int quantidade)
    {
        moedas += quantidade;
        AtualizarMoedas(moedas);
    }
    public void AtualizarMoedas(int valor)
    {
        textoMoedas.text = valor.ToString();
    }

    private void AtualizarCooldown()
    {
        if (habilidade == null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
                habilidade = player.GetComponent<HabilidadeBase>();
            return;
        }

        if (habilidade.EmCooldown)
        {
            textoCooldown.text = Mathf.CeilToInt(habilidade.CooldownAtual).ToString() + "s";
        }
        else
        {
            textoCooldown.text = "HABILIDADE PRONTA!";
        }
    }
}
