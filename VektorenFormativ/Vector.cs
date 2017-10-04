using Math = System.Math;

namespace VektorenFormativ
{
    public struct Vector
    {
        public float x;
        public float y;
        public float z;

        public Vector(float _x, float _y, float _z)
        {

            x = _x;
            y = _y;
            z = _z;
        }

        public static Vector operator +(Vector _a, Vector _b)
        {
            Vector c = new Vector(_a.x + _b.x,
                                  _a.y + _b.y,
                                  _a.z + _b.z);

            return c;
        }

        public static Vector operator -(Vector _a, Vector _b)
        {
            Vector c = new Vector(_a.x - _b.x,
                                  _a.y - _b.y,
                                  _a.z - _b.z);

            return c;
        }

        //todo: figure ou what the fuck this means ( negative version of vector I guess? what would you need that for)
        public static Vector operator -(Vector _a)
        {
            Vector c = new Vector(-_a.x,
                                  -_a.y,
                                  -_a.z);

            return c;
        }

        public static Vector operator *(Vector _a, float _b)
        {
            Vector c = new Vector(_a.x * _b,
                                  _a.y * _b,
                                  _a.z * _b);

            return c;
        }

        public static Vector operator /(Vector _a, float _b)
        {
            Vector c = new Vector(_a.x / _b,
                                  _a.y / _b,
                                  _a.z / _b);

            return c;
        }

        public override bool Equals(object _obj)
        {
            return base.Equals(_obj);
           
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static float Magnitude(Vector _v)
        {
            float c = (float)Math.Sqrt(_v.x * _v.x + _v.y * _v.y + _v.z * _v.z);
            return c;
        }

        public static float SqrMagnitude(Vector _v)
        {
            float c = _v.x * _v.x + _v.y * _v.y + _v.z * _v.z;
            return c;
        }

        public static Vector Cross(Vector _v1, Vector _v2)
        {
            Vector c = new Vector(_v1.y * _v2.z - _v1.z * _v2.y,
                                  _v1.z * _v2.x - _v1.x * _v2.z,
                                  _v1.x * _v2.y - _v1.y * _v2.x);

            return c;
        }

        public static float Dot(Vector _v1, Vector _v2)
        {
            float c = _v1.x * _v2.x + _v1.y * _v2.y + _v1.z * _v2.z;
            return c;
        }

        public static Vector Normalize(Vector _v)
        {
            float mag = Magnitude(_v);
            Vector c = new Vector(_v.x / mag,
                                  _v.y / mag,
                                  _v.z / mag);
            return c;
        }

        public override string ToString()
        {
            return string.Format("({0}, {1}, {2})", x, y, z);
        }
    }
}

