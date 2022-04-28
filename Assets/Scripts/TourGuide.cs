using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TourGuide : MonoBehaviour
{
    public PlayableDirector tourDirector;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartTour()
    {
        tourDirector.Play();
    }

    public void pause()
    {
        tourDirector.Pause();
    }
}
