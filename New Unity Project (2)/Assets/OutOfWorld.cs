using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfWorld : MonoBehaviour
{
    public GameObject GameOver;

    // Start is called before the first frame update
    void Start()
    {
        GameOver.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Time.timeScale = 0;
        GameOver.SetActive(true);
    }
}
