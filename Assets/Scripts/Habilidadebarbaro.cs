using UnityEngine;

public class Habilidadebarbaro : HabilidadeBase
{
    [SerializeField] private float raioDestruicao = 3f;
    [SerializeField] private string tagPedra = "obstacle";
    [SerializeField] private ParticleSystem particulas;


    protected override void UsarHabilidade()
    {
        Collider[] objetos = Physics.OverlapSphere(transform.position, raioDestruicao);

        foreach (Collider col in objetos)
        {
            Debug.Log("Encontrou : " + col.gameObject.name + " | Tag: " + col.gameObject.tag);

            if (col.CompareTag(tagPedra))
            {
                Destroy(col.gameObject);
                particulas.Play();
            }
                
            
            if (col.transform.parent != null && col.transform.parent.CompareTag(tagPedra))
                Destroy(col.transform.parent.gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, raioDestruicao);
    }
}