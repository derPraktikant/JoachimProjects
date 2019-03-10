using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelHandler : MonoBehaviour
{
    public int currentLevel = 0;
    public int latestLevelCompleted = 0;

    public GameObject LevelFinished;

    // Start is called before the first frame update
    void Start()
    {
        LevelFinished.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        LevelFinished.SetActive(true);
        Time.timeScale = 0;
    }
}
