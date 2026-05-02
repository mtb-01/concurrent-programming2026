namespace Project.Logic;

internal interface ICollisionObject
{
    CollisionInfo Collide(ICollisionObject collidingObject, IVector movement);
}
