using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmailDisplay : MonoBehaviour {
    [SerializeField] private Text headerText, senderText, bodyText;

    [SerializeField] private Animator animator;

    [SerializeField] private Button deleteButton, replyButton, yesButton, noButton, confirmButton;

    [SerializeField] private Sprite yesImage, noImage, defaultYesImage, defaultNoImage;

    public Email emailData;

    public GameObject center, smallWindow;

    private bool replying;

    public void LoadFromData() {
        headerText.text = emailData.Title;
        senderText.text = emailData.Sender;
        bodyText.text = emailData.BodyText;
    }

    private void Awake() {
        EmailManager.Instance.emailDisplay = this;
    }

    public void Init() {
        Open();
    }

    // Open the email
    public void Open() {
        replying = EmailManager.Instance.Replying;
        replyButton.interactable = !replying;
        deleteButton.interactable = !replying;
        confirmButton.interactable = replying;

        yesButton.interactable = true;
        noButton.interactable = true;

        replyButton.gameObject.SetActive(!replying);
        deleteButton.gameObject.SetActive(!replying);
        confirmButton.gameObject.SetActive(replying);

        replyButton.interactable = !replying;
        deleteButton.interactable = !replying;
        confirmButton.interactable = replying;

        yesButton.gameObject.GetComponent<Image>().sprite = defaultYesImage;
        noButton.gameObject.GetComponent<Image>().sprite = defaultNoImage;
        smallWindow.SetActive(false);
        EmailManager.Instance.renderLine.ToggleEnabled(true);
        animator.SetBool("EmailOpen", true);
    }

    public void Confirm() {
        StartCoroutine(ConfirmClose());
    }

    private IEnumerator ConfirmClose() {
        Close();
        animator.SetBool("Deleting", true);
        GameManager.emailDeclined = false;
        yield return new WaitForSeconds(2f);
        EmailManager.Instance.Replying = false;
        GameManager.year++;
        GameManager.currentEmailId = GameManager.year * 2 + Random.Range(0, 2);
        EmailManager.Instance.Init(GameManager.currentEmailId);
    }

    // Close the email
    public void Close() {
        EmailManager.Instance.renderLine.ToggleEnabled(false);
        animator.SetBool("EmailOpen", false);
    }

    public void Delete() {
        GameManager.emailDeclined = true;
        replyButton.interactable = false;
        deleteButton.interactable = false;
        Close();
        GameManager.EnvironmentScore += emailData.IgnoreScore;
        StartCoroutine(nextGamemode(!emailData.Positive));
    }

    public void Reply() {
        smallWindow.SetActive(true);
        replyButton.interactable = false;
        deleteButton.interactable = false;
        // Open window
        animator.SetBool("Reply", true);
    }

    public void EndReply(bool agree) {
        yesButton.interactable = false;
        noButton.interactable = false;
        if (agree) {
            yesButton.gameObject.GetComponent<Image>().sprite = yesImage;
            GameManager.EnvironmentScore += emailData.AcceptScore;
            GameManager.emailDeclined = false;
        }
        else {
            noButton.gameObject.GetComponent<Image>().sprite = noImage;
            GameManager.EnvironmentScore += emailData.DenyScore;
            GameManager.emailDeclined = true;
        }

        // XNOR if the email is positive and if it is agreed to
        // (yes, positive) = false
        // (no, positive) = true
        // (yes, !positive) = true
        // (no, !positive) = false
        // true = cut trees, false = skip to next year
        StartCoroutine(waitAfterReply(!(emailData.Positive^agree)));
    }

    private IEnumerator waitAfterReply(bool startCutting) {
        yield return new WaitForSeconds(1f);
        Close();
        StartCoroutine(nextGamemode(startCutting));
    }

    private IEnumerator nextGamemode(bool startCutting) {
        animator.SetBool("Deleting", true);
        yield return new WaitForSeconds(2f);
        if (startCutting) {
            GameManager.year++;
            GameManager.Instance.ChangeYear(GameManager.year);
        } else {
            GameManager.Instance.LoadIsometric();
        }
    }

    // Unused; use to "type out" text
    /*
    public IEnumerator TypeText(string sentence, Text textDisplay) {
        textDisplay.text = "";
        char[] _sentence_array = sentence.ToCharArray();
        for (int i = 0; i < _sentence_array.Length; i++) {
            char letter = _sentence_array[i];
            textDisplay.text += letter;
            if (i % 2 == 1) {
                yield return null;
            }
        }
    }
    */
}
