using System;
using System.Data;

namespace Codes.Service.Domain
{
    public static class ReadColumn
    {
        public static string GetString(DataRow row, string columnName)
        {
            if (row[columnName] == DBNull.Value)
            {
                return String.Empty;
            }

            return (string)row[columnName];
        }

        public static decimal GetDecimal(DataRow row, string columnName)
        {
            if (row[columnName] == DBNull.Value)
            {
                return 0;
            }

            return (decimal)row[columnName];
        }

        public static double GetDouble(DataRow row, string columnName)
        {
            if (row[columnName] == DBNull.Value)
            {
                return 0;
            }

            return (double)row[columnName];
        }
    }
}
