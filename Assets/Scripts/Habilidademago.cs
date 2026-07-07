using UnityEngine;

public class Habilidademago : HabilidadeBase
{
    [SerializeField] private ParticleSystem particulas;
    [SerializeField] private GameObject prefabProjetil;
    [SerializeField] private float velocidadeProjetil = 20f;
    [SerializeField] private Vector3 offsetDisparo = new Vector3(-1f, 0.5f, 0f); 

    protected override void UsarHabilidade()
    {
        if (particulas != null){particulas.Play();}

        if (prefabProjetil == null) return;

        Vector3 posicaoDisparo = transform.position + offsetDisparo;
        GameObject projetil = Instantiate(prefabProjetil, posicaoDisparo, Quaternion.identity);

        Rigidbody rb = projetil.GetComponent<Rigidbody>();
        if (rb != null)
            rb.linearVelocity = Vector3.left * velocidadeProjetil;

        Destroy(projetil, 3f);
    }
}