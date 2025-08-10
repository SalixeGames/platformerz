using Godot;
using System;
using System.Collections;
using Godot.Collections;
using Array = System.Array;

public partial class GlobalScript : Node
{
    public GlobalScript Instance { get; private set; }

    private static string _savingPath = "res://";

    public int Health { get; set; }
    public Array<int> BlobsList { get; set; }

    public override void _Ready()
    {
        Instance = this;
    }

    public void SaveGame(string saveFileId)
    {
        using FileAccess file = FileAccess.Open(_savingPath + saveFileId, FileAccess.ModeFlags.Write);
        GD.Print(_GetMetadata());
        file.StoreVar(_GetMetadata());
        file.Close();
        GD.Print("Game " + saveFileId + " saved.");
    }

    public void LoadGame(string saveFileId)
    {
        using var file = FileAccess.Open(_savingPath + saveFileId, FileAccess.ModeFlags.Read);
        Variant DataVariant = file.GetVar(true);
        Dictionary<String, String> data = DataVariant.As<Dictionary<String, String>>();
        GD.Print(data);
        _SetMetadata(data);
        file.Close();
        
        GD.Print("Game " + saveFileId + " loaded.");
    }

    private Dictionary _GetMetadata()
    {
        Dictionary data = new Dictionary();
        data.Add("Health", Health);
        data.Add("BlobsList", String.Join(';', BlobsList));
        return data;
    }

    private void _SetMetadata(Dictionary<string, string> data)
    {
        Health = data["Health"].ToInt();
        BlobsList = new Array<int>();
        foreach (String blobId in data["BlobsList"].Split(";"))
        {
            BlobsList.Add(blobId.ToInt());
        }
    }
}
