using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Joystick:")]
    [SerializeField] private Joystick joystick;
    
    [Header("Animation:")]
    [SerializeField] private CharacterAnimation characterAnimation;

    [Header("Mouse:")]
    private Vector3 firstMousePosition;
    private Vector3 currentMousePosition;
    private Vector3 direction;
    
    [Header("Player Properties:")]
    [SerializeField] private Player player;
    [SerializeField] private float speed;
    public Rigidbody rb;


    void Update()
    {
        if (LevelManager.instance.isGaming == false)
        {
            return;
        }
        if (player.isDead == true)
        {
            UIManager.instance.HideJoystick();
            return;
        }
        if (Input.GetMouseButtonDown(0)) 
        {
            firstMousePosition = Input.mousePosition;
            joystick.transform.position = firstMousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            currentMousePosition = Input.mousePosition;
            direction = currentMousePosition - firstMousePosition;
            joystick.joystickHandle.transform.position = currentMousePosition;
            if (Vector3.Distance(joystick.joystickHandle.transform.position, joystick.joystickBackground.transform.position) > joystick.joystickRadius)
            {
                joystick.joystickHandle.transform.position = joystick.joystickBackground.transform.position - (joystick.joystickBackground.transform.position - joystick.joystickHandle.transform.position).normalized * joystick.joystickRadius;
            }
            if (Vector3.Distance(joystick.joystickHandle.transform.position, joystick.joystickBackground.transform.position) > joystick.joystickRadius / 2)
            {
                transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.y));
                rb.velocity = new Vector3(direction.x, 0, direction.y).normalized * speed;
                characterAnimation.ChangeAnim(Constant.RUN);
            }
            player.isMoving = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            characterAnimation.ChangeAnim(Constant.IDLE);
            joystick.transform.position += new Vector3(10000, 0, 0);//hide joystick
            rb.velocity = new Vector3(0, 0, 0);
            player.isMoving = false;
        }
        
    }
}
