using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    /*
     * 
     *@author: Nevermore
     *@date: 2019.9.4
     *@description: 控制玩家角色的移动
     * 
     */


    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;


    private Rigidbody2D Player_Rigidbody2D;

    //移动
    public float runSpeed = 40f;
    private float horizontalMove = 0f;
    private float verticalMove = 0f;


    //人物朝向
    private bool Player_FacingRight = false;
    private bool Player_FacingLeft = false;
    private bool Player_FacingUp = false;
    private bool Player_FacingDown = false;

    //smoothdamp函数的一个固定变量
    private Vector3 velocityHorizontal = Vector3.zero;
    private Vector3 velocityVertical = Vector3.zero;



    private void Awake()
    {
        Player_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update () {

        PlayerMoveInput();

	}

    private void FixedUpdate()
    {
        PlayerMove();
    }

    private void PlayerMoveInput()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        verticalMove = Input.GetAxisRaw("Vertical") * runSpeed;
    }

    private void PlayerMove()
    {
        // Move the character by finding the target velocity
        Vector3 targetVelocityHorizontal = new Vector2(horizontalMove*Time.fixedDeltaTime * 10f, Player_Rigidbody2D.velocity.y);
        Vector3 targetVelocityVertical = new Vector2(Player_Rigidbody2D.velocity.x, verticalMove * Time.fixedDeltaTime * 10f);
        // And then smoothing it out and applying it to the character
        Player_Rigidbody2D.velocity = Vector3.SmoothDamp(Player_Rigidbody2D.velocity, targetVelocityHorizontal, ref velocityHorizontal, m_MovementSmoothing);
        Player_Rigidbody2D.velocity = Vector3.SmoothDamp(Player_Rigidbody2D.velocity, targetVelocityVertical, ref velocityVertical, m_MovementSmoothing);

        //因为动画没导入 先写个反转意思一下
        if (horizontalMove > 0 && !Player_FacingRight)
            Flip();
        if (horizontalMove < 0 && Player_FacingRight)
            Flip();
    }


    
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        Player_FacingRight = !Player_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    
}
