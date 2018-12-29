using System;

namespace DaS.MarsRover.Domain
{
    public class Rover
    {
        public char CardinalPoint
        {
            get
            {
                return GetCardinalDirection();
            }
            protected set
            {
                _cardinalPoint = value; 
            }
        }
        public int XAxis { get; protected set; }
        public int YAxis { get; protected set; }
        public int Orientation { get; protected set; }

        private char _cardinalPoint;

        public string Report => $"{XAxis} {YAxis} {CardinalPoint}";

        public Rover(int startingPositionXAxis, int startingPositionYAxis, char cardinalPoint)
        {
            XAxis = startingPositionXAxis;
            YAxis = startingPositionYAxis;
            _cardinalPoint = cardinalPoint;

            SetOrientation(cardinalPoint);
        }

        public void SetXAxis(int newXAxis)
        {
            XAxis = newXAxis;
        }

        public void SetYAxis(int newYAxis)
        {
            YAxis = newYAxis;
        }

        public void ChangeOrientation(char direction)
        {
            switch (direction)
            {
                case 'L':
                    Rotate(-90);
                    break;
                case 'R':
                    Rotate(90);
                    break;
                default:
                    throw new ArgumentException($"Invalid direction: {direction}");
            }
        }

        public void Move()
        {
            switch (Orientation)
            {
                case CardinalConstants.NorthCardinalDegress:
                    YAxis++;
                    break;
                case CardinalConstants.EastCardinalDegress:
                    XAxis++;
                    break;
                case CardinalConstants.SouthCardinalDegrees:
                    YAxis--;
                    break;
                case CardinalConstants.WestCardinalDegress:
                    XAxis--;
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"{Orientation} is not a valid cardinal point degree.");
            }
        }

        private void SetOrientation(char cardinalPoint)
        {
            switch (cardinalPoint)
            {
                case CardinalConstants.NorthCardinalPoint:
                    Orientation = CardinalConstants.NorthCardinalDegress;
                    break;
                case CardinalConstants.EastCardinalPoint:
                    Orientation = CardinalConstants.EastCardinalDegress;
                    break;
                case CardinalConstants.SouthCardinalPoint:
                    Orientation = CardinalConstants.SouthCardinalDegrees;
                    break;
                case CardinalConstants.WestCardinalPoint:
                    Orientation = CardinalConstants.WestCardinalDegress;
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"{cardinalPoint} is not a valid cardinal point.");
            }
        }

        private void Rotate(int degrees)
        {
            Orientation += degrees;

            if (Orientation < 0)
            {
                while (Orientation < 0)
                {
                    Orientation += 360;
                }
            }
            else if (Orientation >= 360)
            {
                while (Orientation >= 360)
                {
                    Orientation -= 360;
                }
            }
        }

        private char GetCardinalDirection()
        {
            switch (Orientation)
            {
                case CardinalConstants.NorthCardinalDegress:
                    return CardinalConstants.NorthCardinalPoint;
                case CardinalConstants.EastCardinalDegress:
                    return CardinalConstants.EastCardinalPoint;
                case CardinalConstants.SouthCardinalDegrees:
                    return CardinalConstants.SouthCardinalPoint;
                case CardinalConstants.WestCardinalDegress:
                    return CardinalConstants.WestCardinalPoint;
                default:
                    throw new ArgumentOutOfRangeException($"{Orientation} is not a valid cardinal point degree.");
            }
        }
    }
}