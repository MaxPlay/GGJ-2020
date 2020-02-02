using System;
using System.IO;
using UnityEngine;

[Serializable]
public class Controls
{
    [SerializeField]
    ControlSettings inputTemplate;

    [SerializeField]
    ControlSettings alternativeInputTemplate;

    ControlSettings input;
    ControlSettings alternativeInput;

    public string InputFile => Path.Combine(Application.persistentDataPath, "input.duck");
    public string AlternativeInputFile => Path.Combine(Application.persistentDataPath, "alt_input.duck");

    public ControlSettings Input => input;
    public ControlSettings AlternativeInput => alternativeInput;

    public void Load()
    {
        LoadData(InputFile, ref input, inputTemplate);
        LoadData(AlternativeInputFile, ref alternativeInput, alternativeInputTemplate);
    }

    private void LoadData(string inputFile, ref ControlSettings input, ControlSettings template)
    {
        if (File.Exists(inputFile))
        {
            input = JsonUtility.FromJson<ControlSettings>(File.ReadAllText(inputFile));
        }
        else
        {
            input = ScriptableObject.CreateInstance<ControlSettings>();
            input.Down = template.Down;
            input.Right = template.Right;
            input.Up = template.Up;
            input.Left = template.Left;
            input.Interact = template.Interact;
            input.Pause = template.Pause;
            File.WriteAllText(inputFile, JsonUtility.ToJson(input));
        }
    }

    public void Save()
    {
        if (File.Exists(InputFile))
            File.Delete(InputFile);
        if (File.Exists(AlternativeInputFile))
            File.Delete(AlternativeInputFile);

        File.WriteAllText(InputFile, JsonUtility.ToJson(input));
        File.WriteAllText(AlternativeInputFile, JsonUtility.ToJson(alternativeInput));
    }
}