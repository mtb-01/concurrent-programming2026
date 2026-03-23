using Microsoft.VisualStudio.TestTools.UnitTesting;
using ;

namespace Project.Data.Test
{
  [TestClass]
  public class BallTest
  {
    [TestMethod]
    public void ConstructorTestMethod()
    {
      Vector vector = new Vector(0.0, 0.0);
      Ball ball = new(vector, vector);
      Assert.AreEqual<Vector>(vector, ball.Position);
      Assert.AreEqual<Vector>(vector, ball.Velocity);
    }

    [TestMethod]
    public void MoveTestMethod()
    {
      Vector initialPosition = new(10.0, 10.0);
      Vector initialVelocity = new(0.0, 0.0);
      Ball ball = new(initialPosition, initialVelocity);
      Vector curentPosition = new(0.0, 0.0);
      int numberOfCallBackCalled = 0;
      ball.EmitNewPositionNotification += (sender, position) => { 
        Assert.IsNotNull(sender); 
        curentPosition = position; 
        numberOfCallBackCalled++; 
        };
      ball.Move(new Vector(0.0, 0.0));
      Assert.AreEqual<int>(1, numberOfCallBackCalled);
      Assert.AreEqual<Vector>(initialPosition, curentPosition);
    }
  }
}