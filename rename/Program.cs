using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rename
{
    using System.IO;

    class Program
    {
        static void Main(string[] args)
        {
            /// Change these 
            string source_directory = @"P:\github\reportspace";
            string output_directory = @"P:\Output"
            string target = "ReportSpace";
            string replace = "Acrowire";

            DirectoryInfo source = new DirectoryInfo(source_directory);
            DirectoryInfo output = new DirectoryInfo(output_directory);

            
            // Directory Structure first 
            foreach (var dir in source.GetDirectories("*", SearchOption.AllDirectories))
            {
                string current_name = dir.FullName;
                string new_name = current_name.Replace(source_directory, output_directory);
                new_name = new_name.Replace(target, replace);

                if (Directory.Exists(new_name))
                {
                    Directory.Delete(new_name, true);
                }


                Directory.CreateDirectory(new_name);
                Console.WriteLine("Created new directory");
            }
            
            // Files 
            foreach (var file in source.GetFiles("*.*", SearchOption.AllDirectories))
            {
                string current_file_name = file.FullName;
                string new_file_name = current_file_name.Replace(source_directory, output_directory);
                new_file_name = new_file_name.Replace(target, replace);

                if (
                       current_file_name.Contains("packages") == false
                    || file.Extension.Contains("dll") == false
                )
                {
                    string file_contents = File.ReadAllText(file.FullName);

                    File.WriteAllText(new_file_name, file_contents.Replace(target, replace));
                    Console.WriteLine("converted {0}", new_file_name);
                }
                else
                {
                    File.Copy(current_file_name, new_file_name, true);
                    Console.WriteLine("copied {0}", new_file_name);
                }
            }



        }
    }
}
