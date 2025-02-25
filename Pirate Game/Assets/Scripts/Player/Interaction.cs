using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] private Transform boat;
    [SerializeField] private Animator animator;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (PlayerInfo.canRow)
            {
                PlayerInfo.canMove = !PlayerInfo.canMove;
                PlayerInfo.isRowing = !PlayerInfo.isRowing;
            }
        }        

        if (PlayerInfo.isRowing)
        {
            transform.position = new Vector3(boat.position.x, boat.position.y + 0.5f, boat.position.z - 4);
            if (Input.GetAxisRaw("Horizontal") > 0 || Input.GetAxisRaw("Vertical") > 0)
            {
                animator.SetBool("isMoving", true);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }
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
