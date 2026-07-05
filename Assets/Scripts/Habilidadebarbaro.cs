using UnityEngine;

public class Habilidadebarbaro : HabilidadeBase
{
    [Header("Dash")]
    [SerializeField] private float raioDestruicao = 3f;
    [SerializeField] private string tagPedra = "obstacle";

    protected override void UsarHabilidade()
    {
        Collider[] objetos = Physics.OverlapSphere(transform.position, raioDestruicao);

        foreach (Collider col in objetos)
        {
            if (col.CompareTag(tagPedra))
                Destroy(col.gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, raioDestruicao);
    }
}