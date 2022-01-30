using UnityEngine;
using UnityEngine.SceneManagement;

public class Endings : MonoBehaviour {

    [SerializeField] private GameObject good, bad;

    public void Accept() {
        Application.Quit();
    }

    public void Retry() {
        SceneManager.LoadScene("MainMenu");
    }

    public void BadEnding() {
        bad.SetActive(true);
    }

    public void GoodEnding() {
        good.SetActive(true);
    }

}
