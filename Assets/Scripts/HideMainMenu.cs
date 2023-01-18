using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class HideMainMenu : MonoBehaviour
{
    public void Setup()
    {
        gameObject.SetActive(true);
    }
    public void StartButton()
    {
        SceneManager.LoadScene("Level");
    }

}
