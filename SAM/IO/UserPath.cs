using System;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace SAM.IO
{
    /// <summary>
    /// Commonly used paths for mod development
    /// </summary>
    public static class UserPath
    {
        /// <summary>
        /// Get the streaming assets path
        /// </summary>
        /// <remarks>
        /// (Points to 'Cities2_Data\StreamingAssets'.)
        /// </remarks>
        public static string StreamingAssets => Application.streamingAssetsPath;

        /// <summary>
        /// Get Data path
        /// </summary>
        /// <remarks>
        /// (On Windows points to 'Cities2_Data' folder.)
        /// </remarks>
        public static string Data => Application.dataPath;

        /// <summary>
        /// Get the library path
        /// </summary>
        /// <remarks>
        /// (Where the SAM library is located.)
        /// </remarks>
        public static string Library => Path.GetDirectoryName( typeof( UserPath ).Assembly.Location );

        /// <summary>
        /// Get the calling executable path
        /// </summary>
        /// <remarks>
        /// (Will typically be the DLL path that is calling this code, e.g. a mod.)
        /// </remarks>
        public static string Caller => Path.GetDirectoryName( Assembly.GetCallingAssembly( ).Location );

        /// <summary>
        /// On Windows, points to the documents path
        /// </summary>
        public static string Documents => Environment.GetFolderPath( Environment.SpecialFolder.MyDocuments );
    }
}
