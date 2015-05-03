using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientWindowsFormsApplication
{
    /// <summary>
    /// function on remote, known nothing local eg. paths etc.
    /// </summary>
    interface IRemoteData
    {
        /// <summary>
        /// inititalize/create an empty root attempt to send / store
        /// </summary>
        /// <returns>returns true if successful, otherwise false</returns>
        bool Initialize();
        
        /// <summary>
        /// create an empty file, fill with random or zeroed data
        /// </summary>
        /// <param name="name">name of file</param>
        /// <param name="len">length of ile</param>
        /// <param name="random">if true fills with random otherwise zeros</param>
        void CreateEmptyFile(string name, int len, bool random = true);
        
        /// <summary>
        /// create a file at name
        /// </summary>
        /// <param name="name">name including name</param>
        /// <param name="data">file data</param>
        void CreateFile(string name, byte[] data);


        /// <summary>
        /// create a directory at name
        /// </summary>
        /// <param name="name">name including name</param>
        void CreateDirectory(string name);

        /// <summary>
        /// read / download file 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        byte[] Read(string name);

        /// <summary>
        /// delete file
        /// </summary>
        /// <param name="name"></param>
        void Delete(string name);
    }
}
