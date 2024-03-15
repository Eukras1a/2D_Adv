using UnityEngine;
public class BoarPatrolState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.normalSpeed;
        currentEnemy.anim.SetBool("walk", false);
    }

    public override void OnUpdate()
    {
        if (currentEnemy.FindPlayer())
        {
            currentEnemy.SetState(EnemyState.Chase);
        }
        if (!currentEnemy.physicsCheck.isGround || (currentEnemy.physicsCheck.TouchLeftWall && currentEnemy.currentDir.x < 0) || (currentEnemy.physicsCheck.TouchRightWall && currentEnemy.currentDir.x > 0))
        {
            if (!currentEnemy.wait)
            {
                currentEnemy.wait = true;
                currentEnemy.anim.SetBool("walk", false);
                currentEnemy.rb.velocity = Vector2.zero;
            }
        }
        else
        {
            currentEnemy.anim.SetBool("walk", true);
        }
    }
    public override void OnFixUpdate()
    {
    }
    public override void OnExit()
    {
        currentEnemy.anim.SetBool("walk", false);
    }
}
