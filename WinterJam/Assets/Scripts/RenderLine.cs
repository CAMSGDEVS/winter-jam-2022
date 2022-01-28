using UnityEngine;

public class RenderLine : MonoBehaviour {

    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private GameObject startPoint, endPoint;

    private Vector3 endPosition;

    private void Start() {
        lineRenderer.alignment = LineAlignment.View;
        endPosition = endPoint.transform.position;
    }

    private void Update() {
        Vector3[] positions = { startPoint.transform.position, endPosition };
        lineRenderer.SetPositions(positions);
    }

}
