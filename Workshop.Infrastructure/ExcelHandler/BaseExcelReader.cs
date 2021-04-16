using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NPOI.SS.UserModel;

namespace Workshop.Infrastructure.ExcelHandler
{
    public class BaseExcelReader
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
                    case "formulatedtype`1":
                        var gType = columns[j].PropType.GenericTypeArguments.FirstOrDefault();
                        var tmpFormularted = ExtractDateFromFomular(cell, gType);
                        AssignValue(objType, columns[j].PropName, result, tmpFormularted);
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
                case CellType.Formula:
                    value = cell.CachedFormulaResultType == CellType.Numeric ? cell.NumericCellValue : 0;
                    break;
                case CellType.Blank:
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
                case CellType.Formula:
                    value = (cell.CachedFormulaResultType == CellType.Numeric
                             || cell.CachedFormulaResultType == CellType.String) ? cell.DateCellValue : default;
                    break;
                case CellType.Blank:
                case CellType.Unknown:
                case CellType.Boolean:
                case CellType.Error:
                    value = default;
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
                case CellType.Formula:
                    value = cell.CachedFormulaResultType == CellType.String
                              ? cell.StringCellValue : string.Empty;
                    break;

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

                case CellType.Numeric:
                    value = cell.NumericCellValue.ToString();
                    break;
                case CellType.String:
                    value = cell.StringCellValue;
                    break;
            }
            return value;
        }

        private IFormulatedType ExtractDateFromFomular<T>(ICell cell) where T : struct
        {
            FormulatedType<T> value = default;
            if (cell.CellType == CellType.Formula)
            {
                var TType = typeof(T);
                string colTypeDesc = TType.Name.ToLowerInvariant();
                T realValue;
                switch (colTypeDesc)
                {
                    case "string":
                        string tmpStr = ExtractStringFromCell(cell);
                        realValue = (T)Convert.ChangeType(tmpStr, TType);
                        break;
                    case "datetime":
                        DateTime tmpDt = ExtractDateFromCell(cell);
                        realValue = (T)Convert.ChangeType(tmpDt, TType);
                        break;
                    case "int":
                    case "int32":
                        double tmpInt = ExtractNumericFromCell(cell);
                        realValue = (T)Convert.ChangeType(tmpInt, TType);
                        break;

                    case "decimal":
                        double tmpDecimal = ExtractNumericFromCell(cell);
                        realValue = (T)Convert.ChangeType(tmpDecimal, TType);
                        break;
                    case "int64":
                    case "long":
                        double tmpLong = ExtractNumericFromCell(cell);
                        realValue = (T)Convert.ChangeType(tmpLong, TType);
                        break;

                    case "double":
                        double tmpDb = ExtractNumericFromCell(cell);
                        realValue = (T)Convert.ChangeType(tmpDb, TType);
                        break;
                    case "single":
                        double tmpSingle = ExtractNumericFromCell(cell);
                        realValue = (T)Convert.ChangeType(tmpSingle, TType);
                        break;
                    case "boolean":
                    case "bool":
                        bool tmpBool = ExtractBooleanFromCell(cell);
                        realValue = (T)Convert.ChangeType(tmpBool, TType);
                        break;
                    default:
                        realValue = new T();
                        break;

                }

                value.Value = realValue;
                value.Formula = cell.CellFormula;
            }

            return value;
        }

        private IFormulatedType ExtractDateFromFomular(ICell cell, Type type)
        {
            IFormulatedType value = default;
            if (cell.CellType == CellType.Formula)
            {
                var TType = type;
                string colTypeDesc = TType.Name.ToLowerInvariant();
                switch (colTypeDesc)
                {
                    case "string":
                        string tmpStr = ExtractStringFromCell(cell);
                        value = new FormulatedType<string>();
                        (value as FormulatedType<string>).Value = tmpStr;
                        break;
                    case "datetime":
                        DateTime tmpDt = ExtractDateFromCell(cell);
                        value = new FormulatedType<DateTime>();
                        (value as FormulatedType<DateTime>).Value = tmpDt;
                        break;
                    case "int":
                    case "int32":
                        double tmpInt = ExtractNumericFromCell(cell);
                        value = new FormulatedType<int>();
                        (value as FormulatedType<int>).Value = Convert.ToInt32(tmpInt);
                        break;

                    case "decimal":
                        double tmpDecimal = ExtractNumericFromCell(cell);
                        value = new FormulatedType<decimal>();
                        (value as FormulatedType<decimal>).Value = Convert.ToDecimal(tmpDecimal);
                        break;
                    case "int64":
                    case "long":
                        double tmpLong = ExtractNumericFromCell(cell);
                        value = new FormulatedType<long>();
                        (value as FormulatedType<long>).Value = Convert.ToInt64(tmpLong);
                        break;

                    case "double":
                        double tmpDb = ExtractNumericFromCell(cell);
                        value = new FormulatedType<double>();
                        (value as FormulatedType<double>).Value = tmpDb;
                        break;
                    case "single":
                        double tmpSingle = ExtractNumericFromCell(cell);
                        value = new FormulatedType<float>();
                        (value as FormulatedType<float>).Value = Convert.ToSingle(tmpSingle);
                        break;
                    case "boolean":
                    case "bool":
                        bool tmpBool = ExtractBooleanFromCell(cell);
                        value = new FormulatedType<bool>();
                        (value as FormulatedType<bool>).Value = Convert.ToBoolean(tmpBool);
                        break;
                    default:
                        value = new FormulatedType<int>();
                        break;

                }

                value.Formula = cell.CellFormula;
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
            return columns.OrderBy(x => x.ColumnOrder).ToList();
        }
    }
}
