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

    private Vector3 prepos=Vector3.zero;


    //攻击
    private bool isAttack = false;


    //人物朝向
    private int Player_Facing = 0; //0,1,2,3,4   发呆，南，北，西，东

    //smoothdamp函数的一个固定变量
    private Vector3 velocityHorizontal = Vector3.zero;
    private Vector3 velocityVertical = Vector3.zero;

    //动画
    public Animator anim;



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
        RefreshAnimation();
        PlayerMove();
        
        Attack(ref isAttack);
    }

    private void PlayerMoveInput()
    {

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        verticalMove = Input.GetAxisRaw("Vertical") * runSpeed;

        if (verticalMove > 0)
            Player_Facing = 2;

        if (verticalMove < 0)
            Player_Facing = 1;

        if (horizontalMove > 0)
            Player_Facing = 4;

        if (horizontalMove < 0)
            Player_Facing = 3;


        //先写个向前攻击

        if (Input.GetKeyDown(KeyCode.X))
        {
            isAttack = true;
            print("#######3");
        }


    }

    private void PlayerMove()
    {
        prepos = gameObject.transform.position;
        // Move the character by finding the target velocity
        Vector3 targetVelocityHorizontal = new Vector2(horizontalMove*Time.fixedDeltaTime * 10f, Player_Rigidbody2D.velocity.y);
        Vector3 targetVelocityVertical = new Vector2(Player_Rigidbody2D.velocity.x, verticalMove * Time.fixedDeltaTime * 10f);
        // And then smoothing it out and applying it to the character
        Player_Rigidbody2D.velocity = Vector3.SmoothDamp(Player_Rigidbody2D.velocity, targetVelocityHorizontal, ref velocityHorizontal, m_MovementSmoothing);
        Player_Rigidbody2D.velocity = Vector3.SmoothDamp(Player_Rigidbody2D.velocity, targetVelocityVertical, ref velocityVertical, m_MovementSmoothing);
        if (prepos == gameObject.transform.position)
            Player_Facing = 0;
    }

    private void RefreshAnimation()
    {
        anim.SetInteger("Player_Facing", Player_Facing);
        anim.SetBool("isAttack", isAttack);
        
    }

    private void Attack(ref bool isAttack)
    {
        isAttack = false;
    }
    
    
    
}
