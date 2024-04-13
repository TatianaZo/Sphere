using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class TrajectoryPredictor : MonoBehaviour
{
    [SerializeField] private GameObject pointPref;
    [SerializeField]private int numOfPoints;
    [SerializeField] Transform hitMarker;
    [HideInInspector]public Vector3 direction;
    public float force;
    public float pointDistance = 0.1f;
    public bool isActive = false;
    LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = numOfPoints; 
    }

    public void AimForce(float force)
    {
        this.force = Mathf.Clamp(8+(force / 50), 4, 20);
    }

    // Update is called once per frame
    void Update()
    {
        float overlap;
        Vector3 pos = transform.position;
        Vector3 nextPos;

        if (!isActive)
        {
            lineRenderer.enabled = false;
            hitMarker.gameObject.SetActive(false);
            return;
        }
        else
        {
            lineRenderer.enabled = true; 
        }
        

        direction = transform.GetChild(0).transform.forward;

        for(int i = 0; i < numOfPoints; i++)
        {
            nextPos = Predict(i * pointDistance);
            overlap = Vector3.Distance(pos, nextPos)*1.1f;

            /*if(Physics.Raycast(pos, direction.normalized * force * (i*pointDistance), out var hit, overlap))
            {
                Debug.DrawRay(pos, direction.normalized * force * (i * pointDistance), Color.red, 1f);
                Debug.Log("попал");
                lineRenderer.SetPosition(i,nextPos);
                MoveHitMarker(hit);
                break;
            }*/

            hitMarker.gameObject.SetActive(false);
            pos = nextPos;
            lineRenderer.SetPosition(i, pos);
        }
    }

    private Vector3 Predict(float t)
    {
        Vector3 pos = transform.position + (direction.normalized * force * t) + (0.5f * Physics.gravity * (t * t));
        return pos;
    }

    private void MoveHitMarker(RaycastHit hit)
    {
        hitMarker.gameObject.SetActive(true);

        // Offset marker from surface
        float offset = 0.025f;
        hitMarker.position = hit.point + hit.normal * offset;
        hitMarker.rotation = Quaternion.LookRotation(hit.normal, Vector3.up);
    }
}
