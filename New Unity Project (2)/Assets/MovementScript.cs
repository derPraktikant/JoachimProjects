using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    private Quaternion playerRotation;
    private float height;
    public int maxJumps = 2;
    public int jumps = 0;
    public int floorCount = 0;
    public Camera mainCam;
    public bool shouldLerp;
    public GameObject Character;
    public Rigidbody RB;
    public Animator AnimatorPlayer;
    public float timeStartedLerping;
    public Vector3 endPosition;
    public Vector3 startPosition;
    public float lerpTime = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        height = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        Animations();
        if (shouldLerp)
        {
            mainCam.transform.localPosition = new Vector3(mainCam.transform.localPosition.x, Lerp(startPosition, endPosition, timeStartedLerping, lerpTime).y, mainCam.transform.localPosition.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Terrain")
        {
            AnimatorPlayer.SetBool("isFalling", false);
            StartLerping(1);
            AnimatorPlayer.SetTrigger("landingTrigger");
            floorCount++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Terrain")
        {
            floorCount--;
            jumps = maxJumps - 1;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Terrain")
        {
            jumps = maxJumps;
        }
    }

    private void PlayerMovement()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.position = new Vector3(transform.position.x - 5.0f * Time.deltaTime, transform.position.y, transform.position.z);
            if (Character.transform.rotation.y != -90)
            {
                playerRotation = Quaternion.Euler(new Vector3(0, -90, 0));
                Character.transform.rotation = playerRotation;
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position = new Vector3(transform.position.x + 5.0f * Time.deltaTime, transform.position.y, transform.position.z);
            if (Character.transform.rotation.y != 90)
            {
                playerRotation = Quaternion.Euler(new Vector3(0, 90, 0));
                Character.transform.rotation = playerRotation;
            }
        }
        if (Input.GetKeyDown(KeyCode.W) && jumps > 0)
        {
            RB.AddForce(new Vector3(0, 200, 0));
            jumps--;
            AnimatorPlayer.SetTrigger("jumpTrigger");
        }
    }

    void Animations()
    {
        if(Input.GetKey(KeyCode.A) ^ Input.GetKey(KeyCode.D))
        {
            AnimatorPlayer.SetBool("isWalking", true);
        }
        else if (Input.GetKey(KeyCode.A) == Input.GetKey(KeyCode.D))
        {
            AnimatorPlayer.SetBool("isWalking", false);
        }

        if(transform.position.y < height && floorCount <= 0)
        {
            AnimatorPlayer.SetBool("isFalling", true);
            StartLerping(0);
        }
    }

    private void LateUpdate()
    {
        height = transform.position.y;
    }

    public Vector3 Lerp(Vector3 start, Vector3 end, float timeStartedLerping, float lerpTime)
    {
        float timeSinceStarted = Time.time - timeStartedLerping;
        float percentageComplete = timeSinceStarted / lerpTime;
        if (percentageComplete > 1)
        {
            shouldLerp = false;
        }

        var result = Vector3.Lerp(start, end, percentageComplete);

        return result;
    }

    private void StartLerping(float newYValue)
    {
        timeStartedLerping = Time.time;
        endPosition = new Vector3(0, newYValue, 0);
        startPosition = mainCam.transform.localPosition;
        shouldLerp = true;
    }
}
