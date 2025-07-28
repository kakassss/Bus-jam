using System.IO;
using UnityEngine;

public class PlayerDataSave
{
    private readonly string _filePath;

    public PlayerDataSave()
    {
        _filePath = Path.Combine(Application.persistentDataPath, "PlayerData.json");
    }

    public void Save(PlayerData progress)
    {
        string json = JsonUtility.ToJson(progress, true);
        File.WriteAllText(_filePath, json);
    }

    public PlayerData Load()
    {
        if (!File.Exists(_filePath))
            return new PlayerData();

        string json = File.ReadAllText(_filePath);
        return JsonUtility.FromJson<PlayerData>(json);
    }

    public void Delete()
    {
        if (File.Exists(_filePath))
            File.Delete(_filePath);
    }
}