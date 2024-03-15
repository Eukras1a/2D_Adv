using UnityEngine;
public class BoarChaseState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.chaseSpeed;
        currentEnemy.anim.SetBool("run", true);
    }

    public override void OnExit()
    {
        currentEnemy.anim.SetBool("run", false);
        currentEnemy.lostTimer = currentEnemy.lostTime;
    }

    public override void OnFixUpdate()
    {
    }

    public override void OnUpdate()
    {
        if (currentEnemy.lostTimer <= 0)
        {
            currentEnemy.SetState(EnemyState.Patrol);
        }
        if (!currentEnemy.physicsCheck.isGround || (currentEnemy.physicsCheck.TouchLeftWall && currentEnemy.currentDir.x < 0) || (currentEnemy.physicsCheck.TouchRightWall && currentEnemy.currentDir.x > 0))
        {
            currentEnemy.transform.localScale = new Vector3(currentEnemy.currentDir.x, 1, 1);
        }
    }
}
