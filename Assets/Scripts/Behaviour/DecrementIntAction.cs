using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Decrement Int", story: "Decrements [Value] By [Amount]", category: "Action", id: "2eb0109bbd2372ca286becbd21491553")]
public partial class DecrementIntAction : Action
{
    [SerializeReference] public BlackboardVariable<int> Value;
    [SerializeReference] public BlackboardVariable<int> Amount;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if(Value != null && Amount != null)
        {
            Value -= Amount;
            return Status.Success;
        }

        return Status.Failure;
    }

    protected override void OnEnd()
    {
    }
}

