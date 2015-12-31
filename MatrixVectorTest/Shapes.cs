using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;

namespace MatrixVectorTest
{
    enum FeatureType { enLine, enPlane, enCircle, enEllipse, enSphere, enCylinder, enSteppedCylinder, enCone };
    enum RefPlane { XY_PLANE, YZ_PLANE, ZX_PLANE };

    class ShapesImpl : Shapes
    {
        public List<Vector3D> CreateLine(Vector3D center, List<Vector3D> points)
        {
            double xAngle = 0.0;
            double yAngle = 0.0;
            double zAngle = 0.0;
            points = CreateLine(center, points, xAngle, yAngle, zAngle);
            return points;
        }
        public List<Vector3D> CreateLine(Vector3D center, List<Vector3D> points, Vector3D Normal)
        {
            double xAngle = 0.0;
            double yAngle = 0.0;
            double zAngle = 0.0;
            Vector3D NormalCos = ConvertToUnitVector(Normal.X, Normal.Y, Normal.Z);
            yAngle = RadianToDegree(Math.Acos(NormalCos.Z));
            zAngle = RadianToDegree(Math.Atan(NormalCos.Y / NormalCos.X));
            zAngle = CalculateZAngle(zAngle, NormalCos);
            points = CreateLine(center, points, xAngle, yAngle, zAngle);
            return points;
        }
        public List<Vector3D> CreatePlane(Vector3D center, double width, double height, Vector3D Normal)
        {
            List<Vector3D> points = new List<Vector3D>();
            double xAngle = 0.0;
            double yAngle = 0.0;
            double zAngle = 0.0;
            Vector3D NormalCos = ConvertToUnitVector(Normal.X, Normal.Y, Normal.Z);
            yAngle = RadianToDegree(Math.Acos(NormalCos.Z));
            zAngle = RadianToDegree(Math.Atan(NormalCos.Y / NormalCos.X));
            zAngle = CalculateZAngle(zAngle, NormalCos);
            points = CreatePlane(center, width, height, xAngle, yAngle, zAngle);
            return points;
        }
        public List<Vector3D> CreateCone(Vector3D center, double radius1, double radius2, double height, double anglet, Vector3D Normal)
        {
            List<Vector3D> points = new List<Vector3D>();
            double xAngle = 0.0;
            double yAngle = 0.0;
            double zAngle = 0.0;
            Vector3D NormalCos = ConvertToUnitVector(Normal.X, Normal.Y, Normal.Z);
            yAngle = RadianToDegree(Math.Acos(NormalCos.Z));
            zAngle = RadianToDegree(Math.Atan(NormalCos.Y / NormalCos.X));
            zAngle = CalculateZAngle(zAngle, NormalCos);
            points = CreateCone(center, radius1, radius2, height, anglet, xAngle, yAngle, zAngle);
            return points;
        }
        public List<Vector3D> CreateCylinder(Vector3D center, double radius, double height, double anglet, Vector3D Normal)
        {
            List<Vector3D> points = new List<Vector3D>();
            double xAngle = 0.0;
            double yAngle = 0.0;
            double zAngle = 0.0;
            SetRadius(radius, radius);
            Vector3D NormalCos = ConvertToUnitVector(Normal.X, Normal.Y, Normal.Z);
            yAngle = RadianToDegree(Math.Acos(NormalCos.Z));
            zAngle = RadianToDegree(Math.Atan(NormalCos.Y / NormalCos.X));

            zAngle = CalculateZAngle(zAngle, NormalCos);
            points = CreateCylinder(center, radius, height, anglet, xAngle, yAngle, zAngle);
            return points;
        }
        public List<Vector3D> CreateSteppedCylinder(Vector3D center, double radius1, double radius2, double height, double anglet, Vector3D Normal)
        {
            List<Vector3D> points = new List<Vector3D>();
            double xAngle = 0.0;
            double yAngle = 0.0;
            double zAngle = 0.0;
            SetRadius(radius1, radius2);
            Vector3D NormalCos = ConvertToUnitVector(Normal.X, Normal.Y, Normal.Z);
            yAngle = RadianToDegree(Math.Acos(NormalCos.Z));
            zAngle = RadianToDegree(Math.Atan(NormalCos.Y / NormalCos.X));

            zAngle = CalculateZAngle(zAngle, NormalCos);
            points = CreateSteppedCylinder(center, radius1, radius2, height, anglet, xAngle, yAngle, zAngle);
            return points;
        }
        public List<Vector3D> CreateCircle(Vector3D center, double Radius, double anglet, Vector3D Normal)
        {
            List<Vector3D> points = new List<Vector3D>();
            double xAngle = 0.0;
            double yAngle = 0.0;
            double zAngle = 0.0;
            SetRadius(Radius, Radius);
            Vector3D NormalCos = ConvertToUnitVector(Normal.X, Normal.Y, Normal.Z);
            yAngle = RadianToDegree(Math.Acos(NormalCos.Z));
            zAngle = RadianToDegree(Math.Atan(NormalCos.Y / NormalCos.X));
            zAngle = CalculateZAngle(zAngle, NormalCos);
            points = CreateEllipse(center, Radius, Radius, anglet, xAngle, yAngle, zAngle);
            return points;
        }
        public List<Vector3D> CreateEllipse(Vector3D center, double aRadius, double bRadius, double anglet, Vector3D Normal)
        {
            List<Vector3D> points = new List<Vector3D>();
            double xAngle = 0.0;
            double yAngle = 0.0;
            double zAngle = 0.0;
 
            SetRadius(aRadius, bRadius);
            Vector3D NormalCos = ConvertToUnitVector(Normal.X, Normal.Y, Normal.Z);
            yAngle = RadianToDegree(Math.Acos(NormalCos.Z));
            zAngle = RadianToDegree(Math.Atan(NormalCos.Y / NormalCos.X));
            zAngle = CalculateZAngle(zAngle, NormalCos);

            points = CreateEllipse(center, aRadius, bRadius, anglet, xAngle, yAngle, zAngle);
            return points;
        }
        public double CalculateZAngle(double zAngle, Vector3D NormalCos)
        {
            if ((NormalCos.X < 0) && (NormalCos.Y > 0))
            {
                zAngle = zAngle + 180.0;
            }
            else if ((NormalCos.X < 0) && (NormalCos.Y < 0))
            {
                zAngle = zAngle + 180.0;
            }

            else if ((NormalCos.X > 0) && (NormalCos.Y < 0))
            {
                zAngle = zAngle + 360;
            }
            else if ((NormalCos.X == 0) && (NormalCos.Y == 0))
            {
                zAngle = 0.0;
            }

            else if ((NormalCos.X == -1) && (NormalCos.Y == 0.0))
            {
                zAngle = 180.0;
            }
            else if ((NormalCos.X == 0.0) && (NormalCos.Y == -1.0))
            {
                zAngle = 270.0;
            }

            return zAngle;
        }
    }
    class Shapes
    {
        static Vector3D _normal;
        static Vector3D _majorAxis;  // Ellipse Only
        static Vector3D _center;
        static double _RadiusA;
        static double _RadiusB;
        static RefPlane _refPlane;
        static double _AngleR, _AngleH;

