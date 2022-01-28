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

    private void Awake() {
        if (_instance != null) {
            Destroy(this);
        } else {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
