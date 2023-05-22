namespace Common
{
    public static class Config
    {
        public static string DatabasePath { get; } = "/data/data.db";
        //public static string DatabasePath { get; } = "C:\\University Work\\SearchEngine-Baseline\\EnronMiniSource\\data.db";
        // public static string DatabasePath { get; } = "C:\\University Work\\SearchEngine-Baseline\\EnronMini\\data.db";
         public static string DataSourcePath { get; } = "/data/EnronMini";
         // public static string DataSourcePath { get; } = "C:\\University Work\\SearchEngine-Baseline\\EnronMiniSource";
        public static int NumberOfFoldersToIndex { get; } = 10; // Use 0 or less for indexing all folders
    }
}