using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TourARSession : MonoBehaviour
{
    public ARSessionOrigin SessionOrigin;
    public ARTrackedImageManager TrackedImageManager;

    public GameObject p_WorldOverlay;

    private GameObject curOverlay;
    private ARTrackedImage curTracked;

    public UnityEvent OnTrackingStarted = new UnityEvent();
    public UnityEvent OnTrackingStopped = new UnityEvent();

    private void Start()
    {
        curOverlay = GameObject.Instantiate(p_WorldOverlay);
        curOverlay.SetActive(false);
    }

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

        foreach (ARTrackedImage removedImage in eventArgs.removed)
        {
            if (curTracked != null && curTracked == removedImage)
            {
                Debug.Log("Tracked image removed");
                TrackingLost();
                curTracked = null;
            }
        }

        foreach (ARTrackedImage newImage in eventArgs.added)
        {
            if(curTracked == null)
            {
                Debug.Log("image found curtrack set");
                curTracked = newImage;
                newImage.destroyOnRemoval = false;
                TrackingFound();
                break;
            }
            else
            {
                break;
            }
        }

        foreach (ARTrackedImage updatedImage in eventArgs.updated)
        {

        }

        
    }

    private void TrackingFound()
    {
        if(curTracked != null)
        {
            Debug.Log("tracking found");
            curOverlay.transform.SetParent(curTracked.transform);
            curOverlay.transform.localPosition = new Vector3(0,0, 0);
            curOverlay.transform.localRotation = Quaternion.identity;
            curOverlay.SetActive(true);
            OnTrackingStarted.Invoke();
        }
    }

    private void TrackingLost()
    {
        Debug.Log("tracking lost");
        curOverlay.transform.SetParent(null);
        GameObject.Destroy(curTracked.gameObject);
        curOverlay.SetActive(false);
        OnTrackingStopped.Invoke();
    }
}
