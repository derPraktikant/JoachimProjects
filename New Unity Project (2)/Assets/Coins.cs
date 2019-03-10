using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    bool triggerEntered = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CoinRotation();
    }

    void CoinRotation()
    {
        transform.Rotate(Vector3.forward, 250.0f * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !triggerEntered)
        {
            triggerEntered = true;
            other.GetComponent<PowerUp>().ExtraJump();
            Destroy(gameObject);
        }
    }

}
