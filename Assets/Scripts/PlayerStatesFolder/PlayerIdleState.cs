using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdleState : PlayerState
{
    private Rigidbody2D rb;
    public Vector2 moveInput;
    public Animator animator;
    public PlayerController player;
    public override void EnterState(PlayerController player)
    {
        // Safe animation - only plays if everything is set up
        //TryPlayAnimation(player, "Idle");
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        if (context.canceled)
        {
            player.ChangeState(new PlayerIdleState());
        }
        else
        {
            player.ChangeState(new PlayerMovingState());

            //if (Input.GetButton("Fire"))
            //{
            //player.HandleShooting();
            //}
        }
    }

    public override void UpdateState(PlayerController player)
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        player.animator.Play("Idle");

        //if (horizontal < 0)
            
            //player.animator.Play("IdleLeft");
        //else if (horizontal > 0)
            
            //player.animator.Play("IdleRight");

        //if (vertical < 0)
            //player.animator.Play("IdleDown");
        //else if (vertical > 0)
            //player.animator.Play("IdleUp");


        //if (Mathf.Abs(Input.GetAxis("Horizontal")) > .2f)
        //if (Mathf.Abs(horizontal) > 0.1f)
        //{
            //player.ChangeState(new PlayerMovingState());
        //}
        //if (Mathf.Abs(Input.GetAxis("Vertical")) > .2f)
        //if (Mathf.Abs(vertical) > 0.1f)
        //{
            //player.ChangeState(new PlayerMovingState());
        //}

    }

    public override void ExitState(PlayerController player) { }

    public override string GetStateName() => "Idle";

    // Safe animation helper
    private void TryPlayAnimation(PlayerController player, string animName)
    {
        if (player.animator != null &&
            player.animator.runtimeAnimatorController != null &&
            player.animator.isActiveAndEnabled)
        {
            try
            {
                player.animator.Play(animName);
            }
            catch
            {
                // Animation doesn't exist - that's okay, continue without it
            }
        }
    }
}
