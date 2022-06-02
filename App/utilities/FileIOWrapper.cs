using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.utilities
{
    /// <summary>
    /// Defines the properties, structure / syntax and methods for the file handler wrapper
    /// </summary>
    public interface IFileIOWrapper
    {
        #region Internal Properties
        /// <summary>
        /// Gets or Sets the fully qualified path of the file to be used
        /// </summary>
        string FilePath { get; set; }
        /// <summary>
        /// Gets or Sets the entire text contained in the file to be used
        /// </summary>
        string FileText { get; set; }
        /// <summary>
        /// Gets or Sets the entire contents contained in the file to be used
        /// </summary>
        string[] FileContents { get; set; }
        #endregion Internal Properties

        #region Internal Methods
        /// <summary>
        /// Checks true if a file exists, false if otherwise
        /// </summary>
        /// <returns>bool</returns>
        bool FileExists();
        /// <summary>
        /// Checks true if a file is accessible, false if otherwise
        /// </summary>
        /// <returns>bool</returns>
        bool FileInUse();
        /// <summary>
        /// Opens and Reads all text from a file
        /// </summary>
        /// <returns>string</returns>
        string ReadAllText();
        /// <summary>
        /// Opens and reads each content and/text from a file line by line and returns it as an array
        /// </summary>
        /// <returns>string[]</returns>
        string[] ReadAllLines();
        #endregion Internal Methods
    }
    public class FileIOWrapper: IFileIOWrapper
    {
        #region Internal Properties
        /// <summary>
        /// Gets or Sets the fully qualified path of the file to be used
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// Gets or Sets the entire text contained in the file to be used
        /// </summary>
        public string FileText { get; set; }
        /// <summary>
        /// Gets or Sets the entire contents contained in the file to be used
        /// </summary>
        public string[] FileContents { get; set; }
        #endregion Internal Properties

        #region Constructor(s)
        /// <summary>
        /// Instantiates a new instance of this class
        /// </summary>
        public FileIOWrapper() { }
        /// <summary>
        /// Instantiates a new instance of this class, with the specified path
        /// </summary>
        /// <param name="filePath">the fully qualified path of the file to be processed</param>
        public FileIOWrapper(string filePath) : this()
        {
            FilePath = filePath;
        }
        #endregion Constructor(s)

        #region Internal Methods
        /// <summary>
        /// Checks true if a file exists, false if otherwise
        /// </summary>
        /// <returns>bool</returns>
        public bool FileExists()
        {
            return FilePath.IsSet() && File.Exists(FilePath);
        }
        /// <summary>
        /// Checks true if a file is accessible, false if otherwise
        /// </summary>
        /// <returns>bool</returns>
        public bool FileInUse()
        {
            try
            {
                using (FileStream stream = File.Open(FilePath, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    stream.Close();
                }
            }
            catch (IOException)
            {
                return true;
            }
            return false;

        }
        /// <summary>
        /// Opens and Reads all text from a file
        /// </summary>
        /// <returns>string</returns>
        public string ReadAllText()
        {
            return (FileText = (FileExists() && !FileInUse()) ? File.ReadAllText(FilePath) : null);
        }
        /// <summary>
        /// Opens and reads each content and/text from a file line by line and returns it as an array
        /// </summary>
        /// <returns>string[]</returns>
        public string[] ReadAllLines()
        {
            return (FileContents = (FileExists() && !FileInUse()) ? File.ReadAllLines(FilePath) : null);
        }
        #endregion Internal Methods
    }
}
