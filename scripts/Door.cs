using Godot;
using System;

public partial class Door : Node2D
{
    [Export]
    public AnimationPlayer AnimationPlayer;

    [Export] 
    public int BlobPrice;

    [Export] public Sprite2D PressE;
    [Export] public Sprite2D CantPressE;
    [Export] public Sprite2D CostLabel;
    [Export] public Godot.Collections.Array<CompressedTexture2D> NumberSprites;

    [Export] public Font TextFont;

    private Font _defaultFont = ThemeDB.FallbackFont;

    public override void _Process(double delta)
    {
        base._Process(delta);
        CostLabel.Texture = NumberSprites[BlobPrice];
        if (Input.IsActionJustPressed("up"))
        {
            BlobPrice += 1;
            if (BlobPrice > NumberSprites.Count - 1)
            {
                BlobPrice = NumberSprites.Count - 1;
            }
        }
        if (Input.IsActionJustPressed("down"))
        {
            BlobPrice -= 1;
            if (BlobPrice < 0)
            {
                BlobPrice = 0;
            }
        }
    }
    
    public void _on_player_area_entered(Node body)
    {
        
    }

    public void _on_player_area_exited(Node body)
    {
        
    }
}
