using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using ARLocation.Session;
using ARLocation;
using UnityEngine.XR.ARFoundation;
using ARLocation.Utils;

public class PosAdjustment : MonoBehaviour
{

    public Button SetAsRootLocation;
    public ARTrackedImageManager TrackedImageManager;
    public PlaceAtLocation locationObjectToUpdate;

    private void OnEnable()
    {
        Debug.Log("enable");
        TrackedImageManager.trackedImagesChanged += OnTrackedImageChange;
    }

    private void OnDisable()
    {
        Debug.Log("disable");
        TrackedImageManager.trackedImagesChanged -= OnTrackedImageChange;
    }

    private void OnTrackedImageChange(ARTrackedImagesChangedEventArgs eventArgs)
    {
        
        if(eventArgs.added.Count > 0)
        {
            Debug.Log("found image");
            UpdateGPSLocationFromWorldPos(eventArgs.added[0].transform.position);
        }

    }

    public void UpdateGPSLocationFromWorldPos(Vector3 worldPos)
    {
        Location location = ARLocationManager.Instance.GetLocationForWorldPosition(worldPos);
        if(location != null)
        {
            Debug.Log("location just updated from " + locationObjectToUpdate.Location.ToString() + " \n to" + location.ToString());
            locationObjectToUpdate.Location = location;
        }
        else
        {
            Debug.LogError("location was null for world pos");
        }
    }
}
