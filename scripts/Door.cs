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
    private int _currentBlobPriceReal;
    private bool _playerNear = false;

    public override void _Ready()
    {
        base._EnterTree();
        _currentBlobPriceReal = BlobPrice - GlobalScript.Instance.BlobsList.Count;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        
        _adjustePrice();
        
        if (Input.IsActionJustPressed("up") && _playerNear)
        {
            BlobPrice += 1;
            if (BlobPrice > NumberSprites.Count - 1 +  GlobalScript.Instance.BlobsList.Count)
            {
                BlobPrice = NumberSprites.Count - 1 +  GlobalScript.Instance.BlobsList.Count;
            }
        }
        if (Input.IsActionJustPressed("down") && _playerNear)
        {
            BlobPrice -= 1;
            if (BlobPrice < 0 + GlobalScript.Instance.BlobsList.Count)
            {
                BlobPrice = 0 + GlobalScript.Instance.BlobsList.Count;
            }
        }

        if (Input.IsActionJustPressed("interact") && _playerNear && _currentBlobPriceReal <= 0)
        {
            AnimationPlayer.Play("open");
        }
    }

    private void _adjustePrice()
    {
        int blobPriceReal = BlobPrice - GlobalScript.Instance.BlobsList.Count;
        if (blobPriceReal < 0)
        {
            blobPriceReal = 0;
        }
        if (_currentBlobPriceReal != blobPriceReal)
        {
            _currentBlobPriceReal = blobPriceReal;
            CostLabel.Texture = NumberSprites[blobPriceReal];
        }
    }
    
    public void _on_player_area_entered(Node body)
    {
        if (_currentBlobPriceReal != 0)
        {
            CantPressE.Show();
        }
        else
        {
            PressE.Show();
        }
        _playerNear = true;
    }

    public void _on_player_area_exited(Node body)
    {
        PressE.Hide();
        CantPressE.Hide();
        _playerNear = false;
    }

    public void _animation_finished(StringName animationName)
    {
        if (animationName == "open")
        {
            QueueFree();
        }
    }
}
