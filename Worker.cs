using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structures
{
    public struct Worker
    {
        #region Поля

        /// <summary>
        /// Идентификатор
        /// </summary>        
        private int iD;

        /// <summary>
        /// Дата и время записи
        /// </summary>        
        private DateTime dateTimeOfRecording;

        /// <summary>
        /// Ф.И.О.
        /// </summary>        
        private string fIO;

        /// <summary>
        /// Возраст
        /// </summary>        
        private int age;

        /// <summary>
        /// Рост
        /// </summary>        
        private int height;

        /// <summary>
        /// Дата рождения
        /// </summary>        
        private DateTime dateOfBirth;

        /// <summary>
        /// Место рождения
        /// </summary>        
        private string placeOfBirth;

        #endregion


        #region Конструктор

        /// <param name = "iD">Идентификатор</param>
        /// <param name = "dateTimeOfRecording">Дата и время записи</param>
        /// <param name = "fIO">Ф.И.О.</param>
        /// <param name = "age">Возраст</param>
        /// <param name = "height">Рост</param>
        /// <param name = "dateOfBirth">Дата рождения</param>
        /// <param name = "placeOfBirth">Место рождения</param>
        public Worker(int iD, DateTime dateTimeOfRecording, string fIO, int age, int height, DateTime dateOfBirth, string placeOfBirth)
        {
            this.iD = iD;
            this.dateTimeOfRecording = dateTimeOfRecording;
            this.fIO = fIO;
            this.age = age;
            this.height = height;
            this.dateOfBirth = dateOfBirth;
            this.placeOfBirth = placeOfBirth;
        }

        public Worker(int iD, DateTime dateTimeOfRecording, string fIO, int age, int height, DateTime dateOfBirth) :
            this(iD, dateTimeOfRecording, fIO, age, height, dateOfBirth, "Unknown")
        { 
        
        }

        public Worker(int iD, DateTime dateTimeOfRecording, string fIO, int age, int height) :
            this(iD, dateTimeOfRecording, fIO, age, height, new DateTime(1800, 1, 1), "Unknown")
        {

        }

        public Worker(int iD, DateTime dateTimeOfRecording, string fIO, int age) :
            this(iD, dateTimeOfRecording, fIO, age, 0, new DateTime(1800, 1, 1), "Unknown")
        {

        }

        public Worker(int iD, DateTime dateTimeOfRecording, string fIO) :
            this(iD, dateTimeOfRecording, fIO, 0, 0, new DateTime(1800, 1, 1), "Unknown")
        {

        }

        public Worker(int iD, DateTime dateTimeOfRecording) :
            this(iD, dateTimeOfRecording, String.Empty, 0, 0, new DateTime(1800, 1, 1), "Unknown")
        {
            this.fIO = $"User{this.ID}";
        }

        public Worker(int iD) :
            this(iD, DateTime.Now, String.Empty, 0, 0, new DateTime(1800, 1, 1), "Unknown")
        {
            this.fIO = $"User{this.ID}";
        }

        #endregion


        #region Свойства

        public int ID
        {
            get { return this.iD; }
            set { this.iD = value; }
        }

        public DateTime DateTimeOfRecording
        {
            get { return this.dateTimeOfRecording; }
            set { this.dateTimeOfRecording = value; }
        }

        public string FIO
        {
            get { return this.fIO; }
            set { this.fIO = value; }
        }

        public int Age        {
            get { return this.age; }
            set { this.age = value; }
        }

        public int Height
        {
            get { return this.height; }
            set { this.height = value; }
        }

        public DateTime DateOfBirth
        {
            get { return this.dateOfBirth; }
            set { this.dateOfBirth = value; }
        }

        public string PlaceOfBirth
        {
            get { return this.placeOfBirth; }
            set { this.placeOfBirth = value; }
        }

        #endregion


        #region Методы

        public string Print()
        {
            return $"{this.iD,-5}" +
                $" {this.dateTimeOfRecording, -25}" +
                $"{this.fIO, -30}" +
                $"{this.age,-10}" +
                $"{this.height, -10}" +
                $"{this.dateOfBirth,-25}" +
                $"{this.placeOfBirth}";
        }

        #endregion
    }
}