        public void SetAngleRH(double AR, double AH)
        {
            _AngleR = AR;
            _AngleH = AH;
        }
        public void GetAngleRH(ref double AR, ref double AH)
        {
            AR= _AngleR;
            AH= _AngleH;
        }
        public void SetRefPlane(Vector3D unit)
        {
            if ((unit.Z >= .9999) && (unit.Z <= 1.0001))
            {
                _refPlane = RefPlane.XY_PLANE;
            }
            else if ((unit.Y >= .9999) && (unit.Y <= 1.0001))
            {
                _refPlane = RefPlane.ZX_PLANE;
            }
            else if ((unit.X >= .9999) && (unit.Z <= 1.0001))
            {
                _refPlane = RefPlane.YZ_PLANE;
            }

        }
        public void SetRefPlane(RefPlane refPlane)
        {
            _refPlane = refPlane;
        }
        public void GetRefPlane(ref RefPlane refPlane)
        {
            refPlane = _refPlane;
        }
        public Shapes()
        {
            _center = new Vector3D();
            _normal = new Vector3D();
        }
        public void SetNormal(double k, double l, double m)
        {
            _normal.X = k;
            _normal.Y = l;
            _normal.Z = m;

        }
        public void SetRadius(double radiusA, double radiusB)
        {
            _RadiusA = radiusA;
            _RadiusB = radiusB;

        }
        public void SetMajorAxis(double k, double l, double m)
        {
            _majorAxis.X = k;
            _majorAxis.Y = l;
            _majorAxis.Z = m;

        }
        public void SetCenter(Vector3D center)
        {
            _center = center;

        }
        public void SetNormal(Vector3D normal)
        {
            _normal = normal;

        }
        public void SetMajorAxis(Vector3D majorAxis)
        {
            _majorAxis = majorAxis;

        }
        public Vector3D ConvertToUnitVector(double xI, double yJ, double kZ)
        {
            Vector3D Normal = new Vector3D(xI, yJ, kZ);
            Normal.Normalize();
            return Normal;
        }
        public Vector3D GetCenter()
        {
            return _center;
        }
        public Vector3D GetNormal()
        {
            return _normal;
        }
        public Vector3D GetMajorAxis()
        {
            return _majorAxis;
        }
        private void GetCenter(ref double X, ref double Y, ref double Z)
        {
            X = _center.X;
            Y = _center.Y;
            Z = _center.Z;
        }
        private void GetRadius(ref double radiusA, ref double radiusB)
        {
            radiusA = _RadiusA;
            radiusB = _RadiusB;
        }
        private void GetNormalAngle(ref double AngleXA, ref double AngleYA, ref double AngleZA)
        {
            AngleXA = (Math.Acos(_normal.X)) * 180.0 / Math.PI;
            AngleYA = (Math.Acos(_normal.Y)) * 180.0 / Math.PI;
            AngleZA = (Math.Acos(_normal.Z)) * 180.0 / Math.PI;
        }
        private void GetMajorAxisAngle(ref double AngleXA, ref double AngleYA, ref double AngleZA)
        {
            AngleXA = (Math.Acos(_majorAxis.X)) * 180.0 / Math.PI;
            AngleYA = (Math.Acos(_majorAxis.Y)) * 180.0 / Math.PI;
            AngleZA = (Math.Acos(_majorAxis.Z)) * 180.0 / Math.PI;
        }

