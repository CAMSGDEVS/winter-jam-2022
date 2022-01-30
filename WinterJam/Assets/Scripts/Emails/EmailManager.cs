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

    public EmailDisplay emailDisplay;
    public RenderLine renderLine;

    private void Awake() {
        if (_instance != null) {
            Destroy(this);
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
        LoadEmailsFromJson();
    }

    public void Init(int emailId = 1) {
        OverwriteEmail(emailId, emailDisplay);
        emailDisplay.Init();
        renderLine.Init();
    }

    // Load email data from json resource file
    public void LoadEmailsFromJson() {
        Emails = JsonUtility.FromJson<EmailWrapper>(
            Resources.Load<TextAsset>("emails").text
        );
    }

    public void OverwriteEmail(int emailId, EmailDisplay display) {
        display.emailData = Emails.emails[emailId];
        display.LoadFromData();
    }
}
