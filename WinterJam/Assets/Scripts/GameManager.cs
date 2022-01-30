using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("GameManager is null");
            }
            return _instance;
        }
    }

    public static int EnvironmentScore = 50;
    public static int treesDestroyed = 0;
    public static int protestersKilled = 0;
    public static int year = 0;
    public static int currentEmailId = 0;
    public static int previousEmailId = 0;
    public static bool emailDeclined = false;

    public GameObject IsometricView, GlobeView;

    [SerializeField] private Endings endings;
    public MachineMovement MachineMovement;

    public void ChangeYear(int year)
    {
        if (year < 5)
        {
            IsometricView.SetActive(false);
            GlobeView.SetActive(true);
            previousEmailId = currentEmailId;
            EmailManager.Instance.Replying = year != 0 && !emailDeclined ? true : false;
            currentEmailId = year * 2 + Random.Range(0, 2);
            EmailManager.Instance.Init(year == 0 ? currentEmailId : previousEmailId);
        }
        else
        {
            GlobeView.SetActive(false);
            IsometricView.SetActive(false);
            endings.BadEnding("Trees Destroyed: " + treesDestroyed + "Proesters Killed: " + protestersKilled + "Planet Improvement Factor: " + EnvironmentScore);
        }
    }

    public void LoadIsometric()
    {
        GlobeView.SetActive(false);
        IsometricView.SetActive(true);
        MachineMovement.Restart();
    }

    private void Awake() {
        _instance = this;
    }

    // Start is called before the first frame update
    public void Start() {
        year = 0;
        ChangeYear(0);
    }
}
