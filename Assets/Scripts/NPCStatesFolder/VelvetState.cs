using UnityEngine;

public abstract class VelvetState
{
    public abstract void EnterState(VelvetWurm velvet);
    public abstract void UpdateState(VelvetWurm velvet);
    public abstract void ExitState(VelvetWurm velvet);
    public abstract string GetStateName();
}