using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project.Data;

namespace Project.Data.Test
{
  [TestClass]
  public class BallTest
  {
    [TestMethod]
    public void ConstructorTestMethod()
    {
      Vector vector = new Vector(0.0, 0.0);
      double mass = 10;
      double circumference = 10;

      Ball ball = new Ball(vector, vector, mass, circumference);

      Assert.AreEqual<IVector>(vector, ball.Position);
      Assert.AreEqual<IVector>(vector, ball.Velocity);
      Assert.AreEqual<double>(mass, ball.Mass);
      Assert.AreEqual<double>(mass, ball.Circumference);
    }

    [TestMethod]
    public void MoveTestMethod()
    {
      Vector initialPosition = new Vector(10.0, 10.0);
      Vector initialVelocity = new Vector(0.0, 0.0);
      double mass = 10;
      double circumference = 10;
      Ball ball = new Ball(initialPosition, initialVelocity, mass, circumference);
      Vector curentPosition = new(0.0, 0.0);

      int numberOfCallBackCalled = 0;
      /* ball.EmitNewPositionNotification += (sender, position) => { 
        Assert.IsNotNull(sender); 
        curentPosition = position; 
        numberOfCallBackCalled++; 
        };
      ball.Move(new Vector(0.0, 0.0));
      Assert.AreEqual<int>(1, numberOfCallBackCalled); */
      Assert.AreEqual<IVector>(initialPosition, curentPosition);
    }
  }
}