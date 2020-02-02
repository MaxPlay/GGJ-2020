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

    private ControlSettingsInstance input;
    private ControlSettingsInstance alternativeInput;

    public string InputFile => Path.Combine(Application.persistentDataPath, "input.duck");
    public string AlternativeInputFile => Path.Combine(Application.persistentDataPath, "alt_input.duck");

    public ControlSettingsInstance Input => input;
    public ControlSettingsInstance AlternativeInput => alternativeInput;

    public void Load()
    {
        LoadData(InputFile, ref input, inputTemplate);
        LoadData(AlternativeInputFile, ref alternativeInput, alternativeInputTemplate);
    }

    private void LoadData(string inputFile, ref ControlSettingsInstance input, ControlSettings template)
    {
        if (File.Exists(inputFile))
        {
            string json = File.ReadAllText(inputFile);
            input = JsonUtility.FromJson<ControlSettingsInstance>(json);
        }
        else
        {
            input = new ControlSettingsInstance
            {
                Down = template.Down,
                Right = template.Right,
                Up = template.Up,
                Left = template.Left,
                Interact = template.Interact,
                Pause = template.Pause
            };
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