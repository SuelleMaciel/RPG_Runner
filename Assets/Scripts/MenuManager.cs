using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject painelInstrucoes;

    public void AbrirInstrucoes()
    {
        painelInstrucoes.SetActive(true);
    }

    public void FecharInstrucoes()
    {
        painelInstrucoes.SetActive(false);
}
    public void JogarAgora()
    {
        SceneManager.LoadScene("Cena"); 
    }
}
