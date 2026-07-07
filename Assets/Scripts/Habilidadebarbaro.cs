using UnityEngine;

public class Habilidadebarbaro : HabilidadeBase
{
    [SerializeField] private string tagPedra = "obstacle";
    [SerializeField] private ParticleSystem particulas;
    [SerializeField] private float janelaDestruicao = 0.7f; // tempo que a habilidade fica ativa

    public bool habilidadeAtiva { get; private set; } = false;
    protected override void UsarHabilidade()
    {
        if (particulas != null)
            particulas.Play();

        habilidadeAtiva = true;
        Invoke(nameof(DesativarHabilidade), janelaDestruicao);
    }

    private void DesativarHabilidade()
    {
        habilidadeAtiva = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (habilidadeAtiva && other.CompareTag(tagPedra))
        {
            Destroy(other.gameObject);
        }
    }
}