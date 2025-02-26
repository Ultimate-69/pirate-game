using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Interaction : MonoBehaviour
{
    [SerializeField] private Transform boat;
    [SerializeField] private Animator animator;
    [SerializeField] private float normalMoveSpeed;
    [SerializeField] private float groundMoveSpeed;
    [SerializeField] private AudioSource rowSound;
    [SerializeField] private LayerMask water;

    private float moveSpeed;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (PlayerInfo.canRow)
            {
                PlayerInfo.canMove = !PlayerInfo.canMove;
                PlayerInfo.isRowing = !PlayerInfo.isRowing;
            }
            if (PlayerInfo.isRowing)
            {
                transform.parent = boat;
                transform.localPosition = new Vector3(0, 15.5f, -40);
            }
            else
            {
                transform.parent = null;
            }
        }        

        if (PlayerInfo.isRowing)
        {
            //transform.position = new Vector3(boat.position.x, boat.position.y + 2f, boat.position.z - 4);
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                if (!rowSound.isPlaying)
                {
                    rowSound.Play();
                }
                animator.SetBool("isMoving", true);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    void FixedUpdate()
    {
        if (PlayerInfo.isRowing)
        {
            bool onWater = Physics.Raycast(boat.transform.position, Vector3.down, 1.5f, water);
            if (onWater)
            {
                moveSpeed = normalMoveSpeed;
            }
            else
            {
                moveSpeed = groundMoveSpeed;
            }
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                boat.transform.Rotate(0, Input.GetAxisRaw("Horizontal"), 0);
            }
            boat.position += boat.transform.forward * Input.GetAxisRaw("Vertical") * moveSpeed * Time.fixedDeltaTime;
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BoatEnter"))
        {
            PlayerInfo.canRow = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("BoatEnter"))
        {
            PlayerInfo.canRow = false;
        }
    }
}
