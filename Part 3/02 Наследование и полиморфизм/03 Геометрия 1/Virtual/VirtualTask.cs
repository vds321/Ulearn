using System;
using System.Collections.Generic;
using System.Linq;

namespace Inheritance.Geometry.Virtual
{
    public abstract class Body
    {
        public Vector3 Position { get; }

        protected Body(Vector3 position)
        {
            Position = position;
        }

        public abstract bool ContainsPoint(Vector3 point);

        public abstract RectangularCuboid GetBoundingBox();
    }

    public class Ball : Body
    {
        public double Radius { get; }

        public Ball(Vector3 position, double radius) : base(position)
        {
            Radius = radius;
        }

        public override bool ContainsPoint(Vector3 point)
        {
            var vector = point - Position;
            var length2 = vector.GetLength2();
            return length2 <= Radius * Radius;
        }

        public override RectangularCuboid GetBoundingBox()
        {
            return new RectangularCuboid(Position, 2 * Radius, 2 * Radius, 2 * Radius);
        }
    }

    public class RectangularCuboid : Body
    {
        public double SizeX { get; }
        public double SizeY { get; }
        public double SizeZ { get; }

        public RectangularCuboid(Vector3 position, double sizeX, double sizeY, double sizeZ) : base(position)
        {
            SizeX = sizeX;
            SizeY = sizeY;
            SizeZ = sizeZ;
        }

        public override bool ContainsPoint(Vector3 point)
        {
            var minPoint = new Vector3(
                Position.X - SizeX / 2,
                Position.Y - SizeY / 2,
                Position.Z - SizeZ / 2);
            var maxPoint = new Vector3(
                Position.X + SizeX / 2,
                Position.Y + SizeY / 2,
                Position.Z + SizeZ / 2);

            return point >= minPoint && point <= maxPoint;
        }

        public override RectangularCuboid GetBoundingBox()
        {
            return this;
        }
    }

    public class Cylinder : Body
    {
        public double SizeZ { get; }

        public double Radius { get; }

        public Cylinder(Vector3 position, double sizeZ, double radius) : base(position)
        {
            SizeZ = sizeZ;
            Radius = radius;
        }

        public override bool ContainsPoint(Vector3 point)
        {
            var vectorX = point.X - Position.X;
            var vectorY = point.Y - Position.Y;
            var length2 = vectorX * vectorX + vectorY * vectorY;
            var minZ = Position.Z - SizeZ / 2;
            var maxZ = minZ + SizeZ;

            return length2 <= Radius * Radius && point.Z >= minZ && point.Z <= maxZ;
        }

        public override RectangularCuboid GetBoundingBox()
        {
            return new RectangularCuboid(Position, 2 * Radius, 2 * Radius, SizeZ);
        }
    }

    public class CompoundBody : Body
    {
        public IReadOnlyList<Body> Parts { get; }
        private readonly List<Vector3> _maxPoints = new List<Vector3>();
        private readonly List<Vector3> _minPoints = new List<Vector3>();

        public CompoundBody(IReadOnlyList<Body> parts) : base(parts[0].Position)
        {
            Parts = parts;
        }

        public override bool ContainsPoint(Vector3 point)
        {
            return Parts.Any(body => body.ContainsPoint(point));
        }

        public override RectangularCuboid GetBoundingBox()
        {
            foreach (var part in Parts)
            {
                var points = GetMinMaxPoint(part);
                if (part is CompoundBody) continue;
                _maxPoints.Add(points.MaxPoint);
                _minPoints.Add(points.MinPoint);
            }

            var sizeX = _maxPoints.Select(p => p.X).Max() - _minPoints.Select(p => p.X).Min();
            var sizeY = _maxPoints.Select(p => p.Y).Max() - _minPoints.Select(p => p.Y).Min();
            var sizeZ = _maxPoints.Select(p => p.Z).Max() - _minPoints.Select(p => p.Z).Min();

            var posX = (_maxPoints.Select(p => p.X).Max() + _minPoints.Select(p => p.X).Min()) / 2;
            var posY = (_maxPoints.Select(p => p.Y).Max() + _minPoints.Select(p => p.Y).Min()) / 2;
            var posZ = (_maxPoints.Select(p => p.Z).Max() + _minPoints.Select(p => p.Z).Min()) / 2;

            var position = new Vector3(posX, posY, posZ);

            return new RectangularCuboid(position, sizeX, sizeY, sizeZ);
        }

        private (Vector3 MaxPoint, Vector3 MinPoint) GetMinMaxPoint(Body body)
        {
            switch (body)
            {
                case Ball ball:
                    return (
                        new Vector3(
                            ball.Position.X + ball.Radius,
                            ball.Position.Y + ball.Radius,
                            ball.Position.Z + ball.Radius),
                        new Vector3(
                            ball.Position.X - ball.Radius,
                            ball.Position.Y - ball.Radius,
                            ball.Position.Z - ball.Radius)
                    );
                case Cylinder cylinder:
                    return (
                        new Vector3(
                            cylinder.Position.X + cylinder.Radius,
                            cylinder.Position.Y + cylinder.Radius,
                            cylinder.Position.Z + cylinder.SizeZ / 2),
                        new Vector3(
                            cylinder.Position.X - cylinder.Radius,
                            cylinder.Position.Y - cylinder.Radius,
                            cylinder.Position.Z - cylinder.SizeZ / 2)
                    );
                case RectangularCuboid cuboid:
                    return (
                        new Vector3(
                            cuboid.Position.X + cuboid.SizeX / 2,
                            cuboid.Position.Y + cuboid.SizeY / 2,
                            cuboid.Position.Z + cuboid.SizeZ / 2),
                        new Vector3(
                            cuboid.Position.X - cuboid.SizeX / 2,
                            cuboid.Position.Y - cuboid.SizeY / 2,
                            cuboid.Position.Z - cuboid.SizeZ / 2)
                    );
                case CompoundBody compoundBody:
                    {
                        foreach (var part in compoundBody.Parts)
                        {
                            var points = GetMinMaxPoint(part);
                            _maxPoints.Add(points.MaxPoint);
                            _minPoints.Add(points.MinPoint);
                        }
                        break;
                    }
            }
            return (default, default);
        }
    }
}