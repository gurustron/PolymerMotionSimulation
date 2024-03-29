﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PolymerMotionSimulation
{
    public class Bead: IEquatable<Bead>
    {
        public string Name { get; set; }
        public Point2d Location { get; private set; }

        public Bead()
        {
        }

        public Bead(string name, Point2d location)
        {
            Name = name;
            Location = location;
        }

        public Bead(string name, double x, double y)
        {
            Name = name;
            Location = new Point2d(x, y);
        }

        public void SetLocation(Point2d newLocation)
        {
            Location = newLocation;
        }

        public double GetPairPotential(Bead otherBead)
        {
            return EnergyFunction.SquareWellPairPotential(this.Location, otherBead.Location);
        }

        public double GetHarmonicPotential(Bead otherBead)
        {
            return EnergyFunction.HarmonicPotential(this.Location, otherBead.Location);
        }

        public double GetPairPotential(Point2d otherLocation)
        {
            return EnergyFunction.SquareWellPairPotential(this.Location, otherLocation);
        }

        public double GetHarmonicPotential(Point2d otherLocation)
        {
            return EnergyFunction.HarmonicPotential(this.Location, otherLocation);
        }

        #region public static Point2d GetRandomPoint(double radius)
        public Point2d GetRandomPoint(double radius)
        {
            Random random = Global.Random;
            double x;
            double y;
            do
            {
                double r = radius * Math.Sqrt(random.NextDouble());
                double theta = random.NextDouble() * 2 * Math.PI;

                x = Location.X + r * Math.Cos(theta);
                y = Location.Y + r * Math.Sin(theta);
            }
            while (!(Global.BottomLeft.X <= x && Global.BottomRight.X >= x)
                    || !(Global.BottomLeft.Y <= y && Global.TopLeft.Y >= y));

            return new Point2d(x, y);
        }
        #endregion

        #region override string ToString()
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(Name + " ");
            sb.Append(Location.ToString());

            return sb.ToString();
        } 
        #endregion

        #region equality comparison
        /// <summary>
        /// Equality comparisons
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            if (!(other is Bead)) return false;
            return Equals((Bead)other); // Calls method below
        }
        public bool Equals(Bead other) // Implements IEquatable<Point2d>
        {
            return Location == other.Location && Name == other.Name;
        }
        public override int GetHashCode()
        {
            return this.Location.GetHashCode() * 67 + Name.GetHashCode(); // 67 = some prime number
        }
        public static bool operator ==(Bead a1, Bead a2)
        {
            if (ReferenceEquals(a1, null) && ReferenceEquals(a2, null)) return true;
            if (ReferenceEquals(a1, null) || ReferenceEquals(a2, null)) return false;
            return a1.Equals(a2);
        }
        public static bool operator !=(Bead a1, Bead a2)
        {
            return !(a1==a2);
        } 
        #endregion
    }
}
