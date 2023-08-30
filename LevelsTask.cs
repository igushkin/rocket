using System;
using System.Collections.Generic;

namespace func_rocket;

public class LevelsTask
{
    public static readonly Physics StandardPhysics = new Physics();
    public static readonly Rocket StandartRocket = new Rocket(new Vector(200, 700), Vector.Zero, -0.5 * Math.PI);
    public static readonly Vector StandartTarget = new Vector(700, 500);

    public static IEnumerable<Level> CreateLevels()
    {
        yield return Zero();
        yield return Heavy();
        yield return Up();
        yield return WhiteHole();
        yield return BlackHole();
        yield return BlackAndWhite();
    }

    private static Level Zero()
    {
        Gravity gravity = (size, loc) => new Vector(0, 0);
        return CreateLevel("Zero", gravity);
    }

    private static Level Heavy()
    {
        Gravity gravity = (size, loc) => new Vector(0, 0.9);
        return CreateLevel("Heavy", gravity);
    }

    private static Level Up()
    {
        Gravity gravity = (size, loc) => new Vector(0, -300 / (size.Length - loc.Y + 300));
        return CreateLevel("Up", gravity);
    }

    private static Level WhiteHole()
    {
        Gravity gravity = (size, loc) =>
        {
            var delta = loc - StandartTarget;
            var power = 140 * delta.Length / (delta.Length * delta.Length + 1);
            var newX = power * Math.Cos(delta.Angle);
            var newY = power * Math.Sin(delta.Angle);
            return new Vector(newX, newY);
        };
        return CreateLevel("WhiteHole", gravity);
    }

    private static Level BlackHole()
    {
        Gravity gravity = (size, loc) =>
        {
            var delta = loc - (StandartTarget + StandartRocket.Location) / 2;
            var power = 300 * delta.Length / (delta.Length * delta.Length + 1);
            var newX = -power * Math.Cos(delta.Angle);
            var newY = -power * Math.Sin(delta.Angle);
            return new Vector(newX, newY);
        };
        return CreateLevel("BlackHole", gravity);
    }

    private static Level BlackAndWhite()
    {
        Gravity gravity = (size, loc) => (BlackHole().Gravity(size, loc) + WhiteHole().Gravity(size, loc)) / 2;
        return CreateLevel("BlackAndWhite", gravity);
    }

    private static Level CreateLevel(string name, Gravity gravity)
    {
        return new Level(name, StandartRocket, StandartTarget, gravity, StandardPhysics);
    }
}