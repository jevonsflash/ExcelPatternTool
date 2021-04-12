using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using NPOI.XSSF.UserModel;
using System.IO;
using NPOI.SS.UserModel;

namespace SimpleExcelImport
{
    internal class XLSXReader : BaseExcelReader, IExcelReader
    {
        MemoryStream mem;
        private XSSFWorkbook document;
        private ISheet sheet;
        public XLSXReader(byte[] data)
        {
            mem = new MemoryStream(data);
            document = new XSSFWorkbook(mem);
        }

        public XLSXReader(string filePath)
        {
            mem = null;
            document = new XSSFWorkbook(filePath);
        }
        public List<T> ReadRows<T>(int sheetNumber, int rowsToSkip)
        {
            List<T> result = new List<T>();
            var columns = GetTypeDefinition(typeof(T));
            sheet = document.GetSheetAt(sheetNumber);
            int firstRow = sheet.FirstRowNum;
            int lastRow = sheet.LastRowNum;
            for (int i = sheet.FirstRowNum + rowsToSkip; i <= lastRow; i++)
            {
                T objectInstance;
                IRow row = sheet.GetRow(i);
                if (row != null)
                {
                    objectInstance = GetDataToObject<T>(row, columns);
                    result.Add(objectInstance);
                }

            }
            return result;
        }

        private T GetDataToObject<T>(IRow row, List<Column> columns)
        {
            T result = (T)Activator.CreateInstance(typeof(T));
            Type objType = typeof(T);
            for (int j = 0; j < columns.Count; j++)
            {
                ICell cell = row.GetCell(columns[j].ColumnOrder - 1);
                string colTypeDesc = columns[j].PropType.Name.ToLowerInvariant();

                switch (colTypeDesc)
                {
                    case "string":
                        string tmpStr = ExtractStringFromCell(cell);
                        AssignValue(objType, columns[j].PropName, result, (object)tmpStr);
                        break;
                    case "datetime":
                        DateTime tmpDt = ExtractDateFromCell(cell);
                        AssignValue(objType, columns[j].PropName, result, (object)tmpDt);
                        break;
                    case "int":
                    case "int32":
                    case "int64":
                    case "decimal":
                    case "long":
                    case "double":
                    case "single":
                        double tmpDbl = ExtractNumericFromCell(cell);
                        if (colTypeDesc == "float" || colTypeDesc == "single")
                        {
                            AssignValue(objType, columns[j].PropName, result, Convert.ToSingle(tmpDbl));
                        }
                        else if (colTypeDesc == "decimal")
                        {
                            AssignValue(objType, columns[j].PropName, result, Convert.ToDecimal(tmpDbl));
                        }
                        else
                        {
                            AssignValue(objType, columns[j].PropName, result, (object)tmpDbl);
                        }
                        break;
                    case "boolean":
                    case "bool":
                        bool tmpBool = ExtractBooleanFromCell(cell);
                        AssignValue(objType, columns[j].PropName, result, (object)tmpBool);
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
            objType.InvokeMember(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty, Type.DefaultBinder, (object)instance, new object[] { data });
        }
    }
}
