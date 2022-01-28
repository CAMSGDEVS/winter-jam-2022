using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    [SerializeField] private InputField inputField;
    [SerializeField] private Text warning;
    [SerializeField] private string password = "bruh";

    private void Start() {
        inputField.onEndEdit.AddListener(CheckPassword);
    }

    public void ExitMenu() {
        Debug.Log("Exit");
        Application.Quit();
        // Secret ending where the earth is saved?
    }

    public void CheckPassword(string pass) {
        if (pass == password) {
            // Play animation
            SceneManager.LoadScene("EmailTest");

        } else {
            warning.gameObject.SetActive(true);
        }
    }

}
