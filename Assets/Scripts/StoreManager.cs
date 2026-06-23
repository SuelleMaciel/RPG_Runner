using UnityEngine;
using UnityEngine.SceneManagement;

public class StoreManager : MonoBehaviour
{
    public void Recomecar()
    {
        SceneManager.LoadScene("Cena");
        Debug.Log("Clicou");
    }
}
