using System;
using System.Threading;

namespace Matrix
{
    class Program
    {
        class Matrix
        {
            static object locker = new object(); //ссылука на объект который будет использован для ограничения доступа в критическую секцию

            Random rand;

            const string litters = "0123456789$"; //символы которые бужут на консоли

            public int Colunm { get; set; } //обработчик колонки(колонка)


            public Matrix(int col)
            {
                Colunm = col;
                rand = new Random((int)DateTime.Now.Ticks); // случайное число
            }

            private char GetChar()
            {
                return litters.ToCharArray()[rand.Next(0, 9)]; //получение случайного числа из сгенерированого массива из чисел, и обращение к элменту по случайному индексу
            }

            public void Move() // основной метод
            {
                int count; //создаем счётчик
                int randomNumber; //случайное число

                while (true) // программа будет выполняться бесконечно
                {
                    randomNumber = rand.Next(3, 12); //случайное число
                    count = 0;
                    Thread.Sleep(rand.Next(20, 5000)); //случайная пауза
                    for (int i = 0; i < 40; i++) //количество итераций
                    {
                        lock (locker) //критическая секция, чтобы потоки работали поочереди
                        {
                            Console.ForegroundColor = ConsoleColor.Black; 
                            Console.CursorTop = i - count; //управление курсором по вертикали
                            for (int j = i - count - 1; j < i; j++)
                            {
                                Console.CursorLeft = Colunm; //управление курсором по горизонтали
                                Console.WriteLine(" "); //отображаем пробелы чтобы последовательности были раздельные
                            }

                            if (count < randomNumber) //если текущая итерация меньше случайного числа
                                count++; //то увеличиваем счётчик
                          
                            if (39 - i < count) //если 39(максимальное значение) - счётчика цикла for меньше счётчика count
                                count--; // то уменьшаем счётчик
                            Console.CursorTop = i - count + 1; //передвигаем курсор
                            //Снизу Colunm одинаковая в этом случае
                            //Разное количество случайных символов
                            //Чем меньше счётчик тем меньше будет выдавтся символов в разделенной цепочке
                  
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            for (int j = 0; j < count - 2; j++)
                            {
                                Console.CursorLeft = Colunm;
                                Console.WriteLine(GetChar());
                            }
                            if (count >= 3) //если пройдет проверку то на текущей итерации выполниться только раз
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.CursorLeft = Colunm;
                                Console.WriteLine(GetChar());
                            }
                            if (count >= 1) //если пройдет проверку то на текущей итерации выполниться только раз
                            {
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.CursorLeft = Colunm;
                                Console.WriteLine(GetChar());
                            }

                            Thread.Sleep(7); // тут регулировать скорость
                        }
                    }
                }
            }
        }
    static void Main(string[] args)
        {
            //Console.SetWindowSize(80, 42);

            //Matrix instance;

            //for (int i = 0; i < 24; i++)  // количество потоков, задаеться в зависимости от необходимой частоты смены картины(значений элементов)
            //{
            //    instance = new Matrix(i * 3); //положение курсора сдвигается с геометрической прогрессией с шагом 3
            //    new Thread(instance.Move).Start();
            //}

            //Управления курсором
            Console.CursorLeft = 10; //управление курсором по горизонтали,если не задано то умолчанию 0
            Console.WriteLine("A");  //Строка
            Console.CursorLeft = 20; //управление курсором по горизонтали,если не задано то умолчанию 0
            Console.CursorTop = 10;  //управление курсором по вертикали,если не задано то умолчанию 0
            Console.WriteLine("E");  //Строка
            Console.CursorLeft = 30; //управление курсором по горизонтали,если не задано то умолчанию 0
            Console.CursorTop = 20;  //управление курсором по вертикали,если не задано то умолчанию 0
            Console.WriteLine("D");  //Строка
            Console.CursorLeft = 30; //управление курсором по горизонтали,если не задано то умолчанию 0
            Console.CursorTop = 0;   //управление курсором по вертикали,если не задано то умолчанию 0
            Console.WriteLine("B");  //Строка
            Console.CursorLeft = 10; //управление курсором по горизонтали,если не задано то умолчанию 0
            Console.CursorTop = 20;  //управление курсором по вертикали,если не задано то умолчанию 0
            Console.WriteLine("C");

            //Задержка
            Console.ReadKey();
        }
    }
}
