using System;
using System.Collections.Generic;
using static System.Math;
using static Algebra.Inputs;

namespace Algebra
{
    public class Monome
    {
        private double factor;
        private int degree;

        public double Factor
        {
            get { return factor; }
            set
            {
                if (double.IsNaN(value))
                    throw new ArgumentException($"Значение не может быть не числом!", nameof(Factor));
                factor = value;
            }
        }

        public int Degree
        {
            get { return degree; }
            set
            {
                if (double.IsNaN(value))
                    throw new ArgumentException($"Значение не может быть не числом!", nameof(Degree));
                degree = value;
            }
        }

        /// <summary>
        /// Конструктор монома.
        /// </summary>
        /// <param name="factor">числовой множитель</param>
        /// <param name="degree">степень переменной</param>
        public Monome(double factor = 0, int degree = 0)
        {
            Degree = degree;
            Factor = factor;
        }
        
        public static Monome Read()
        {
            var factor = Input("Введите множитель");
            var degree = (int) Input("Введите степень");
            return new Monome(factor, degree);
        }
        
        /// <summary>
        /// Конструктор копирования.
        /// </summary>
        /// <param name="another">Копируемый объект</param>
        public Monome(Monome another)
        {
            Factor = another.factor;                
            Degree = another.degree;
        }

        public override string ToString() => $"{Factor:0.###}*x^{Degree}";

        //исключена потеря данных
        /// <summary>
        /// Неявное приведение типа <see cref="double"/> к типу <see cref="Monome"/>.
        /// </summary>
        /// <param name="arg">Объект, приводимый к типу <see cref="Monome"/>.</param>
        /// <returns></returns>
        public static implicit operator Monome(double arg) => new Monome(arg);
        
        public override bool Equals(object obj)
        {
            var m = obj as Monome;
            if (ReferenceEquals(m, null))
                throw new ArgumentException("Объект не может быть приведён к типу Monome",nameof(obj));
            return Abs(Factor - m.Factor) < double.Epsilon && Degree == m.Degree;
        }

        #region Operators

        public static Polynome operator +(Monome m1, Monome m2)
        {
            var result = new Polynome(m1);
            return result + m2;
        }
        
        public static Polynome operator -(Monome m1, Monome m2)
        {
            var result = new Polynome(m1);
            return result - m2;
        }
        
        public static Polynome operator *(Monome m1, Monome m2)
        {
            var result = new Polynome(m1);
            return result * m2;
        }

        public static Monome operator /(Monome m1, Monome m2) => new Monome(m1.Factor / m2.Factor, m1.Degree - m2.Degree);
        
        public static Polynome operator +(Monome m, double arg) => new Polynome(m, arg);
        
        public static Polynome operator -(Monome m, double arg) => new Polynome(m, -arg);

        public static Monome operator *(Monome m, double arg) =>  new Monome(m.Factor * arg, m.Degree);
        
        public static Monome operator /(Monome m, double arg) => new Monome(m.Factor / arg, m.Degree);
        
        public static Polynome operator +(double arg, Monome m) =>  m + arg;

        public static Polynome operator -(double arg, Monome m) => new Polynome(arg, new Monome(-m.Factor, m.Degree));

        public static Monome operator *(double arg, Monome m) =>  m * arg;

        public static Monome operator /(double arg, Monome m) => new Monome(arg / m.Factor, -m.Degree);

        public static Monome operator ^(Monome m, double arg) => new Monome(m.Factor, (int) (m.Degree * arg));

        #endregion
    }

    /// <summary>
    /// Сравнение по степени.
    /// </summary>
    public sealed class Comparer : IComparer<Monome>
    {
        public int Compare(Monome x, Monome y)
        {
            if (y != null && x != null)
                return x.Degree.CompareTo(y.Degree);
            throw new ArgumentNullException();
        }
    } 
}