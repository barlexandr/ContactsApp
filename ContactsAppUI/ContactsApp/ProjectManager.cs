using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace ContactsApp
{
    /// <summary>
    /// Класс, реализующий сохранение данных в файл и загрузки из него.
    /// </summary>
    public class ProjectManager
    {
        /// <summary>
        /// Путь к файлу.
        /// </summary>
        private static string stringMyDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "ContactsApp.notes";

        /// <summary>
        /// Метод, выполняющий запись в файл
        /// </summary>
        /// <param name="notes">Экземпляр проекта для сериализации</param>
        public static void ProjectSerialization(Project contact)
        {
            // Экземпляр сериалиатора
            JsonSerializer serializer = new JsonSerializer();

            // Преобразование из string в System.IO.Stream
            byte[] byteArray = Encoding.UTF8.GetBytes(stringMyDocumentsPath);
            MemoryStream stream = new MemoryStream(byteArray);

            // Открываем поток для записи в файл с указанием пути
            using (StreamWriter sw = new StreamWriter(@stream))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                // Вызов сериализатора и передача объекта сериализации
                serializer.Serialize(writer, contact);
            }
        }

        /// <summary>
        /// Метод, выполняющий чтение из файла
        /// </summary>
        /// <returns>Экземпляр проекта, считанный из файла</returns>
        public static Project ProjectDeserialization()
        {
            //Переменная, в которую будет помещен результат десериализации
            Project project = null;

            //Экземпляр сериализатора
            JsonSerializer serializer = new JsonSerializer();

            // Преобразование из string в System.IO.Stream
            byte[] byteArray = Encoding.UTF8.GetBytes(stringMyDocumentsPath);
            MemoryStream stream = new MemoryStream(byteArray);

            //Открываем поток для чтения из файла с указанием пути
            using (StreamReader sr = new StreamReader(@stream))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                //Вызываем десериализацию и явно преобразуем результат в целевой тип данных
                project = (Project) serializer.Deserialize<Project>(reader);
            }

            return project;
        }
    }
}