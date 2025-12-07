using UnityEngine;

public abstract class EnemyState
{
    public abstract void EnterState(Enemy enemy);
    public abstract void UpdateState(Enemy enemy);
    public abstract void ExitState(Enemy enemy);
    public abstract string GetStateName();
}