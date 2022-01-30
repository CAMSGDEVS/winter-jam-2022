using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    [SerializeField] private InputField inputField;
    [SerializeField] private Text warning;
    [SerializeField] private string password = "a1b3d0";
    [SerializeField] private Endings endings;

    private void Start() {
        inputField.onEndEdit.AddListener(CheckPassword);
    }

    public void ExitMenu() {
        endings.GoodEnding();
    }

    public void CheckPassword(string pass) {
        if (pass == password) {
            // Play animation here
            SceneManager.LoadScene("EmailTest");

        } else {
            warning.gameObject.SetActive(true);
        }
    }

}
