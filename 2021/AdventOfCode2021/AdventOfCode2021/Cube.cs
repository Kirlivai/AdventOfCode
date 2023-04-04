using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public class Cube
    {
        public bool On { get; set; }
        public int xMin { get; set; }
        public int xMax { get; set; }
        public int yMin { get; set; }
        public int yMax { get; set; }
        public int zMin { get; set; }
        public int zMax { get; set; }

        public Cube(bool isOn, int xmin, int xmax, int ymin, int ymax, int zmin, int zmax)
        {
            On = isOn;
            xMin = xmin;
            xMax = xmax;
            yMin = ymin;
            yMax = ymax;
            zMin = zmin;
            zMax = zmax;
        }
    }
}