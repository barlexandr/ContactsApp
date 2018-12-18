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
        /// Стандартный путь к файлу.
        /// </summary>
        public static readonly string _stringMyDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
                                                      "/ContactsApp" + "/ContactsApp.notes";

        /// <summary>
        /// Метод, выполняющий запись в файл 
        /// </summary>
        /// <param name="contact">Экземпляр проекта для сериализации</param>
        /// <param name="fileContactAppPath">Путь к файлу</param>
        public static void ProjectSerialization(Project contact, string fileContactAppPath)
        {
            // Экземпляр сериалиатора
            JsonSerializer serializer = new JsonSerializer();

            //Проверка на папку. Если нет папки ContactsApp, то создаем ее.
            if(!System.IO.Directory.Exists(fileContactAppPath))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
                                          "/ContactsApp");
            }

            using (StreamWriter sw = new StreamWriter(fileContactAppPath))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                // Вызов сериализатора и передача объекта сериализации
                serializer.Serialize(writer, contact);
            }
        }

        /// <summary>
        /// Метод, выполняющий чтение из файла 
        /// </summary>
        /// <param name="fileContactAppPath">Путь к файлу</param>
        /// <returns>Экземпляр проекта, считанный из файла</returns>
        public static Project ProjectDeserialization(string fileContactAppPath)
        {
            //Переменная, в которую будет помещен результат десериализации
            Project project = new Project();

            //Экземпляр сериализатора
            JsonSerializer serializer = new JsonSerializer();

            //Проверка на наличие файла
            if (!System.IO.Directory.Exists(fileContactAppPath))
            {
                //Открываем поток для чтения из файла с указанием пути
                using (StreamReader sr = new StreamReader(fileContactAppPath))
                using (JsonReader reader = new JsonTextReader(sr))
                {
                    //Вызываем десериализацию и явно преобразуем результат в целевой тип данных
                    project = (Project)serializer.Deserialize<Project>(reader);
                }
            }
       
            return project;
        }
    }
}