
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Project.Logic.Test;

[TestClass]
public class BallTest
{
    [TestMethod]
    public void SimulateTest()
    {
        Vector position = new Vector(10, 10);
        Vector velocity = new Vector(10, 10);
        double moveDelay = 1;
        Ball ball = new Ball(position, velocity, 10, 10)
        {
            MoveDelay = moveDelay
        };
        Assert.AreEqual(position, ball.Position);
        Assert.AreEqual(velocity, ball.Velocity);
        
        ball.Simulate(null);

        position = new Vector(position.X + velocity.X * moveDelay, position.Y + velocity.Y * moveDelay);
        Assert.AreEqual(position, ball.Position);
        Assert.AreEqual(velocity, ball.Velocity);

        IVector currentPosition = new Vector(0, 0);
        int numberOfCallbacksCalled = 0;
        int callbacksToCall = 10;
        ball.NewPositionNotification += (sender, position) => {
            Assert.IsNotNull(sender);
            currentPosition = position;
            numberOfCallbacksCalled++;
        };
        for (int i = 0; i < callbacksToCall; i++)
        {
            ball.Simulate(null);
        }
        Assert.AreEqual(currentPosition, ball.Position);
        Assert.AreEqual(callbacksToCall, numberOfCallbacksCalled);
    }
}
