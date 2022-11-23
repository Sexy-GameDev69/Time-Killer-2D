using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    public Rigidbody2D RB;

    public Vector2 mousePosition;
    public Camera sceneCamera;

    private float horizontal;

    public Weapon weapon;

    [SerializeField] KeyCode Jump = KeyCode.Space;
    public bool IsGrounded;
    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(Jump) && IsGrounded == true)
        {
            jump();
        }
        mousePosition = sceneCamera.ScreenToWorldPoint(Input.mousePosition);
        if(Input.GetMouseButtonDown(0))
        {
            weapon.Fire();
        }
    }

    private void FixedUpdate()
    {
        RB.velocity = new Vector2(horizontal * Speed, RB.velocity.y);
        Vector2 aimDirection = mousePosition - RB.position;
        float aimAngle = Mathf.Atan2(aimDirection.y,aimDirection.x)*Mathf.Rad2Deg-90f;
        RB.rotation = aimAngle;
    }

    public void jump()
    {
        RB.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        IsGrounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsGrounded = true;
        }
        if (collision.gameObject.CompareTag("KillPlayer"))
        {
            Debug.Log("Player Dead");
            Application.Quit();
        }
    }
}
