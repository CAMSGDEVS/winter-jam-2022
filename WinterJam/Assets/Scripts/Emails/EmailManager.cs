using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmailManager : MonoBehaviour
{
    private static EmailManager _instance;
    public static EmailManager Instance { 
        get {
            if (_instance == null) {
                Debug.Log("Email Manager is null");
            }
            return _instance;
        }
    }

    public EmailWrapper Emails { get; set; }
    public EmailWrapper ResultEmails { get; set; }

    public EmailDisplay emailDisplay;
    public RenderLine renderLine;
    public bool Replying;

    private void Awake() {
        if (_instance != null) {
            Destroy(this);
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
        LoadEmailsFromJson();
    }

    public void Init(int emailId) {
        OverwriteEmail(emailId, emailDisplay);
        emailDisplay.Init();
        renderLine.Init();
    }

    // Load email data from json resource file
    public void LoadEmailsFromJson() {
        Emails = JsonUtility.FromJson<EmailWrapper>(
            Resources.Load<TextAsset>("emails").text
        );
        ResultEmails = JsonUtility.FromJson<EmailWrapper>(
            Resources.Load<TextAsset>("resultEmails").text
        );
    }

    public void OverwriteEmail(int emailId, EmailDisplay display) {
        if (!Replying) display.emailData = Emails.emails[emailId];
        else display.emailData = ResultEmails.emails[emailId];
        display.LoadFromData();
    }
}
