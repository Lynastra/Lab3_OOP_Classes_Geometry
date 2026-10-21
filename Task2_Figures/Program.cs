using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Lab3_Classes_Console_task2
{
    // Базовый класс
    public class Figure
    {
        // Статическое поле: счётчик созданных фигур.
        // Читать можно откуда угодно, менять - только внутри класса.
        public static int Count { get; private set; } = 0;

        // Поле-экземпляр параметризованного класса: координаты точек (x, y подряд)
        public List<int> Coordinates = new List<int>();

        public string NameOfFigure = "";

        // Метод валидации, которым пользуются наследники
        protected static void ValidatePositive(double value, string paramName)
        {
            if (value <= 0)
                throw new ArgumentException($"{paramName} должен быть больше нуля!");
        }

        public void SetName(string newName)
        {
            NameOfFigure = newName;
        }

        public string GetName()
        {
            return NameOfFigure;
        }

        public void AddPoint(int x, int y)
        {
            Coordinates.Add(x);
            Coordinates.Add(y);
        }

        // Конструктор по умолчанию
        public Figure()
        {
            Count++;
            NameOfFigure = "Безымянная фигура";
            Console.WriteLine($"[Создана {NameOfFigure}] Вызван конструктор по умолчанию базового класса.");
        }

        // Конструктор с аргументами
        public Figure(string name)
        {
            Count++;
            NameOfFigure = name;
            Console.WriteLine($"[Создана {NameOfFigure}] Вызван конструктор с аргументом базового класса.");
        }

        public virtual double GetArea() => 0;
        public virtual double GetPerimeter() => 0;

        // Деструктор в стиле методички: Trace.WriteLine + GetName()
        ~Figure()
        {
            Trace.WriteLine($"[Удалена {this.GetName()}] Вызван деструктор базового класса.");
        }
    }

    // Круг
    public class Circle : Figure
    {
        private double _radius;

        public double Radius
        {
            get { return _radius; }
            set
            {
                // Вызов метода валидации, описанного в базовом классе
                ValidatePositive(value, "Радиус");
                _radius = value;
            }
        }

        // Конструктор по умолчанию: явно вызывает конструктор по умолчанию родителя
        public Circle() : base()
        {
            SetName("Круг по умолчанию");
            Radius = 1;
            Console.WriteLine($"[Создана {NameOfFigure}] Вызван конструктор по умолчанию наследника Circle.");
        }

        // Конструктор с аргументами: вызывает конструктор родителя с аргументом
        public Circle(string name, double radius) : base(name)
        {
            Radius = radius; // Сработает сеттер с проверкой
            Console.WriteLine($"[Создана {NameOfFigure}] Вызван конструктор наследника Circle.");
        }

        public override double GetArea() => Math.PI * Radius * Radius;
        public override double GetPerimeter() => 2 * Math.PI * Radius;

        ~Circle()
        {
            Trace.WriteLine($"[Удалена {this.GetName()}] Вызван деструктор наследника Circle.");
        }
    }

    // Прямоугольник
    public class Rectangle : Figure
    {
        private int _sideA;
        private int _sideB;

        public int SideA
        {
            get { return _sideA; }
            set
            {
                ValidatePositive(value, "Сторона A");
                _sideA = value;
            }
        }

        public int SideB
        {
            get { return _sideB; }
            set
            {
                ValidatePositive(value, "Сторона B");
                _sideB = value;
            }
        }

        // Конструктор по умолчанию: ссылается на свой другой конструктор через this(...)
        public Rectangle() : this("Прямоугольник 1x1", 1, 1)
        {
            Console.WriteLine($"[Создана {NameOfFigure}] Вызван конструктор по умолчанию наследника Rectangle.");
        }

        // Конструктор с аргументами
        public Rectangle(string name, int sideA, int sideB) : base(name)
        {
            SideA = sideA;
            SideB = sideB;
            Console.WriteLine($"[Создана {NameOfFigure}] Вызван конструктор наследника Rectangle.");
        }

        public override double GetArea() => SideA * SideB;
        public override double GetPerimeter() => (SideA + SideB) * 2;

        ~Rectangle()
        {
            Trace.WriteLine($"[Удалена {this.GetName()}] Вызван деструктор наследника Rectangle.");
        }
    }

    // Квадрат наследуется от прямоугольника
    public class Square : Rectangle
    {
        // Конструктор по умолчанию: ссылается на свой другой конструктор через this(...)
        public Square() : this("Квадрат 1x1", 1)
        {
            Console.WriteLine($"[Создана {NameOfFigure}] Вызван конструктор по умолчанию наследника Square.");
        }

        // Конструктор квадрата вызывает конструктор прямоугольника
        public Square(string name, int side) : base(name, side, side)
        {
            Console.WriteLine($"[Создана {NameOfFigure}] Вызван конструктор наследника Square.");
        }

        public void SetSide(int side)
        {
            SideA = side;
            SideB = side;
        }

        ~Square()
        {
            Trace.WriteLine($"[Удалена {this.GetName()}] Вызван деструктор наследника Square.");
        }
    }

    // Основная программа
    internal class Program
    {
        // Все объекты создаются и используются в отдельном методе.
        // После выхода из него ни одной ссылки на объекты не остаётся,
        // и сборщик мусора может их собрать (см. README, раздел про деструкторы).
        // NoInlining запрещает встраивать метод в Main: это страховка, чтобы
        // локальные переменные не оказались снова в Main.
        [MethodImpl(MethodImplOptions.NoInlining)]
        static void RunDemo()
        {
            Console.WriteLine("--- Конструкторы по умолчанию ---");
            Circle defCircle = new Circle();
            Rectangle defRect = new Rectangle();
            Square defSquare = new Square();

            Console.WriteLine("\n--- Конструкторы с аргументами ---");
            Figure baseFigure = new Figure("Базовая фигура");
            Circle myCircle = new Circle("Круг-1", 5.0);
            Rectangle myRect = new Rectangle("Прямоугольник-1", 4, 6);
            Square mySquare = new Square("Квадрат-1", 5);

            Console.WriteLine("\n--- Площади и периметры ---");
            Console.WriteLine($"Площадь круга: {myCircle.GetArea():F2}, Периметр (длина окружности): {myCircle.GetPerimeter():F2}");
            Console.WriteLine($"Площадь прямоугольника: {myRect.GetArea()}, Периметр: {myRect.GetPerimeter()}");
            Console.WriteLine($"Площадь квадрата: {mySquare.GetArea()}, Периметр: {mySquare.GetPerimeter()}");

            Console.WriteLine("\n--- Поле-экземпляр List<int> ---");
            myRect.AddPoint(0, 0);
            myRect.AddPoint(4, 6);
            Console.WriteLine($"Координаты ({myRect.GetName()}): {string.Join(", ", myRect.Coordinates)}");

            Console.WriteLine("\n--- Проверка в сеттере ---");
            try
            {
                // Исключение возникнет уже после создания объекта и вызова конструктора Figure,
                // поэтому для такого "недостроенного" объекта деструкторы тоже сработают
                new Circle("Плохой круг", -1);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            Console.WriteLine($"\nВсего создано фигур (static): {Figure.Count}");
        }

        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // По умолчанию Trace пишет только в окно отладчика (Visual Studio),
            // поэтому подключаем вывод в консоль, чтобы сообщения деструкторов были видны.
            Trace.Listeners.Add(new ConsoleTraceListener());

            Console.WriteLine("=== Создание объектов ===");
            RunDemo();

            Console.WriteLine("\n=== Удаление объектов (сборка мусора) ===");
            GC.Collect();
            GC.WaitForPendingFinalizers();

            Console.WriteLine("\nКонец программы.");
            Console.ReadKey();
        }
    }
}
