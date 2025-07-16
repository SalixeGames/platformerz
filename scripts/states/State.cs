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
		if (@event.IsActionPressed("attack") && fsm.PreviousState != fsm.States["attack"])
		{
			return fsm.States["attack"];
		}
		return this;
	}

	public virtual State AnimationEnd(string animationName) 
	{
		return this;
	}
}