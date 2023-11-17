using System;

namespace P02.Graphic_Editor
{
    public class GraphicEditor
    {
        public void DrawShape(IShape shape)
        {
            Console.WriteLine($"I'm {shape.GetType().Name}");

            //if (shape is Circle)
            //{
            //    Console.WriteLine("I'm Circle");
            //}
            //else if (shape is Rectangle)
            //{
            //    Console.WriteLine("I'm Rectangle");
            //}
            //else if (shape is Square)
            //{
            //    Console.WriteLine("I'm Square");
            //}
        }
    }
}
