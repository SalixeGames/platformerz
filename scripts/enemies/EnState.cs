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
	public bool PlayerInVision = false;
	public DateTime StateTimer = DateTime.MinValue;

	public virtual void Enter()
	{
		fsm.Controller.UpdateAnim(Name);
		StateTimer = DateTime.Now;
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
		PlayerInVision = true;
		GD.Print("Player In Vision");
		return this;
	}

	public virtual EnState OnPlayerOutVision(Area2D visionArea)
	{
		PlayerInVision = false;
		GD.Print("Player out");
		return this;
	}

	public virtual EnState OnHurt(float damage)
	{
		fsm.Controller.Health -= damage;
		return fsm.EnStates["knock_back"];
	}
}