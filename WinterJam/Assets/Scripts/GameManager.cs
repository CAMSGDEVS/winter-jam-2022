using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance {
        get {
            if (_instance == null) {
                Debug.Log("GameManager is null");
            }
            return _instance;
        }
    }

    public static int EnvironmentScore = 50;
    public static int treesDestroyed = 0;
    public static int protestersKilled = 0;
    public static int year = 0;

    public GameObject IsometricView, GlobeView;

    [SerializeField] private Endings endings;
    public GenerateTiles GenerateTiles;

    public void ChangeYear(int year) {
        if (year < 5) {
            IsometricView.SetActive(false);
            GlobeView.SetActive(true);
            EmailManager.Instance.Init(year * 2 + Random.Range(0, 2));
        } else {
            GlobeView.SetActive(false);
            IsometricView.SetActive(false);
            endings.BadEnding();
        }
    }

    public void LoadIsometric() {
        GlobeView.SetActive(false);
        IsometricView.SetActive(true);
        GenerateTiles.Reset();
    }

    private void Awake() {
        if (_instance != null) {
            Destroy(this);
        } else {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start() {
        ChangeYear(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
