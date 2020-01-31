using System;

public class Objective
{
    public bool Grind { get; set; }

    public bool Grip { get; set; }

    public bool Smith { get; set; }

    public static Objective Generate()
    {
        return new Objective()
        {
            Grind = GameManager.Instance.Random.Next(2) > 0,
            Grip = GameManager.Instance.Random.Next(2) > 0,
            Smith = GameManager.Instance.Random.Next(2) > 0
        };
    }

    public override string ToString()
    {
        return $"Objective requires Grind = {Grind}, Grip = {Grip}, Smith = {Smith}";
    }
}
