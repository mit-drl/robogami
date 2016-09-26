﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;
using System.Windows.Controls;

namespace FBE_CSharpUI
{
    class RotationGizmo
    {
        private TorusVisual3D x, y, z, xHit, yHit, zHit;
        private ContainerUIElement3D containerX, containerY, containerZ;
        private static readonly Vector3D[] AXES = new Vector3D[]{new Vector3D(1, 0, 0), new Vector3D(0, 1, 0), new Vector3D(0, 0, 1)};
        private int whichAxis = 0;
        private Point lastPosition;
        private static readonly Material xMaterial= new DiffuseMaterial(Brushes.LightCoral.MakeTransparent(0.6));
        private static readonly Material yMaterial = new DiffuseMaterial(Brushes.LightSkyBlue.MakeTransparent(0.6));
        private static readonly Material zMaterial = new DiffuseMaterial(Brushes.LightGreen.MakeTransparent(0.6));
        private static readonly int innderRadius = 35;
        private static readonly int outerRadius = 37;
        private static readonly int selectionMargin = 3;

        public event Action<Rotation3D> Rotated;

        public RotationGizmo()
        {
            x = new TorusVisual3D(innderRadius, outerRadius);
            y = new TorusVisual3D(innderRadius, outerRadius);
            z = new TorusVisual3D(innderRadius, outerRadius);
            xHit = new TorusVisual3D(innderRadius-selectionMargin, outerRadius+selectionMargin);
            yHit = new TorusVisual3D(innderRadius-selectionMargin, outerRadius+selectionMargin);
            zHit = new TorusVisual3D(innderRadius-selectionMargin, outerRadius+selectionMargin);
            
            containerX = new ContainerUIElement3D();
            containerX.Children.Add(x);
            containerX.Children.Add(xHit);
            containerY = new ContainerUIElement3D();
            containerY.Children.Add(y);
            containerY.Children.Add(yHit);
            containerZ = new ContainerUIElement3D();
            containerZ.Children.Add(z);
            containerZ.Children.Add(zHit);

            x.Material = xMaterial;
            y.Material = yMaterial;
            z.Material = zMaterial;
            x.BackMaterial = y.BackMaterial = z.BackMaterial = null;
            xHit.Material = yHit.Material = zHit.Material = new EmissiveMaterial(Brushes.Transparent);
            xHit.BackMaterial = yHit.BackMaterial = zHit.BackMaterial = null;

            var torii = new[] {x, y, z};
            var hits = new[] {xHit, yHit, zHit};
            var containers = new[] {containerX, containerY, containerZ};
            for (int i = 0; i < 3; i++)
            {
                int iCopy = i;
                var torus = torii[i];
                var hit = hits[i];
                var container = containers[i];
                var curMat = torus.Material;
                container.MouseEnter += (sender, args) =>
                {
                    torus.Material = new DiffuseMaterial(Brushes.Yellow);
                };
                container.MouseLeave += (sender, args) =>
                {
                    torus.Material = curMat;
                };
                container.MouseDown += (sender, args) =>
                {
                    Mouse.Capture(container);
                    whichAxis = iCopy + 1;
                    lastPosition = args.GetPosition(container);
                };
                container.MouseMove += (sender, args) =>
                {
                    if (whichAxis != 0)
                    {
                        Point curPosition = args.GetPosition(container);
                        double offset = curPosition.Y - lastPosition.Y;
                        double angle = offset / 300 * 180;

                        Rotation3D rotation = new AxisAngleRotation3D(Transform.Transform(AXES[whichAxis - 1]), angle);
                        if (Rotated != null)
                        {
                            Rotated(rotation);
                        }

                        lastPosition = curPosition;
                    }
                };
                container.MouseUp += (sender, args) =>
                {
                    Mouse.Capture(null);
                    whichAxis = 0;
                };

            }
        }

        public void Update(Point3D center, Vector3D xAxis, Vector3D yAxis, Vector3D zAxis, HelixViewport3D viewport)
        {
            Matrix3D mat = new Matrix3D();
            mat.M11 = xAxis.X;
            mat.M21 = yAxis.X;
            mat.M31 = zAxis.X;
            mat.M12 = xAxis.Y;
            mat.M22 = yAxis.Y;
            mat.M32 = zAxis.Y;
            mat.M13 = xAxis.Z;
            mat.M23 = yAxis.Z;
            mat.M33 = zAxis.Z;
            mat.OffsetX = center.X;
            mat.OffsetY = center.Y;
            mat.OffsetZ = center.Z;

            Matrix3D xRotate = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 0, 1), 90)).Value;
            Matrix3D zRotate = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), 90)).Value;

            containerX.Transform = new MatrixTransform3D(Matrix3D.Multiply(xRotate, mat));
            if (containerY.Visibility == Visibility.Visible) containerY.Transform = new MatrixTransform3D(mat);
            if (containerZ.Visibility == Visibility.Visible) containerZ.Transform = new MatrixTransform3D(Matrix3D.Multiply(zRotate, mat));
            this.Transform = new MatrixTransform3D(mat);

        }

        public MatrixTransform3D Transform { get; set; }

        public void SetVisible(bool visible)
        {
            containerX.Visibility = visible ? Visibility.Visible : Visibility.Collapsed;
            containerY.Visibility = visible ? Visibility.Visible : Visibility.Collapsed;
            containerZ.Visibility = visible ? Visibility.Visible : Visibility.Collapsed;
        }

        public void AddToViewport(Viewport3D viewport)
        {
            viewport.Children.Add(containerX);
            viewport.Children.Add(containerY);
            viewport.Children.Add(containerZ);
        }

        public void SetxAxisVisible()
        {
            containerX.Visibility = Visibility.Visible;
            containerY.Visibility = Visibility.Collapsed;
            containerZ.Visibility = Visibility.Collapsed;
        }
    }
}
