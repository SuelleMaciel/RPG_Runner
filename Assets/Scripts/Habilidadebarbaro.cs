using System.Collections;
using UnityEngine;

public class Habilidadebarbaro : HabilidadeBase
{
    [Header("Configurações do Dash")]
    [SerializeField] private string tagInimigo = "enemy";
    [SerializeField] private ParticleSystem particulas;
    
    [Tooltip("Tempo total que a habilidade fica ativa e invulnerável")]
    [SerializeField] private float janelaDestruicao = 0.7f; 
    
    [Tooltip("Distância que o Bárbaro se lança para frente")]
    [SerializeField] private float forcaDoDash = 4f;

    public bool habilidadeAtiva { get; private set; } = false;

    protected override void UsarHabilidade()
    {
        if (particulas != null)
            particulas.Play();

        habilidadeAtiva = true;
        
        StartCoroutine(RotinaDeDash());
    }

    private IEnumerator RotinaDeDash()
    {
        float tempoPassado = 0f;
        float xInicial = transform.position.x;
        
        float xAlvo = xInicial - forcaDoDash; 

        float tempoAvanco = janelaDestruicao * 0.4f;
        while (tempoPassado < tempoAvanco)
        {
            tempoPassado += Time.deltaTime;
            float t = tempoPassado / tempoAvanco;
            
            float novoX = Mathf.Lerp(xInicial, xAlvo, Mathf.Sin(t * Mathf.PI * 0.5f));
            
            transform.position = new Vector3(novoX, transform.position.y, transform.position.z);
            yield return null;
        }

        yield return new WaitForSeconds(janelaDestruicao * 0.2f);

        tempoPassado = 0f;
        float tempoVolta = janelaDestruicao * 0.4f;
        while (tempoPassado < tempoVolta)
        {
            tempoPassado += Time.deltaTime;
            float t = tempoPassado / tempoVolta;
            
            float novoX = Mathf.Lerp(xAlvo, xInicial, t);
            transform.position = new Vector3(novoX, transform.position.y, transform.position.z);
            yield return null;
        }

        transform.position = new Vector3(xInicial, transform.position.y, transform.position.z);
        habilidadeAtiva = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (habilidadeAtiva && other.CompareTag(tagInimigo))
        {
            Destroy(other.gameObject);
        }
    }
}
