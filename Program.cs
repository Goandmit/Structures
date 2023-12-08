using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structures
{
    public class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"workersStorage.txt";

            Repository repositoryExemplar = new Repository(filePath);            

            void Menu()
            {
                Console.WriteLine("Выберите опцию");
                Console.WriteLine();
                Console.WriteLine("1 - Добавить запись");
                Console.WriteLine("2 - Вывести все записи");
                Console.WriteLine("3 - Вывести запись по ID");
                Console.WriteLine("4 - Удалить запись по ID");
                Console.WriteLine("5 - Вывести записи в диапазоне дат их добавления");
                Console.WriteLine("6 - Вывести все записи в сортированном виде");
                Console.WriteLine("7 - Редактировать запись по ID");
                Console.WriteLine("8 - Сгенерировать записи");                
                Console.WriteLine("9 - Выйти");
                Console.WriteLine();

                string userInput = $"{Console.ReadLine()}";
                Console.WriteLine();

                switch (userInput)
                {
                    case "1":                        
                        repositoryExemplar.AddCustomWorker();                        
                        Menu();
                        break;
                    case "2":                        
                        repositoryExemplar.GetAllWorkersPrint();                        
                        Menu();
                        break;                    
                    case "3":                        
                        repositoryExemplar.GetWorkerByIdPrint();                        
                        Menu();
                        break;
                    case "4":                        
                        repositoryExemplar.DeleteWorkerPrint();                        
                        Menu();
                        break;
                    case "5":                        
                        repositoryExemplar.GetWorkersBetweenTwoDatesPrint();                        
                        Menu();
                        break;
                    case "6":                        
                        repositoryExemplar.SortingWorkers();                        
                        Menu();
                        break;
                    case "7":                        
                        repositoryExemplar.EditWorkerByIDPrint();                        
                        Menu();
                        break;
                    case "8":                        
                        repositoryExemplar.GenerateWorkersPrint();                        
                        Menu();
                        break;
                    case "9":
                        Environment.Exit(0);
                        Menu();
                        break;                   
                    default:
                        repositoryExemplar.GetOnlySuggestedOption();
                        Menu();
                        break;
                }               
            }

            Menu();
        }       
    }
}