using UnityEngine;

public class MoverCenario : MonoBehaviour
{
    public float velocidadeCorrida = 20f;

    void Update()
    {
        transform.Translate(Vector3.right * velocidadeCorrida * Time.deltaTime, Space.World);
    }
}