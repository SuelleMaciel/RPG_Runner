using UnityEngine;

public class MoverCenario : MonoBehaviour
{
    void Update()
    {
        float velocidade = GameManager.instancia != null ? GameManager.instancia.VelocidadeAtual : 10f;
        transform.Translate(Vector3.right * velocidade * Time.deltaTime, Space.World);
    }
}