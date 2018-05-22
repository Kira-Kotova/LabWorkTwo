using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Algebra
{
    public class Polynome : IEnumerable<Monome>
    {
        private  LinkedList<Monome> polynomeList;

        /// <summary>
        /// Конструктор копирования
        /// </summary>
        /// <param name="monomes"></param>
        public Polynome(IEnumerable<Monome> monomes)
        {
            var result = new LinkedList<Monome>();
            foreach (var m in monomes)
                result.AddLast(new Monome(m));
            polynomeList = result;
        }

        public Polynome(params Monome[] monomes)
        {
            polynomeList = new LinkedList<Monome>(monomes);
        }

        /// <summary>
        /// Неявное приведение типа <see cref="Monome"/> к типу <see cref="Polynome"/>.
        /// </summary>
        /// <param name="value">Объект, приводимый к типу <see cref="Polynome"/>.</param>
        /// <returns></returns>
        public static implicit operator Polynome(Monome value) => new Polynome(value);

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var m in polynomeList) 
                sb.AppendFormat(m.Factor >= Double.Epsilon ? $"+{m}" : $"{m}");
            return sb.ToString();
        }

        public IEnumerator<Monome> GetEnumerator() => polynomeList.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        
        #region Operators

        public static Polynome operator +(Polynome p1, Polynome p2)
        {
            var result = new Polynome(p1);
            foreach (var monome in p2)
                result += monome;
            return result;
        }
        
        public static Polynome operator -(Polynome p1, Polynome p2)
        {
            var result = new Polynome(p1);
            foreach (var monome in p2)
                result -= monome;
            return result;
        }
        
        public static Polynome operator *(Polynome p1, Polynome p2)
        {
            var result = new Polynome(p1);
            foreach (var monome in p2)
                result *= monome;
            return result;
        }

        public static Polynome operator +(Polynome p, Monome m)
        {
            var result = new Polynome(p);
            var current = result.polynomeList.First;
            while (current.Next != null && current.Value.Degree != m.Degree)
                current = current.Next;
            if (current.Value.Degree == m.Degree)
                current.Value.Factor += m.Factor;
            else
                result.polynomeList.AddLast(m);
            return result;
        }



        public static Polynome operator -(Polynome p, Monome m)
        {
            var result = new Polynome(p);
            var current = result.polynomeList.First;
            while (current.Next != null && current.Value.Degree != m.Degree)
                current = current.Next;
            if (current.Value.Degree == m.Degree)
                current.Value.Factor -= m.Factor;
            else
                result.polynomeList.AddLast(new Monome(-m.Factor, m.Degree));
            return result;
        }
        
        public static Polynome operator *(Polynome p, Monome m)
        {
            var result = new Polynome(p);
            var current = result.polynomeList.First;
            while (current.Next != null)
                current = current.Next;
            current.Value.Factor *= m.Factor;
            current.Value.Degree += m.Degree;
            return result;
        }

        public static Polynome operator +(Monome m, Polynome p) => p + m;
        //для минуса "Сложно.Всё.Конец."(с)
        public static Polynome operator *(Monome m, Polynome p) => p * m;
        
        public static Polynome operator +(Polynome p, double arg)
        {
            var result = new Polynome(p);
            result.polynomeList.AddLast(arg);
            return result;
        }

        public static Polynome operator -(Polynome p, double arg)
        {
            var result = new Polynome(p);
            result.polynomeList.AddLast(-arg);
            return result;
        }

        public static Polynome operator *(Polynome p, double arg)
        {
            var result = new Polynome(p);
            var current = result.polynomeList.First;
            while (current.Next != null)
            {
                current.Value.Factor *= arg;
                current = current.Next;
            }
            return result;
        }
        #endregion        
        
    }

}