        public List<Vector3D> CreateLine(Vector3D center, List<Vector3D> points, double xAngle, double yAngle, double zAngle)
        {
            List<Vector3D> rotPoints = new List<Vector3D>();
            List<Vector3D> transPoints = new List<Vector3D>();
            RefPlane refPlane = RefPlane.XY_PLANE;

            rotPoints = RotatePoints(xAngle, yAngle, zAngle, points);
            transPoints = TranslatePoints(center.X, center.Y, center.Z, rotPoints);

            Vector3D Normal = points[1] - points[0];
            Normal.Normalize();
            SetNormal(Normal.X, Normal.Y, Normal.Z);
            _normal = RotateVector(xAngle, yAngle, zAngle, _normal);
            GetRefPlane(ref refPlane);
            CalculateAngleAR(ref _AngleR, ref _AngleH, _normal, refPlane);
            _center = center;

            return transPoints;
        }
        public List<Vector3D> CreatePlane(Vector3D center, double width, double height, double xAngle, double yAngle, double zAngle)
        {
            List<Vector3D> points = new List<Vector3D>();
            List<Vector3D> rotPoints = new List<Vector3D>();
            List<Vector3D> transPoints = new List<Vector3D>();
            RefPlane refPlane = RefPlane.XY_PLANE;

            Vector3D point1 = new Vector3D(0.0 + width / 2.0, 0.0, 0.0);
            Vector3D point2 = new Vector3D(0.0 - width / 2.0, 0.0, 0.0);
            Vector3D point3 = new Vector3D(0.0, 0.0 + height / 2.0, 0.0);
            Vector3D point4 = new Vector3D(0.0, 0.0 - height / 2.0, 0.0);
            points.Add(point1);
            points.Add(point2);
            points.Add(point3);
            points.Add(point4);

            rotPoints = RotatePoints(xAngle, yAngle, zAngle, points);
            transPoints = TranslatePoints(center.X, center.Y, center.Z, rotPoints);
            SetNormal(0.0, 0.0, 1.0);
            _normal = RotateVector(xAngle, yAngle, zAngle, _normal);
            GetRefPlane(ref refPlane);
            CalculateAngleAR(ref _AngleR, ref _AngleH, _normal, refPlane);
            _center = center;

            return transPoints;
        }
        public List<Vector3D> CreateCone(Vector3D center, double radius1, double radius2, double height, double anglet, double xAngle, double yAngle, double zAngle)
        {
            List<Vector3D> pointsTop = new List<Vector3D>();
            List<Vector3D> rotPoints = new List<Vector3D>();
            List<Vector3D> transPoints = new List<Vector3D>();
            List<Vector3D> pointsBottom = new List<Vector3D>();
            RefPlane refPlane = RefPlane.XY_PLANE;

            pointsTop = CreateEllipse(height / 2.0, radius1, radius1, anglet);
            pointsBottom = CreateEllipse(-height / 2.0, radius2, radius2, anglet);

            //            pointsTop = pointsTop.Union(pointsBottom).ToList();
            pointsBottom = pointsBottom.Union(pointsTop).ToList();

            rotPoints = RotatePoints(xAngle, yAngle, zAngle, pointsBottom);
            transPoints = TranslatePoints(center.X, center.Y, center.Z, rotPoints);
            SetNormal(0.0, 0.0, -1.0);
            _normal = RotateVector(xAngle, yAngle, zAngle, _normal);
            GetRefPlane(ref refPlane);
            CalculateAngleAR(ref _AngleR, ref _AngleH, _normal, refPlane);
            _center = RotateVector(xAngle, yAngle, zAngle, center);

            return transPoints;

        }
        public List<Vector3D> CreateCylinder(Vector3D center, double radius, double height, double anglet, double xAngle, double yAngle, double zAngle)
        {
            List<Vector3D> pointsTop = new List<Vector3D>();
            List<Vector3D> rotPoints = new List<Vector3D>();
            List<Vector3D> transPoints = new List<Vector3D>();
            List<Vector3D> pointsBottom = new List<Vector3D>();
            RefPlane refPlane = RefPlane.XY_PLANE;

            pointsTop = CreateEllipse(height / 2.0, radius, radius, anglet);
            pointsBottom = CreateEllipse(-height / 2.0, radius, radius, anglet);

            pointsBottom = pointsBottom.Union(pointsTop).ToList();

            rotPoints = RotatePoints(xAngle, yAngle, zAngle, pointsBottom);
            transPoints = TranslatePoints(center.X, center.Y, center.Z, rotPoints);
            SetNormal(0.0, 0.0, 1.0);
            _normal = RotateVector(xAngle, yAngle, zAngle, _normal);

            GetRefPlane(ref refPlane);
            CalculateAngleAR(ref _AngleR, ref _AngleH, _normal, refPlane);

            _center = center;

            return transPoints;

        }
        public List<Vector3D> CreateSteppedCylinder(Vector3D center, double radius1, double radius2, double height, double anglet, double xAngle, double yAngle, double zAngle)
        {
            List<Vector3D> pointsTop = new List<Vector3D>();
            List<Vector3D> rotPoints = new List<Vector3D>();
            List<Vector3D> transPoints = new List<Vector3D>();
            List<Vector3D> pointsBottom = new List<Vector3D>();
            RefPlane refPlane = RefPlane.XY_PLANE;

            pointsTop = CreateEllipse(height / 2.0, radius1, radius1, anglet);
            pointsBottom = CreateEllipse(-height / 2.0, radius2, radius2, anglet);

            pointsBottom = pointsBottom.Union(pointsTop).ToList();

            rotPoints = RotatePoints(xAngle, yAngle, zAngle, pointsBottom);
            transPoints = TranslatePoints(center.X, center.Y, center.Z, rotPoints);
            SetNormal(0.0, 0.0, 1.0);
            _normal = RotateVector(xAngle, yAngle, zAngle, _normal);

            GetRefPlane(ref refPlane);
            CalculateAngleAR(ref _AngleR, ref _AngleH, _normal, refPlane);

            _center = center;

            return transPoints;

        }
        public List<Vector3D> CreateCircle(Vector3D center, double Radius, double anglet, double xAngle, double yAngle, double zAngle)
        {
            List<Vector3D> points = new List<Vector3D>();

            points = CreateEllipse(center, Radius, Radius, anglet, xAngle, yAngle, zAngle);

            return points;
        }
        public List<Vector3D> CreateSphere(Vector3D center, double Radius, double anglet1, double anglet2)
        {
            List<Vector3D> points = new List<Vector3D>();
            List<Vector3D> pointResults = new List<Vector3D>();
            double angle = 0.0;
            for (angle = 0.0; angle < 360.0 - 1; angle += anglet2)
            {
                pointResults = CreateEllipse(center, Radius, Radius, anglet1, angle, 0.0, 0.0);
                foreach (Vector3D vec in pointResults)
                {
                    points.Add(vec);
                }

            }
            return points;
        }
        public List<Vector3D> CreateEllipse(Vector3D center, double aRadius, double bRadius, double anglet, double xAngle, double yAngle, double zAngle)
        {
            // x=a*cos(t)
            // y=b*sin(t)
            Vector3D point = new Vector3D();
            List<Vector3D> points = new List<Vector3D>();
            List<Vector3D> rotPoints = new List<Vector3D>();
            List<Vector3D> transPoints = new List<Vector3D>();

            RefPlane refPlane = RefPlane.XY_PLANE;

            double angleRad = 0.0;
            anglet = anglet * Math.PI / 180.0;
            for (angleRad = 0.0; angleRad < 2 * Math.PI; angleRad += anglet)
            {
                point.X = aRadius * Math.Cos(angleRad);
                point.Y = bRadius * Math.Sin(angleRad);
                point.Z = 0.0;
                points.Add(point);
            }

            rotPoints = RotatePoints(xAngle, yAngle, zAngle, points);
            transPoints = TranslatePoints(center.X, center.Y, center.Z, rotPoints);
            if (aRadius >= bRadius)
            {
                SetMajorAxis(-1.0, 0.0, 0.0);
            }
            else
            {
                SetMajorAxis(0.0, -1.0, 0.0);
            }
            SetNormal(0.0, 0.0, 1.0);
            _normal = RotateVector(xAngle, yAngle, zAngle, _normal);
            _majorAxis = RotateVector(xAngle, yAngle, zAngle, _majorAxis);
            GetRefPlane(ref refPlane);
            CalculateAngleAR(ref _AngleR, ref _AngleH, _majorAxis, refPlane);
            _center = center;
            return transPoints;

        }
        public List<Vector3D> CreateEllipse(double Z, double aRadius, double bRadius, double anglet)
        {
            // x=a*cos(t)
            // y=b*sin(t)
            Vector3D point = new Vector3D();
            List<Vector3D> points = new List<Vector3D>();
            double angleRad = 0.0;
            anglet = anglet * Math.PI / 180.0;
            for (angleRad = 0.0; angleRad < 2 * Math.PI; angleRad += anglet)
            {
                point.X = aRadius * Math.Cos(angleRad);
                point.Y = bRadius * Math.Sin(angleRad);
                point.Z = Z;
                points.Add(point);
            }

            return points;

        }
        public double DegreeToRadian(double angle)
        {
            return angle * Math.PI / 180.0;

        }
        public double RadianToDegree(double angle)
        {
            return (angle / Math.PI) * 180.0;

        }
        public Vector3D RotatePoint(double xAngle, double yAngle, double zAngle, Vector3D point)
        {
            Matrix3D mt3d = new Matrix3D();


            Matrix3D mt3dX = new Matrix3D(1.0, 0.0, 0.0, 0.0,
                                          0.0, Math.Cos(DegreeToRadian(xAngle)), -Math.Sin(DegreeToRadian(xAngle)), 0.0,
                                          0.0, Math.Sin(DegreeToRadian(xAngle)), Math.Cos(DegreeToRadian(xAngle)), 0.0,
                                          0.0, 0.0, 0.0, 1.0);

            Matrix3D mt3dY = new Matrix3D(Math.Cos(DegreeToRadian(yAngle)), 0.0, Math.Sin(DegreeToRadian(yAngle)), 0.0,
                                          0.0, 1.0, 0.0, 0.0,
                                          -Math.Sin(DegreeToRadian(yAngle)), 0.0, Math.Cos(DegreeToRadian(yAngle)), 0.0,
                                          0.0, 0.0, 0.0, 1.0);

            Matrix3D mt3dZ = new Matrix3D(Math.Cos(DegreeToRadian(zAngle)), -Math.Sin(DegreeToRadian(zAngle)), 0.0, 0.0,
                                          Math.Sin(DegreeToRadian(zAngle)), Math.Cos(DegreeToRadian(zAngle)), 0.0, 0.0,
                                          0.0, 0.0, 1.0, 0.0,
                                          0.0, 0.0, 0.0, 1.0);
            //               mt3dX.Invert();
            //               mt3dY.Invert();
            //               mt3dZ.Invert();
            mt3d = mt3dY * mt3dX * mt3dZ;
            Vector3D pointRet = new Vector3D();

            pointRet = Vector3D.Multiply(point, mt3d);
            pointRet.X = Math.Round(pointRet.X, 10);
            pointRet.Y = Math.Round(pointRet.Y, 10);
            pointRet.Z = Math.Round(pointRet.Z, 10);


            return pointRet;
        }

