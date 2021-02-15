namespace Heirloom.Extras.Animation
{
    internal static class Helper
    {
        public static Mathematics.Matrix GetHeirloomMatrix(DragonBones.Matrix dMatrix)
        {
            var hMatrix = Mathematics.Matrix.Identity;
            hMatrix.M0 = dMatrix.A;
            hMatrix.M1 = dMatrix.C;
            hMatrix.M2 = dMatrix.Tx;
            hMatrix.M3 = dMatrix.B;
            hMatrix.M4 = dMatrix.D;
            hMatrix.M5 = dMatrix.Ty;
            return hMatrix;
        }
    }
}
