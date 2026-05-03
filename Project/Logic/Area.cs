using System;

namespace Project.Logic;

internal class Area : ICollisionObject
{
    public double SizeX { get; init; }
    public double SizeY { get; init; }

    public Area(double sizeX, double sizeY)
    {
        SizeX = sizeX;
        SizeY = sizeY;
    }

    public CollisionInfo Collide(ICollisionObject collidingObject, IVector movement)
    {
        IBall? collidingBall = collidingObject as IBall;
        if (collidingBall != null)
        {
            return CollideBall(collidingBall, movement);
        }
        return new CollisionInfo();
    }

    public CollisionInfo CollideBall(IBall collidingBall, IVector movement)
    {
        Vector movePosition = new Vector(collidingBall.Position.X + movement.X, collidingBall.Position.Y + movement.Y);
        double intersectionRight = Math.Max(movePosition.X + collidingBall.Circumference/2 - SizeX, 0);
        double intersectionLeft = Math.Min(movePosition.X - collidingBall.Circumference/2, 0);
        double intersectionBottom = Math.Max(movePosition.Y + collidingBall.Circumference/2 - SizeY, 0);
        double intersectionTop = Math.Min(movePosition.Y - collidingBall.Circumference/2, 0);

        Vector intersection = new Vector()
        {
            X = (Math.Abs(intersectionLeft) > intersectionRight) ? intersectionLeft : intersectionRight,
            Y = (Math.Abs(intersectionTop) > intersectionBottom) ? intersectionTop : intersectionBottom
        };

        if (Math.Abs(intersection.X) < 0.01 && Math.Abs(intersection.Y) < 0.01)
        {
            return new CollisionInfo();
        }

        double moveSubtractX = 0;
        if (intersection.X != 0)
        {
            moveSubtractX = Math.Clamp(intersection.X / movement.X, 0, 1);
        }
        double moveSubtractY = 0;
        if (intersection.Y != 0)
        {
            moveSubtractY = Math.Clamp(intersection.Y / movement.Y, 0, 1);
        }

        double moveFraction = 1 - Math.Max(moveSubtractX, moveSubtractY);

        Vector ballVelocity = new Vector(collidingBall.Velocity.X, collidingBall.Velocity.Y);
        Vector newVelocity = ballVelocity - ballVelocity.Projected(intersection.Normalized()) * 2;

        return new CollisionInfo(true, moveFraction, newVelocity);
    }
}
