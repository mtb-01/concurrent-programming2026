namespace Project.Data
{
  internal class Ball : IBall
  {

    private Vector Position;
    
    private void Move(Vector delta)
    {
      Position = new Vector(Position.x + delta.x, Position.y + delta.y);
      // new position
    }

  }
}
