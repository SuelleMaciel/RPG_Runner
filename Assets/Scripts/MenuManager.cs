using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void JogarAgora()
    {
        SceneManager.LoadScene("Cena"); 
    }
}