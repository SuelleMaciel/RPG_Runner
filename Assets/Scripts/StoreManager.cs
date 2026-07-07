using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoreManager : MonoBehaviour
{
    [SerializeField] private Button botaoComprar1; // -1s cooldown
    [SerializeField] private Button botaoComprar2; // +10 vida

    void Start()
    {
        // desativa se ja foi comprado
        if (PlayerPrefs.GetInt("Comprou_Cooldown", 0) == 1)
            botaoComprar1.interactable = false;

        if (PlayerPrefs.GetInt("Comprou_Vida", 0) == 1)
            botaoComprar2.interactable = false;
    }

    public void ComprarCooldown()
    {
        int reducaoAtual = PlayerPrefs.GetInt("ReducaoCooldown", 0);
        PlayerPrefs.SetInt("ReducaoCooldown", reducaoAtual + 1); // cada compra reduz 1s

        PlayerPrefs.SetInt("Comprou_Cooldown", 1);
        botaoComprar1.interactable = false;

        Debug.Log("Cooldown reduzido em 1s");
    }

    public void ComprarVida()
    {
        int vidaExtra = PlayerPrefs.GetInt("VidaExtra", 0);
        PlayerPrefs.SetInt("VidaExtra", vidaExtra + 10);

        PlayerPrefs.SetInt("Comprou_Vida", 1);
        botaoComprar2.interactable = false;

        Debug.Log("vida aumentou em 10");
    }

    public void Recomecar()
    {
        SceneManager.LoadScene("Cena");
    }
}