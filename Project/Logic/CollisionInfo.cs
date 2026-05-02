namespace Project.Logic;

internal class CollisionInfo
{
    public bool Collided { get; init; }
    public double MoveFraction { get; init; }
    public Vector NewVelocity { get; init; }

    public CollisionInfo()
    {
        Collided = false;
        MoveFraction = 1;
        NewVelocity = new Vector(0, 0);
    }

    public CollisionInfo(bool collided, double moveFraction, Vector newVelocity)
    {
        Collided = collided;
        MoveFraction = moveFraction;
        NewVelocity = newVelocity;
    }
}

