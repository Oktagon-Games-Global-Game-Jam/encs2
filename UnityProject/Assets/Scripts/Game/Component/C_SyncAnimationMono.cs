using Unity.Entities;

[GenerateAuthoringComponent]
public struct C_SyncAnimationMono : IComponentData
{
    public int Id;
    public E_State State;
}

public enum E_State
{
    Walking,
    Attacking,
    Win
}