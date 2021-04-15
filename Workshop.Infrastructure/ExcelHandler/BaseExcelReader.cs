using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NPOI.SS.UserModel;

namespace Workshop.Infrastructure.ExcelHandler
{
    internal class BaseExcelReader
    {

        internal T GetDataToObject<T>(IRow row, List<Column> columns)
        {
            T result = (T)Activator.CreateInstance(typeof(T));
            Type objType = typeof(T);
            for (int j = 0; j < columns.Count; j++)
            {
                ICell cell = row.GetCell(columns[j].ColumnOrder);
                string colTypeDesc = columns[j].PropType.Name.ToLowerInvariant();

                switch (colTypeDesc)
                {
                    case "string":
                        string tmpStr = ExtractStringFromCell(cell);
                        AssignValue(objType, columns[j].PropName, result, tmpStr);
                        break;
                    case "datetime":
                        DateTime tmpDt = ExtractDateFromCell(cell);
                        AssignValue(objType, columns[j].PropName, result, tmpDt);
                        break;
                    case "int":
                    case "int32":
                        double tmpInt = ExtractNumericFromCell(cell);
                        AssignValue(objType, columns[j].PropName, result, Convert.ToInt32(tmpInt));
                        break;

                    case "decimal":
                        double tmpDecimal = ExtractNumericFromCell(cell);
                        AssignValue(objType, columns[j].PropName, result, Convert.ToDecimal(tmpDecimal));
                        break;
                    case "int64":
                    case "long":
                        double tmpLong = ExtractNumericFromCell(cell);
                        AssignValue(objType, columns[j].PropName, result, Convert.ToInt64(tmpLong));
                        break;

                    case "double":
                        double tmpDb = ExtractNumericFromCell(cell);
                        AssignValue(objType, columns[j].PropName, result, Convert.ToDouble(tmpDb));
                        break;
                    case "single":
                        double tmpSingle = ExtractNumericFromCell(cell);
                        AssignValue(objType, columns[j].PropName, result, Convert.ToSingle(tmpSingle));
                        break;
                    case "boolean":
                    case "bool":
                        bool tmpBool = ExtractBooleanFromCell(cell);
                        AssignValue(objType, columns[j].PropName, result, Convert.ToBoolean(tmpBool));
                        break;
                    default:
                        double tmpDef = ExtractNumericFromCell(cell);
                        AssignValue(objType, columns[j].PropName, result, (object)tmpDef);
                        break;

                }
            }
            return result;
        }

        private bool ExtractBooleanFromCell(ICell cell)
        {
            Boolean value = false;
            switch (cell.CellType)
            {
                case CellType.Unknown:
                case CellType.Blank:
                case CellType.Error:
                case CellType.Formula:
                    value = false;
                    break;
                case CellType.Boolean:
                    value = cell.BooleanCellValue;
                    break;
                case CellType.Numeric:
                    value = cell.NumericCellValue > 0 ? true : false;
                    break;
                case CellType.String:
                    bool.TryParse(cell.StringCellValue, out value);
                    break;
            }
            return value;
        }

        private double ExtractNumericFromCell(ICell cell)
        {
            double value = 0;
            switch (cell.CellType)
            {
                case CellType.Blank:
                case CellType.Formula:
                case CellType.Unknown:
                    value = 0;
                    break;
                case CellType.Boolean:
                    value = cell.BooleanCellValue ? 1 : 0;
                    break;
                case CellType.Error:
                    value = cell.ErrorCellValue;
                    break;
                case CellType.Numeric:
                    value = cell.NumericCellValue;
                    break;
                case CellType.String:
                    double.TryParse(cell.StringCellValue, out value);
                    break;
            }
            return value;
        }

        private DateTime ExtractDateFromCell(ICell cell)
        {
            DateTime value = DateTime.Now;
            switch (cell.CellType)
            {
                case CellType.Blank:
                case CellType.Unknown:
                case CellType.Boolean:
                case CellType.Error:
                case CellType.Formula:
                    value = DateTime.Now;
                    break;
                case CellType.Numeric:
                    value = cell.DateCellValue;
                    break;
                case CellType.String:
                    value = cell.DateCellValue;
                    break;
            }
            return value;
        }

        private string ExtractStringFromCell(ICell cell)
        {
            string value = string.Empty;
            if (cell == null)
            {
                return string.Empty;
            }
            switch (cell.CellType)
            {
                case CellType.Blank:
                case CellType.Unknown:
                    value = string.Empty;
                    break;
                case CellType.Boolean:
                    value = cell.BooleanCellValue.ToString();
                    break;
                case CellType.Error:
                    value = "Error Code:" + cell.ErrorCellValue.ToString();
                    break;
                case CellType.Formula:
                    value = cell.CellFormula;
                    break;
                case CellType.Numeric:
                    value = cell.NumericCellValue.ToString();
                    break;
                case CellType.String:
                    value = cell.StringCellValue;
                    break;
            }
            return value;
        }

        private void AssignValue(Type objType, string propertyName, object instance, object data)
        {
            objType.InvokeMember(propertyName,
                BindingFlags.DeclaredOnly |
                BindingFlags.Public | BindingFlags.NonPublic |
                BindingFlags.Instance | BindingFlags.SetProperty, null, instance, new Object[] { data });
        }

        internal List<Column> GetTypeDefinition(Type type)
        {
            List<Column> columns = new List<Column>();
            foreach (var prop in type.GetProperties())
            {
                var tmp = new Column();
                var attrs = System.Attribute.GetCustomAttributes(prop);
                tmp.PropName = prop.Name;
                tmp.PropType = prop.PropertyType;
                tmp.ColumnName = prop.Name;
                tmp.ColumnOrder = int.MaxValue;
                foreach (var attr in attrs)
                {
                    if (attr is ExcelImport)
                    {
                        ExcelImport attribute = (ExcelImport)attr;
                        tmp.ColumnName = attribute.GetName();
                        tmp.ColumnOrder = attribute.order;
                        tmp.Ignore = attribute.ignore;
                    }
                }
                if (!tmp.Ignore)
                {
                    columns.Add(tmp);
                }
            }
            return columns.OrderBy(x=>x.ColumnOrder).ToList();
        }
    }
}
