using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Declaration ONLY - Start

    bool isOnGround = true;
    float JumpForce = 10.0f;
    float gravityModifier = 2.0f;
    float boundaryLimit = 20.0f;
    float speed = 10.0f;

    Rigidbody playerRb;
    Renderer playerRdr;

    public Material[] playerMtrs;

    //Declaration ONly - End

    // Start is called before the first frame update
    void Start()
    {
        isOnGround = true;
        Physics.gravity *= gravityModifier;

        playerRb = GetComponent<Rigidbody>();
        playerRdr = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.forward * Time.deltaTime * verticalInput * speed);
        transform.Translate(Vector3.right * Time.deltaTime * horizontalInput * speed);

        //Front & Back Border
        if(transform.position.z < -boundaryLimit)
        {
            transform.position = new Vector3(transform.position.x , transform.position.y, -boundaryLimit);
            playerRdr.material.color = playerMtrs[2].color;
        }
        else if(transform.position.z > boundaryLimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, boundaryLimit);
            playerRdr.material.color = playerMtrs[3].color;
        }

        //Left & Right Border
        if (transform.position.x < -boundaryLimit)
        {
            transform.position = new Vector3(-boundaryLimit,transform.position.y, transform.position.z);
            playerRdr.material.color = playerMtrs[4].color;
        }
        else if (transform.position.x > boundaryLimit)
        {
            transform.position = new Vector3(boundaryLimit,transform.position.y, transform.position.z);
            playerRdr.material.color = playerMtrs[5].color;
        }

        PlayerJump();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Listen for the collision with the GamePlane TAG

        if(collision.gameObject.CompareTag("GamePlane"))
        {
            isOnGround = true;

            playerRdr.material.color = playerMtrs[1].color;
        }
    }

    private void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            isOnGround = false;

            playerRdr.material.color = playerMtrs[0].color;
        }
    }
}
