using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SystemFileReader2
{
    public class Item
    {
        public string Name { get; set; }
        public Item Parent { get; set; }
        public string Path { get; set; }
    }

    public class Folder : Item
    {
        public List<Item> Items { get; set; }

        public Folder()
        {
            Items = new List<Item>();
        }
    }

    public class File : Item
    {
        public string Extension { get; set; }
    }



    public class SystemFileReader
    {
        public Folder CurrentFolder
        {
            get;
            private set;
        }

        private void ProcessDirectory(Folder targetDirectory)
        {
            // Recurse into subdirectories of this directory.
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory.Path);
            foreach (string subdirectory in subdirectoryEntries)
            {
                Folder newFolder = CreateFolder(subdirectory);
                targetDirectory.Items.Add(newFolder);
                ProcessDirectory(newFolder);
            }

            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(targetDirectory.Path);
            foreach (string fileName in fileEntries)
                ProcessFile(fileName, targetDirectory);


        }

        private Folder CreateFolder(string path)
        {
            DirectoryInfo currentFolder = new DirectoryInfo(path);
            Folder newFolder = new Folder();
            newFolder.Name = currentFolder.Name;
            newFolder.Path = path;
            return newFolder;
        }

        private void ProcessFile(string path, Folder parent)
        {
            FileInfo file = new FileInfo(path);
            parent.Items.Add(new File() { Name = file.Name, Extension = file.Extension, Parent = parent, Path = path });
        }

        public void ReadFolder(string path)
        {
            if (Directory.Exists(path))
            {
                CurrentFolder = CreateFolder(path);
                ProcessDirectory(CurrentFolder);
            }
            else
            {
                throw new ArgumentException("Path is not exist");
            }
        }

        public void SaveToFile(string path)
        {

        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            string nesting = "";
            CreateFileSystemString(result, CurrentFolder, nesting);
            return result.ToString();
        }
        public object Clone(StringBuilder nesting)
        {
            StringBuilder nesting2;
            nesting2 = nesting;
            return nesting2;
        }
        public void CreateFileSystemString(StringBuilder result, Folder folder, String nesting)
        {
            using (StreamWriter sw = new StreamWriter(@"D:\Games\awgd.txt"))
            {
                
                result.AppendFormat("{0}{1}", nesting, folder.Name);
                result.AppendLine();
                //sw.WriteLine("{0}{1}", "_" + nesting, file.Name);
                foreach (Item item in folder.Items)
                {
                    Folder dir = item as Folder;
                    if (dir != null)
                    {

                        CreateFileSystemString(result, dir, nesting + "_");
                    }
                    File file = item as File;
                    if (file != null)
                    {
                        result.AppendFormat("{0}{1}", "_" + nesting, file.Name);
                        result.AppendLine();
                        sw.WriteLine("{0}{1}", "_" + nesting, file.Name);
                    }
                }
            }


            using (StreamWriter sw = new StreamWriter("result2.txt"))
            {

            }


        }
    }
}
