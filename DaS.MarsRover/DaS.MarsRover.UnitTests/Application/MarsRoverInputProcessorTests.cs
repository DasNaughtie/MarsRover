namespace DaS.MarsRover.UnitTests.Application
{
    using MarsRover.Application.Services;
    using MarsRover.Application.Validation;
    using Moq;
    using Moq.AutoMock;
    using System;
    using System.Linq;
    using MarsRover.Domain;
    using Shouldly;
    using TestStack.BDDfy;
    using Xunit;

    public class MarsRoverInputProcessorTests
    {
        private AutoMocker _mocker;
        private Mock<IPlateauValidator> _validator;
        private MarsRoverInputProcessor _inputProcessor;
        private string _input;
        private string[] _results;

        public MarsRoverInputProcessorTests()
        {
            _mocker = new AutoMocker();
            _validator = _mocker.GetMock<IPlateauValidator>();
            _inputProcessor = _mocker.CreateInstance<MarsRoverInputProcessor>();
        }

        [Fact]
        public void ProcessValidInputReturnsExpected()
        {
            var expectedResults = new[] {"1 3 N", "5 1 E"};

            this.Given(x => x.GivenAValidInput())
                .And(x => x.GivenAValidValidator())
                .When(x => x.WhenICallInputProcessor())
                .Then(x => x.ThenTheRoverResultIsAsExpected(expectedResults))
                .And(x => x.ThenTheValidatorIsCalled())
                .BDDfy();
        }

        private void GivenAValidInput()
        {
            _input = string.Join(
                Environment.NewLine,
                "5 5",
                "1 2 N",
                "LMLMLMLMM",
                "3 3 E",
                "MMRMMRMRRM");
        }

        private void GivenAValidValidator()
        {
            _validator.Setup(x => x.ValidateRoverPositionOnPlateau(It.IsAny<Plateau>(), It.IsAny<Rover>())).Verifiable();
        }

        private void WhenICallInputProcessor()
        {
            var inputArray = _input.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            _results = _inputProcessor.ProcessInput(inputArray);
        }

        private void ThenTheRoverResultIsAsExpected(string[] expectedResults)
        {
            for (int i = 0; i < _results.Length; i++)
            {
                _results[i].SequenceEqual(expectedResults[i]);
            }
        }

        private void ThenTheValidatorIsCalled()
        {
            _validator.Verify(x => x.ValidateRoverPositionOnPlateau(It.IsAny<Plateau>(), It.IsAny<Rover>()), Times.AtLeast(_input.Split('M').Length - 1));
        }
    }
}