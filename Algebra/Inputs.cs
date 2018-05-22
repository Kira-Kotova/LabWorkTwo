using static System.Console;

namespace Algebra
{
    public static class Inputs
    {
        public static double Input(string message)
        {
            double result;
            bool check;
            do
            {
                WriteLine(message);
                check = double.TryParse(ReadLine(), out result);
                if (!check)
                    WriteLine("Ошибка ввода! Возможно введено не число.");
            } while (!check);

            return result;
        }
    }
}