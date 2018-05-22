using Algebra;
using static System.Console;

namespace ForTest
{
    internal class Program
    {
        public static void Main()
        {
            var m1 = Monome.Read();
            var m2 = new Monome(3, 4);
            var p1 = new Polynome(m1, m2);
            var p2 = new Polynome(p1);
            var sum = m1 + m2;
            var diff = m1 - m2;
            var div = m1 / m2;
            var mult = m1 * m2;
            var max = new Comparer().Compare(m1, m2);
            var equals = m1.Equals(m2);
            var answer = equals ? " " : " не ";
            var sump = p1 + p2;
            var diffp = p1 - p2;
            var multp = p1 * p2; 
            WriteLine($"Мономы: 1) {m1} 2) {m2}\nСумма:{sum}\nРазность:{diff}\nЧастное:{div}\nПроизведение:{mult}\nMax:{max}\nМономы{answer}эквивалентны\n");
            WriteLine($"Полиномы: 1) {p1} 2) {p2}\nСумма:{sump}\nРазность:{diffp}\nПроизведение:{multp}");
        }
    }
}
