using UnityEngine;

public abstract class ActionBehavior : MonoBehaviour //abstract �߻�ȭ �϶�
{
    public bool IsPatternEnd;

    public abstract void OnStart();
    public abstract void OnUpdate();
    public abstract void OnEnd();
}
