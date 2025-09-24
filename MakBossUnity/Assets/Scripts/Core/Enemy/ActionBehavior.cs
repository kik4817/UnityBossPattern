using UnityEngine;

public abstract class ActionBehavior : MonoBehaviour //abstract 추상화 하라
{
    public bool IsPatternEnd;

    public abstract void OnStart();
    public abstract void OnUpdate();
    public abstract void OnEnd();
}
