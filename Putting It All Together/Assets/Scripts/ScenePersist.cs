using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersist : MonoBehaviour
{
    int startingSceneIndex;

    private void Awake()
    {
        int numScenePersits = FindObjectsOfType<ScenePersist>().Length;
        if (numScenePersits > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

    }
    void Start()
    {
        startingSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    void Update()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex != startingSceneIndex)
        {
            Destroy(gameObject);
        }
    }
}
