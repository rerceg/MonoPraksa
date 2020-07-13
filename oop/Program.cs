using System;
using System.Collections.Generic;

namespace Shapes
{
    interface IShape
    {
        public double Area();
        public double Perimeter();
    }

    class Square : IShape
    {
        protected double side;
        
        public Square(double side)
        {
            this.side = side;
        }
        public double Perimeter()
        {
            return 4 * side;
        }
        public double Area()
        {
            return side * side;
        }
    }

    class Triangle : IShape
    {
        protected double a, b, c;
        public Triangle(double a, double b, double c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }
        public double Perimeter()
        {
            return a + b + c;
        }
        public double Area()
        {
            double halfPerimeter = Perimeter() / 2;
            return Math.Sqrt(halfPerimeter*(halfPerimeter - a)*(halfPerimeter - b) * (halfPerimeter - c));
        }
    }

    class Circle : IShape
    {
        protected double radius;
        public Circle(double radius)
        {
            this.radius = radius;
        }
        public double Perimeter()
        {
            return 2 * radius * Math.PI;
        }
        public double Area()
        {
            return radius * radius * Math.PI;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<IShape> oblici = new List<IShape>() {
                new Square(5),
                new Triangle(3,4,5),
                new Circle(5)
            };

            double ukupnaPovrsina = 0;

            foreach (IShape oblik in oblici)
            {
                ukupnaPovrsina += oblik.Area();
            }

            Console.WriteLine(String.Format("Ukupna površina svih oblika je {0}", ukupnaPovrsina));
        }
    }
}
