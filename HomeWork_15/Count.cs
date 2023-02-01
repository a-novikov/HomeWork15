using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_15
{
    public class Count
    {
        static int[,] massives;
        static List<int> values;       

        public void Run()
        {
            DateTime startTime = DateTime.Now;
            int start = 1000_000_000;
            int end = 2000_000_000;            
            int n = 100; // Количество задач
            values = new List<int>();

            CreateMassiveStartEnd(start, end, n);
            var items = Enumerable.Range(0, n).ToList();

            foreach (var item in items)
            {
                //Task.Run(() =>
                //{
                //    Console.WriteLine($"Старт задачи {item}");
                //    SetMassive(item);
                //});
                Console.WriteLine($"Старт задачи {item}");
                SetMassiveAsync(item);
            }

            while (!TestValues(n)) { }

            Console.WriteLine($"Итоговое время = {DateTime.Now - startTime}");
            Console.WriteLine($"Количество чисел = {FinalCount()}");
            Console.ReadKey();
        }


        /// <summary>
        /// Асинхронная обертка обычного метода SetMassive
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        async Task SetMassiveAsync(int item)
        {
            await Task.Run(() => SetMassive(item));        
        }

        /// <summary>
        /// Проверка того, что заполнен весь итоговый массив
        /// </summary>
        /// <returns></returns>
        bool TestValues(int n)
        {
            if (values == null) return false;
            else
            {
                if (values.Count == n) return true;
                else return false;
            }            
        }

        /// <summary>
        /// Сумма элементов массива
        /// </summary>
        /// <returns></returns>
        int FinalCount()
        {
            int count = 0;
            foreach (var item in values)
            {
                count += item;
            }
            return count;
        }

        /// <summary>
        /// Массив из начала-конец чисел
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="n"></param>
        void CreateMassiveStartEnd(int start, int end, int n)
        {
            massives = new int[n, 2]; 
            int delta = (end - start) / n;

            for (int i = 0; i < n; i++)
            {
                massives[i, 0] = start;
                massives[i, 1] = start + delta - 1;
                start = massives[i, 1] + 1;
                if (i == n - 1) massives[i, 1] += 1;
            }
        }


        /// <summary>
        /// Проверка числа на соответствие условиям
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        bool TestNumber(int number)
        {
            int value = 0;
            int lastValue = number % 10;

            while (number != 0)
            {
                value += number % 10;
                number /= 10;
            }
            if (lastValue == 0) return false;
            if (value % lastValue == 0) return true;
            else return false;
        }

        void SetMassive(int i)
        {
            int count = 0;
            for (int j = massives[i, 0]; j <= massives[i, 1]; j++)
            {
                if (TestNumber(j)) count++;
            }
            values.Add(count);
            Console.WriteLine($"Конец задачи {i}, количество = {count}");
        }

    }
}
