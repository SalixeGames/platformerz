using System;
using Godot;
using Platformerz.scripts.player;

namespace Platformerz.scripts.enemies;

[GlobalClass]
public partial class EnState : Resource
{
	[Export] public string Name;
	public EnStateMachine fsm;
	public BaseEnemy Controller;
	public Area2D Target;

	public virtual void Enter()
	{
		fsm.Controller.UpdateAnim(Name);
	}
	public virtual void Exit() {}

	public virtual void Ready(EnStateMachine stateMachine)
	{
	}

	public virtual EnState Update(float delta)
	{
		return this;
	}
	public virtual EnState PhysicsUpdate(float delta) 
	{
		return this;
	}

	public virtual EnState AnimationEnd(StringName animationName) 
	{
		return this;
	}

	public virtual EnState OnPlayerInVision(Area2D visionArea)
	{
		return this;
	}

	public virtual EnState OnPlayerOutVision(Area2D visionArea)
	{
		return this;
	}

	public virtual EnState OnHurt(float damage)
	{
		fsm.Controller.Health -= damage;
		return fsm.EnStates["knock_back"];
	}
}