        public List<Vector3D> RotatePoints(double xAngle, double yAngle, double zAngle, List<Vector3D> points)
        {
            
            List<Vector3D> pointResults = new List<Vector3D>();
            Vector3D pointRet = new Vector3D();

            for (int i = 0; i <= points.Count - 1; i++)
            {
                pointRet = RotatePoint(xAngle, yAngle, zAngle, points[i]);
                pointRet.X = Math.Round(pointRet.X, 10);
                pointRet.Y = Math.Round(pointRet.Y, 10);
                pointRet.Z = Math.Round(pointRet.Z, 10);
                pointResults.Add(pointRet);
            }


            return pointResults;
        }
         public Vector3D RotateVector(double xAngle, double yAngle, double zAngle, Vector3D vector)
        {

            Vector3D vectorRet = RotatePoint(xAngle, yAngle, zAngle, vector);

            return vectorRet;
        }
        List<Vector3D> TranslatePoints(double xTranslation, double yTranslation, double zTranslation, List<Vector3D> points)
        {

            List<Vector3D> pointResults = new List<Vector3D>();
            Vector3D pointRet = new Vector3D();

            for (int i = 0; i <= points.Count - 1; i++)
            {
                pointRet = Vector3D.Add(points[i], new Vector3D(xTranslation, yTranslation, zTranslation));
                pointResults.Add(pointRet);
            }


            return pointResults;
        }
        public int CalculateAngleAR(ref double arAngle, ref double ahAngle, Vector3D DirectionCos, RefPlane refPlane)
        {
            if (refPlane == RefPlane.XY_PLANE)
            {
                arAngle = RadianToDegree(Math.Atan(DirectionCos.Y / DirectionCos.X));
                ahAngle = RadianToDegree(Math.Acos(DirectionCos.Z));

                if ((DirectionCos.X < 0) && (DirectionCos.Y > 0))
                {
                    arAngle = arAngle + 180.0;
                }
                else if ((DirectionCos.X < 0) && (DirectionCos.Y < 0))
                {
                    arAngle = arAngle + 180.0;
                }

                else if ((DirectionCos.X > 0) && (DirectionCos.Y < 0))
                {
                    arAngle = arAngle + 360;
                }
                
                else if ((DirectionCos.X == 0) && (DirectionCos.Y == 0))
                {
                    arAngle = 0.0;
                }

                else if ((DirectionCos.X == -1) && (DirectionCos.Y == 0.0))
                {
                    arAngle = 180.0;
                }
                else if ((DirectionCos.X == 0.0) && (DirectionCos.Y == -1.0))
                {
                    arAngle = 270.0;
                }

            }
            else if (refPlane == RefPlane.YZ_PLANE)
            {

                arAngle = RadianToDegree(Math.Atan(DirectionCos.Z / DirectionCos.Y));
                ahAngle = RadianToDegree(Math.Acos(DirectionCos.X));
                if ((DirectionCos.Y < 0) && (DirectionCos.Z > 0))
                {
                    arAngle = arAngle + 180.0;
                }
                else if ((DirectionCos.Y < 0) && (DirectionCos.Z < 0))
                {
                    arAngle = arAngle + 180.0;
                }
                else if ((DirectionCos.Y == 0) && (DirectionCos.Z == 0))
                {
                    arAngle = 0.0;
                }

                else if ((DirectionCos.Y > 0) && (DirectionCos.Z < 0))
                {
                    arAngle = arAngle + 360;
                }

            }
            else if (refPlane == RefPlane.ZX_PLANE)
            {

                arAngle = RadianToDegree(Math.Atan(DirectionCos.X / DirectionCos.Z));
                ahAngle = RadianToDegree(Math.Acos(DirectionCos.Y));
                if ((DirectionCos.Z < 0) && (DirectionCos.X > 0))
                {
                    arAngle = arAngle + 180.0;
                }
                else if ((DirectionCos.Z < 0) && (DirectionCos.X < 0))
                {
                    arAngle = arAngle + 180.0;
                }
                else if ((DirectionCos.Z == 0) && (DirectionCos.X == 0))
                {
                    arAngle = 0.0;
                }

                else if ((DirectionCos.Z > 0) && (DirectionCos.X < 0))
                {
                    arAngle = arAngle + 360;
                }


            }
            return 0;
        }

