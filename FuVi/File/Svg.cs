﻿namespace FuVi.File
{
    public class Svg
    {
        private readonly string _svg;

        internal Svg(string svg)
        {
            _svg = svg;
        }

        public void Save(string path)
        {
            using StreamWriter writer = new StreamWriter(path);
            writer.Write(_svg);
        }
    }
}
