using System;

namespace Lab3_Classes_Console_task1
{
    // Класс для операций с прямоугольником
    public class Rectangle
    {
        // Поля
        public int SideA;
        public int SideB;
        public string Name = "";

        // Методы
        public int GetPerimeter()
        {
            return (SideA + SideB) * 2;
        }

        public int GetArea()
        {
            return SideA * SideB;
        }

        public void SetName(string newName)
        {
            Name = newName;
        }
    }

    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("Работа с первым объектом");
            Rectangle myRectangle1 = new Rectangle();
            myRectangle1.SideA = 5;
            myRectangle1.SideB = 10;
            myRectangle1.SetName("Большой прямоугольник");

            Console.WriteLine($"Фигура: {myRectangle1.Name}");
            Console.WriteLine($"Периметр: {myRectangle1.GetPerimeter()}");
            Console.WriteLine($"Площадь: {myRectangle1.GetArea()}");

            Console.WriteLine("\nРабота со вторым объектом");
            Rectangle myRectangle2 = new Rectangle();
            myRectangle2.SideA = 2;
            myRectangle2.SideB = 3;
            myRectangle2.SetName("Маленький прямоугольник");

            Console.WriteLine($"Фигура: {myRectangle2.Name}");
            Console.WriteLine($"Периметр: {myRectangle2.GetPerimeter()}");
            Console.WriteLine($"Площадь: {myRectangle2.GetArea()}");

            Console.ReadKey();
        }
    }
}
