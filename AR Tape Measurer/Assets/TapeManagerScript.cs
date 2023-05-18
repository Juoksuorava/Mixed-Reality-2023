using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;

public class TapeManagerScript : MonoBehaviour
{
    ARRaycastManager raycastManager;
    public GameObject[] tapePoints;
    public float distanceBetweenPoints = 0f;
    public GameObject reticle;
    int currentTapePoint = 0;
    bool placementEnabled = true;
    public TMP_Text displayText;
    public LineRenderer tapeLine;

    // Start is called before the first frame update
    void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDistance();
        List<ARRaycastHit> hits = new List<ARRaycastHit> ();
        raycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.PlaneWithinPolygon);

        if (hits.Count > 0)
        {
            reticle.transform.position = hits[0].pose.position;
            reticle.transform.rotation = hits[0].pose.rotation;

            if (currentTapePoint == 1)
            {
                DrawLine();
            }
            if (!reticle.activeInHierarchy && currentTapePoint < 2)
            {
                reticle.SetActive(true);
            }
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (currentTapePoint < 2)
                {
                    PlaceTapePoint(hits[0].pose.position, currentTapePoint);
                }
                placementEnabled = false;
            }
            else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                placementEnabled = true;
            }
        }
        else if (hits.Count == 0 || currentTapePoint == 2)
        {
            reticle.SetActive(false);
        }
        displayText.text = $"{distanceBetweenPoints} m";
    }

    private void UpdateDistance()
    {
        if (currentTapePoint == 0)
        {
            distanceBetweenPoints = 0f;
        }
        else if (currentTapePoint == 1)
        {
            distanceBetweenPoints = Vector3.Distance(tapePoints[0].transform.position, reticle.transform.position);
        }
        else if (currentTapePoint == 2)
        {
            distanceBetweenPoints = Vector3.Distance(tapePoints[0].transform.position, tapePoints[1].transform.position);
        }
    }

    public void PlaceTapePoint(Vector3 tapePointPosition, int tapePointIndex)
    {
        tapePoints[tapePointIndex].SetActive(true);
        tapePoints[tapePointIndex].transform.position = tapePointPosition;
        if (currentTapePoint == 1)
        {
            DrawLine();
        }
        currentTapePoint++;
    }

    void DrawLine()
    {
        tapeLine.enabled = true;
        tapeLine.SetPosition(0, tapePoints[0].transform.position);
        if (currentTapePoint == 1)
        {
            tapeLine.SetPosition(1, reticle.transform.position);

        }
        else if (currentTapePoint == 2)
        {
            tapeLine.SetPosition(1, tapePoints[1].transform.position);

        }
    }
}
