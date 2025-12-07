using UnityEngine;

public class EnemyFlyState : EnemyState
{
    public override void EnterState(Enemy enemy)
    {
        // Safe animation - only plays if everything is set up
        enemy.animator.Play("Poofanim");
        enemy.animator.SetTrigger("Death");
        
    }

    public override void UpdateState(Enemy enemy)
    {
        enemy.animator.SetTrigger("Death");
        enemy.animator.Play("Poofanim");
        GameManager.Instance.AddScore(25);
    }

    public override void ExitState(Enemy enemy) 
    { Debug.Log("YOU ARE DEAD"); }

    public override string GetStateName() => "Death";

    // Safe animation helper
    private void TryPlayAnimation(Enemy enemy, string animName)
    {
        if (enemy.animator != null &&
            enemy.animator.runtimeAnimatorController != null &&
            enemy.animator.isActiveAndEnabled)
        {
            try
            {
                enemy.animator.Play(animName);
            }
            catch
            {
                // Animation doesn't exist - that's okay, continue without it
            }
        }
    }
}


