using System;

namespace func_rocket;

public class ControlTask
{
	public static Turn ControlRocket(Rocket rocket, Vector target)
	{
        var position = rocket.Location;
        var vect = target - position;
        var angle = Math.Atan2(vect.Y, vect.X);

        var target2 = position + rocket.Velocity;
        var res = target2 - position;
        var a = Math.Atan2(res.Y, res.X);


        if (Math.Abs(rocket.Direction - angle) < 0.45)
        {
            if (a < angle)
                return Turn.Right;
            else
                return Turn.Left;
        }
        else if (rocket.Direction < angle)
            return Turn.Right;
        else
            return Turn.Left;
    }
}