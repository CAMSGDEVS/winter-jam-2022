using UnityEngine;

public class RotateEarth : MonoBehaviour {

    [SerializeField] private float speed = 10;

    private void Update() {
        transform.Rotate(Vector3.up * Time.deltaTime * speed);
    }

}
