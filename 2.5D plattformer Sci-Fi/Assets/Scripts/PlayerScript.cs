using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float halfedPlayerSize;
    public float halfedPlayerWidth;
    public float jumpVelocity;
    public float betterFallFloat;
    public float maxSpeed;
    public float runningAcceleration;
    public float runningBreak;

    private Vector3 offsetLeftV3, offsetRightV3;
    private Vector3 jumpVelocityV3;
    private Vector3 betterFallForceV3;
    private Rigidbody playerRB;
    private float oldHeight;

    // Start is called before the first frame update
    void Start()
    {
        betterFallForceV3 = new Vector3(0, -betterFallFloat, 0);
        oldHeight = transform.position.y;
        playerRB = GetComponent<Rigidbody>();
        offsetLeftV3 = new Vector3(-halfedPlayerWidth, 0, 0);
        offsetRightV3 = new Vector3(halfedPlayerWidth, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        betterFalling();
        Debug.Log(isGrounded());
    }

    bool isGrounded()
    {
        return (Physics.Raycast(transform.position + offsetLeftV3, Vector3.down, halfedPlayerSize) || Physics.Raycast(transform.position + offsetRightV3, Vector3.down, halfedPlayerSize));
    }

    void Movement()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpVelocityV3 = playerRB.velocity;
            jumpVelocityV3.y += jumpVelocity;
            playerRB.velocity = jumpVelocityV3;
        }

        //Debug.Log(Input.GetAxisRaw("Horizontal"));
        if (Input.GetAxisRaw("Horizontal") != 0 && Mathf.Abs(playerRB.velocity.x) < maxSpeed)
        {
            playerRB.AddForce(new Vector3(Input.GetAxis("Horizontal") * runningAcceleration, 0, 0));
        }
        else if (Input.GetAxisRaw("Horizontal") == 0 && Mathf.Abs(playerRB.velocity.x) > 1)
        {
            playerRB.AddForce(new Vector3(Mathf.Sign(playerRB.velocity.x) * -runningBreak, 0, 0));
        }
    }

    void betterFalling()
    {
        if(oldHeight > transform.position.y)
        {
            playerRB.AddForce(betterFallForceV3);
        }
        oldHeight = transform.position.y;
    }
}
