namespace Test
{
    class Program
    {
        //Функция генерации исходных файлов с числами
        public static void CreateFile(string directoryPath)
        {
            string fileName = Console.ReadLine();
            string filePath = directoryPath + @"\" + fileName + ".txt";

            //Выбор количества чисел в файле
            Random rnd = new Random();
            int amount = rnd.Next(100, 1000);

            //Записывание случайных чисел
            for (int i = 0; i < amount; i++)
            {
                int number = rnd.Next(1, 1000);
                File.AppendAllText(filePath, number.ToString() + "\n");
            }
        }

        //Основная функция
        static void Main(string[] args)
        {
            string directoryPath = Console.ReadLine();
            string resultPath = string.Concat(directoryPath, @"\result.txt");

            if (File.Exists(resultPath))
                File.Delete(resultPath);

            //Создание новых файлов с данными
            Console.WriteLine("Сколько новых файлов с числами хотите создать?");
            string answer = Console.ReadLine();

            int amount = 0;
            _ = int.TryParse(answer, out amount);

            for (int i = 0; i < amount; i++)
            {
                Program.CreateFile(directoryPath);
            }

            //Получение адреса всех файлов с данными из каталога
            var files = Directory.GetFiles(directoryPath);

            Dictionary<int, int> numberMap = new Dictionary<int, int>();
            List<int> numberList = new List<int>();

            //Чтение и запись целых чисел, удолетворяющих условию 2.2, в HashMap
            foreach (var file in files)
            {
                StreamReader reader = new StreamReader(file);

                int number = 0;
                string? line;

                while ((line = reader.ReadLine()) != null)
                {
                    _ = int.TryParse(line, out number);

                    if (numberMap.ContainsKey(number))
                    {
                        numberMap[number] += 1;
                    }
                    else
                    {
                        if (number % 4 == 3)
                            numberMap.Add(number, 1);
                    }
                }

                reader.Close();
            }

            //Проверка чисел на уникальность
            foreach (KeyValuePair<int, int> kvPair in numberMap)
            {
                if (kvPair.Value == 1)
                    numberList.Add(kvPair.Key);
            }

            //Сортировка по убыванию
            numberList.Sort();
            numberList.Reverse();

            //Запись отобранных чисел в файл result.txt
            foreach (var number in numberList)
            {
                File.AppendAllText(resultPath, number.ToString() + "\n");
            }
        }
    }
}