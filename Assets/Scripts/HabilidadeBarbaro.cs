using System.Collections;
using UnityEngine;

public class HabilidadeBarbaro : HabilidadeBase
{
    [Header("Configurações do Dash")]
    [SerializeField] private string tagInimigo = "enemy";
    [SerializeField] private ParticleSystem particulas;
    
    [Tooltip("Tempo total que a habilidade fica ativa e invulnerável")]
    [SerializeField] private float janelaDestruicao = 0.7f; 
    
    [Tooltip("Quantas vezes o cenário fica mais rápido durante o Dash")]
    [SerializeField] private float multiplicadorVelocidade = 1.5f;

    public bool habilidadeAtiva { get; private set; } = false;

    protected override void UsarHabilidade()
    {
        habilidadeAtiva = true;
        
        StartCoroutine(RotinaDeDash());
    }

    private IEnumerator RotinaDeDash()
    {
        if (GameManager.instancia != null)
        {
            GameManager.instancia.multiplicadorDash = multiplicadorVelocidade;
        }

        yield return new WaitForSeconds(janelaDestruicao);

        if (GameManager.instancia != null)
        {
            GameManager.instancia.multiplicadorDash = 1f;
        }

        habilidadeAtiva = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (habilidadeAtiva && other.CompareTag(tagInimigo))
        {
            Destroy(other.gameObject);
            if (particulas != null)
            {
                particulas.Play();
            }
        }
    }
}
