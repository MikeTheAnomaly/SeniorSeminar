using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;
using UnityEngine.UI;
public class TourARSession : MonoBehaviour
{
    public ARSessionOrigin SessionOrigin;
    public ARTrackedImageManager TrackedImageManager;

    public GameObject p_WorldOverlay;

    private GameObject curOverlay;
    private ARTrackedImage curTracked;

    public UnityEvent OnTrackingStarted = new UnityEvent();
    public UnityEvent OnTrackingStopped = new UnityEvent();

    /// <summary>
    /// debug tracked object
    /// </summary>
    public Button xrot;
    public Button yrot;
    public Button zrot;
    public TMP_Text debugText;

    private void Start()
    {
        curOverlay = GameObject.Instantiate(p_WorldOverlay);
        curOverlay.SetActive(false);

        xrot.onClick.AddListener(() =>
        {
            curOverlay.transform.rotation = Quaternion.Euler(curOverlay.transform.rotation.eulerAngles.x + 90, curOverlay.transform.rotation.eulerAngles.y, curOverlay.transform.rotation.eulerAngles.z);
            DebugUpdate();
        });
        
        yrot.onClick.AddListener(() => { curOverlay.transform.rotation = Quaternion.Euler(curOverlay.transform.rotation.eulerAngles.x, curOverlay.transform.rotation.eulerAngles.y + 90, curOverlay.transform.rotation.eulerAngles.z); DebugUpdate(); });
        zrot.onClick.AddListener(() => { curOverlay.transform.rotation = Quaternion.Euler(curOverlay.transform.rotation.eulerAngles.x, curOverlay.transform.rotation.eulerAngles.y, curOverlay.transform.rotation.eulerAngles.z + 90); DebugUpdate(); });
    }

    private void DebugUpdate()
    {
        debugText.text = $"obj: x:{curOverlay.transform.rotation.eulerAngles.x} y:{curOverlay.transform.rotation.eulerAngles.y} z:{curOverlay.transform.rotation.eulerAngles.z}";
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
