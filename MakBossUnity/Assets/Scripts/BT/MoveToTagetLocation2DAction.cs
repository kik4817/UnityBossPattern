using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "MoveToTagetLocation2D", story: "[Self] move to [TargetLocation] .", category: "Action/Navigation", id: "c9abc091cb5e6797ab3942402512707d")]
public partial class MoveToTagetLocation2DAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<Vector3> TargetLocation;
    [SerializeReference] public BlackboardVariable<float> Speed;
    [SerializeReference] public BlackboardVariable<float> StoppingDistance;
    //Animator animator;
    Rigidbody2D rigidbody2D;

    const string run = "IsRun";

    // Animator를 접근을 해서. SetBool 이동하라. Self GameObject Animator를 가져와서, animator 변수의 저장을 하고, Update true, Success, false
    
    protected override Status OnStart()
    {
        if (Vector3.Distance(Self.Value.transform.position, TargetLocation.Value) < StoppingDistance)
        {
            return Status.Success;
        }        

        // 몬스터에 rigidbody2d없으면 Status를 failure로 만들어주세요
        if(Self.Value.TryGetComponent<Rigidbody2D>(out Rigidbody2D rigid))
        {
            rigidbody2D = rigid;
            return Status.Running;
        }

        else 
        {
            return Status.Failure;
        }


        //Self.Value.transform.position; // 머쉬룸에 위치
        //TargetLocation.Value // 비교
    }

    protected override Status OnUpdate()
    {
        if (Vector3.Distance(Self.Value.transform.position, TargetLocation.Value) < StoppingDistance)
        {
            //animator.SetBool(run, true);
            //Self.Value.gameObject.GetComponent<Animator>();

            rigidbody2D.linearVelocity = Vector2.zero;

            return Status.Success;
        }

        else
        {
            rigidbody2D.linearVelocity = (TargetLocation.Value - Self.Value.transform.position) * Speed.Value; //normalized로 하면 일정한 속도

            return Status.Running;
        }            
    }
}

