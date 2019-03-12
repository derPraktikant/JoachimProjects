using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject Character;
    public float yOffset, zOffset;

    private Vector3 offsetV3;

    // Start is called before the first frame update
    void Start()
    {
        offsetV3 = new Vector3(0, yOffset, zOffset);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Character.transform.position + offsetV3;
    }
}
