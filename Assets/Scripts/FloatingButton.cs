using UnityEngine;

public class FloatingButton : MonoBehaviour
{
    public float amplitude = 18f; // distância do movimento
    public float speed = 2f;     // velocidade

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        float y = Mathf.Sin(Time.time * speed) * amplitude;
        transform.localPosition = startPos + new Vector3(0, y, 0);
    }
}