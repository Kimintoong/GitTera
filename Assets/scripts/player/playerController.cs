using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class playerController : MonoBehaviour
{
    public PlayerInputControl inputControl;
    public Vector2 inputDirection;
    public float speed;
    public Rigidbody2D rb;
    private void Awake()
        {
        inputControl = new PlayerInputControl();
        rb = GetComponent<Rigidbody2D>();
        }
    private void OnEnable()
    {
            inputControl.Enable();
    }
    private void OnDisable()
    {
            inputControl.Disable();
    }
    private void Update()
    {
        inputDirection = inputControl.Gameplay.Move.ReadValue<Vector2>();
    }
    private void FixedUpdate() 
    {
        Move();
    }
    public void Move() 
    {
        rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, inputDirection.y * speed * Time.deltaTime);
    }
}
