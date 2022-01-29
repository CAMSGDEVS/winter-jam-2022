using UnityEngine;

public class RenderLine : MonoBehaviour {

    [SerializeField] private LineRenderer lineRenderer;
    public GameObject startPoint;
    public GameObject endPoint;

    private Vector3 endPosition;

    private void Awake() {
        lineRenderer.alignment = LineAlignment.View;
        endPoint.transform.position = Vector3.Scale(
            EmailManager.Instance.emailDisplay.center.transform.position,
            new Vector3(1280 / 720 * 11.547f / 1280f / 2, 11.547f / 1280f / 2)
        );

        endPosition = endPoint.transform.position;
    }

    private void Update() {
        Vector3[] positions = { startPoint.transform.position, endPosition };
        lineRenderer.SetPositions(positions);
    }

}
