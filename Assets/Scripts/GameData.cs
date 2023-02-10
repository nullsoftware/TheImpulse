using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NullSoftware.Serialization;
using UnityEngine;

public class GameData
{
    private static readonly BinarySerializer<GameData> _serializer
        = new BinarySerializer<GameData>();

    #region Properties

    public DateTime LastPlayTime { get; set; }

    public ushort WinCount { get; set; }

    public ushort LoseCount { get; set; }

    #endregion

    public static GameData Load(string saveFileName)
    {
        ValidateName(saveFileName);
        string path = Path.Combine(Application.persistentDataPath, saveFileName);

        if (File.Exists(path))
        {
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                return _serializer.Deserialize(fs);
            }
        }

        return null;
    }

    public static void Save(string saveFileName, GameData data)
    {
        ValidateName(saveFileName);
        string path = Path.Combine(Application.persistentDataPath, saveFileName);

        using (FileStream fs = new FileStream(path, FileMode.Create))
        {
            _serializer.Serialize(fs, data);
        }
    }

    private static void ValidateName(string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName))
        {
            throw new ArgumentNullException(nameof(fileName));
        }

        char[] invalidChars = Path.GetInvalidFileNameChars();

        for (int i = 0; i < fileName.Length; i++)
        {
            if (invalidChars.Contains(fileName[i]))
            {
                throw new NotSupportedException("File name contains invalid characters.");
            }
        }
    }
}
