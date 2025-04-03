using HelixToolkit.Wpf;
using System.Windows.Media.Media3D;

namespace Fluid3DView.View._3DModels
{
    public class View3DModel : MeshNormalsVisual3D
    {
        public View3DModel()
        {
            string path_ = @"E:\VisualStudio\repos\Fluid3DView\Fluid3DView\bin\Debug\net8.0-windows\ObjFiles\200_200.obj";
            var modelImporter = new ModelImporter();
            var model3DGroup = modelImporter.Load(path_) as Model3DGroup;
            foreach (var item in model3DGroup.Children)
            {
                if (item is GeometryModel3D geometryModel)
                {


                    this.Children.Add(new ModelVisual3D { Content = item });
                }
            }
            var rotation = new AxisAngleRotation3D(new Vector3D(0,1,1), 180);
            var rotateTransform = new RotateTransform3D(rotation);

            this.Transform = rotateTransform;

        }
    }
}
