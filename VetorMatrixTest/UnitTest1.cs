using Microsoft.VisualStudio.TestTools.UnitTesting;
using VektorenFormativ;

namespace VectorMatrixTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void VectorTest()
        {
            Vector v = new Vector(3, 4, 0);

            Assert.AreEqual(25, Vector.SqrMagnitude(v));
            Assert.AreEqual(5, Vector.Magnitude(v));

            v = new Vector(2,0,0);
            Vector v2 = new Vector(0,2,0);
            Assert.AreEqual(new Vector(0,0,1), Vector.Normalize(Vector.Cross(v, v2)));
            Assert.AreEqual(0, Vector.Dot(v, v2));
        }

        [TestMethod]
        public void MatrixTest()
        {
            Matrix m = Matrix.Translate(new Vector(10, 0, 0));
            Vector v = new Vector(1,1,1);
            v = m * v;

            Assert.AreEqual(new Vector(11,1,1),v);

            m = Matrix.Rotation(new Vector(90,0,0));
            v = new Vector(1,1,0);
            v = m * v;

            Assert.AreEqual(new Vector(1, 0, 1), v);

            m = Matrix.Rotation(new Vector(90, 90, 0));
            v = new Vector(1, 1, 0);
            v = m * v;

            Assert.AreEqual(new Vector(1, 0, -1), v);

            m = Matrix.Rotation(new Vector(90, 90, 90));
            v = new Vector(1, 1, 0);
            v = m * v;

            Assert.AreEqual(new Vector(0, 1, -1), v);

            m = Matrix.Scale(new Vector(2,3,4));
            v = new Vector(2,2,2);
            v = m * v;

            Assert.AreEqual(new Vector(4, 6, 8),v);

            m = Matrix.TRS(new Vector(10, 0, 0), new Vector(90, 0, 0), new Vector(2, 3, 4));
            v = new Vector(0,1,2);
            v = m * v;

            Assert.AreEqual(new Vector(10,-8,3), v);
        }

        [TestMethod]
        public void CollisionTest()
        {
            Sphere s = new Sphere(new Vector(0,0,0), 5);
            Sphere s2 = new Sphere(new Vector(5, 5, 5), 3);
            Vector p = new Vector(3,0,0);
            Cuboid c = new Cuboid(new []{new Vector(-1, -1, 1), new Vector(1, -1, 1),
                                        new Vector(1, -1, -1), new Vector(-1, -1, -1),
                                        new Vector(-1, 1, 1), new Vector(1, 1, 1),
                                        new Vector(1, 1, -1), new Vector(-1, 1, -1) });
            Cuboid c2 = new Cuboid(new[]{new Vector(0, -1, 1), new Vector(1, -1, 0),
                                        new Vector(0, -1, -1), new Vector(-1, -1, 0),
                                        new Vector(0, 1, 1), new Vector(1, 1, 0),
                                        new Vector(0, 1, -1), new Vector(-1, 1, 0) });

            Assert.AreEqual(Collisions.PointInSphere(p, s), true);

            p = new Vector(6, 0, 0);
            Assert.AreEqual(Collisions.PointInSphere(p, s), false);


            Assert.AreEqual(Collisions.PointInCuboid(p, c), false);

            p = new Vector(0.5f, 0.5f, 0.5f);
            Assert.AreEqual(Collisions.PointInCuboid(p, c), true);

            Assert.AreEqual(Collisions.SphereInSphere(s, s2), false);

            s2.m_Radius = 4;
            Assert.AreEqual(Collisions.SphereInSphere(s, s2), true);

            Assert.AreEqual(Collisions.CuboidInSphere(c, s), true);

            Assert.AreEqual(Collisions.CuboidInSphere(c, s2), false);

            Vector dir = Vector.Normalize(new Vector(1, 0, 1));

            Assert.AreEqual(Collisions.CuboidInCuboid(c, c2), true);

            for (int i = 0; i < 8; i++)
            {
                c2.m_Vertices[i] += dir * 2.5f;
            }

            Assert.AreEqual(Collisions.CuboidInCuboid(c, c2), false);

            c2 = new Cuboid(new[]{new Vector(5, -1, 1), new Vector(6, -1, 0),
                                        new Vector(5, -1, -1), new Vector(4, -1, 0),
                                        new Vector(5, 1, 1), new Vector(6, 1, 0),
                                        new Vector(5, 1, -1), new Vector(4, 1, 0) });
            Assert.AreEqual(Collisions.CuboidInCuboid(c, c2), false);

        }
    }
}
