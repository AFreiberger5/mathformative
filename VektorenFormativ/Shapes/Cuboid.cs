namespace VektorenFormativ
{
    public class Cuboid
    {
        public readonly Vector[] m_Vertices = new Vector[8];

        /// <summary>
        /// Im Uhrzeigersinn von unten nach oben angeben
        /// </summary>
        /// <param name="_vertices"></param>
        public Cuboid(Vector[] _vertices)
        {
            if (_vertices.Length != 8)
            {
                throw new System.ArgumentOutOfRangeException();
            }

            m_Vertices = _vertices;
        }
    }
}
