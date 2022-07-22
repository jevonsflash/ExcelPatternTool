using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Workshop.Core.Excel.Attributes;
using Workshop.Core.Excel.Core.AdvancedTypes;
using Workshop.Core.Excel.Models;
using Workshop.Core.Excel.Models.Interfaces;

namespace Workshop.Core.Excel.Core
{
    public class BaseReader : BaseHandler
    {

        internal T GetDataToObject<T>(IRow row, List<ColumnMetadata> columns) where T : IExcelEntity
        {
            T result = (T)Activator.CreateInstance(typeof(T));
            Type objType = typeof(T);
            for (int j = 0; j < columns.Count; j++)
            {
                ICell cell = row.GetCell(columns[j].ColumnOrder);
                if (cell==null)
                {
                    Console.WriteLine($"第 {row.RowNum} 行， 第 {columns[j].ColumnOrder} 列  不符合MetaData定义规范，跳过");
                    continue;
                }
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
                        var tmpFormularted = ExtractAdvancedFromCell(cell, gType, typeof(FormulatedType<>));
                        if (cell.CellType != CellType.Formula)
                        {
                            switch (colTypeDesc)
                            {
                                case "string":
                                    tmpFormularted.SetValue(ExtractStringFromCell(cell));
                                    break;
                                case "datetime":
                                    tmpFormularted.SetValue(ExtractStringFromCell(cell));
                                    break;
                                case "int":
                                case "int32":
                                    tmpFormularted.SetValue(ExtractNumericFromCell(cell));
                                    break;

                                case "decimal":
                                    tmpFormularted.SetValue(ExtractNumericFromCell(cell));
                                    break;
                                case "int64":
                                case "long":
                                    tmpFormularted.SetValue(ExtractNumericFromCell(cell));
                                    break;

                                case "double":
                                    tmpFormularted.SetValue(ExtractNumericFromCell(cell));
                                    break;
                                case "single":
                                    tmpFormularted.SetValue(ExtractNumericFromCell(cell));
                                    break;
                                case "boolean":
                                case "bool":
                                    tmpFormularted.SetValue(ExtractBooleanFromCell(cell));
                                    break;
                            }
                        }
                        else
                        {
                            (tmpFormularted as IFormulatedType).Formula = cell.CellFormula;
                        }

                        AssignValue(objType, columns[j].PropName, result, tmpFormularted);
                        break;

                    case "commentedtype`1":
                        var commentedType = columns[j].PropType.GenericTypeArguments.FirstOrDefault();
                        var tmpCommented = ExtractAdvancedFromCell(cell, commentedType, typeof(CommentedType<>));
                        if (cell.CellComment != null)
                        {
                            (tmpCommented as ICommentedType).Comment = cell.CellComment.String.String;
                        }

                        AssignValue(objType, columns[j].PropName, result, tmpCommented);
                        break;

                    case "styledtype`1":
                        var styledType = columns[j].PropType.GenericTypeArguments.FirstOrDefault();
                        var tmpStyled = ExtractAdvancedFromCell(cell, styledType, typeof(StyledType<>));
                        if (cell.CellComment != null)
                        {
                            (tmpStyled as IStyledType).Comment = cell.CellComment.String.String;
                        }

                        (tmpStyled as IStyledType).StyleMetadata = CellStyleToMeta(cell.CellStyle);
                        AssignValue(objType, columns[j].PropName, result, tmpStyled);
                        break;

                    case "fulladvancedtype`1":
                        var fullarmedType = columns[j].PropType.GenericTypeArguments.FirstOrDefault();
                        var tmpFullarmed = ExtractAdvancedFromCell(cell, fullarmedType, typeof(FullAdvancedType<>));
                        if (cell.CellType != CellType.Formula)
                        {
                            switch (colTypeDesc)
                            {
                                case "string":
                                    tmpFullarmed.SetValue(ExtractStringFromCell(cell));
                                    break;
                                case "datetime":
                                    tmpFullarmed.SetValue(ExtractStringFromCell(cell));
                                    break;
                                case "int":
                                case "int32":
                                    tmpFullarmed.SetValue(ExtractNumericFromCell(cell));
                                    break;

                                case "decimal":
                                    tmpFullarmed.SetValue(ExtractNumericFromCell(cell));
                                    break;
                                case "int64":
                                case "long":
                                    tmpFullarmed.SetValue(ExtractNumericFromCell(cell));
                                    break;

                                case "double":
                                    tmpFullarmed.SetValue(ExtractNumericFromCell(cell));
                                    break;
                                case "single":
                                    tmpFullarmed.SetValue(ExtractNumericFromCell(cell));
                                    break;
                                case "boolean":
                                case "bool":
                                    tmpFullarmed.SetValue(ExtractBooleanFromCell(cell));
                                    break;
                            }

                        }
                        else
                        {
                            (tmpFullarmed as IFullAdvancedType).Formula = cell.CellFormula;
                            if (cell.CellComment != null)
                            {
                                (tmpFullarmed as IFullAdvancedType).Comment = cell.CellComment.String.String;
                            }

                        }
                        (tmpFullarmed as IFullAdvancedType).StyleMetadata = CellStyleToMeta(cell.CellStyle);

                        AssignValue(objType, columns[j].PropName, result, tmpFullarmed);
                        break;
                    default:
                        double tmpDef = ExtractNumericFromCell(cell);
                        AssignValue(objType, columns[j].PropName, result, tmpDef);
                        break;

                }
            }

