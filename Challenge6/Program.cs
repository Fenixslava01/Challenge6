using System;
using System.IO;
using System.Text;



namespace Challenge6
{
    class Program
    {
        static void InsertData() {
            string[] fields = {"ID", "Дата и время добавления записи", "Ф. И. О.", "Возраст", "Рост", "Дата рождения", "Место рождения" };
            string result = "";
            foreach (string field in fields)
            {
                if (field != "Дата и время добавления записи")
                {
                    Console.WriteLine("Введите " + field.ToLower() + " cотрудника: ");
                    result += Console.ReadLine() + "#";
                }
                else {
                    result += DateTime.Now.ToString("dd-MM-yyyy hh:mm") + "#";
                }
            }
            using (FileStream fstream = new FileStream("tabel.txt", FileMode.Append))
            {
                byte[] buffer = Encoding.Default.GetBytes(result+"\n");
                fstream.Write(buffer, 0, buffer.Length);
            }
            ReadData();
        }
        static void InsertData(string text) {
            using (FileStream fstream = new FileStream("tabel.txt", FileMode.Append))
            {
                byte[] buffer = Encoding.Default.GetBytes(text);
                fstream.Write(buffer, 0, buffer.Length);
            }
        }
        static void ReadData() {
            using (FileStream fstream = File.OpenRead("tabel.txt"))
            {
                byte[] buffer = new byte[fstream.Length];
                fstream.Read(buffer, 0, buffer.Length);
                string textFromFile = Encoding.Default.GetString(buffer);
                string formatedText = textFromFile.Replace("#", "   ");
                Console.WriteLine($"Список сотрудников: \n{formatedText}");
            }
        }
        static void Main(string[] args)
        {
            bool isOperationNeed = true;
            #region Создание справочника сотрудников
            if (!File.Exists("tabel.txt")) {
                string text = "1#20.12.2021 00:12#Иванов Иван Иванович#25#176#05.05.1992#город Москва\n" +
                    "2#15.12.2021 03:12#Алексеев Алексей Иванович#24#176#05.11.1980#город Томск\n";
                InsertData(text);
            }
            #endregion
            while (isOperationNeed)
            {
                Console.WriteLine("Справочник сотрудников: \nВведите 1 для вывода данных на экран или 2 для внесения данных.\nЛибо любую другую цифру для выхода...");
                int userChoice = Convert.ToInt16(Console.ReadLine());
                if (userChoice == 2)
                {
                    InsertData();
                }
                else if (userChoice == 1)
                {
                    ReadData();
                }
                else {
                    isOperationNeed = false;
                }
            }

        }
    }
}
