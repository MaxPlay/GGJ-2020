using System;

public class Objective
{
    public bool Grind { get; set; }

    public bool Grip { get; set; }

    public bool Smith { get; set; }

    public Customer Owner { get; set; }

    public static Objective Generate()
    {
        int data = GameManager.Instance.Random.Next(1, 8);

        return new Objective()
        {
            Grind = (data & 0b001) == 0b001,
            Grip = (data & 0b010) == 0b010,
            Smith = (data & 0b100) == 0b100
        };
    }

    public override string ToString()
    {
        return $"Objective requires Grind = {Grind}, Grip = {Grip}, Smith = {Smith}";
    }

    public bool DoesMatch(Sword sword)
    {
        if (sword.Heat > 0.0f)
            return false;

        return sword.Sharpness == 1.0f && sword.HasHandle && sword.Sharpness == 1.0f;
    }
}
