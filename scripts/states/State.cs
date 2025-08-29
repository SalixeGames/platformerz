using System;
using Godot;

[GlobalClass]
public partial class State : Resource
{
	[Export] public string Name;
	public StateMachine fsm;
	public CharacterBody2D Controller;

	public virtual void Enter()
	{
		fsm.Controller.UpdateAnim(Name);
	}
	public virtual void Exit() {}

	public virtual void Ready(StateMachine stateMachine)
	{
	}

	public virtual State Update(float delta)
	{
		if (DateTime.Now - fsm.LastTransition > TimeSpan.FromSeconds(0.35) || fsm.PreviousState.Name != "wall_slide") fsm.Controller.CanAerialStraffe = true;
		return this;
	}
	public virtual State PhysicsUpdate(float delta) 
	{
		return this;
	}
	public virtual State HandleInput(InputEvent @event)
	{
		bool hasAttack = GlobalScript.Instance.PowersList.Contains(GlobalScript.Powerups.Attack);

		if (@event.IsActionPressed("attack") && hasAttack && fsm.Controller.CanAttack)
		{
			fsm.Controller.Direction.Y = -fsm.Controller.jumpVelocity/2;
			fsm.Controller.Velocity = fsm.Controller.Direction;
			return fsm.States["attack"];
		}
		return this;
	}

	public virtual State AnimationEnd(string animationName) 
	{
		return this;
	}
}