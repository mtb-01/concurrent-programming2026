using System;

namespace Project.Data
{
    internal class Ball : IBall
    {
        public event EventHandler<IVector>? NewPositionNotification;

        private Vector Position;
        
        private void Move(Vector delta)
        {
            Position = new Vector(Position.x + delta.x, Position.y + delta.y);
            EmitNewPositionNotification();
        }

        private void EmitNewPositionNotification()
        {
            NewPositionNotification?.Invoke(this, Position);
        }
    }
}
