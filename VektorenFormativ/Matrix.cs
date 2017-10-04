using Math = System.Math;

namespace VektorenFormativ
{
    public class Matrix
    {
        private readonly float[,] Values = new float[4, 4];
        public float x;
        public float y;
       
        public Matrix()
        {
            x = 0;
            y = 0;
        }

        public Matrix(float[,] _values)
        {
        }

        public static Matrix Translate(Vector _v)
        {
            
        }

        public static Matrix Rotation(Vector _v)
        {
        }

        private static Matrix RotateX(float _x)
        {
        }

        private static Matrix RotateY(float _y)
        {
        }

        private static Matrix RotateZ(float _z)
        {
        }

        public static Matrix Scale(Vector _v)
        {
        }

        public static Matrix TRS(Vector _pos, Vector _rot, Vector _scale)
        {
        }

        public static Matrix operator *(Matrix _m1, Matrix _m2)
        {
        }

        public static Vector operator *(Matrix _m, Vector _v)
        {
        }
    }
}
