using HelixToolkit.Wpf;
using System.Windows;
using System.Windows.Media.Media3D;

namespace Fluid3DView.View._3DModels
{
    public class View3DModel : ScreenSpaceVisual3D
    {
        public static readonly DependencyProperty CenterProperty = DependencyProperty.Register(
            "Center", typeof(Point3D), typeof(CubeVisual3D), new UIPropertyMetadata(new Point3D(), GeometryChanged));

        public Point3D Center
        {
            get
            {
                return (Point3D)this.GetValue(CenterProperty);
            }

            set
            {
                this.SetValue(CenterProperty, value);
            }
        }

        protected override void UpdateGeometry()
        {
            
            throw new NotImplementedException();
        }

        protected override bool UpdateTransforms()
        {
            throw new NotImplementedException();
        }
    }
}
