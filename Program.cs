using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace FinalTask
{
    public class Program
    {
        static void Main(string[] args)            
        {           
            var FileName = String.Empty;
            Console.WriteLine("Введите путь и имя файла для бинарного чтения");
            do
            {
                FileName = Console.ReadLine();
                if (File.Exists(FileName))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    using (FileStream fs = new FileStream(FileName, FileMode.OpenOrCreate))
                    {
                        try
                        {                            
                            var dirDesk = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)+"\\Students\\";
                            Student[] students = (Student[])formatter.Deserialize(fs);
                            if (!Directory.Exists(dirDesk))
                                Directory.CreateDirectory(dirDesk);
                            else
                            {
                                Console.WriteLine("Каталог Students уже существует на рабочем столе!\nПереименуйте или удалите его во избежание дублирования информации.");
                                break;
                            }
                            foreach (Student st in students)
                            {
                                if(!File.Exists(dirDesk + st.Group + ".txt"))
                                {
                                    using (StreamWriter streamW = File.CreateText(dirDesk + st.Group + ".txt"))
                                    {
                                        streamW.WriteLine(st.Name + ", " + st.DateOfBirth.ToShortDateString());
                                    }
                                }
                                else
                                {
                                    using (StreamWriter streamW = File.AppendText(dirDesk + st.Group + ".txt"))
                                    {
                                        streamW.WriteLine(st.Name + ", " + st.DateOfBirth.ToShortDateString());
                                    }
                                }
                            }
                            break;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Путь указан не верно или файл не существует, \nповторите ввод или закройте программу");
                }
            } while (true);
            Console.WriteLine("Программа завершила работу, нажмите клавишу AnyKey");
            Console.ReadKey();
        }        
    }
}
