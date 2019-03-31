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
        if (movement.enabled == true)
        {
            Move();
        }
    }

    void Move()
    {
        float xDirection = SimpleInput.GetAxis("Horizontal");
        float yDirection = SimpleInput.GetAxisRaw("Vertical");
        movement.Move(xDirection, yDirection);
        direction.CheckDirection(xDirection);
    }
}
