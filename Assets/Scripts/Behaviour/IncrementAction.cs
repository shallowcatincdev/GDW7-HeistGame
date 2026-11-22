using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Increment", story: "Increments [Int] By [Amount]", category: "Action", id: "ac8d94cf23b417b873ae6dcd5db65c3a")]
public partial class IncrementAction : Action
{
    [SerializeReference] public BlackboardVariable<int> Int;
    [SerializeReference] public BlackboardVariable<int> Amount;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (Int != null && Amount != null)
        {
            Int -= Amount;
            return Status.Success;
        }

        return Status.Failure;
    }

    protected override void OnEnd()
    {
    }
}

