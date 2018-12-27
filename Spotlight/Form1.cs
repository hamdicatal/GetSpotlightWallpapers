using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spotlight
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string GetCurrentUserPath()
        {
            string path = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)).FullName;
            if (Environment.OSVersion.Version.Major >= 6)
            {
                path = Directory.GetParent(path).ToString();
            }

            return path;
        }

        string CreateFolder()
        {
            string path = GetCurrentUserPath();

            // Specify a "currently active folder"
            string activeDir = path + "//Desktop//";

            DateTime time = DateTime.Now;

            //Create a new subfolder under the current active folder
            string folderName = "Spotlight-" + time.Hour;
            string newPath = System.IO.Path.Combine(activeDir, folderName);

            // Create the subfolder
            System.IO.Directory.CreateDirectory(newPath);

            // Create a new file name. This example generates
            // a random string.
            string newFileName = System.IO.Path.GetRandomFileName();

            // Combine the new file name with the path
            newPath = System.IO.Path.Combine(newPath, newFileName);

            return folderName;
        }

        void Copy(string sourceDir, string targetDir)
        {
            Directory.CreateDirectory(targetDir);

            foreach (var file in Directory.GetFiles(sourceDir))
                File.Copy(file, Path.Combine(targetDir, Path.GetFileName(file)));

            foreach (var directory in Directory.GetDirectories(sourceDir))
                Copy(directory, Path.Combine(targetDir, Path.GetFileName(directory)));
        }

        void RenameThem()
        {
            string fPath = GetCurrentUserPath() + "\\Desktop\\" + CreateFolder() + "\\";
            string fNewName = "spotlight";

            string fExt;
            string fFromName;
            string fToName;
            int i = 1;
     
            //copy all files from fPath to files array
            FileInfo[] files = new DirectoryInfo(fPath).GetFiles();
            //loop through all files
            foreach (var f in files)
            {
                //get the filename without the extension
                fFromName = Path.GetFileNameWithoutExtension(f.Name);
                //get the file extension
                fExt = Path.GetExtension(f.Name);
 
                //set fFromName to the path + name of the existing file
                fFromName = string.Format("{0}{1}", fPath, f.Name);
                //set the fToName as path + new name + _i + file extension
                fToName = string.Format("{0}{1}_{2}{3}.jpg", fPath, fNewName,i.ToString(), fExt);
 
                //rename the file by moving to the same place and renaming
                File.Move(fFromName, fToName);
                //increment i
                i++;
            }
        }

        void DeleteOthers()
        {
            string fPath = GetCurrentUserPath() + "\\Desktop\\" + CreateFolder() + "\\";
            string fExt;
            string fFromName;
            int i = 1;

            //copy all files from fPath to files array
            FileInfo[] files = new DirectoryInfo(fPath).GetFiles();
            //loop through all files
            foreach (var f in files)
            {
                //get the filename without the extension
                fFromName = Path.GetFileNameWithoutExtension(f.Name);
                //get the file extension
                fExt = Path.GetExtension(f.Name);

                Image newImage = Image.FromFile(fPath + f.Name);
                Size eski = newImage.Size;

                newImage.Dispose();

                Size yeni = new Size(1920, 1080);

                if (eski != yeni)
                {
                    if (File.Exists(fPath + f.Name))
                    {
                        File.Delete(fPath + f.Name);
                    }
                }
                i++;
            }
        }
        
        private void btnFetch_Click(object sender, EventArgs e)
        {
            string newPath = CreateFolder();
            //CopyFiles(newPath);
            string path = GetCurrentUserPath();
            // Specify a "currently active folder"
            string activeDir = path + "\\AppData\\Local\\Packages\\Microsoft.Windows.ContentDeliveryManager_cw5n1h2txyewy\\LocalState\\Assets";

            string activeDir2 = path + "\\Desktop\\" + CreateFolder();

            Copy(activeDir, activeDir2);

            RenameThem();

            DeleteOthers();
        }

        private void lblBilgi_Click(object sender, EventArgs e)
        {

        }
        
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void lblAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Windows'un sağladığı Spotlight duvar kağıtlarını .jpg formatında kaydetmeye yarayan araçtır. Windows 10 1607 sürümünde denenmiştir. Kullanmadan önce masaüstünde Spotlight isimli bir klasör olmadığından emin olun...\n\nHamdi Çatal - http://www.hamdicatal.com/", "Hakkında");
        }
    }
}
