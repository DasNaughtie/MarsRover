using System.Collections.Generic;
using System.Linq;

namespace DaS.MarsRover.Domain
{
    public class Plateau
    {
        public IEnumerable<int> XAxisRange { get; }
        public IEnumerable<int> YAxisRange { get; }

        private const int PlateauXAxisStartingPosition = 0;
        private const int PlateauYAxisStartingPosition = 0;

        public Plateau(int xAxisBourdary, int yAxisBoudary)
        {
            XAxisRange = Enumerable.Range(PlateauXAxisStartingPosition, xAxisBourdary);
            YAxisRange = Enumerable.Range(PlateauYAxisStartingPosition, yAxisBoudary);
        }
    }
}