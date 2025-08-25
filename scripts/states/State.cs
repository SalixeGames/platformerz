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
		return this;
	}
	public virtual State PhysicsUpdate(float delta) 
	{
		return this;
	}
	public virtual State HandleInput(InputEvent @event)
	{
		bool hasAttack = GlobalScript.Instance.PowersList.Contains(GlobalScript.Powerups.Attack);
		bool lastStateAttack = fsm.PreviousState == fsm.States["attack"] || Name == "attack";

		TimeSpan deltaState = (DateTime.Now - fsm.LastTransition);
		bool lastAttackOld = deltaState > TimeSpan.FromSeconds(3);
		bool lastStateOld = TimeSpan.FromSeconds(0.1) < deltaState;
		
		bool canAttack = hasAttack && (!lastStateAttack || lastAttackOld) && (lastStateOld || !lastStateAttack);
		if (@event.IsActionPressed("attack") && canAttack)
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