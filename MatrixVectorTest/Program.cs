using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using MatrixVectorTest;

namespace ConsoleApplication2
{
    class Program
    {
        static void TestPoints()
        {
            Shapes geoShapes = new Shapes();

            double radiusA = 4.0;
            double radiusB = 2.0;
            double iVector = 1.0;
            double jVector = 1.0;
            double kVector = 1.0;
            
            List<Vector3D> points = new List<Vector3D>();
            List<Vector3D> pointResults = new List<Vector3D>();
            List<Vector3D> pointResultsA = new List<Vector3D>();
            Vector3D center1b = new Vector3D();
            center1b.X = 0.0; center1b.Y = 0.0; center1b.Z = 0.0;

            points = geoShapes.CreateEllipse(0.0, radiusA, radiusB, 60.0);
            Vector3D directionCos = geoShapes.ConvertToUnitVector(iVector, jVector, kVector);

            double xAngle = 0.0;
            double yAngle = 0.0;
            double zAngle = 0.0;
            yAngle = geoShapes.RadianToDegree(Math.Acos(directionCos.Z));
            zAngle = geoShapes.RadianToDegree(Math.Atan(directionCos.Y / directionCos.X));
            pointResults = geoShapes.RotatePoints(xAngle, yAngle, zAngle, points);
            pointResultsA = geoShapes.RotatePoints(xAngle, yAngle, zAngle, points);

        }
        static void RotateVectors()
        {

            Shapes geoShapes = new Shapes();
            Vector3D major1 = new Vector3D();
            Vector3D major2 = new Vector3D();
            Vector3D major3 = new Vector3D();
            Vector3D major4 = new Vector3D();

            major1.X = 0.70710678; major1.Y = -0.70710678; major1.Z = 0.0;
            major2 = geoShapes.RotateVector(0.0, 90.0, 0.0, major1);
            major3 = geoShapes.RotateVector(90.0, 0.0, 0.0, major1);
            major4 = geoShapes.RotateVector(0.0, 0.0, 90.0, major1);

            Vector3D norm1 = new Vector3D();
            Vector3D norm2 = new Vector3D();
            Vector3D norm3 = new Vector3D();
            Vector3D norm4 = new Vector3D();

            norm1.X = 0.0; norm1.Y = 0.0; norm1.Z = 1.0;
            norm2 = geoShapes.RotateVector(30.0, 0.0, 0.0, norm1);
            norm3 = geoShapes.RotateVector(0.0, 30.0, 0.0, norm1);
            norm4 = geoShapes.RotateVector(30.0, 30.0, 30.0, norm1);
        
        }
        static void EllipseMultipleRot()
        {
            ShapesImpl geoShapes = new ShapesImpl();

            Vector3D center = new Vector3D();
            center.X = 2.5; center.Y = 2.5; center.Z = 2.0;

            Vector3D direction = new Vector3D();
            Vector3D normal = new Vector3D();
            Vector3D majorAxis = new Vector3D();

            direction.X = 1.0; direction.Y = 1.0; direction.Z = 1.0;
            List<Vector3D> points = new List<Vector3D>();
            List<Vector3D> pointsYZ = new List<Vector3D>();
            List<Vector3D> pointsZX = new List<Vector3D>();

            string currpath = Directory.GetCurrentDirectory();
            Directory.CreateDirectory(currpath + @"\MultipleRot");
            string path = currpath + @"\MultipleRot\";
            
            double aRadius = 2.0;
            double bRadius = 4.0;

            direction.X = 0.0; direction.Y = 0.0; direction.Z = 1.0;
            points = geoShapes.CreateEllipse(center, aRadius, bRadius, 60.0, direction);
            points = geoShapes.RotatePoints(0.0, 0.0, 30.0, points);
            geoShapes.SetNormal(normal);
            normal = geoShapes.GetNormal();
            normal = geoShapes.RotateVector(0.0, 0.0, 30.0, normal);
            geoShapes.SetNormal(normal);
            majorAxis = geoShapes.GetMajorAxis();
            majorAxis = geoShapes.RotateVector(0.0, 0.0, 30.0, majorAxis);
            geoShapes.SetMajorAxis(majorAxis);
            geoShapes.OutputPoints(FeatureType.enEllipse, "Ellipse-XY-30", points, path + @"testcaseEllipse-XY-30.txt");
            
            pointsYZ = geoShapes.RotatePoints(0.0, 90.0, 0.0, points);
            center = geoShapes.GetCenter();
            center = geoShapes.RotateVector(0.0, 90.0, 0.0, center);
            geoShapes.SetCenter(center);
            normal = geoShapes.GetNormal();
            normal = geoShapes.RotateVector(0.0, 90.0, 0.0, normal);
            geoShapes.SetNormal(normal);
            majorAxis = geoShapes.GetMajorAxis();
            majorAxis = geoShapes.RotateVector(0.0, 90.0, 0.0, majorAxis);
            geoShapes.SetMajorAxis(majorAxis);
            geoShapes.OutputPoints(FeatureType.enEllipse, "Ellipse-YZ-30", pointsYZ, path + @"testcaseEllipse-YZ-30.txt");
            
            pointsZX = geoShapes.RotatePoints(90.0, 0.0, 0.0, points);
            center = geoShapes.GetCenter();
            center = geoShapes.RotateVector(90.0, 0.0, 0.0, center);
            geoShapes.SetCenter(center);
            normal = geoShapes.GetNormal();
            normal = geoShapes.RotateVector(90.0, 0.0, 0.0, normal);
            geoShapes.SetNormal(normal);
            majorAxis = geoShapes.GetMajorAxis();
            majorAxis = geoShapes.RotateVector(90.0, 0.0, 0.0, majorAxis);
            geoShapes.SetMajorAxis(majorAxis);
            geoShapes.OutputPoints(FeatureType.enEllipse, "Ellipse-ZX-30", pointsZX, path + @"testcaseEllipse-ZX-30.txt");
            points.Clear();
            

        }
        static void TestShapes()
        {
            ShapesImpl geoShapes = new ShapesImpl();
            geoShapes.SetRefPlane(RefPlane.XY_PLANE);

            Vector3D center = new Vector3D();
            center.X = 2.5; center.Y = 2.5; center.Z = 2.0;
            
            Vector3D direction = new Vector3D();
            direction.X = 1.0; direction.Y = 1.0; direction.Z = 1.0;
            List<Vector3D> points = new List<Vector3D>();
            List<Vector3D> ptsTop = new List<Vector3D>();
            List<Vector3D> ptsBottom = new List<Vector3D>();

            string currpath = Directory.GetCurrentDirectory();
            Directory.CreateDirectory(currpath + @"\UnitVector");
            string path = currpath + @"\UnitVector\";
            points.Add(new Vector3D(0.0, 0.0, 0.0));
            points.Add(new Vector3D(1.0, 2.0, 1.0));
            points = geoShapes.CreateLine(center, points, direction);
            geoShapes.OutputPoints(FeatureType.enLine, "line-1", points, path + @"testcaseLine-1.qvb");
            points.Clear();

            points.Add(new Vector3D(0.0, 0.0, 0.0));
            points.Add(new Vector3D(1.0, 2.0, 1.0));
            points = geoShapes.CreateLine(center, points);
            geoShapes.OutputPoints(FeatureType.enLine, "line-2", points, path + @"testcaseLine-2.qvb");
            points.Clear();
            
            double aRadius = 2.0;
            double bRadius = 4.0;

            double radius = 4.0;
            double height = 2.0;
            direction.X = 0.0; direction.Y = 0.0; direction.Z = 1.0;
            points = geoShapes.CreateEllipse(center, aRadius, bRadius, 60.0, direction);
            geoShapes.OutputPoints(FeatureType.enEllipse, "Ellipse-XY", points, path + @"testcaseEllipse-XY.qvb");
            points.Clear();
            
            points = geoShapes.CreateCone(center, 2.0, 4.0, 4.0, 90, direction);
            geoShapes.OutputPoints(FeatureType.enCone, "Cone-simple", points, path + @"testcaseCone-simple.qvb");
            points.Clear();

            direction.X = 0.0; direction.Y = 1.0; direction.Z = 0.0;
            points = geoShapes.CreateEllipse(center, aRadius, bRadius, 60.0, direction);
            geoShapes.OutputPoints(FeatureType.enEllipse, "Ellipse-ZX", points, path + @"testcaseEllipse-ZX.qvb");
            points.Clear();

            direction.X = 1.0; direction.Y = 0.0; direction.Z = 0.0;
            points = geoShapes.CreateEllipse(center, aRadius, bRadius, 60.0, direction);
            geoShapes.OutputPoints(FeatureType.enEllipse, "Ellipse-YZ", points, path + @"testcaseEllipse-YZ.qvb");
            points.Clear();

            direction.X = 0.0; direction.Y = 1.0; direction.Z = 1.0;
            points = geoShapes.CreateEllipse(center, aRadius, bRadius, 60.0, direction);
            geoShapes.OutputPoints(FeatureType.enEllipse, "Ellipse-YZ-1", points, path + @"testcaseEllipse-YZ-1.qvb");
            points.Clear();

            direction.X = 1.0; direction.Y = 0.0; direction.Z = 1.0;
            points = geoShapes.CreateEllipse(center, aRadius, bRadius, 60.0, direction);
            geoShapes.OutputPoints(FeatureType.enEllipse, "Ellipse-ZX-1", points, path + @"testcaseEllipse-ZX-1.qvb");
            points.Clear();

           
            direction.X = 1.0; direction.Y = 1.0; direction.Z = 1.0;
            points = geoShapes.CreateCylinder(center, radius, height, 120.0, direction);
            geoShapes.OutputPoints(FeatureType.enCylinder, "Cylinder-1", points, path + @"testcaseCylinder-1.qvb");
            points.Clear();
            
            points = geoShapes.CreateEllipse(center, aRadius, bRadius, 60.0, direction);
            geoShapes.OutputPoints(FeatureType.enEllipse, "Ellipse-P1_P1_P1", points, path + @"testcaseEllipse-P1_P1_P1.qvb");
            points.Clear();

            points = geoShapes.CreateCircle(center, radius, 60.0, direction);
            geoShapes.OutputPoints(FeatureType.enCircle, "Circle-1", points, path + @"testcaseCircle-1.qvb");
            points.Clear();

            points = geoShapes.CreateCone(center, 2.0, 4.0, 4.0, 90, direction);
            geoShapes.OutputPoints(FeatureType.enCone, "Cone-1", points, path + @"testcaseCone-1.qvb");
            points.Clear();

            points = geoShapes.CreateSphere(center, 2.0, 120.0, 120.0);
            geoShapes.OutputPoints(FeatureType.enSphere, "Sphere-1", points, path + @"testcaseSphere-1.qvb");
            points.Clear();

            points = geoShapes.CreatePlane(center, 4.0, 4.0, direction);
            geoShapes.OutputPoints(FeatureType.enPlane, "Plane-1", points, path + @"testcasePlane-1.qvb");
            points.Clear();


            direction.X = -1.0; direction.Y = 1.0; direction.Z = 1.0;
            points = geoShapes.CreateCylinder(center, radius, height, 120.0, direction);
            geoShapes.OutputPoints(FeatureType.enCylinder, "Cylinder-2", points, path + @"testcaseCylinder-2.qvb");
            points.Clear();

            direction.X = -1.0; direction.Y = -1.0; direction.Z = 1.0;
            points = geoShapes.CreateCylinder(center, radius, height, 120.0, direction);
            geoShapes.OutputPoints(FeatureType.enCylinder, "Cylinder-3", points, path + @"testcaseCylinder-3.qvb");
            points.Clear();

            points=geoShapes.CreateSteppedCylinder(center, 2.0, 4.0, height, 120.0, direction);
            geoShapes.OutputPoints(FeatureType.enSteppedCylinder, "SteppedCylinder-1", points, path + @"testcaseSteppedCylinder-1.qvb");
            points.Clear();
            
            points = geoShapes.CreateEllipse(center, aRadius, bRadius, 60.0, direction);
            geoShapes.OutputPoints(FeatureType.enEllipse, "Ellipse-M1_M1_P1", points, path + @"testcaseEllipse-M1_M1_P1.qvb");
            points.Clear();
           
            points = geoShapes.CreateCone(center, 2.0, 4.0, 2.0, 90, direction);
            geoShapes.OutputPoints(FeatureType.enCone, "Cone-2", points, path + @"testcaseCone-2.qvb");
            points.Clear();

            direction.X = 1.0; direction.Y = -1.0; direction.Z = 1.0;
            points = geoShapes.CreateCylinder(center, radius, height, 120.0, direction);
            geoShapes.OutputPoints(FeatureType.enCylinder, "Cylinder-4", points, path + @"testcaseCylinder-4.qvb");
            points.Clear();
            
            points = geoShapes.CreateEllipse(center, aRadius, bRadius, 60.0, direction);
            geoShapes.OutputPoints(FeatureType.enEllipse, "Ellipse-P1_M1_P1", points, path + @"testcaseEllipse-P1_M1_P1.qvb");
            points.Clear();

           
            direction.X = 1.0; direction.Y = 1.0; direction.Z = -1.0;
            points = geoShapes.CreateCylinder(center, radius, height, 120.0, direction);
            geoShapes.OutputPoints(FeatureType.enCylinder, "Cylinder-5", points, path + @"testcaseCylinder-5.qvb");
            points.Clear();

            points = geoShapes.CreateEllipse(center, aRadius, bRadius, 60.0, direction);
            geoShapes.OutputPoints(FeatureType.enEllipse, "Ellipse-P1-P1-M1", points, path + @"testcaseEllipse-P1-P1-M1.qvb");
            points.Clear();

            direction.X = 0.70710678; direction.Y = -0.70710678; direction.Z = 0.0;
            points = geoShapes.CreateEllipse(center, aRadius, bRadius, 60.0, direction);
            geoShapes.OutputPoints(FeatureType.enEllipse, "Ellipse-1", points, path + @"testcaseEllipse-1.qvb");
            points.Clear();

            direction.X = 1.0; direction.Y = 1.0; direction.Z = 2.449489742789;
            points = geoShapes.CreateEllipse(center, aRadius, bRadius, 60.0, direction);
            geoShapes.OutputPoints(FeatureType.enEllipse, "Ellipse-P1_P1_P245", points, path + @"testcaseEllipse-P1_P1_P245.qvb");
            points.Clear();

        }