        public void OutputPoints(FeatureType type, string label, List<Vector3D> pointResults, string fileName)
        {
            double angleX = 0.0, angleY = 0.0, angleZ = 0.0;
            double majorAngleX = 0.0, majorAngleY = 0.0, majorAngleZ = 0.0;
            double centerX = 0.0, centerY = 0.0, centerZ = 0.0;
            double radiusA = 0.0, radiusB = 0.0;
            double diameterA = 0.0, diameterB = 0.0;
            double AngleAR = 0.0, AngleAH = 0.0;
            double X = 0.0, Y = 0.0, Z = 0.0;

            string label_keyin = "";
            const string quote = "\"";
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileName))
            {
                file.WriteLine("Sub Main");

                switch (type)
                {

                    case FeatureType.enLine:
                        GetCenter(ref centerX, ref centerY, ref centerZ);
                        file.WriteLine("'X       = " + centerX.ToString());
                        file.WriteLine("'Y       = " + centerY.ToString());
                        file.WriteLine("'Z       = " + centerZ.ToString());
                        GetNormalAngle(ref angleX, ref angleY, ref angleZ);
                        file.WriteLine("'X-Angle = " + angleX.ToString());
                        file.WriteLine("'Y-Angle = " + angleY.ToString());
                        file.WriteLine("'Z-Angle = " + angleZ.ToString());
                        file.WriteLine(@"Measure.Line Label:=" + quote + label + quote + ", ProjPlane:=NO_PLANE, Outlier:=0");
                        OutputPoints(pointResults, file);
                        file.WriteLine("Measure.EndMeas");
                        file.WriteLine("Results.ReportFeature   Show:=X_ and Y_ and Z_ and AR_ and XA_ and YA_ and ZA_ and ST_, Tag:=0");
                        file.WriteLine(" ");
                        GetAngleRH(ref AngleAR, ref AngleAH);
                        label_keyin = label + "-Keyin";
                        //                       string sAngle = AngleAR.ToString();
                        file.WriteLine("FeatureDB.KeyinLine   X:= {0}, Y:= {1}, Z:= {2}, AR:= {3}, AH:= {4}, ST:= {5}, Label:= {6}",
                                        centerX.ToString(), centerY.ToString(), centerZ.ToString(), AngleAR.ToString(), AngleAH.ToString(),
                                        "0.0", quote + label_keyin + quote);
                        file.WriteLine(@"FeatureDB(""{5}"").SetLineNoms   X:= {0}, Y:= {1}, Z:= {2}, AR:= {3}, AH:= {4}",
                                        centerX.ToString(), centerY.ToString(), centerZ.ToString(), AngleAR.ToString(), AngleAH.ToString(), label_keyin);
                        file.WriteLine("Results.ReportFeature   Show:=X_ and Y_ and Z_ and AR_ and XA_ and YA_ and ZA_ and ST_, Tag:=0");
                        break;
                    case FeatureType.enPlane:
                        GetCenter(ref centerX, ref centerY, ref centerZ);
                        file.WriteLine("'X       = " + centerX.ToString());
                        file.WriteLine("'Y       = " + centerY.ToString());
                        file.WriteLine("'Z       = " + centerZ.ToString());
                        GetNormalAngle(ref angleX, ref angleY, ref angleZ);
                        file.WriteLine("'X-Angle = " + angleX.ToString());
                        file.WriteLine("'Y-Angle = " + angleY.ToString());
                        file.WriteLine("'Z-Angle = " + angleZ.ToString());
                        file.WriteLine("Measure.Plane Label:=" + quote + label + quote + ", Outlier:=0");
                        OutputPoints(pointResults, file);
                        file.WriteLine("Measure.EndMeas");
                        file.WriteLine("Results.ReportFeature   Show:=X_ and Y_ and Z_ and AR_ and XA_ and YA_ and ZA_ and FT_, Tag:=0");
                        file.WriteLine(" ");
                        GetAngleRH(ref AngleAR, ref AngleAH);
                        label_keyin = label + "-Keyin";
                        //                       string sAngle = AngleAR.ToString();
                        file.WriteLine("FeatureDB.KeyinPlane   X:= {0}, Y:= {1}, Z:= {2}, AR:= {3}, AH:= {4}, FT:= {5}, Label:= {6}",
                                        centerX.ToString(), centerY.ToString(), centerZ.ToString(), AngleAR.ToString(), AngleAH.ToString(),
                                        "0.0", quote + label_keyin + quote);
                        file.WriteLine(@"FeatureDB(""{5}"").SetPlaneNoms   X:= {0}, Y:= {1}, Z:= {2}, AR:= {3}, AH:= {4}",
                                        centerX.ToString(), centerY.ToString(), centerZ.ToString(), AngleAR.ToString(), AngleAH.ToString(), label_keyin);
                        file.WriteLine("Results.ReportFeature   Show:=X_ and Y_ and Z_ and AR_ and XA_ and YA_ and ZA_ and FT_, Tag:=0");
                        break;
                    case FeatureType.enCircle:
                        GetCenter(ref centerX, ref centerY, ref centerZ);
                        file.WriteLine("'X       = " + centerX.ToString());
                        file.WriteLine("'Y       = " + centerY.ToString());
                        file.WriteLine("'Z       = " + centerZ.ToString());
                        file.WriteLine("Measure.Circle Label:=" + quote + label + quote + ", ProjPlane:=NO_PLANE, Outlier:=0");
                        OutputPoints(pointResults, file);
                        file.WriteLine("Measure.EndMeas");
                        file.WriteLine("Results.ReportFeature   Show:=X_ and Y_ and Z_ and CR_ and D_ and RD_, Tag:=0");

                        file.WriteLine(" ");
                        GetRadius(ref radiusA, ref radiusB);
                        diameterA = radiusA * 2.0;
                        label_keyin = label + "-Keyin";
                        file.WriteLine("FeatureDB.KeyinCircle   X:= {0}, Y:= {1}, Z:= {2}, D:= {3}, CR:= {4}, Label:= {5}",
                                        centerX.ToString(), centerY.ToString(), centerZ.ToString(), diameterA.ToString(), 0.0, quote + label + "-Keyin" + quote);
                        file.WriteLine(@"FeatureDB(""{4}"").SetCircleNoms   X:= {0}, Y:= {1}, Z:= {2}, D:= {3}",
                                        centerX.ToString(), centerY.ToString(), centerZ.ToString(), diameterA.ToString(), label_keyin);
                        file.WriteLine("Results.ReportFeature   Show:=X_ and Y_ and Z_ and CR_ and D_ and RD_, Tag:=0");
                        break;
                    case FeatureType.enEllipse:
                        GetCenter(ref centerX, ref centerY, ref centerZ);
                        file.WriteLine("'X       = " + centerX.ToString());
                        file.WriteLine("'Y       = " + centerY.ToString());
                        file.WriteLine("'Z       = " + centerZ.ToString());
                        GetMajorAxisAngle(ref majorAngleX, ref majorAngleY, ref majorAngleZ);
                        file.WriteLine("'X-Angle2 = " + majorAngleX.ToString());
                        file.WriteLine("'Y-Angle2 = " + majorAngleY.ToString());
                        file.WriteLine("'Z-Angle2 = " + majorAngleZ.ToString());
                        GetNormalAngle(ref angleX, ref angleY, ref angleZ);
                        file.WriteLine("'X-Angle = " + angleX.ToString());
                        file.WriteLine("'Y-Angle = " + angleY.ToString());
                        file.WriteLine("'Z-Angle = " + angleZ.ToString());

                        file.WriteLine("Measure.Ellipse Label:=" + quote + label + quote + ", ProjPlane:=NO_PLANE");
                        OutputPoints(pointResults, file);
                        file.WriteLine("Measure.EndMeas");
                        file.WriteLine("Results.ReportFeature   Show:=X_ and Y_ and Z_ and AR2_ and XA2_ and YA2_ and ZA2_ and D1_ and D2_, Tag:=0");
                        file.WriteLine(@"Print ""XA="" ;FeatureDB.item(tag:=0).XA.Actual");
                        file.WriteLine(@"Print ""YA="" ;FeatureDB.item(tag:=0).YA.Actual");
                        file.WriteLine(@"Print ""ZA="" ;FeatureDB.item(tag:=0).ZA.Actual");


                        file.WriteLine(" ");
                        GetAngleRH(ref AngleAR, ref AngleAH);
                        GetRadius(ref radiusA, ref radiusB);
                        diameterA = radiusA * 2.0;
                        diameterB = radiusB * 2.0;
                        label_keyin = label + "-Keyin";
                        string sAngle = AngleAR.ToString();
                        file.WriteLine("FeatureDB.KeyinEllipse   X:= {0}, Y:= {1}, Z:= {2}, AR2:= {3}, AH2:= {4}, D1:= {5}, D2:= {6}, Label:= {7}",
                                        centerX.ToString(), centerY.ToString(), centerZ.ToString(), AngleAR.ToString(), AngleAH.ToString(),
                                        diameterA.ToString(), diameterB.ToString(), quote + label_keyin + quote);
                        file.WriteLine(@"FeatureDB(""{7}"").SetEllipseNoms   X:= {0}, Y:= {1}, Z:= {2}, AR2:= {3}, AH2:= {4}, D1:= {5}, D2:= {6}",
                                        centerX.ToString(), centerY.ToString(), centerZ.ToString(), AngleAR.ToString(), AngleAH.ToString(),
                                        diameterA.ToString(), diameterB.ToString(), label_keyin);
                        file.WriteLine("Results.ReportFeature   Show:=X_ and Y_ and Z_ and AR2_ and XA2_ and YA2_ and ZA2_ and D1_ and D2_, Tag:=0");

                        break;
                    case FeatureType.enSphere:
                        GetCenter(ref centerX, ref centerY, ref centerZ);
                        file.WriteLine("'X       = " + centerX.ToString());
                        file.WriteLine("'Y       = " + centerY.ToString());
                        file.WriteLine("'Z       = " + centerZ.ToString());
                        file.WriteLine("Measure.Sphere Label:=" + quote + label + quote + ", Outlier:=0");
                        OutputPoints(pointResults, file);
                        file.WriteLine("Measure.EndMeas");
                        file.WriteLine("Results.ReportFeature   Show:=X_ and Y_ and Z_ and SP_ and D_ and RD_, Tag:=0");

                        file.WriteLine(" ");
                        GetRadius(ref radiusA, ref radiusB);
                        diameterA = radiusA * 2.0;
                        label_keyin = label + "-Keyin";
                        file.WriteLine("FeatureDB.KeyinSphere   X:= {0}, Y:= {1}, Z:= {2}, D:= {3}, SP:= {4}, Label:= {5}",
                                        centerX.ToString(), centerY.ToString(), centerZ.ToString(), diameterA.ToString(), 0.0, quote + label + "-Keyin" + quote);
                        file.WriteLine(@"FeatureDB(""{4}"").SetSphereNoms   X:= {0}, Y:= {1}, Z:= {2}, D:= {3}",
                                        centerX.ToString(), centerY.ToString(), centerZ.ToString(), diameterA.ToString(), label_keyin);
                        file.WriteLine("Results.ReportFeature   Show:=X_ and Y_ and Z_ and SP_ and D_ and RD_, Tag:=0");

                        break;
                    case FeatureType.enCylinder:
                        GetCenter(ref centerX, ref centerY, ref centerZ);
                        file.WriteLine("'X       = " + centerX.ToString());
                        file.WriteLine("'Y       = " + centerY.ToString());
                        file.WriteLine("'Z       = " + centerZ.ToString());
                        GetNormalAngle(ref angleX, ref angleY, ref angleZ);
                        file.WriteLine("'X-Angle = " + angleX.ToString());
                        file.WriteLine("'Y-Angle = " + angleY.ToString());
                        file.WriteLine("'Z-Angle = " + angleZ.ToString());
                        file.WriteLine("Measure.Cylinder Label:=" + quote + label + quote + ", Outlier:=0");
                        OutputPoints(pointResults, file);
                        file.WriteLine("Measure.EndMeas");
                        file.WriteLine("Results.ReportFeature   Show:=X_ and Y_ and Z_ and AR_ and XA_ and YA_ and ZA_ and CL_ and D_ and RD_, Tag:=0");
                        file.WriteLine(" ");
                        GetAngleRH(ref AngleAR, ref AngleAH);
                        label_keyin = label + "-Keyin";
                        //                       string sAngle = AngleAR.ToString();
                        file.WriteLine("FeatureDB.KeyinCylinder   X:= {0}, Y:= {1}, Z:= {2}, AR:= {3}, AH:= {4}, D:= {5}, CL:= {6}, Label:= {7}",
                                        centerX.ToString(), centerY.ToString(), centerZ.ToString(), AngleAR.ToString(), AngleAH.ToString(),
                                        diameterA.ToString(), "0.0", quote + label_keyin + quote);
                        file.WriteLine(@"FeatureDB(""{6}"").SetCylinderNoms   X:= {0}, Y:= {1}, Z:= {2}, AR:= {3}, AH:= {4}, D:= {5}",
                                        centerX.ToString(), centerY.ToString(), centerZ.ToString(), AngleAR.ToString(), AngleAH.ToString(),
                                        diameterA.ToString(), label_keyin);
                        file.WriteLine("Results.ReportFeature   Show:=X_ and Y_ and Z_ and AR_ and XA_ and YA_ and ZA_ and CL_ and D_ and RD_, Tag:=0");
                        break;
                    case FeatureType.enSteppedCylinder:
                        GetCenter(ref centerX, ref centerY, ref centerZ);
                        file.WriteLine("'X       = " + centerX.ToString());
                        file.WriteLine("'Y       = " + centerY.ToString());
                        file.WriteLine("'Z       = " + centerZ.ToString());
                        GetNormalAngle(ref angleX, ref angleY, ref angleZ);
                        file.WriteLine("'X-Angle = " + angleX.ToString());
                        file.WriteLine("'Y-Angle = " + angleY.ToString());
                        file.WriteLine("'Z-Angle = " + angleZ.ToString());
                        file.WriteLine("Measure.SteppedCylinder Label:=" + quote + label + quote);
                        int numberOfPoints = pointResults.Count;

                        OutputPoints(pointResults, file, true);
                        file.WriteLine("Measure.EndMeas");
                        file.WriteLine("Results.ReportFeature   Show:=X_ and Y_ and Z_ and AR_ and XA_ and YA_ and ZA_ and D1_ and D2_ and R1_ and R2_, Tag:=0");
                        file.WriteLine(" ");
                        GetRadius(ref radiusA, ref radiusB);
                        diameterA = radiusA * 2.0;
                        diameterB = radiusB * 2.0;
 
                        GetAngleRH(ref AngleAR, ref AngleAH);
                        label_keyin = label + "-Keyin";
                        //                       string sAngle = AngleAR.ToString();
                        file.WriteLine("FeatureDB.KeyinSteppedCylinder   X:= {0}, Y:= {1}, Z:= {2}, AR:= {3}, AH:= {4}, D1:= {5}, D2:= {6}, Label:= {7}",
                                        centerX.ToString(), centerY.ToString(), centerZ.ToString(), AngleAR.ToString(), AngleAH.ToString(),
                                        diameterA.ToString(), diameterB.ToString(), quote + label_keyin + quote);
                        file.WriteLine(@"FeatureDB(""{7}"").SetSteppedCylinderNoms   X:= {0}, Y:= {1}, Z:= {2}, AR:= {3}, AH:= {4}, D1:= {5}, D2:= {6}",
                                        centerX.ToString(), centerY.ToString(), centerZ.ToString(), AngleAR.ToString(), AngleAH.ToString(),
                                        diameterA.ToString(), diameterB.ToString(), label_keyin);
                        file.WriteLine("Results.ReportFeature   Show:=X_ and Y_ and Z_ and AR_ and XA_ and YA_ and ZA_ and D1_ and D2_ and R1_ and R2_, Tag:=0");

                        break;
                    case FeatureType.enCone:
                        GetCenter(ref centerX, ref centerY, ref centerZ);
                        //                       file.WriteLine("'X       = " + centerX.ToString());
                        //                       file.WriteLine("'Y       = " + centerY.ToString());
                        //                       file.WriteLine("'Z       = " + centerZ.ToString());
                        GetNormalAngle(ref angleX, ref angleY, ref angleZ);
                        file.WriteLine("'X-Angle = " + angleX.ToString());
                        file.WriteLine("'Y-Angle = " + angleY.ToString());
                        file.WriteLine("'Z-Angle = " + angleZ.ToString());
                        file.WriteLine("Measure.Cone Label:=" + quote + label + quote + ", Outlier:=0");
                        OutputPoints(pointResults, file);
                        file.WriteLine("Measure.EndMeas");
                        file.WriteLine("Results.ReportFeature   Show:=X_ and Y_ and Z_ and AR_ and XA_ and YA_ and ZA_ and CN_ and TA_ and SA_, Tag:=0");
                        file.WriteLine(" ");
                        GetAngleRH(ref AngleAR, ref AngleAH);
                        label_keyin = label + "-Keyin";
                        //                       string sAngle = AngleAR.ToString();
                        file.WriteLine("FeatureDB.KeyinCone   X:= {0}, Y:= {1}, Z:= {2}, AR:= {3}, AH:= {4}, D:= 0.0, TA:=30.0 ,CN:= 0.0, Label:= {5}",
                                        centerX.ToString(), centerY.ToString(), centerZ.ToString(), AngleAR.ToString(), AngleAH.ToString(), quote + label_keyin + quote);
                        file.WriteLine(@"FeatureDB(""{5}"").SetConeNoms   X:= {0}, Y:= {1}, Z:= {2}, AR:= {3}, AH:= {4}, TA:=30.0",
                                        centerX.ToString(), centerY.ToString(), centerZ.ToString(), AngleAR.ToString(), AngleAH.ToString(), label_keyin);
                        file.WriteLine("Results.ReportFeature   Show:=X_ and Y_ and Z_ and AR_ and XA_ and YA_ and ZA_ and CN_ and TA_ and SA_, Tag:=0");
                        break;
                    default:
                        break;

                }
                file.WriteLine("End Sub");
                file.Close();

            }
        }
        public void OutputPoints(List<Vector3D> pointResults, string fileName)
        {

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileName))
            {
                for (int i = 0; i <= pointResults.Count - 1; i++)
                {
                    file.WriteLine("Measure.KeyinDataPoint   X:=" + pointResults[i].X.ToString() + "," +
                                   " Y:=" + pointResults[i].Y.ToString() + "," +
                                   " Z:=" + pointResults[i].Z.ToString());
                }
            }

        }
        public void OutputPoints(List<Vector3D> pointResults, System.IO.StreamWriter file)
        {

            for (int i = 0; i <= pointResults.Count - 1; i++)
            {
                file.WriteLine("Measure.KeyinDataPoint   X:=" + pointResults[i].X.ToString() + "," +
                                " Y:=" + pointResults[i].Y.ToString() + "," +
                                " Z:=" + pointResults[i].Z.ToString());
            }

        }
        public void OutputPoints(List<Vector3D> pointResults, System.IO.StreamWriter file, bool stepped)
        {

            for (int i = 0; i <= pointResults.Count - 1; i++)
            {
                file.WriteLine("Measure.KeyinDataPoint   X:=" + pointResults[i].X.ToString() + "," +
                                " Y:=" + pointResults[i].Y.ToString() + "," +
                                " Z:=" + pointResults[i].Z.ToString());

                if (stepped && (i == pointResults.Count / 2 - 1))
                {
                    file.WriteLine("Measure.NextStep");
                }
            }

        }
    }

}
