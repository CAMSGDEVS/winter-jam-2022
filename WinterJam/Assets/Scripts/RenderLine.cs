using UnityEngine;
using System.Collections;

public class RenderLine : MonoBehaviour {

    [SerializeField] private LineRenderer lineRenderer;
    public GameObject[] startPoints;
    public GameObject endPoint;

    private Vector3 endPosition;
    private int startPointIndex;

    private void Awake() {
        EmailManager.Instance.renderLine = this;
        startPointIndex = Random.Range(0, startPoints.Length);
    }

    public void Init() {
        lineRenderer.alignment = LineAlignment.View;
        endPoint.SetActive(true);
        lineRenderer.enabled = false;
        endPoint.transform.position = Vector3.Scale(
            EmailManager.Instance.emailDisplay.center.transform.position,
            // 11.547 = screen height, scale location of email center by camera screen ratio at (0,0,0)
            new Vector3(1280 / 720 * 11.547f / 2 / 1280f, 11.547f / 2 / 1280f)
        );

        endPosition = endPoint.transform.position;
    }

    public void ChangeStartPoint() {
        startPointIndex = Random.Range(0, startPoints.Length);
    }

    private void Update() {
        Vector3[] positions = { startPoints[startPointIndex].transform.position, endPosition };
        lineRenderer.SetPositions(positions);
    }

    public void ToggleEnabled(bool enabled) {
        StartCoroutine(turnOn(enabled));
        if (!enabled) {
            endPoint.SetActive(false);
        }
    }

    private IEnumerator turnOn(bool enabled) {
        yield return new WaitForSeconds(0.5f);
        lineRenderer.enabled = enabled;
    }
}
