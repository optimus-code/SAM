using System;
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

        /// <summary>
        /// Exports a Texture2D to a file in 16-bit RAW format.
        /// </summary>
        /// <param name="texture">The Texture2D to export. Expected to be in a 16-bit format.</param>
        /// <param name="filePath">The file path where the texture should be saved.</param>
        /// <returns>True if the texture was successfully saved, false otherwise.</returns>
        public static bool ExportTo16BitRaw( this Texture2D texture, string filePath )
        {
            if ( texture == null )
            {
                Debug.LogError( "ExportTo16BitRaw failed: Texture2D is null." );
                return false;
            }

            if ( texture.format != TextureFormat.R16 )
            {
                Debug.LogError( $"ExportTo16BitRaw failed: Texture2D is wrong format '{texture.format}' expected R16." );
                return false;
            }

            try
            {
                // Get raw texture data
                var rawData = texture.GetRawTextureData( );

                // Write the raw data to the file
                File.WriteAllBytes( filePath, rawData );

                Debug.Log( $"Texture exported successfully to {filePath} at resolution {texture.width} by {texture.height} format {texture.format}" );
                return true;
            }
            catch ( Exception e )
            {
                Debug.LogError( $"ExportTo16BitRaw failed: Unable to write file at {filePath}. Error: {e.Message}" );
                return false;
            }
        }

        /// <summary>
        /// Loads a 16-bit RAW file into a Texture2D.
        /// </summary>
        /// <param name="texture">The Texture2D to load the data into.</param>
        /// <param name="filePath">The file path of the 16-bit RAW file.</param>
        /// <param name="width">The width of the texture.</param>
        /// <param name="height">The height of the texture.</param>
        /// <returns>True if the texture was successfully loaded, false otherwise.</returns>
        public static bool Load16BitRaw( string filePath, int width, int height )
        {
            var texture = new Texture2D( width, height, TextureFormat.R16, false );

            try
            {
                byte[] rawData = File.ReadAllBytes( filePath );

                if ( rawData.Length != width * height * 2 )
                {
                    Debug.LogError( "Load16BitRaw failed: The size of the RAW file does not match the specified dimensions." );
                    return false;
                }

                texture.LoadRawTextureData( rawData );
                texture.Apply( );

                Debug.Log( "Texture loaded successfully from RAW file." );
                return true;
            }
            catch ( IOException e )
            {
                Debug.LogError( $"Load16BitRaw failed: Unable to read file at {filePath}. Error: {e.Message}" );
                return false;
            }
        }
    }

}
