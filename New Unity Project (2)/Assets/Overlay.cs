using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overlay : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject GameOver;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(MainMenu.activeInHierarchy)
            {
                Time.timeScale = 1;
                MainMenu.SetActive(false);
                GameOver.SetActive(false);
            }
            else
            {
                Time.timeScale = 0;
                MainMenu.SetActive(true);
            }
        }
    }
}
