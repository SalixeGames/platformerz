using Godot;
using System;

public partial class Blob : Area2D
{
    [Export] public AnimationPlayer Animator;
    
    [Export] public int id;
    
    public override void _Ready()
    {
        base._Ready();
        Animator.Play("blob_anim");
        if (GlobalScript.Instance.BlobsList.Contains(id))
        {
            QueueFree();
        }
    }
}
