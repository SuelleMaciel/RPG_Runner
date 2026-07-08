using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoreManager : MonoBehaviour
{
    [SerializeField] private Button botaoComprar1; // -1s cooldown
    [SerializeField] private Button botaoComprar2; // +1 vida

    void Start()
    {
        bool comprouCooldown = PlayerPrefs.GetInt("Comprou_Cooldown", 0) == 1;
        bool comprouVida = PlayerPrefs.GetInt("Comprou_Vida", 0) == 1;

        if (comprouCooldown || comprouVida)
        {
            DesativarBotoes();
        }
    }

    public void ComprarCooldown()
    {
        int reducaoAtual = PlayerPrefs.GetInt("ReducaoCooldown", 0);
        PlayerPrefs.SetInt("ReducaoCooldown", reducaoAtual + 1); 

        PlayerPrefs.SetInt("Comprou_Cooldown", 1);
        
        DesativarBotoes();

        Debug.Log("Cooldown reduzido em 1s. Opção de Vida bloqueada!");
    }

    public void ComprarVida()
    {
        int vidaExtra = PlayerPrefs.GetInt("VidaExtra", 0);
        PlayerPrefs.SetInt("VidaExtra", vidaExtra + 1); 

        PlayerPrefs.SetInt("Comprou_Vida", 1);
        
        DesativarBotoes();

        Debug.Log("Vida aumentou em 1. Opção de Cooldown bloqueada!");
    }

    private void DesativarBotoes()
    {
        if (botaoComprar1 != null) botaoComprar1.interactable = false;
        if (botaoComprar2 != null) botaoComprar2.interactable = false;
    }

    public void Recomecar()
    {
        PlayerPrefs.SetInt("Comprou_Cooldown", 0);
        PlayerPrefs.SetInt("Comprou_Vida", 0);
        PlayerPrefs.Save();
        
        SceneManager.LoadScene("Cena");
    }
}