            AssignValue(objType, "RowNumber", result, row.RowNum);

            return result;
        }


        private bool ExtractBooleanFromCell(ICell cell)
        {
            bool value = false;
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
                    value = cell.CachedFormulaResultType == CellType.Numeric
                             || cell.CachedFormulaResultType == CellType.String ? cell.DateCellValue : default;
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

        private IAdvancedType ExtractDateFromFomular<T>(ICell cell, Type iType) where T : struct
        {
            var value = IAdvancedTypeFactory(iType, typeof(T));
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

                value.SetValue(realValue);
            }

            return value;
        }


        private IAdvancedType IAdvancedTypeFactory(Type iType, Type GenericType)
        {
            var type = iType.MakeGenericType(GenericType);
            IAdvancedType result = Activator.CreateInstance(type) as IAdvancedType;
            return result;
        }

        private IAdvancedType ExtractAdvancedFromCell(ICell cell, Type type, Type iType)
        {
            var value = IAdvancedTypeFactory(iType, type);

            var TType = type;
            string colTypeDesc = TType.Name.ToLowerInvariant();
            switch (colTypeDesc)
            {
                case "string":
                    string tmpStr = ExtractStringFromCell(cell);
                    value.SetValue(tmpStr);
                    break;
                case "datetime":
                    DateTime tmpDt = ExtractDateFromCell(cell);
                    value.SetValue(tmpDt);
                    break;
                case "int":
                case "int32":
                    double tmpInt = ExtractNumericFromCell(cell);
                    value.SetValue(Convert.ToInt32(tmpInt));
                    break;

                case "decimal":
                    double tmpDecimal = ExtractNumericFromCell(cell);
                    value.SetValue(Convert.ToDecimal(tmpDecimal));
                    break;
                case "int64":
                case "long":
                    double tmpLong = ExtractNumericFromCell(cell);
                    value.SetValue(Convert.ToInt64(tmpLong));
                    break;

                case "double":
                    double tmpDb = ExtractNumericFromCell(cell);
                    value.SetValue(tmpDb);
                    break;
                case "single":
                    double tmpSingle = ExtractNumericFromCell(cell);
                    value.SetValue(Convert.ToSingle(tmpSingle));
                    break;
                case "boolean":
                case "bool":
                    bool tmpBool = ExtractBooleanFromCell(cell);
                    value.SetValue(Convert.ToBoolean(tmpBool));
                    break;
                default:
                    value = new FormulatedType<int>();
                    break;
            }
            return value;
        }


        private void AssignValue(Type objType, string propertyName, object instance, object data)
        {
            objType.InvokeMember(propertyName,
                BindingFlags.DeclaredOnly |
                BindingFlags.Public | BindingFlags.NonPublic |
                BindingFlags.Instance | BindingFlags.SetProperty, null, instance, new object[] { data });
        }

        internal List<ColumnMetadata> GetTypeDefinition(Type type)
        {
            List<ColumnMetadata> columns = new List<ColumnMetadata>();
            foreach (var prop in type.GetProperties())
            {
                var tmp = new ColumnMetadata();
                var attrs = Attribute.GetCustomAttributes(prop);
                tmp.PropName = prop.Name;
                tmp.PropType = prop.PropertyType;
                tmp.ColumnName = prop.Name;
                tmp.ColumnOrder = int.MaxValue;
                foreach (var attr in attrs)
                {
                    if (attr is ImportableAttribute)
                    {
                        ImportableAttribute attribute = (ImportableAttribute)attr;
                        tmp.ColumnName = attribute.Name;
                        tmp.ColumnOrder = attribute.Order;
                        tmp.Ignore = attribute.Ignore;
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
