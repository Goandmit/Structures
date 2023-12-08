using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Structures
{
    public class Repository
    {
        private string filePath;

        public Repository(string filePath)
        {
            this.filePath = filePath;
        }

        #region Присвоить идентификатор

        /// <summary>
        /// Присвоить идентификатор
        /// </summary>
        private int AssignID()
        {
            string streamString;
            int assignableID;

            if (System.IO.File.Exists("IDStorage.txt"))
            {
                using (StreamReader streamReader = new StreamReader("IDStorage.txt", Encoding.Unicode))
                {
                    streamString = $"{streamReader.ReadLine()}";
                }

                assignableID = Convert.ToInt32(streamString) + 1;
                streamString = assignableID.ToString();
            }
            else
            {
                streamString = "1";
                assignableID = Convert.ToInt32(streamString);
            }

            using (StreamWriter streamWriter = new StreamWriter("IDStorage.txt", false, Encoding.Unicode))
            {
                streamWriter.WriteLine(streamString);
            }

            return assignableID;
        }

        #endregion


        #region Получить от пользователя ввод только числового значения

        /// <summary>
        /// Получить от пользователя ввод только числового значения
        /// </summary>
        private int GetOnlyNumber()
        {
            string userInput;
            int requiredValue;
            bool status = false;

            do
            {                
                userInput = $"{Console.ReadLine()}";
                status = int.TryParse(userInput, out var result);

                if (status == true)
                {
                    break;
                }
                else
                {                    
                    Console.WriteLine("Введите число");
                }
                
            } 
            while (status == false);                    

            return requiredValue = Convert.ToInt32(userInput);
        }

        #endregion


        #region Получить от пользователя ввод только в формате даты

        /// <summary>
        /// Получить от пользователя ввод только в формате даты
        /// </summary>
        private DateTime GetOnlyDateTime()
        {
            string userInput;
            DateTime requiredValue;
            bool status = false;

            do
            {
                userInput = $"{Console.ReadLine()}";
                status = DateTime.TryParse(userInput, out DateTime result);

                if (status == true)
                {
                    break;
                }
                else
                {                    
                    Console.WriteLine("Введите дату (в формате дд.мм.гггг)");
                }
            }
            while (status == false);

            return requiredValue = Convert.ToDateTime(userInput);             
        }

        #endregion


        #region Записать поля структуры в файл

        /// <summary>
        /// Записать поля структуры в файл
        /// </summary>
        private void WriteWorkerToFile(Worker workerStruct)
        {
            string streamString = $"{workerStruct.ID}#" +
                $"{workerStruct.DateTimeOfRecording}#" +
                $"{workerStruct.FIO}#" +
                $"{workerStruct.Age}#" +
                $"{workerStruct.Height}#" +
                $"{workerStruct.DateOfBirth}#" +
                $"{workerStruct.PlaceOfBirth}";

            using (StreamWriter streamWriter = new StreamWriter(this.filePath, true, Encoding.Unicode))
            {
                streamWriter.WriteLine(streamString);
            }
        }

        #endregion


        #region Записать поля структуры, введенные пользователем, в файл

        /// <summary>
        /// Записать поля структуры, введенные пользователем, в файл
        /// </summary>
        public void AddCustomWorker()
        {
            Worker workerStruct = new Worker();            

            workerStruct.ID = AssignID();

            workerStruct.DateTimeOfRecording = DateTime.Now;

            Console.WriteLine("Введите данные сотрудника");
            Console.WriteLine();

            Console.WriteLine("Ф.И.О. (в формате \"Иванов Иван Иванович\")");            
            workerStruct.FIO = $"{Console.ReadLine()}";
            Console.WriteLine();

            Console.WriteLine("Возраст (в годах)");            
            workerStruct.Age = GetOnlyNumber();
            Console.WriteLine();

            Console.WriteLine("Рост (в сантиметрах)");            
            workerStruct.Height = GetOnlyNumber();
            Console.WriteLine();

            Console.WriteLine("Дата рождения (в формате дд.мм.гггг)");            
            workerStruct.DateOfBirth = GetOnlyDateTime();
            Console.WriteLine();

            Console.WriteLine("Место рождения (в формате \"город Москва\")");            
            workerStruct.PlaceOfBirth = $"{Console.ReadLine()}";
            Console.WriteLine();

            WriteWorkerToFile(workerStruct);
            
            Console.WriteLine("Запись добавлена");
            Console.WriteLine();
        }

        #endregion
        

        #region Проверить, существует ли файл и не является ли он пустым

        /// <summary>
        /// Проверить, существует ли файл и не является ли он пустым
        /// </summary>
        private bool CheckBeforeReading(string filePath)
        {
            bool status = false;

            if (System.IO.File.Exists(filePath))
            {
                if (new FileInfo(filePath).Length > 6)
                {
                    status = true;
                }
                else
                {                    
                    Console.WriteLine("Все записи были удалены. Добавьте или сгенерируйте новые записи");
                    Console.WriteLine();
                }
            }
            else
            {                
                Console.WriteLine("Не было создано ни одной записи либо удален содержащий их файл. " +
                    "Добавьте или сгенерируйте новые записи");
                Console.WriteLine();
            }

            return status;
        }

        #endregion


        #region Вывести в консоль заголовки для столбцов записей

        /// <summary>
        /// Вывести в консоль заголовки для столбцов записей
        /// </summary>
        private void TitlesPrint()
        {
            string[] titles = { "ID", "Дата создания записи", "Ф.И.О.", "Возраст",
                "Рост", "Дата рождения", "Место рождения" };
            
            Console.WriteLine($"{titles[0],-5} {titles[1],-24} {titles[2],-30}" +
                $"{titles[3],-9} {titles[4],-9} {titles[5],-24} {titles[6]}");
            Console.WriteLine();
        }

        #endregion


        #region Получить все записи из файла и положить их в массив структур

        /// <summary>
        /// Получить все записи из файла и положить их в массив структур
        /// </summary>
        private Worker[] GetAllWorkers()
        {
            Worker[] workersArray = new Worker[1];            

            using (StreamReader streamReader = new StreamReader(this.filePath, Encoding.Unicode))
            {
                int i = 0;

                while (!streamReader.EndOfStream)
                {
                    if (i >= workersArray.Length)
                    {
                        Array.Resize(ref workersArray, workersArray.Length + 1);
                    }

                    string streamString = $"{streamReader.ReadLine()}";
                    string[] streamStringSplited = streamString.Split("#");                    

                    workersArray[i] = new Worker(Convert.ToInt32(streamStringSplited[0]),
                        Convert.ToDateTime(streamStringSplited[1]),
                        streamStringSplited[2],
                        Convert.ToInt32(streamStringSplited[3]),
                        Convert.ToInt32(streamStringSplited[4]),
                        Convert.ToDateTime(streamStringSplited[5]),
                        streamStringSplited[6]);

                    i++;
                }
            }           

            return workersArray;            
        }

        #endregion


        #region Вывести в консоль все записи из файла

        /// <summary>
        /// Вывести в консоль все записи из файла
        /// </summary>
        public void GetAllWorkersPrint()
        {
            bool status = CheckBeforeReading(this.filePath);                        

            if (status == true)
            {
                Worker[] workersArray = GetAllWorkers();

                Console.WriteLine("Текущая информация о сотрудниках");
                Console.WriteLine();

                TitlesPrint();

                for (int i = 0; i < workersArray.Length; i++)
                {                    
                    Console.WriteLine(workersArray[i].Print());
                }

                Console.WriteLine();
            }
        }

        #endregion


        #region Проверить, существует ли переданный в метод идентификатор

        /// <summary>
        /// Проверить, существует ли переданный в метод идентификатор
        /// </summary>
        private bool CheckIdExsists(string filePath, int id)
        {
            bool status = false;

            using (StreamReader streamReader = new StreamReader(filePath, Encoding.Unicode))
            {
                while (!streamReader.EndOfStream)
                {
                    string streamString = $"{streamReader.ReadLine()}";
                    string[] streamStringSplited = streamString.Split("#");

                    if (Convert.ToInt32(streamStringSplited[0]) == id)
                    {
                        status = true;
                    }
                }
            }

            if (status == false)
            {                
                Console.WriteLine("Нет записи с таким идентификатом");
                Console.WriteLine();
            }

            return status;
        }

        #endregion


        #region Получить запись из файла по ее идентификатору, переданному в метод, и положить в структуру

        /// <summary>
        /// Получить запись из файла по ее идентификатору, переданному в метод, и положить в структуру
        /// </summary>
        private Worker GetWorkerById(int id)
        {
            Worker workerStruct = new Worker();

            using (StreamReader streamReader = new StreamReader(this.filePath, Encoding.Unicode))
            {
                while (!streamReader.EndOfStream)
                {
                    string streamString = $"{streamReader.ReadLine()}";
                    string[] streamStringSplited = streamString.Split("#");

                    if (Convert.ToInt32(streamStringSplited[0]) == id)
                    {
                        workerStruct = new Worker(Convert.ToInt32(streamStringSplited[0]),
                            Convert.ToDateTime(streamStringSplited[1]),
                            streamStringSplited[2],
                            Convert.ToInt32(streamStringSplited[3]),
                            Convert.ToInt32(streamStringSplited[4]),
                            Convert.ToDateTime(streamStringSplited[5]),
                            streamStringSplited[6]);
                    }
                }
            }

            return workerStruct;
        }

        #endregion


        #region Вывести в консоль запись из файла по ее идентификатору, введенному пользователем

        /// <summary>
        /// Вывести в консоль запись из файла по ее идентификатору, введенному пользователем
        /// </summary>
        public void GetWorkerByIdPrint()
        {
            bool status = CheckBeforeReading(this.filePath);

            if (status == true)
            {
                Console.WriteLine("Введите ID");
                int id = GetOnlyNumber();
                Console.WriteLine();

                status = CheckIdExsists(this.filePath, id);

                if (status == true)
                {
                    Worker workerStruct = GetWorkerById(id);

                    TitlesPrint();
                    
                    Console.WriteLine(workerStruct.Print());
                    Console.WriteLine();
                }
            }            
        }

        #endregion


        #region Удалить запись из файла по ее идентификатору, переданному в метод

        /// <summary>
        /// Удалить запись из файла по ее идентификатору, переданному в метод
        /// </summary>
        private void DeleteWorker(int id)
        {            
            string[] streamStringArray = new string[1];
            string[] streamStringSplited = new string[1];                      
            
            using (StreamReader streamReader = new StreamReader(this.filePath, Encoding.Unicode))
            {
                int i = 0;

                while (!streamReader.EndOfStream)
                {
                    if (i >= streamStringArray.Length)
                    {
                        Array.Resize(ref streamStringArray, streamStringArray.Length + 1);
                    }

                    streamStringArray[i] = $"{streamReader.ReadLine()}";                    

                    i++;
                }
            }

            ArrayList streamStringList = new ArrayList(streamStringArray);

            for (int i = 0; i < streamStringArray.Length; i++)
            {
                if (i >= streamStringSplited.Length)
                {
                    Array.Resize(ref streamStringSplited, streamStringSplited.Length * 2);
                }

                streamStringSplited = streamStringArray[i].Split("#");

                if (Convert.ToInt32(streamStringSplited[0]) == id)
                {                    
                    streamStringList.RemoveAt(i);                
                }               
            }

            using (StreamWriter streamWriter = new StreamWriter(this.filePath, false, Encoding.Unicode))
            {
                foreach (string streamString in streamStringList)
                {
                    streamWriter.WriteLine(streamString);
                }
            }            
        }

        #endregion


        #region Удалить запись из файла по ее идентификатору, введенному пользователем

        /// <summary>
        /// Удалить запись из файла по ее идентификатору, введенному пользователем
        /// </summary>
        public void DeleteWorkerPrint()
        {
            bool status = CheckBeforeReading(this.filePath);

            if (status == true)
            {
                Console.WriteLine("Введите ID");
                int id = GetOnlyNumber();
                Console.WriteLine();

                status = CheckIdExsists(this.filePath, id);

                if (status == true)
                {
                    DeleteWorker(id);
                   
                    Console.WriteLine("Запись удалена");
                    Console.WriteLine();
                }
            }                            
        }

        #endregion


        #region Проверить, существует ли переданный в метод диапазон дат

        /// <summary>
        /// Проверить, существует ли переданный в метод диапазон дат
        /// </summary>
        private bool CheckRangeOfDatesExsists(string filePath, DateTime dateFrom, DateTime dateTo)
        {
            bool status = false;

            using (StreamReader streamReader = new StreamReader(filePath, Encoding.Unicode))
            {
                while (!streamReader.EndOfStream)
                {
                    string streamString = $"{streamReader.ReadLine()}";
                    string[] streamStringSplited = streamString.Split("#");

                    if (Convert.ToDateTime(streamStringSplited[1]).Date >= dateFrom.Date
                    && Convert.ToDateTime(streamStringSplited[1]).Date <= dateTo.Date)
                    {
                        status = true;
                    }
                }
            }

            if (status == false)
            {                
                Console.WriteLine("Записи во введенном вами диапазоне отсутствуют " +
                    "либо диапазон введен неверно");
                Console.WriteLine("Дата НАЧАЛА отбора должна быть более ранней, " +
                    "чем дата КОНЦА отбора");
                Console.WriteLine();
            }

            return status;
        }

        #endregion


        #region Получить записи из файла в переданном в метод диапазоне дат и положить их в массив структур

        /// <summary>
        /// Получить записи из файла в переданном в метод диапазоне дат и положить их в массив структур
        /// </summary>
        private Worker[] GetWorkersBetweenTwoDates(DateTime dateFrom, DateTime dateTo)
        {
            Worker[] workersArray = new Worker[1];
            int indexWorkersArray = 0;
            string[] streamStringArray = new string[1];            

            using (StreamReader streamReader = new StreamReader(this.filePath, Encoding.Unicode))
            {
                int i = 0;

                while (!streamReader.EndOfStream)
                {
                    if (i >= streamStringArray.Length)
                    {
                        Array.Resize(ref streamStringArray, streamStringArray.Length + 1);
                    }

                    streamStringArray[i] = $"{streamReader.ReadLine()}";

                    i++;
                }
            }           

            for (int i = 0; i < streamStringArray.Length; i++)
            {
                string[] streamStringSplited = streamStringArray[i].Split("#");                

                if (Convert.ToDateTime(streamStringSplited[1]).Date >= dateFrom.Date
                    && Convert.ToDateTime(streamStringSplited[1]).Date <= dateTo.Date)
                {
                    Array.Resize(ref workersArray, workersArray.Length + 1);

                    workersArray[indexWorkersArray] = new Worker(Convert.ToInt32(streamStringSplited[0]),
                        Convert.ToDateTime(streamStringSplited[1]),
                        streamStringSplited[2],
                        Convert.ToInt32(streamStringSplited[3]),
                        Convert.ToInt32(streamStringSplited[4]),
                        Convert.ToDateTime(streamStringSplited[5]),
                        streamStringSplited[6]);

                    indexWorkersArray++;
                }                
            }

            Array.Resize(ref workersArray, workersArray.Length - 1);

            return workersArray;
        }

        #endregion


        #region Вывести в консоль записи из файла в диапазоне дат, заданном пользователем

        /// <summary>
        /// Вывести в консоль записи из файла в диапазоне дат, заданном пользователем
        /// </summary>
        public void GetWorkersBetweenTwoDatesPrint()
        {
            bool status = CheckBeforeReading(this.filePath);

            if (status == true)
            {
                Console.WriteLine("Введите дату НАЧАЛА отбора записей (в формате дд.мм.гггг)");
                DateTime dateFrom = GetOnlyDateTime();                

                Console.WriteLine("Введите дату КОНЦА отбора записей (в формате дд.мм.гггг)");
                DateTime dateTo = GetOnlyDateTime();
                Console.WriteLine();

                status = CheckRangeOfDatesExsists(this.filePath, dateFrom, dateTo);

                if (status == true)
                {
                    Worker[] workersArray = GetWorkersBetweenTwoDates(dateFrom, dateTo);

                    Console.WriteLine($"Данные о сотрудниках, добавленные с {dateFrom.ToShortDateString()}" +
                        $" по {dateTo.ToShortDateString()}:");
                    Console.WriteLine();

                    TitlesPrint();

                    for (int i = 0; i < workersArray.Length; i++)
                    {
                        Console.WriteLine(workersArray[i].Print());
                    }

                    Console.WriteLine();
                }
            }           
        }

        #endregion


        #region Проинформировать пользователя о том, что принимается только ввод предложенных значений

        /// <summary>
        /// Проинформировать пользователя о том, что принимается только ввод предложенных значений
        /// </summary>
        public void GetOnlySuggestedOption()
        {            
            Console.WriteLine("Введите один из предложенных вариантов");
            Console.WriteLine();
        }

        #endregion


        #region Сортировать записи по одному из полей, выбранному пользователем

        /// <summary>
        /// Сортировать записи по одному из полей, выбранному пользователем
        /// </summary>
        public void SortingWorkers()
        {
            bool status = CheckBeforeReading(this.filePath);

            if (status == true)
            {
                Worker[] workersArray = GetAllWorkers();                               

                var workersArrayOrderly = workersArray.OrderBy(w => w.ID);
                
                Console.WriteLine("По какому полю сортировать?");
                Console.WriteLine();
                Console.WriteLine("ID - 1");
                Console.WriteLine("Дата добавления записи - 2");
                Console.WriteLine("Ф.И.О. - 3");
                Console.WriteLine("Возраст - 4");
                Console.WriteLine("Рост - 5");
                Console.WriteLine("Дата рождения - 6");
                Console.WriteLine("Место рождения - 7");
                Console.WriteLine();

                string userInput = $"{Console.ReadLine()}";
                Console.WriteLine();

                string SelectSortingDirection()
                {                    
                    Console.WriteLine("По возрастанию - 1");
                    Console.WriteLine("По убыванию - 2");
                    Console.WriteLine();

                    string userInput = $"{Console.ReadLine()}";
                    Console.WriteLine();

                    return userInput;
                }                

                switch (userInput)
                {
                    case "1":                        
                        
                        switch (userInput = SelectSortingDirection())
                        {
                            case "1":
                                workersArrayOrderly = workersArray.OrderBy(w => w.ID);
                                break;
                            case "2":
                                workersArrayOrderly = workersArray.OrderByDescending(w => w.ID);
                                break;
                            default:
                                GetOnlySuggestedOption();
                                break;
                        }                        
                        break;
                    case "2":                        
                        switch (userInput = SelectSortingDirection())
                        {
                            case "1":
                                workersArrayOrderly = workersArray.OrderBy(w => w.DateTimeOfRecording);
                                break;
                            case "2":
                                workersArrayOrderly = workersArray.OrderByDescending(w => w.DateTimeOfRecording);
                                break;
                            default:
                                GetOnlySuggestedOption();
                                break;
                        }                        
                        break;
                    case "3":                        
                        switch (userInput = SelectSortingDirection())
                        {
                            case "1":
                                workersArrayOrderly = workersArray.OrderBy(w => w.FIO);
                                break;
                            case "2":
                                workersArrayOrderly = workersArray.OrderByDescending(w => w.FIO);
                                break;
                            default:
                                GetOnlySuggestedOption();
                                break;
                        }
                        break;
                    case "4":                        
                        switch (userInput = SelectSortingDirection())
                        {
                            case "1":
                                workersArrayOrderly = workersArray.OrderBy(w => w.Age);
                                break;
                            case "2":
                                workersArrayOrderly = workersArray.OrderByDescending(w => w.Age);
                                break;
                            default:
                                GetOnlySuggestedOption();
                                break;
                        }                        
                        break;
                    case "5":                        
                        switch (userInput = SelectSortingDirection())
                        {
                            case "1":
                                workersArrayOrderly = workersArray.OrderBy(w => w.Height);
                                break;
                            case "2":
                                workersArrayOrderly = workersArray.OrderByDescending(w => w.Height);
                                break;
                            default:
                                GetOnlySuggestedOption();
                                break;
                        }                        
                        break;
                    case "6":                        
                        switch (userInput = SelectSortingDirection())
                        {
                            case "1":
                                workersArrayOrderly = workersArray.OrderBy(w => w.DateOfBirth);
                                break;
                            case "2":
                                workersArrayOrderly = workersArray.OrderByDescending(w => w.DateOfBirth);
                                break;
                            default:
                                GetOnlySuggestedOption();
                                break;
                        }                        
                        break;
                    case "7":                        
                        switch (userInput = SelectSortingDirection())
                        {
                            case "1":
                                workersArrayOrderly = workersArray.OrderBy(w => w.PlaceOfBirth);
                                break;
                            case "2":
                                workersArrayOrderly = workersArray.OrderByDescending(w => w.PlaceOfBirth);
                                break;
                            default:
                                GetOnlySuggestedOption();
                                break;
                        }                        
                        break;
                    default:
                        GetOnlySuggestedOption();                        
                        break;
                }                

                Console.WriteLine("Текущая информация о сотрудниках");
                Console.WriteLine();

                TitlesPrint();

                foreach (Worker i in workersArrayOrderly)
                {
                    Console.WriteLine(i.Print());
                }

                Console.WriteLine();
            }
        }

        #endregion


        #region Редактировать запись по идентификатору, переданному в метод

        /// <summary>
        /// Редактировать запись по идентификатору, переданному в метод
        /// </summary>
        private void EditWorkerByID(int id)
        {
            Worker workerStruct = new Worker();
            string[] streamStringArray = new string[1];            
            string continueEditing = String.Empty;

            using (StreamReader streamReader = new StreamReader(this.filePath, Encoding.Unicode))
            {
                int i = 0;

                while (!streamReader.EndOfStream)
                {
                    if (i >= streamStringArray.Length)
                    {
                        Array.Resize(ref streamStringArray, streamStringArray.Length + 1);
                    }

                    streamStringArray[i] = $"{streamReader.ReadLine()}";                    

                    string[] streamStringSplited = streamStringArray[i].Split("#");                    

                    if (Convert.ToInt32(streamStringSplited[0]) == id)
                    {
                        workerStruct = new Worker(Convert.ToInt32(streamStringSplited[0]),
                            Convert.ToDateTime(streamStringSplited[1]),
                            streamStringSplited[2],
                            Convert.ToInt32(streamStringSplited[3]),
                            Convert.ToInt32(streamStringSplited[4]),
                            Convert.ToDateTime(streamStringSplited[5]),
                            streamStringSplited[6]);
                    }

                    i++;
                }
            }

            while (continueEditing != "1")
            {                
                Console.WriteLine("Какое поле редактировать?");
                Console.WriteLine();
                Console.WriteLine("Ф.И.О. - 1");
                Console.WriteLine("Возраст - 2");
                Console.WriteLine("Рост - 3");
                Console.WriteLine("Дата рождения - 4");
                Console.WriteLine("Место рождения - 5");
                Console.WriteLine();

                string userInput = $"{Console.ReadLine()}";
                Console.WriteLine();
                
                Console.WriteLine("Введите данные сотрудника");
                Console.WriteLine();

                switch (userInput)
                {
                    case "1":
                        Console.WriteLine("Ф.И.О. (в формате \"Иванов Иван Иванович\")");
                        workerStruct.FIO = $"{Console.ReadLine()}";
                        break;
                    case "2":
                        Console.WriteLine("Возраст (в годах)");
                        workerStruct.Age = GetOnlyNumber();
                        break;
                    case "3":
                        Console.WriteLine("Рост (в сантиметрах)");
                        workerStruct.Height = GetOnlyNumber();
                        break;
                    case "4":
                        Console.WriteLine("Дата рождения (в формате дд.мм.гггг)");
                        workerStruct.DateOfBirth = GetOnlyDateTime();
                        break;
                    case "5":
                        Console.WriteLine("Место рождения (в формате \"город Москва\")");
                        workerStruct.PlaceOfBirth = $"{Console.ReadLine()}";
                        break;
                    default:
                        GetOnlySuggestedOption();
                        break;
                }

                Console.WriteLine();
                
                Console.WriteLine("Чтобы закончить редактирование введите 1");                
                continueEditing = $"{Console.ReadLine()}";
                Console.WriteLine();
            }

            for (int i = 0; i < streamStringArray.Length; i++)
            {
                string[] streamStringSplited = streamStringArray[i].Split("#");

                if (Convert.ToInt32(streamStringSplited[0]) == id)
                {
                    streamStringArray[i] = $"{workerStruct.ID}#" +
                $"{workerStruct.DateTimeOfRecording}#" +
                $"{workerStruct.FIO}#" +
                $"{workerStruct.Age}#" +
                $"{workerStruct.Height}#" +
                $"{workerStruct.DateOfBirth}#" +
                $"{workerStruct.PlaceOfBirth}";
                }
            }

            using (StreamWriter streamWriter = new StreamWriter(this.filePath, false, Encoding.Unicode))
            {
                foreach (string streamString in streamStringArray)
                {
                    streamWriter.WriteLine(streamString);
                }
            }
        }

        #endregion


        #region Редактировать запись по идентификатору, введенному пользователем

        /// <summary>
        /// Редактировать запись по идентификатору, введенному пользователем
        /// </summary>
        public void EditWorkerByIDPrint()
        {
            bool status = CheckBeforeReading(this.filePath);

            if (status == true)
            {                
                Console.WriteLine("Введите ID");
                int id = GetOnlyNumber();
                Console.WriteLine();

                status = CheckIdExsists(this.filePath, id);

                if (status == true)
                {
                    EditWorkerByID(id);
                    
                    Console.WriteLine("Запись обновлена");
                    Console.WriteLine();
                }
            }
        }

        #endregion


        #region Сгенерировать записи в количестве, переданном в метод

        /// <summary>
        /// Сгенерировать записи в количестве, переданном в метод
        /// </summary>
        private void GenerateWorkers(int quantity)
        {
            for (int i = 0; i < quantity; i++)
            {
                Worker workerStruct = new Worker(AssignID());

                WriteWorkerToFile(workerStruct);
            }
        }

        #endregion


        #region Сгенерировать записи в количестве, введенном пользователем

        /// <summary>
        /// Сгенерировать записи в количестве, введенном пользователем
        /// </summary>
        public void GenerateWorkersPrint()
        {
            Console.WriteLine("Введите количество записей, которое нужно сгенерировать");
            int quantity = GetOnlyNumber();
            Console.WriteLine();

            GenerateWorkers(quantity);

            Console.WriteLine("Записи добавлены");
            Console.WriteLine();
        }

        #endregion        
    }
}