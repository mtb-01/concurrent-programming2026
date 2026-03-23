namespace Project.Data
{
  internal class Vector : IVector
  {
    public double x;
    public double y;

    public Vector(double XComponent, double YComponent)
    {
      x = XComponent;
      y = YComponent;
    }
  }
}
