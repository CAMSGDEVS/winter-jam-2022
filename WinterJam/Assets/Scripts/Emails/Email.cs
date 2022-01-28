using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Email
{
    public string Title { get; set; }
    public string Sender { get; set; }
    public string BodyText { get; set; }

    public int AcceptScore { get; set; }
    public int DenyScore { get; set; }
    public int IgnoreScore { get; set; }
}
