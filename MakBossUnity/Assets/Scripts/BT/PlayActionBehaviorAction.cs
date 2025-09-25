using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "PlayActionBehavior", story: "play [ActionBeahavior] .", category: "Action/Patern", id: "c5eed272e286a52bafed33b932b9104f")]
public partial class PlayActionBehaviorAction : Action
{
    [SerializeReference] public BlackboardVariable<ActionBehavior> ActionBeahavior;

    protected override Status OnStart()
    {
        ActionBeahavior.Value.OnStart();
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        // 패턴이 성공할 때 까지
        if (ActionBeahavior.Value.IsPatternEnd)
        {
            return Status.Success;
        }

        else
        {
            ActionBeahavior.Value.OnUpdate();
            return Status.Running;
        }
    }

    protected override void OnEnd()
    {
        ActionBeahavior.Value.OnEnd();
    }
}

