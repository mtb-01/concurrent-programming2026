using System;
using System.Formats.Asn1;
using Microsoft.Testing.Platform.Extensions.Messages;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Project.Logic.Test
{
    [TestClass]
    public class AreaTest
    {
        [TestMethod]
        public void ConstructorTestMethod()
        {
            double X = 10;
            double Y = 20;
            Area area = new Area(X, Y);
            Assert.AreEqual<double>(X, area.SizeX);
            Assert.AreEqual<double>(Y, area.SizeY);
        }

        [TestMethod]
        public void ColllideBallTestMethod()
        {
            double X = 10;
            double Y = 10;
            Area area = new Area(X, Y);

            Vector position = new Vector(9, 9);
            Vector velocity = new Vector(5, 5);
            Ball ball = new Ball(position, velocity, 10, 10, new TestLogicImplementation());

            Vector movement = new Vector(3, 3);
            Vector defaultVelocity = new Vector(0, 0);

            CollisionInfo info = area.CollideBall(ball, movement);

            Vector movePosition = new Vector(ball.Position.X + movement.X, ball.Position.Y + movement.Y);
            double intersectionRight = Math.Max(movePosition.X + ball.Circumference/2 - area.SizeX, 0);
            double intersectionLeft = Math.Min(movePosition.X - ball.Circumference/2, 0);
            double intersectionBottom = Math.Max(movePosition.Y + ball.Circumference/2 - area.SizeY, 0);
            double intersectionTop = Math.Min(movePosition.Y - ball.Circumference/2, 0);

            Vector intersection = new Vector()
            {
                X = (Math.Abs(intersectionLeft) > intersectionRight) ? intersectionLeft : intersectionRight,
                Y = (Math.Abs(intersectionTop) > intersectionBottom) ? intersectionTop : intersectionBottom
            };
            
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

            Vector ballVelocity = new Vector(ball.Velocity.X, ball.Velocity.Y);
            Vector newVelocity = ballVelocity - ballVelocity.Projected(intersection.Normalized()) * 2;

            Assert.IsTrue(info.Collided);
            Assert.AreEqual<double>(moveFraction, info.MoveFraction);
            Assert.AreEqual<Vector>(newVelocity, info.NewVelocity);
            

            CollisionInfo info2 = area.Collide(area, movement);
            Assert.IsFalse(info2.Collided);
            Assert.AreEqual<double>(1, info2.MoveFraction);
            Assert.AreEqual<Vector>(defaultVelocity, info2.NewVelocity);

            CollisionInfo info3 = area.Collide(ball, movement);
            Assert.AreEqual<bool>(info.Collided, info3.Collided);
            Assert.AreEqual<double>(info.MoveFraction, info3.MoveFraction);
            Assert.AreEqual<Vector>(info.NewVelocity, info3.NewVelocity);
        }
    }
}