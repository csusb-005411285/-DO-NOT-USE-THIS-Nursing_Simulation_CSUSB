using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AWS;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            CredentialManager cred = new CredentialManager(
                "11",
                "11",
                "11");
            S3.UploadFile(@"C:\Users\Ajay Kotian\Music\yourfile.mp3", "NursingProject", "sickMusic");
            Console.WriteLine(CredentialManager.getCredential().GetCredentials().AccessKey);
            Console.ReadKey();
            

        }
    }
}
