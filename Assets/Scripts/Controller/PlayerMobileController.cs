using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMobileController : MonoBehaviour
{
    [SerializeField] private TurtleTale.SessionData sessionData;
    [SerializeField] private GameObject JoyStickCanvas;

    private Movement movement;
    private Direction direction;
    private PlayerController playerController;
    private Vector3 targetPos;

    private void Awake()
    {
//#if UNITY_ANDROID || UNITY_IOS
        // If we're in an android or ios device
        movement = GetComponent<Movement>();
        direction = GetComponent<Direction>();
        playerController = GetComponent<PlayerController>();
        playerController.enabled = false;

        JoyStickCanvas.SetActive(true);
        enabled = true;
//#else
//        JoyStickCanvas.SetActive(false);
//        enabled = false;
//#endif
    }


    private void Update()
    {
        if (movement.enabled == true && Input.GetMouseButtonDown(0))
        {
           CastRay();
        }

        /*
        if (movement.enabled == true)
        {
            Move();
        }
        */
    }

    void FixedUpdate()
    {
        if (movement.enabled == true)
        {
            // Move towards target position
            if (transform.position.x != targetPos.x)
            {
                //  animator.SetBool("isMoving", true);
                Move2();
                //transform.position = Vector3.MoveTowards(transform.position, targetPos, movement.xSpeed * Time.deltaTime);

                // Reached target destination
                if (transform.position.x == targetPos.x)
                {
                    //   animator.SetBool("isMoving", false);
                    Debug.Log("Reached target destination");
                }
            }
            // transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), transform.position.y, transform.position.z);
        }
    }

    void Move()
    {
        float xDirection = SimpleInput.GetAxis("Horizontal");
        float yDirection = SimpleInput.GetAxisRaw("Vertical");
        movement.Move(xDirection, yDirection);
        direction.CheckDirection(xDirection);
    }

    void Move2()
    {
     //   float xDirection = SimpleInput.GetAxis("Horizontal");
      //  float yDirection = SimpleInput.GetAxisRaw("Vertical");
        movement.TapToMove(targetPos.x, targetPos.y);
       // direction.CheckDirection(targetPos.x);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, movement.xSpeed * Time.deltaTime);
    }

    void CastRay()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPoint.z = Camera.main.transform.position.z;
        Ray ray = new Ray(worldPoint, new Vector3(0, 0, 1));
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

        if (hit)
        {
            targetPos = hit.point; // Set target pos
            direction.CheckDirection(targetPos.x); // Face target pos
            // Debug.Log("Target Hit:" + targetPos.x);
        }
    }
}
