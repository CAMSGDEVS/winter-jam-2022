using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmailDisplay : MonoBehaviour {
    [SerializeField] private Text headerText, senderText, bodyText;

    [SerializeField] private Animator animator;

    [SerializeField] private Button deleteButton, replyButton, yesButton, noButton;

    [SerializeField] private Sprite yesImage, noImage;

    public Email emailData;

    public GameObject center, smallWindow;

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
        smallWindow.SetActive(false);
        EmailManager.Instance.renderLine.ToggleEnabled(true);
        animator.SetBool("EmailOpen", true);
    }

    // Close the email
    public void Close() {
        EmailManager.Instance.renderLine.ToggleEnabled(false);
        animator.SetBool("EmailOpen", false);
    }

    public void Delete() {
        replyButton.interactable = false;
        deleteButton.interactable = false;
        Close();
        GameManager.EnvironmentScore += emailData.IgnoreScore;
        StartCoroutine(waitBeforeDeletion());
    }

    public void Reply() {
        smallWindow.SetActive(true);
        replyButton.interactable = false;
        deleteButton.interactable = false;
        // Open window
        animator.SetBool("Reply", true);
    }

    public void Yes() {
        yesButton.gameObject.GetComponent<Image>().sprite = yesImage;
        yesButton.interactable = false;
        noButton.interactable = false;
        GameManager.EnvironmentScore += emailData.AcceptScore;
        StartCoroutine(waitAfterReply());
    }

    public void No() {
        noButton.gameObject.GetComponent<Image>().sprite = noImage;
        yesButton.interactable = false;
        noButton.interactable = false;
        GameManager.EnvironmentScore += emailData.DenyScore;
        StartCoroutine(waitAfterReply());
    }

    private IEnumerator waitAfterReply() {
        yield return new WaitForSeconds(1f);
        Close();
        StartCoroutine(waitBeforeDeletion());
    }

    private IEnumerator waitBeforeDeletion() {
        animator.SetBool("Deleting", true);
        yield return new WaitForSeconds(2f);
        Destroy(transform.parent.gameObject);
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
