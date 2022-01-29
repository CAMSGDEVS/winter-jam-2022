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

    [SerializeField]
    private GameObject emailPrefab;

    public EmailDisplay emailDisplay;

    private void Awake() {
        if (_instance != null) {
            Destroy(this);
        }
        _instance = this;
    }

    // Load email data from json resource file
    public void LoadEmailsFromJson() {
        Emails = JsonUtility.FromJson<EmailWrapper>(
            Resources.Load<TextAsset>("emails").text
        );
    }

    public void Start() {
        DontDestroyOnLoad(gameObject);
        LoadEmailsFromJson();
    }

    public void OverwriteEmail(int emailId, EmailDisplay display) {
        display.emailData = Emails.emails[emailId];
        display.LoadFromData();
    }
}
