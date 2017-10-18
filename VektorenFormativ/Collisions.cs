using System.Collections.Generic;

namespace VektorenFormativ
{
    public static class Collisions
    {
        public static bool PointInCuboid(Vector _point, Cuboid _quad)
        { 
            // create a cuboid at _point, of which all edges length are 0
            Cuboid pointCuboid = new Cuboid(new Vector[8] { _point, _point, _point, _point, _point, _point, _point, _point });

            // use the cuboid with cuboid check
            return CuboidInCuboid(pointCuboid, _quad);
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

            Vector cube2_30 = _quad2.m_Vertices[3] - _quad2.m_Vertices[0];
            Vector cube2_32 = _quad2.m_Vertices[3] - _quad2.m_Vertices[2];
            Vector cube2_37 = _quad2.m_Vertices[3] - _quad2.m_Vertices[7];

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
                // Musterlösung nimmt hier nur das Punktprodukt ohne Verrrechnung mit Magnitude?
                if (p < _min)
                    _min = p;
                if (p > _max) 
                    _max = p;
            }

        }
        private static Vector GetMinMax(Sphere _sphere, Vector _axis)
        {
            // calculate the projection of the center
            float proj = Vector.Dot(_sphere.m_Center, _axis);

            float min = proj - _sphere.m_Radius;
            float max = proj + _sphere.m_Radius;

            return new Vector(min, max, 0.0f);
        }
        private static Vector GetMinMax(Cuboid _cuboid, Vector _axis)
        {
            float min = float.PositiveInfinity;
            float max = float.NegativeInfinity;

            for (int i = 0; i < 8; ++i)
            {
                float proj = Vector.Dot(_cuboid.m_Vertices[i], _axis);

                if (proj < min) min = proj;
                if (proj > max) max = proj;
            }

            return new Vector(min, max, 0.0f);
        }

        private static float Project(Vector _axis, Vector _point)
        {
            // punktprodukt von  normal und vertex, dann durch betrag von normalen teilen, das alles mit normaler malnehmen
            float projection = (Vector.Dot(_axis, _point) / Vector.SqrMagnitude(_axis))*Vector.SqrMagnitude(_axis);
            return projection;
        }

        public static bool CuboidInSphere(Cuboid _cuboid, Sphere _sphere)
        {
            Vector[] axises = new Vector[3];

            // get the three edges of the first cuboid
            Vector aU = _cuboid.m_Vertices[1] - _cuboid.m_Vertices[0];
            Vector aV = _cuboid.m_Vertices[3] - _cuboid.m_Vertices[0];
            Vector aW = _cuboid.m_Vertices[4] - _cuboid.m_Vertices[0];

            // Cuboid down/up
            axises[0] = Vector.Cross(aU, aV);

            // Cuboid front/back
            axises[1] = Vector.Cross(aU, aW);

            // Cuboid left/right
            axises[2] = Vector.Cross(aV, aW);

            // iterate over them
            for (int i = 0; i < axises.Length; ++i)
            {
                Vector axis = axises[i];

                // ignore all axis which have no length
                if (Vector.SqrMagnitude(axis) == 0.0f) continue;

                axis = Vector.Normalize(axis);

                // get the min max of the first cuboid
                Vector minMax1 = GetMinMax(_cuboid, axis);
                float min1 = minMax1.x;
                float max1 = minMax1.y;

                // get the min max of the second cuboid
                Vector minMax2 = GetMinMax(_sphere, axis);
                float min2 = minMax2.x;
                float max2 = minMax2.y;

                // check if an intersection on the axis happened
                bool intersection = (min1 > min2 && min1 < max2)
                                    || (min2 > min1 && min2 < max1);

                // if no intersection on the given axis happend
                if (!intersection)
                {
                    // we found a dividing plane
                    return false;
                }
            }

            // intersections on all axis found, there is no dividing plane
            return true;
        }
    }
}
