using UnityEngine;

public class TimeExpManager : MonoBehaviour
{
    [Header("Configurações de EXP")]
    public float expPorSegundo = 5f;
    public float expParaOProximoNivel = 100f; 
    
    private float tempoDecorrido = 0f;
    private float expAtual = 0f;

    void Update()
    {
        tempoDecorrido += Time.deltaTime;

        if (tempoDecorrido >= 1f)
        {
            tempoDecorrido -= 1f; 
            GanharExperiencia(expPorSegundo);
        }
    }

    void GanharExperiencia(float quantidade)
    {
        expAtual += quantidade;

        Debug.Log($"EXP: {expAtual}/{expParaOProximoNivel} de exp");
        
        float porcentagemXP = expAtual / expParaOProximoNivel;

        if (HUDgameplay.instancia != null)
        {
            HUDgameplay.instancia.AtualizarXP(porcentagemXP);
        }

        if (expAtual >= expParaOProximoNivel)
        {
            SubirDeNivel();
        }
    }

    void SubirDeNivel()
    {
        expAtual = 0f; 

        expParaOProximoNivel += 50f; 
        
        Debug.Log("Subiu de Nível!");
        
        if (HUDgameplay.instancia != null)
        {
            HUDgameplay.instancia.AtualizarXP(0f);
        }
    }
}