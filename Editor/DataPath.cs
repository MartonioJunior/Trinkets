using UnityEngine;

namespace MartonioJunior.Trinkets.Editor
{
    public class DataPath
    {
        #region Constants
        const string PackageSubfolder = "Packages/com.martonio.trinkets/";
        #endregion
        #region Static Properties
        public static string Default => GameDataFolder();
        #endregion
        #region Static Methods
        private static string GameDataFolder()
        {
            string dataPath = Application.dataPath;
            return dataPath.Substring(0, Mathf.Max(dataPath.Length - 7, 0)) + "/";
        }

        public static string PackagePath(string relativePath)
        {
            return Default+PackageSubfolder+relativePath;
        }
        #endregion
    }
}