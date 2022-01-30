using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Endings : MonoBehaviour {

    [SerializeField] private GameObject good, bad;
    [SerializeField] private Text badStats;

    public void Accept() {
        Application.Quit();
    }

    public void Retry() {
        SceneManager.LoadScene("MainMenu");
    }

    public void BadEnding(string stats) {
        bad.SetActive(true);
        badStats.text = stats;
    }

    public void GoodEnding() {
        good.SetActive(true);
    }

}
