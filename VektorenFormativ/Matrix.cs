using Math = System.Math;

namespace VektorenFormativ
{
    public class Matrix
    {
        private readonly float[,] Values = new float[4, 4];

        public Matrix()
        {
        }
        
        public Matrix(float[,] _values)
        {
            Values = _values;
        }

        public static Matrix Translate(Vector _v)
        {
            Matrix Translation = new Matrix();
            Translation.Values[3, 0] = _v.x;
            Translation.Values[3, 1] = _v.y;
            Translation.Values[3, 2] = _v.z;
            for (int i = 0; i < 4; i++)
            {
                Translation.Values[i, i] = 1f;
            }
            return Translation;
        }

        public static Matrix Rotation(Vector _v)
        {
            Matrix Rotation = new Matrix();
            Rotation = RotateX(_v.x) * RotateY(_v.y) * RotateZ(_v.z);
            return Rotation;
        }

        private static Matrix RotateX(float _x)
        {
            _x *= (float)Math.PI * 2/360;
            Math.Round(Math.Cos(_x), 2);
            Matrix Rotation = new Matrix();
            Rotation.Values[0, 0] = 1f;
            Rotation.Values[1, 1] = (float)Math.Round(Math.Cos(_x), 2);
            Rotation.Values[2, 2] = (float)Math.Round(Math.Cos(_x), 2);
            Rotation.Values[2, 1] = -(float)Math.Round(Math.Sin(_x), 2);
            Rotation.Values[1, 2] = (float)Math.Round(Math.Sin(_x), 2);
            Rotation.Values[3, 3] = 1f;
            return Rotation;
        }

        private static Matrix RotateY(float _y)
        {
            _y *= (float)Math.PI * 2 / 360;

            Matrix Rotation = new Matrix();
            Rotation.Values[1, 1] = 1f;
            Rotation.Values[0, 0] = (float)Math.Round(Math.Cos(_y),2);
            Rotation.Values[0, 2] = (float)-(Math.Round(Math.Sin(_y),2));
            Rotation.Values[2, 0] = (float)Math.Round(Math.Sin(_y),2);
            Rotation.Values[2, 2] = (float)Math.Round(Math.Cos(_y),2);
            Rotation.Values[3, 3] = 1f;
            return Rotation;
        }

        private static Matrix RotateZ(float _z)
        {
            _z *= (float)Math.PI * 2 / 360;

            Matrix Rotation = new Matrix();
            Rotation.Values[2, 2] = 1f;
            Rotation.Values[0, 0] = (float)Math.Round(Math.Cos(_z),2);
            Rotation.Values[1, 1] = (float)Math.Round(Math.Cos(_z),2);
            Rotation.Values[0, 1] = (float)Math.Round(Math.Sin(_z),2);
            Rotation.Values[1, 0] = (float)-Math.Round((Math.Sin(_z)),2);
            Rotation.Values[3, 3] = 1f;
            return Rotation;
        }

        public static Matrix Scale(Vector _v)
        {
            Matrix Scaling = new Matrix();
            Scaling.Values[0, 0] = _v.x;
            Scaling.Values[1, 1] = _v.y;
            Scaling.Values[2, 2] = _v.z;
            Scaling.Values[3, 3] = 1f;
            return Scaling;
        }

        public static Matrix TRS(Vector _pos, Vector _rot, Vector _scale)
        {
            Matrix TRS = new Matrix();
            TRS = Scale(_scale) * Rotation(_rot) * Translate(_pos);
            return TRS;
        }

        public static Matrix operator *(Matrix _m1, Matrix _m2)
        {
            // nur für 4x4 Matrizen
            Matrix Result = new Matrix();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        Result.Values[i, j] += _m1.Values[i, k] * _m2.Values[k, j];
                    }
                }
            }

            return Result;
        }

        public static Vector operator *(Matrix _m, Vector _v)
        {
            //Vector NewVector = new Vector();
            //NewVector.x = ((_v.x * _m.Values[0, 0]) + (_v.x * _m.Values[1, 0]) + (_v.x * _m.Values[2, 0]) + (1 * _m.Values[3, 0]));
            //NewVector.y = ((_v.y * _m.Values[0, 1]) + (_v.y * _m.Values[1, 1]) + (_v.y * _m.Values[2, 1]) + (1 * _m.Values[3, 1]));
            //NewVector.z = ((_v.z * _m.Values[0, 2]) + (_v.z * _m.Values[1, 2]) + (_v.z * _m.Values[2, 2]) + (1 * _m.Values[3, 2]));

             Vector NewVector = new Vector();
             NewVector.x = ((_v.x * _m.Values[0, 0]) + (_v.y * _m.Values[1, 0]) + (_v.z * _m.Values[2, 0]) + (1 * _m.Values[3, 0]));
             NewVector.y = ((_v.x * _m.Values[0, 1]) + (_v.y * _m.Values[1, 1]) + (_v.z * _m.Values[2, 1]) + (1 * _m.Values[3, 1]));
             NewVector.z = ((_v.x * _m.Values[0, 2]) + (_v.y * _m.Values[1, 2]) + (_v.z * _m.Values[2, 2]) + (1 * _m.Values[3, 2]));

            return NewVector;
        }
    }
}
