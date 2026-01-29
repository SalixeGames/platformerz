using System;
using Godot;

namespace Platformerz.scripts.enemies;

[GlobalClass]
public partial class EnState : Resource
{
	[Export] public string Name;
	public EnStateMachine fsm;
	public BaseEnemy Controller;

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

	public virtual EnState AnimationEnd(string animationName) 
	{
		return this;
	}

	public virtual EnState OnPlayerInVision(Area2D visionArea)
	{
		GD.Print(Name + " has player entered");
		return this;
	}

	public virtual EnState OnPlayerOutVision(Area2D visionArea)
	{
		GD.Print(Name + " has player exited");
		return this;
	}
}