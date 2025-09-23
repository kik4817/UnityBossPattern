using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetPlayerToTarget", story: "Find player to [TargetLocation] .", category: "Action/Find", id: "b0323d7c657ff670b07f331593ba8e13")]
public partial class SetPlayerToTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<Vector3> TargetLocation;

    protected override Status OnStart()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.Log("�±װ� Player�� ���谡 �Ǿ����� �ʽ��ϴ�.");
            return Status.Failure;
        }

        else
        {
            TargetLocation.Value = player.transform.position;
            return Status.Success;
        }
    }
}

