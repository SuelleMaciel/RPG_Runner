using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class SeletorPersonagem : MonoBehaviour
{
    [SerializeField] private GameObject[] personagens; 
    [SerializeField] private Button[] botoes;         
    private int indiceSelecionado = -1;
    private Animator[] animators;
    private Outline[] contornos;

    void Start()
    {
        animators = new Animator[personagens.Length];
        contornos = new Outline[botoes.Length];

        for (int i = 0; i < personagens.Length; i++)
            animators[i] = personagens[i].GetComponent<Animator>();

        for (int i = 0; i < botoes.Length; i++)
        {
            contornos[i] = botoes[i].GetComponent<Outline>();
            if (contornos[i] != null)
                contornos[i].enabled = false; 
        }

        foreach (var anim in animators)
            anim.SetBool("waving", false);
    }

    public void SelecionarPersonagem(int indice)
    {
        if (indiceSelecionado >= 0 && indiceSelecionado != indice)
        {
            animators[indiceSelecionado].SetBool("waving", false);
            if (contornos[indiceSelecionado] != null)
                contornos[indiceSelecionado].enabled = false;
        }

        indiceSelecionado = indice;
        PlayerPrefs.SetInt("PersonagemEscolhido", indice);

        animators[indice].SetBool("waving", true);
        if (contornos[indice] != null)
            contornos[indice].enabled = true;
    }

    public void Jogar()
    {
        if (indiceSelecionado < 0)
        {
            Debug.LogWarning("Nenhum personagem selecionado!");
            return;
        }
        SceneManager.LoadScene("Cena");
    }

    public void Sair()
    {
        Application.Quit();
    }
}