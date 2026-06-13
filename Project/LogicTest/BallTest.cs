
using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Project.Logic.Test;

internal class TestLogicImplementation : LogicAbstractAPI
{
    private readonly List<Ball> listOfBalls = new List<Ball>();
    private Area area = new Area(100, 100);

    public override void ClearBalls() {}
    public override void CreateBall() {}

    public override IVector GetAreaSize()
    {
        throw new NotImplementedException();
    }

    public override List<IBall> GetBalls()
    {
        throw new NotImplementedException();
    }

    public override int GetElapsedSeconds()
    {
        throw new NotImplementedException();
    }

    public override bool IsStarted()
    {
        return false;
    }

    public override void Start(double moveDelay) {}

    public override void StartLayer(int initialBallCount)
    {
        throw new NotImplementedException();
    }

    public override void Stop() {}

    internal override ICollisionObject GetArea()
    {
        return area;
    }

    internal override List<Ball> GetLogicBalls()
    {
        return new List<Ball>(listOfBalls);
    }

    internal override void WriteDiagnostic(string text)
    {
        return;
    }
}

[TestClass]
public class BallTest
{
    [TestMethod]
    public void SimulateTest()
    {
        Vector position = new Vector(10, 10);
        Vector velocity = new Vector(10, 10);
        double moveDelay = 1;
        int ID = 1;
        Ball ball = new Ball(ID, position, velocity, 10, 10, new TestLogicImplementation())
        {
            MoveDelay = moveDelay
        };
        Assert.AreEqual(ID, ball.ID);
        Assert.AreEqual(position, ball.Position);
        Assert.AreEqual(velocity, ball.Velocity);
        Assert.AreEqual(moveDelay, ball.MoveDelay);
        
        ball.Simulate(moveDelay);

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
            ball.Simulate(moveDelay);
        }
        Assert.AreEqual(currentPosition, ball.Position);
        Assert.AreEqual(callbacksToCall, numberOfCallbacksCalled);
    }

    [TestMethod]
    public void ColllideBallTest()
    {
        Vector position = new Vector(110, 110);
        Vector velocity = new Vector(-10, -10);
        double mass = 10;
        double diameter = 30;
        int ID = 1;

        Vector position2 = new Vector(130, 130);
        Vector velocity2 = new Vector(10, 10);
        double mass2 = 10;
        double diameter2 = 20;
        int ID2 = 2;

        Vector movement = new Vector(10, 10);

        Ball ballONE = new Ball(ID, position, velocity, mass, diameter, new TestLogicImplementation());
        Ball ballTWO = new Ball(ID2, position2, velocity2, mass2, diameter2, new TestLogicImplementation());
        
        CollisionInfo info = ballONE.Collide(ballTWO, movement);


        double radiusSum = ballONE.Diameter / 2 + ballTWO.Diameter / 2;

        double moveFraction = radiusSum - new Vector(ballONE.Position.X - ballTWO.Position.X, ballONE.Position.Y - ballTWO.Position.Y).Length();
        moveFraction = Math.Clamp(moveFraction / new Vector(movement.X, movement.Y).Length(), 0, 1);

        Vector collisionPosition = new Vector(ballTWO.Position.X + movement.X * moveFraction, ballTWO.Position.Y + movement.Y * moveFraction);
        Vector collisionDirection = new Vector(ballONE.Position.X - collisionPosition.X, ballONE.Position.Y - collisionPosition.Y).Normalized();

        Vector ballVelocity = new Vector(ballTWO.Velocity.X, ballTWO.Velocity.Y);
        Vector thisVelocity = new Vector(ballONE.Velocity.X, ballONE.Velocity.Y);

        double ballVelocityProjectedLength = ballVelocity.Dot(collisionDirection);

        double thisVelocityProjectedLength = thisVelocity.Dot(collisionDirection);

        double massSum = ballONE.Mass + ballTWO.Mass;

        double newBallVelocityProjectedLength = (thisVelocityProjectedLength * (2*ballONE.Mass) + ballVelocityProjectedLength * (ballTWO.Mass - ballONE.Mass)) / massSum;
            
        Vector ballNewVelocity = ballVelocity + collisionDirection * (newBallVelocityProjectedLength - ballVelocityProjectedLength);
        
        bool collided = true;

        if (ballVelocityProjectedLength <= 0)
        {
            collided = false;
            moveFraction = 1;
            ballNewVelocity = new Vector(0,0);
        }

        Assert.AreEqual(info.Collided, collided);
        Assert.AreEqual<double>(moveFraction, info.MoveFraction);
        Assert.AreEqual<Vector>(ballNewVelocity, info.NewVelocity);       

    }


    [TestMethod]
    public void ColllideBallNegativeTest()
    {
        Vector position = new Vector(10, 10);
        Vector velocity = new Vector(-10, -10);
        double mass = 10;
        double diameter = 10;
        int ID = 1; 

        Vector position3 = new Vector(100, 200);
        Vector velocity3 = new Vector(1, 1);
        double mass3 = 5;
        double diameter3 = 5;
        int ID3 = 3;

        Vector movement = new Vector(10, 20);

        Ball ballONE = new Ball(ID, position, velocity, mass, diameter, new TestLogicImplementation());

        Ball ballTHREE = new Ball(ID3, position3, velocity3, mass3, diameter3, new TestLogicImplementation());

        CollisionInfo infoNoCollision = ballONE.Collide(ballTHREE, movement);

        double moveFraction = 1;
        Vector newVelocity = new Vector(0,0);;
        Vector ballMovePosition = new Vector(ballTHREE.Position.X + movement.X, ballTHREE.Position.Y + movement.Y);
        double distanceSquared = Math.Pow(ballONE.Position.X - ballMovePosition.X, 2) + Math.Pow(ballONE.Position.Y - ballMovePosition.Y, 2);
        double radiusSum = ballONE.Diameter / 2 + ballTHREE.Diameter / 2;
        if (Math.Pow(radiusSum, 2) < distanceSquared)
        {
            moveFraction = 1;
            newVelocity = new Vector(0,0);
        }

        Assert.IsFalse(infoNoCollision.Collided);
        Assert.AreEqual<double>(moveFraction, infoNoCollision.MoveFraction);
        Assert.AreEqual<Vector>(newVelocity, infoNoCollision.NewVelocity);
        
    }
}
