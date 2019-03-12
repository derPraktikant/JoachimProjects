using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float halfedPlayerSize;
    public float jumpVelocity;
    public float betterFallFloat;
    public float maxSpeed;
    public float runningAcceleration;
    public float runningBreak;
    public Animator anim;

    private Vector3 jumpVelocityV3;
    private Vector3 allVelocityV3;
    private Vector3 betterFallForceV3;
    private Rigidbody playerRB;
    private float oldHeight;

    // Start is called before the first frame update
    void Start()
    {
        betterFallForceV3 = new Vector3(0, -betterFallFloat, 0);
        oldHeight = transform.position.y;
        playerRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        betterFalling();
        animationAndRotation();
        Debug.Log(isGrounded());
    }

    bool isGrounded()
    {
        return (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + halfedPlayerSize, transform.position.z), Vector3.down, halfedPlayerSize + 0.05f));
    }

    void Movement()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            jumpVelocityV3 = playerRB.velocity;
            jumpVelocityV3.y += jumpVelocity;
            playerRB.velocity = jumpVelocityV3;
        }

        //Debug.Log(Input.GetAxisRaw("Horizontal"));
        if (Input.GetAxisRaw("Horizontal") != 0) // && Mathf.Abs(playerRB.velocity.x) < maxSpeed)
        {
            playerRB.AddForce(new Vector3(Input.GetAxis("Horizontal") * runningAcceleration, 0, 0));
            allVelocityV3 = playerRB.velocity;
            if(Mathf.Abs(allVelocityV3.x) > maxSpeed && Input.GetAxisRaw("Horizontal") != 0)
            {
                //Debug.Log("exceeded speed limit");
                allVelocityV3.x = maxSpeed * Mathf.Sign(allVelocityV3.x);
                playerRB.velocity = allVelocityV3;
            }
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

    void animationAndRotation()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 90 * Mathf.Sign(playerRB.velocity.x), 0));
        }
        anim.SetFloat("MoveSpeed", Mathf.Abs(playerRB.velocity.x) / maxSpeed, 0.1f, Time.deltaTime);
        anim.SetBool("Grounded", isGrounded());
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            anim.SetTrigger("Jump");
        }
    }
}