        static void TestShapesMatrixRot()
        {
            List<Vector3D> points = new List<Vector3D>();
            List<Vector3D> pointResults = new List<Vector3D>();
            Shapes geoShapes = new Shapes();
            
            string currpath = Directory.GetCurrentDirectory();
            Directory.CreateDirectory(currpath + @"\MatrixRotation");
            string path = currpath + @"\MatrixRotation\";
            

            Vector3D center1b = new Vector3D();
            center1b.X = 0.0; center1b.Y = 0.0; center1b.Z = 0.0;
            List<Vector3D> pointbuf = new List<Vector3D>();
            pointbuf.Add(new Vector3D(0.0, 0.0, 0.0));
            pointbuf.Add(new Vector3D(2.0, 1.0, 0.5));
            points = geoShapes.CreateLine(center1b, pointbuf, 54.7356, 0.00, 45.00);
            geoShapes.OutputPoints(FeatureType.enLine, "Ellipse-geo", points, path +@"testcaseEllipse-geo.txt");
            pointbuf.Clear();

            Vector3D center1a = new Vector3D();
            center1a.X = 2.0; center1a.Y = 1.0; center1a.Z = 0.0;
            double aRadius1 = 1.0;
            double bRadius1 = 0.5;
            points = geoShapes.CreateEllipse(center1a, aRadius1, bRadius1, 60.0, 0.0, 0.0, 0.0);
            geoShapes.OutputPoints(FeatureType.enEllipse, "Ellipse-geo", points, path +@"testcaseEllipse-geo.txt");
            points.Clear();


            Vector3D center = new Vector3D();
            center.X = 1.0; center.Y = 2.0; center.Z = 3.0;
            double aRadius = 2.0;
            double bRadius = 4.0;

            points = geoShapes.CreateEllipse(center, aRadius, bRadius, 60.0, 30.0, 0.0, 0.0);
            geoShapes.OutputPoints(FeatureType.enEllipse, "Ellipse-2-x30y0z0", points, path + @"testcaseEllipse-2-x30y0z0.txt");
            points.Clear();

            points = geoShapes.CreateEllipse(center, aRadius, bRadius, 60.0, 0.0, 30.0, 0.0);
            geoShapes.OutputPoints(FeatureType.enEllipse, "Ellipse-2-x0y30z0", points, path + @"testcaseEllipse-2-x0y30z0.txt");
            points.Clear();

            points = geoShapes.CreateEllipse(center, aRadius, bRadius, 60.0, 30.0, 30.0, 0.0);
            geoShapes.OutputPoints(FeatureType.enEllipse, "Ellipse-2-x30y30z0", points, path + @"testcaseEllipse-2-x30y30z0.txt");
            points.Clear();
            
            points = geoShapes.CreateEllipse(center, aRadius, bRadius, 60.0, 30.0, 30.0, 30.0);
            geoShapes.OutputPoints(FeatureType.enEllipse, "Ellipse-2-x30y30z30", points, path + @"testcaseEllipse-1-x30y30z30.txt");
            points.Clear();

            points = geoShapes.CreateEllipse(center, bRadius, aRadius, 60.0, 0.0, 0.0, 0.0);
            geoShapes.OutputPoints(FeatureType.enEllipse, "Ellipse-1_MajorX-XY", points, path +@"testcaseEllipse-1_MajorX-XY.txt");
            points.Clear();

            points = geoShapes.CreateEllipse(center, aRadius, bRadius, 60.0, 0.0, 0.0, 0.0);
            geoShapes.OutputPoints(FeatureType.enEllipse, "Ellipse-1_MajorY-XY", points, path +@"testcaseEllipse-1_MajorY-XY.txt");
            points.Clear();

            // Rotation around Z
            points = geoShapes.CreateEllipse(center, aRadius, bRadius, 60.0, 0.0, 0.0, 30.0);
            geoShapes.OutputPoints(FeatureType.enEllipse, "Ellipse-1_30Z", points, path +@"testcaseEllipse-1_30Z.txt");
            points.Clear();

            points = geoShapes.CreateEllipse(center, aRadius, bRadius, 60.0, 0.0, 0.0, -30.0);
            geoShapes.OutputPoints(FeatureType.enEllipse, "Ellipse-1_Minus30Z", points, path +@"testcaseEllipse-1_Minus30Z.txt");
            points.Clear();
            
            center.X = 0.0; center.Y = 0.0; center.Z = 0.0;
            points = geoShapes.CreateEllipse(center, aRadius, bRadius, 60.0, 0.0, 0.0, 30.0);
            geoShapes.OutputPoints(FeatureType.enEllipse, "Ellipse-2-Angle30Z", points, path + @"testcaseEllipse-2-Angle30Z.txt");
            points.Clear();

            points = geoShapes.CreateEllipse(center, aRadius, bRadius, 60.0, 0.0, 0.0, 150.0);
            geoShapes.OutputPoints(FeatureType.enEllipse, "Ellipse-2-Angle150Z", points, path + @"testcaseEllipse-2-Angle150Z.txt");
            points.Clear();

            points = geoShapes.CreateEllipse(center, aRadius, bRadius, 60.0, 0.0, 0.0, 210.0);
            geoShapes.OutputPoints(FeatureType.enEllipse, "Ellipse-2-Angle210Z", points, path + @"testcaseEllipse-2-Angle210Z.txt");
            points.Clear();

            points = geoShapes.CreateEllipse(center, aRadius, bRadius, 60.0, 0.0, 0.0, 330.0);
            geoShapes.OutputPoints(FeatureType.enEllipse, "Ellipse-2-Angle330Z", points, path + @"testcaseEllipse-2-Angle330Z.txt");
            points.Clear();


            // Rotation around Y
            points = geoShapes.CreateEllipse(center, bRadius, aRadius, 60.0, 0.0, -90.0, 0.0);
            geoShapes.OutputPoints(FeatureType.enEllipse, "Ellipse-1_MajorZ-YZ", points, path + @"testcaseEllipse-1_MajorZ-YZ.txt");
            points.Clear();

            points = geoShapes.CreateEllipse(center, aRadius, bRadius, 60.0, 0.0, -90.0, 0.0);
            geoShapes.OutputPoints(FeatureType.enEllipse, "Ellipse-1_MajorY-YZ", points, path + @"testcaseEllipse-1_MajorY-YZ.txt");
            points.Clear();


            // Rotation around X
            points = geoShapes.CreateEllipse(center, aRadius, bRadius, 60.0, 30.0, -90.0, 0.0);
            geoShapes.OutputPoints(FeatureType.enEllipse, "Ellipse-1_30X-YZ", points, path + @"testcaseEllipse-1_30X-YZ.txt");
            points.Clear();

            points = geoShapes.CreateEllipse(center, aRadius, bRadius, 60.0, -30.0, -90.0, 00.0);
            geoShapes.OutputPoints(FeatureType.enEllipse, "Ellipse-1_Minus30X-YZ", points, path + @"testcaseEllipse-1_Minus30X-YZ.txt");
            points.Clear();






            double radius = 4.0;
            double height = 2.0;
            points = geoShapes.CreateCylinder(center, radius, height, 120.0, 0.0, 0.0, 00.0);
            geoShapes.OutputPoints(FeatureType.enCylinder, "Cylinder-1", points, path +@"testcaseCylinder-1.txt");
            points.Clear();

            points = geoShapes.CreateCylinder(center, radius, height, 120.0, 30.0, 0.0, 00.0);
            geoShapes.OutputPoints(FeatureType.enCylinder, "Cylinder-2", points, path +@"testcaseCylinder-2.txt");
            points.Clear();
            points = geoShapes.CreateCylinder(center, radius, height, 120.0, -30.0, 0.0, 00.0);
            geoShapes.OutputPoints(FeatureType.enCylinder, "Cylinder-2-1", points, path +@"testcaseCylinder-2-1.txt");
            points.Clear();

            points = geoShapes.CreateCylinder(center, radius, height, 120.0, 0.0, 30.0, 00.0);
            geoShapes.OutputPoints(FeatureType.enCylinder, "Cylinder-3", points, path +@"testcaseCylinder-3.txt");
            points.Clear();

            points = geoShapes.CreateCylinder(center, radius, height, 120.0, 0.0, -30.0, 00.0);
            geoShapes.OutputPoints(FeatureType.enCylinder, "Cylinder-3-1", points, path +@"testcaseCylinder-3-1.txt");
            points.Clear();

            points = geoShapes.CreateCylinder(center, radius, height, 120.0, 54.7356, 0.0, 45.0);
            geoShapes.OutputPoints(FeatureType.enCylinder, "Cylinder-4", points, path +@"testcaseCylinder-4.txt");
            points.Clear();

            points = geoShapes.CreateCylinder(center, radius, height, 120.0, 54.7356, 0.0, 135.0);
            geoShapes.OutputPoints(FeatureType.enCylinder, "Cylinder-4-1", points, path +@"testcaseCylinder-4-1.txt");
            points.Clear();

            points = geoShapes.CreateCylinder(center, radius, height, 120.0, 180.0 - 54.7356, 0.0, 225.0);
            geoShapes.OutputPoints(FeatureType.enCylinder, "Cylinder-4-2", points, path +@"testcaseCylinder-4-2.txt");
            points.Clear();

            points = geoShapes.CreateCylinder(center, radius, height, 120.0, 180.0 - 54.7356, 0.0, 315.0);
            geoShapes.OutputPoints(FeatureType.enCylinder, "Cylinder-4-3", points, path +@"testcaseCylinder-4-3.txt");
            points.Clear();

            points = geoShapes.CreateCircle(center, radius, 60.0, 30.0, 0.0, 0.0);
            geoShapes.OutputPoints(FeatureType.enCircle, "Circle-1", points, path +@"testcaseCircle-1.txt");
            Vector3D center1 = geoShapes.RotateVector(30.0, 0.0, 0.0, center);
            Vector3D center2 = new Vector3D(0.0, radius, 0.0);
            center2 = geoShapes.RotateVector(30.0, 0.0, 0.0, center2);
            points.Clear();

            center.X = 1.0; center.Y = 2.0; center.Z = 3.0;
            points = geoShapes.CreateCircle(center, radius, 90.0, 30.0, 0.0, 0.0);
            geoShapes.OutputPoints(FeatureType.enCircle, "Circle-2", points, path +@"testcaseCircle-2.txt");
            points.Clear();

            points = geoShapes.CreateCircle(center, radius, 30.0, 0.0, 30.0, 0.0);
            geoShapes.OutputPoints(FeatureType.enCircle, "Circle-3", points, path +@"testcaseCircle-3.txt");
            points.Clear();

            points = geoShapes.CreateCircle(center, radius, 30.0, 30.0, 0.0, 0.0);
            geoShapes.OutputPoints(FeatureType.enCircle, "Circle-4", points, path +@"testcaseCircle-4.txt");
            points.Clear();

            center.X = 0.0; center.Y = 0.0; center.Z = 0.0;
            points = geoShapes.CreateCone(center, 2.0, 4.0, 2.0, 90, 0.0, 0.0, 0.0);
            geoShapes.OutputPoints(FeatureType.enCone, "Cone-1A", points, path +@"testcaseCone-1A.txt");
            points.Clear();

            center.X = 1.5; center.Y = 2.5; center.Z = 0.0;
            points = geoShapes.CreateCone(center, 2.0, 4.0, 2.0, 90, 0.0, 0.0, 0.0);
            geoShapes.OutputPoints(FeatureType.enCone, "Cone-1", points, path +@"testcaseCone-1.txt");
            points.Clear();

            points = geoShapes.CreateCone(center, 2.0, 4.0, 2.0, 60, 0.0, 0.0, 0.0);
            geoShapes.OutputPoints(FeatureType.enCone, "Cone-1-a", points, path +@"testcaseCone-1-a.txt");
            points.Clear();

            points = geoShapes.CreateCone(center, 2.0, 4.0, 4.0, 90, 0.0, 0.0, 0.0);
            geoShapes.OutputPoints(FeatureType.enCone, "Cone-1-1", points, path +@"testcaseCone-1-1.txt");
            points.Clear();

            points = geoShapes.CreateCone(center, 2.0, 4.0, 4.0, 90, 90.0, 0.0, 0.0);
            geoShapes.OutputPoints(FeatureType.enCone, "Cone-2", points, path +@"testcaseCone-2.txt");
            points.Clear();

            points = geoShapes.CreateCone(center, 2.0, 4.0, 4.0, 90, 0.0, 90.0, 0.0);
            geoShapes.OutputPoints(FeatureType.enCone, "Cone-3", points, path +@"testcaseCone-3.txt");
            points.Clear();

            points = geoShapes.CreateCone(center, 2.0, 4.0, 4.0, 90, 30.0, 0.0, 0.0);
            geoShapes.OutputPoints(FeatureType.enCone, "Cone-4", points, path +@"testcaseCone-4.txt");
            points.Clear();

            points = geoShapes.CreateCone(center, 2.0, 4.0, 4.0, 90, -30.0, 0.0, 0.0);
            geoShapes.OutputPoints(FeatureType.enCone, "Cone-4-1", points, path +@"testcaseCone-4-1.txt");
            points.Clear();

            points = geoShapes.CreateCone(center, 2.0, 4.0, 4.0, 90, 0.0, 30.0, 0.0);
            geoShapes.OutputPoints(FeatureType.enCone, "Cone-5", points, path +@"testcaseCone-5.txt");
            points.Clear();

            points = geoShapes.CreateCone(center, 2.0, 4.0, 4.0, 90, 0.0, -30.0, 0.0);
            geoShapes.OutputPoints(FeatureType.enCone, "Cone-5-1", points, path +@"testcaseCone-5-1.txt");
            points.Clear();

            points = geoShapes.CreateCone(center, 2.0, 4.0, 4.0, 90, 30.0, 30.0, 0.0);
            geoShapes.OutputPoints(FeatureType.enCone, "Cone-6", points, path +@"testcaseCone-6.txt");
            points.Clear();

            center.X = 1.5; center.Y = 2.5; center.Z = 2.0;
            points = geoShapes.CreateCone(center, 2.0, 4.0, 4.0, 60.0, 54.7356, 0.0, 45.0);
            geoShapes.OutputPoints(FeatureType.enCone, "Cone-7", points, path +@"testcaseCone-7.txt");
            points.Clear();
            // origin
            center.X = 0.0; center.Y = 0.0; center.Z = 0.0;
            points = geoShapes.CreateCone(center, 2.0, 4.0, 4.0, 60.0, 54.7356, 0.0, 45.0);
            geoShapes.OutputPoints(FeatureType.enCone, "Cone-1-a", points, path +@"testcaseCone-1-a.txt");
            points.Clear();

            points = geoShapes.CreateCone(center, 2.0, 4.0, 2.0, 60, 0.0, 0.0, 0.0);
            geoShapes.OutputPoints(FeatureType.enCone, "Cone-1-b", points, path +@"testcaseCone-1-b.txt");
            points.Clear();

            center.X = 1.5; center.Y = 2.5; center.Z = 4.0;
            points = geoShapes.CreateSphere(center, 2.0, 120.0, 120.0);
            geoShapes.OutputPoints(FeatureType.enCone, "Sphere-1", points, path +@"testcaseSphere-1.txt");
            points.Clear();

            center.X = 0.0; center.Y = 0.0; center.Z = 2.0;
            points = geoShapes.CreatePlane(center, 4.0, 4.0, 0.0, 0.0, 0.0);
            geoShapes.OutputPoints(FeatureType.enPlane, "Plane-1", points, path +@"testcasePlane-1.txt");
            points.Clear();

            center.X = 0.0; center.Y = 0.0; center.Z = 2.0;
            points = geoShapes.CreatePlane(center, 4.0, 4.0, 30.0, 0.0, 0.0);
            geoShapes.OutputPoints(FeatureType.enPlane, "Plane-2", points, path +@"testcasePlane-2.txt");
            points.Clear();

            center.X = 0.0; center.Y = 0.0; center.Z = 2.0;
            points = geoShapes.CreatePlane(center, 4.0, 4.0, -30.0, 0.0, 0.0);
            geoShapes.OutputPoints(FeatureType.enPlane, "Plane-2-1", points, path +@"testcasePlane-2-1.txt");
            points.Clear();

            center.X = 0.0; center.Y = 0.0; center.Z = 2.0;
            points = geoShapes.CreatePlane(center, 4.0, 4.0, 0.0, 30.0, 0.0);
            geoShapes.OutputPoints(FeatureType.enPlane, "Plane-3", points, path +@"testcasePlane-3.txt");
            points.Clear();

            center.X = 0.0; center.Y = 0.0; center.Z = 2.0;
            points = geoShapes.CreatePlane(center, 4.0, 4.0, 0.0, -30.0, 0.0);
            geoShapes.OutputPoints(FeatureType.enPlane, "Plane-3-1", points, path +@"testcasePlane-3-1.txt");
            points.Clear();

            center.X = 2.0; center.Y = 2.0; center.Z = 2.0;
            points = geoShapes.CreatePlane(center, 4.0, 4.0, 54.7356, 0.0, 45.0);
            geoShapes.OutputPoints(FeatureType.enPlane, "Plane-4", points, path +@"testcasePlane-4.txt");
            points.Clear();

            center.X = 1.5; center.Y = 2.0; center.Z = 2.0;
            points = geoShapes.CreatePlane(center, 4.0, 4.0, 0.0, -90.0, 0.0);
            geoShapes.OutputPoints(FeatureType.enPlane, "Plane-5", points, path +@"testcasePlane-5.txt");
            points.Clear();

            center.X = 5.0; center.Y = 2.0; center.Z = 2.0;
            points = geoShapes.CreatePlane(center, 4.0, 4.0, 0.0, 90.0, 0.0);
            geoShapes.OutputPoints(FeatureType.enPlane, "Plane-6", points, path +@"testcasePlane-6.txt");
            points.Clear();

            center.X = 5.0; center.Y = 6.0; center.Z = 2.0;
            points = geoShapes.CreatePlane(center, 4.0, 4.0, 90.0, 0.0, 0.0);
            geoShapes.OutputPoints(FeatureType.enPlane, "Plane-7", points, path +@"testcasePlane-7.txt");
            points.Clear();

            center.X = 5.0; center.Y = 6.0; center.Z = 2.0;
            points = geoShapes.CreatePlane(center, 4.0, 4.0, 89.9, 0.0, 0.0);
            geoShapes.OutputPoints(FeatureType.enPlane, "Plane-8", points, path +@"testcasePlane-8.txt");
            points.Clear();

        }
        static void Main(string[] args)
        {
            TestShapes();
            EllipseMultipleRot();
            RotateVectors();
            TestShapesMatrixRot();
        }
    }
}
