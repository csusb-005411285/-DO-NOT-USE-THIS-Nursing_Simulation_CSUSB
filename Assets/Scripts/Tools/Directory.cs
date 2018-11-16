namespace Tools
{
    /// <summary>
    /// Static class for finding local project directory on any machine
    /// </summary>
    public static class Directory
    {
        private static string homeDirectory;
        private static string tempDirectory;

        static Directory()
        {
            homeDirectory = System.IO.Directory.GetCurrentDirectory() + @"\";
            tempDirectory = homeDirectory + @"\Temp\";  //if you change this directory make sure to add it to the .gitignore
            System.IO.Directory.CreateDirectory(tempDirectory);
        }

        /// <summary>
        /// returns root directory. DO NOT DROP FILES HERE, USE GetTempDirectory()
        /// </summary>
        /// <returns>String of root project directory where the .sln or built .exe reside)</returns>
        private static string GetHomeDirectory() //only use this if you promise not to bloat the repo
        { 
            return homeDirectory;   //I'm serious, be careful with this
        }

        /// <summary>
        /// returns location of 'Temp' Directory in the format: C:\Folder\ProjectFolder\Temp\
        /// </summary>
        /// <returns>String of 'Temp' folder within the project directory</returns>
        public static string GetTempDirectory() //files in this folder are ignored by git
        {
            return tempDirectory;
        }
    }
}
