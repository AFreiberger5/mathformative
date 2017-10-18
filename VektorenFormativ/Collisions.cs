using System.Collections.Generic;

namespace VektorenFormativ
{
    public static class Collisions
    {
        public static bool PointInCuboid(Vector _point, Cuboid _quad)
        {
            return true;
        }

        public static bool PointInSphere(Vector _point, Sphere _sphere)
        {
            return Vector.SqrMagnitude(_sphere.m_Center - _point) < (_sphere.m_Radius * _sphere.m_Radius);
        }

        public static bool SphereInSphere(Sphere _sphere1, Sphere _sphere2)
        {
            return Vector.SqrMagnitude(_sphere2.m_Center - _sphere1.m_Center) <
                ((_sphere1.m_Radius + _sphere2.m_Radius) * (_sphere1.m_Radius + _sphere2.m_Radius));
        }

        public static bool CuboidInCuboid(Cuboid _quad1, Cuboid _quad2)
        {
            //Edges bestimmen
            Vector cube1_30 = _quad1.m_Vertices[3] - _quad1.m_Vertices[0];
            Vector cube1_32 = _quad1.m_Vertices[3] - _quad1.m_Vertices[2];
            Vector cube1_37 = _quad1.m_Vertices[3] - _quad1.m_Vertices[7];

            Vector cube2_30 = _quad1.m_Vertices[3] - _quad1.m_Vertices[0];
            Vector cube2_32 = _quad1.m_Vertices[3] - _quad1.m_Vertices[2];
            Vector cube2_37 = _quad1.m_Vertices[3] - _quad1.m_Vertices[7];

            //Normalen berechnen
            List<Vector> normals = new List<Vector>();

            //normale für den boden
            normals.Add(Vector.Cross(cube1_30, cube1_32));
            normals.Add(Vector.Cross(cube2_30, cube2_32));

            //normale für die frontwand
            normals.Add(Vector.Cross(cube1_30, cube1_37));
            normals.Add(Vector.Cross(cube2_30, cube2_37));

            //normale für rechts
            normals.Add(Vector.Cross(cube1_32, cube1_37));
            normals.Add(Vector.Cross(cube2_32, cube2_37));

            //kombinormalen bestimmen
            normals.Add(Vector.Cross(cube1_30, cube2_30));
            normals.Add(Vector.Cross(cube1_30, cube2_30));
            normals.Add(Vector.Cross(cube1_30, cube2_30));
            normals.Add(Vector.Cross(cube1_32, cube2_32));
            normals.Add(Vector.Cross(cube1_32, cube2_32));
            normals.Add(Vector.Cross(cube1_32, cube2_32));
            normals.Add(Vector.Cross(cube1_37, cube2_37));
            normals.Add(Vector.Cross(cube1_37, cube2_37));
            normals.Add(Vector.Cross(cube1_37, cube2_37));

            foreach (Vector normal in normals)
            {
                // hat die normale keine Länge nächstes objekt anfangen
                if (Vector.SqrMagnitude(normal) == 0)
                    continue;
                //Projezieren und Min-Max bestimmen
                float cube1Min;
                float cube1Max;
                GetMinMax(normal, _quad1, out cube1Min, out cube1Max);

                float cube2Min;
                float cube2Max;
                GetMinMax(normal, _quad2, out cube2Min, out cube2Max);
                //Wenn sich beide nicht überschneiden => keine Kollision
                if ((cube1Min < cube2Min && cube2Min > cube1Max)
                        ||
                   (cube1Min < cube2Min && cube2Min > cube1Max))
                    return false;
            }

            // Überschneidungen auf allen Achsen gefunden => Kollision
            return true;
        }

        private static void GetMinMax(Vector _normal, Cuboid _cube, out float _min, out float _max)
        {
            _max = float.MinValue;
            _min = float.MaxValue;

            foreach (Vector vertex in _cube.m_Vertices)
            {
                float p = Project(_normal, vertex);

                if (p < _min)
                    _min = p;
                if (p > _max) 
                    _max = p;
            }

        }

        private static float Project(Vector _axis, Vector _point)
        {
            // punktprodukt von  normal und vertex, dann durch betrag von normalen teilen, das alles mit normaler malnehmen
            float projection = (Vector.Dot(_axis, _point) / Vector.SqrMagnitude(_axis))*Vector.SqrMagnitude(_axis);
            return projection;
        }

        public static bool CuboidInSphere(Cuboid _quad, Sphere _sphere)
        {
            return true;
        }
    }
}
