using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EmailManager : MonoBehaviour
{
    public Emails Emails { get; set; }
    private Emails currentEmails;

    public Animator animator;

    [SerializeField]
    private GameObject emailPrefab;
    public List<GameObject> LoadedEmails { get; set; }

    // Load email data from json resource file
    public void LoadEmailsFromJson() {
        Debug.Log(Resources.Load<TextAsset>("emails").text);
        Emails = JsonUtility.FromJson<Emails>(
            Resources.Load<TextAsset>("emails").text
        );
        for (int i = 0; i < Emails.emails.Length; i++) {
            Debug.Log(Emails.emails[i].BodyText);
        }
    }

    public void Start() {
        LoadEmailsFromJson();
    }

    // Placeholder for now
    public void OpenEmailMenu(int numEmails) {
        currentEmails = new Emails();
        currentEmails.emails = new Email[numEmails];

        LoadedEmails = new List<GameObject>();

        // Generate the email objects and add them to the current emails list
        for (int i = 0; i < numEmails; i++) {
            // Instantiate as child of canvas (script should be placed on canavs)
            GameObject email = Instantiate(emailPrefab, Vector3.zero, Quaternion.identity, gameObject.transform);
            LoadedEmails.Add(email);

            EmailDisplay emailDisplay = email.GetComponentInChildren<EmailDisplay>();

            emailDisplay.emailData = Emails.emails[i];
            emailDisplay.LoadFromEmail();
        }
    }

    public void OpenEmailMenu(int[] emailNums) {
        currentEmails = new Emails();
        currentEmails.emails = new Email[emailNums.Length];

        LoadedEmails = new List<GameObject>();

        // Generate the email objects and add them to the current emails list
        for (int i = 0; i < emailNums.Length; i++) {  
            // Instantiate as child of canvas (script should be placed on canvas)
            GameObject email = Instantiate(emailPrefab, Vector3.zero, Quaternion.identity, gameObject.transform);
            LoadedEmails.Add(email);

            EmailDisplay emailDisplay = email.GetComponentInChildren<EmailDisplay>();

            emailDisplay.emailData = Emails.emails[emailNums[i]];
            emailDisplay.LoadFromEmail();
        }
    }

    public void CloseEmailMenu() {
        // Destroy the loaded emails
        for (int i = 0; i < LoadedEmails.Count; i++) {
            if (LoadedEmails[i] != null) {
                Destroy(LoadedEmails[i]);
            }
        }
        LoadedEmails = null;
    }

    
}
