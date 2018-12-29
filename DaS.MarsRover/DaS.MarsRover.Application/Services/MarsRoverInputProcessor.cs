namespace DaS.MarsRover.Application.Services
{
    using System;
    using Domain;
    using Validation;

    public class MarsRoverInputProcessor
    {
        private const int InputRowsPerRoverInstruction = 2;
        private readonly IPlateauValidator _validator;
        private Plateau _plateau;

        public MarsRoverInputProcessor(IPlateauValidator validator)
        {
            _validator = validator;
        }

        public string[] ProcessInput(string[] input)
        {
            if (input == null || input.Length <= 0)
            {
                throw new ArgumentNullException(nameof(input));
            }

            try
            {
                var plateauBoundaryInput = input[0].Trim();

                ProcessPlateauBoundary(plateauBoundaryInput);

                var results = new string[(input.Length - 1) / InputRowsPerRoverInstruction];
                var counter = 0;

                for (int i = 1; i < input.Length; i += InputRowsPerRoverInstruction)
                {
                    //process row 1
                    var roverStartingPosInput = input[i].Trim();

                    var rover = ProcessRoverStartingPosition(roverStartingPosInput);

                    //process row 2
                    var roverInstructions = input[i + 1].Trim();

                    ProcessRoverInstructions(roverInstructions, rover);

                    results[counter] = rover.Report;

                    counter++;
                }

                return results;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private void ProcessPlateauBoundary(string input)
        {
            var plateauSizeCoordinates = input.Split(' ');

            if (plateauSizeCoordinates.Length != 2)
            {
                throw new ArgumentException("unable to obtain plateau size coordinates.");
            }

            var isXAxisNumeric = int.TryParse(plateauSizeCoordinates[0], out int xAxis);
            var isYAxisNumeric = int.TryParse(plateauSizeCoordinates[1], out int yAxis);

            if (!isXAxisNumeric || !isYAxisNumeric)
            {
                throw new ArgumentException("unable to obtain plateau size coordinates.");
            }

            _plateau = new Plateau(xAxis, yAxis);
        }

        private Rover ProcessRoverStartingPosition(string input)
        {
            var roverStartingPosInputs = input.Split(' ');

            if (roverStartingPosInputs.Length != 3)
            {
                throw new ArgumentException($"unable to obtain MarsRover starting position.");
            }

            var isXAxisNumeric = int.TryParse(roverStartingPosInputs[0], out int roverCurrentXAxisPosition);
            var isYAxisNumeric = int.TryParse(roverStartingPosInputs[1], out int roverCurrentYAxisPosition);
            var roverStartingCardinalPoint = roverStartingPosInputs[2];

            if (!isXAxisNumeric || !isYAxisNumeric)
            {
                throw new ArgumentException($"unable to obtain MarsRover starting position.");
            }

            var rover = CreateRover(roverCurrentXAxisPosition, roverCurrentYAxisPosition, roverStartingCardinalPoint[0]);

            ValidateRoverPositionOnPlateau(rover);

            return rover;
        }

        private void ProcessRoverInstructions(string roverInstructions, Rover rover)
        {
            foreach (var roverInstruction in roverInstructions)
            {
                switch (roverInstruction)
                {
                    case 'L':
                    case 'R':
                        rover.ChangeOrientation(roverInstruction);
                        break;
                    case 'M':
                        rover.Move();
                        ValidateRoverPositionOnPlateau(rover);
                        break;
                    default:
                        throw new ArgumentException("unable to process MarsRover instructions.");
                }
            }
        }

        private void ValidateRoverPositionOnPlateau(Rover rover)
        {
            _validator.ValidateRoverPositionOnPlateau(_plateau, rover);
        }

        private Rover CreateRover(int xAxis, int yAxis, char cardinalPoint)
        {
            return new Rover(xAxis, yAxis, cardinalPoint);
        }
    }
}