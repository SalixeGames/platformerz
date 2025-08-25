using Godot;
using System;

public partial class Powerups : Area2D
{
    [Export] public AnimationPlayer Animator;
    [Export] public GlobalScript.Powerups Powerup;
    
    public override void _Ready()
    {
        base._Ready();
        Animator.Play("powerup_anim");
        
        if (GlobalScript.Instance.PowersList.Contains(Powerup))
        {
            QueueFree();
        }
    }
}
