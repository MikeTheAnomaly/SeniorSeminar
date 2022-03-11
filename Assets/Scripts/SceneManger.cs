using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManger : MonoBehaviour
{
    public void LoadScene(int num)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(num);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
    }
}
