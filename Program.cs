namespace dz3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Решение квадратного уравнения a * x^2 + b * x + c = 0");

            int a, b, c;

            while (true)
            {
                try
                {
                    Console.Write("Введите значение a: ");
                    a = ParseInput(Console.ReadLine());

                    Console.Write("Введите значение b: ");
                    b = ParseInput(Console.ReadLine());

                    Console.Write("Введите значение c: ");
                    c = ParseInput(Console.ReadLine());

                    break;  // Выход из цикла, если все значения успешно распарсены
                }
                catch (FormatException ex)
                {
                    FormatData(ex.Message);
                }
            }

            try
            {
                SolveQuadraticEquation(a, b, c);
            }
            catch (QuadraticEquationNoRealSolutionsException)
            {
                FormatData("Вещественных значений не найдено", Severity.Warning);
            }
        }

        static int ParseInput(string input)
        {
            if (!int.TryParse(input, out int value))
            {
                throw new FormatException($"Значение '{input}' не является целым числом.");
            }
            return value;
        }

        static void FormatData(string message, Severity severity = Severity.Error)
        {
            Console.BackgroundColor = severity == Severity.Error ? ConsoleColor.Red : ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        static void SolveQuadraticEquation(int a, int b, int c)
        {
            double discriminant = b * b - 4 * a * c;
            const double eps = 1e-10;
            if (discriminant > 0)
            {
                double x1 = (-b + Math.Sqrt(discriminant)) / (2 * a);
                double x2 = (-b - Math.Sqrt(discriminant)) / (2 * a);
                Console.WriteLine($"x1 = {x1}, x2 = {x2}");
            }
            else if (Math.Abs(discriminant) < eps)
            {
                double x = -b / (2.0 * a);
                Console.WriteLine($"x = {x}");
            }
            else
            {
                throw new QuadraticEquationNoRealSolutionsException();
            }
        }
    }

    class QuadraticEquationNoRealSolutionsException : Exception
    {
        public QuadraticEquationNoRealSolutionsException()
            : base("Вещественных значений не найдено")
        {
        }
    }

    enum Severity
    {
        Error,
        Warning
    }
}