namespace DaS.MarsRover.Application.Validation
{
    using Domain;
    using System;
    using System.Linq;

    public interface IPlateauValidator
    {
        void ValidateRoverPositionOnPlateau(Plateau plateau, Rover rover);
    }

    public class PlateauValidator : IPlateauValidator
    {
        private const string PositionErrorMessage = "unable to obtain MarsRover starting position.";

        public void ValidateRoverPositionOnPlateau(Plateau plateau, Rover rover)
        {
            if (!plateau.XAxisRange.Contains(rover.XAxis) || !plateau.YAxisRange.Contains(rover.YAxis))
            {
                throw new ArgumentException(PositionErrorMessage);
            }
        }
    }
}