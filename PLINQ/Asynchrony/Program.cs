namespace Asynchrony
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    internal class Program {

        static async Task Main(string[] args)
        {
            #region Task_1
            var Task_1 = Lesson_1.Task1();
            var Task_2 = Lesson_1.Task2();
            int num1 = Task_1.Result;
            int num2 = await Task_2;

            Console.WriteLine($"{num1} : {num2} = {num1 + num2}");
            #endregion
        }

    }
    
}

