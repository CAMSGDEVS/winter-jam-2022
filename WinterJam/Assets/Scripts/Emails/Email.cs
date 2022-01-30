[System.Serializable]
public class Email
{
    public string Title;
    public string Sender;
    public string BodyText;

    public int AcceptScore;
    public int DenyScore;
    public int IgnoreScore;
    public bool Positive;
}