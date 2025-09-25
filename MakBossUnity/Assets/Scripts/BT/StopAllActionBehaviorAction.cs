using System;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using System.Linq;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "StopAllActionBehavior", story: "[Self] stop all [ActionBehavior] .", category: "Action/Patern", id: "155d371f9d5e2c6f2b79e868880d3714")]
public partial class StopAllActionBehaviorAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<List<GameObject>> ActionBehavior;

    List<ActionBehavior> stopActions = new();
    protected override Status OnStart()
    {
        // 코드가 바뀔 때 마다 stopActions List에 데이터를 수거하고 있다.
        // 만약에 stopActions 데이터가 없으면? 찾아서 멈출  것들을 설정해라

        if (stopActions.Count <= 0)
        {
            foreach (var action in ActionBehavior.Value)
            {
                stopActions = action.GetComponents<ActionBehavior>().ToList();
            }
        }

        foreach(var action in stopActions)
        {
            action.OnStop();
        }

        Self.Value.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;

        return Status.Success;
    }
}

