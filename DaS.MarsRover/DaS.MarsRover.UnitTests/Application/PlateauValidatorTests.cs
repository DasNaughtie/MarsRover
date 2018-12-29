namespace DaS.MarsRover.UnitTests.Application
{
    using MarsRover.Application.Validation;
    using MarsRover.Domain;
    using Moq.AutoMock;
    using System;
    using Shouldly;
    using TestStack.BDDfy;
    using Xunit;

    public class PlateauValidatorTests
    {
        private Rover _rover;
        private AutoMocker _mocker;
        private IPlateauValidator _validator;
        private Plateau _plateau;
        private Exception _exception;

        public PlateauValidatorTests()
        {
            _mocker = new AutoMocker();
            _validator = _mocker.CreateInstance<PlateauValidator>();
        }

        [Fact]
        public void InvalidRoverFailsPlateauValidation()
        {
            this.Given(x => x.GivenAPlateau())
                .And(x => x.GivenAInvalidRover())
                .When(x => x.WhenICallValidator())
                .Then(x => x.ThenAnExceptionIsThrown())
                .BDDfy();
        }

        private void GivenAPlateau()
        {
            _plateau = new Plateau(5, 5);
        }

        private void GivenAInvalidRover()
        {
            _rover = new Rover(1, 6, 'N');
        }

        private void WhenICallValidator()
        {
            _exception = Assert.Throws<ArgumentException>(() => _validator.ValidateRoverPositionOnPlateau(_plateau, _rover));
        }

        private void ThenAnExceptionIsThrown()
        {
            _exception.GetType().ShouldBe(typeof(ArgumentException));
        }
    }
}