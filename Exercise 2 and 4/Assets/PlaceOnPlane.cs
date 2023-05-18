using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem;

public class PlaceOnPlane : MonoBehaviour
{
    public GameObject instantiatedObject;
    public GameObject placedPrefab;
    Pose addedPose;
    public ARRaycastManager rayManager;
    Vector2 touchPos;
    List<ARRaycastHit> s_hits = new List<ARRaycastHit>();
    public void AddObject(InputAction.CallbackContext context)
    {
        touchPos = context.ReadValue<Vector2>();
        if (rayManager.Raycast(touchPos, s_hits, TrackableType.PlaneWithinPolygon))
        {
            // Raycast hits are sorted by distance, so the first one
            // will be the closest hit.
            addedPose = s_hits[0].pose;
            if (instantiatedObject == null)
            {
                instantiatedObject = Instantiate(placedPrefab, addedPose.position, addedPose.rotation);
            }
            else
            {
                instantiatedObject.transform.position = addedPose.position;
            }
        }
    }
}
