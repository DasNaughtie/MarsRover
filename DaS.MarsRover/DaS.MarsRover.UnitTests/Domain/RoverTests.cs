namespace DaS.MarsRover.UnitTests.Domain
{
    using MarsRover.Domain;
    using Shouldly;
    using Xunit;

    public class RoverTests
    {
        [Fact]
        public void RoverWithSouthCardinalReturns180Orientation()
        {
            var rover = new Rover(5, 5, 'S');

            rover.Orientation.ShouldBe(180);
        }

        [Fact]
        public void RoverWithSouthCardinalRotateLeftReturnsEastCardinal()
        {
            var rover = new Rover(5, 5, 'S');

            rover.ChangeOrientation('L');

            rover.CardinalPoint.ShouldBe('E');
        }

        [Fact]
        public void RoverWithSouthCardinalRotateRightReturnsWestCardinal()
        {
            var rover = new Rover(5, 5, 'S');

            rover.ChangeOrientation('R');

            rover.CardinalPoint.ShouldBe('W');
        }

        [Fact]
        public void RoverWithNorthCardinalInputMMovesYAxis1Position()
        {
            var rover = new Rover(0, 0, 'N');
             rover.Move();

            rover.XAxis.ShouldBe(0);
            rover.YAxis.ShouldBe(1);
        }
    }
}