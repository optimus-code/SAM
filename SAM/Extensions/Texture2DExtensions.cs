using System.IO;
using UnityEngine;

namespace SAM.Extensions
{
    /// <summary>
    /// Extension methods for Unity's Texture2D class.
    /// </summary>
    public static class Texture2DExtensions
    {
        /// <summary>
        /// Exports a Texture2D to a file in PNG format.
        /// </summary>
        /// <param name="texture">The Texture2D to export.</param>
        /// <param name="filePath">The file path where the texture should be saved.</param>
        /// <returns>True if the texture was successfully saved, false otherwise.</returns>
        public static bool ExportToPNG( this Texture2D texture, string filePath )
        {
            if ( texture == null )
            {
                Debug.LogError( "ExportToPNG failed: Texture2D is null." );
                return false;
            }

            // Convert the texture to a byte array in PNG format
            var pngData = texture.EncodeToPNG( );

            if ( pngData == null )
            {
                Debug.LogError( "ExportToPNG failed: Error in encoding texture to PNG." );
                return false;
            }

            try
            {
                // Write the byte array to the specified file path
                File.WriteAllBytes( filePath, pngData );
                Debug.Log( $"Texture exported successfully to {filePath}" );
                return true;
            }
            catch ( IOException e )
            {
                // Handle any IO exceptions (like path not found, no permission etc.)
                Debug.LogError( $"ExportToPNG failed: Unable to write file at {filePath}. Error: {e.Message}" );
                return false;
            }
        }
    }

}
