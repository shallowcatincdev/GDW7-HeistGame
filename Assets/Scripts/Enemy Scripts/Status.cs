using System;
using Unity.Behavior;

[BlackboardEnum]
public enum Status
{
	Idle,
	Patroling,
	Investigating,
	Hunting,
	Fighting
}

public enum Mode { stationary, patrol };
