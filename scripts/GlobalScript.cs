using Godot;
using System;
using Godot.Collections;

public partial class GlobalScript : Node
{
    public GlobalScript Instance { get; private set; }

    private static string _savingPath = "res://";

    public int Health { get; set; }

    public override void _Ready()
    {
        Instance = this;
    }

    public void SaveGame(string saveFileId)
    {
        using FileAccess file = FileAccess.Open(_savingPath + saveFileId, FileAccess.ModeFlags.Write);

        file.StoreVar(_GetMetadata());
        file.Close();
        GD.Print("Game " + saveFileId + " saved.");
    }

    public void LoadGame(string saveFileId)
    {
        using var file = FileAccess.Open(_savingPath + saveFileId, FileAccess.ModeFlags.Read);
        Variant DataVariant = file.GetVar(true);
        Dictionary data = DataVariant.As<Dictionary>();
        _SetMetadata(data);
        file.Close();
        
        GD.Print("Game " + saveFileId + " loaded.");
    }

    private Dictionary _GetMetadata()
    {
        Dictionary data = new Dictionary();
        data.Add("Health", Health);
        return data;
    }

    private void _SetMetadata(Dictionary data)
    {
        Health = (int)data["Health"];
    }
}
