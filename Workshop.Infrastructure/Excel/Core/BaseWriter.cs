﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.Configuration;
using NPOI.SS.UserModel;
using Workshop.Infrastructure.Attributes;
using Workshop.Infrastructure.Models;
using Workshop.Infrastructure.Services;

namespace Workshop.Infrastructure.Core
{
    public class BaseWriter : BaseHandler
    {
        internal IEnumerable<ColumnMetadata> GetTypeDefinition(Type type)
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
                tmp.DefaultForNullOrInvalidValues = string.Empty;

                foreach (var attr in attrs)
                {
                    if (attr is ExportableAttribute)
                    {
                        ExportableAttribute attribute = (ExportableAttribute)attr;
                        tmp.ColumnName = attribute.Name;
                        tmp.ColumnOrder = attribute.Order;
                        tmp.Format = attribute.Format;
                        tmp.FieldValueType = attribute.Type;
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

        internal ICellStyle GetStyleWithFormat(ICellStyle baseStyle, string dataFormat)
        {
            var cellStyle = this.Document.CreateCellStyle();
            cellStyle.CloneStyleFrom(baseStyle);

            if (string.IsNullOrWhiteSpace(dataFormat))
                return cellStyle;

            short builtIndDataFormat = StyleBuilderProvider.GetStyleBuilder(this.Document).GetBuiltIndDataFormat(dataFormat);
            if (builtIndDataFormat != -1)
            {
                cellStyle.DataFormat = builtIndDataFormat;
            }
            else
            {
                IDataFormat dataFormat2 = this.Document.CreateDataFormat();
                cellStyle.DataFormat = dataFormat2.GetFormat(dataFormat);
            }
            return cellStyle;



        }

        public StyleMetadata GetClassStyleDefinition(Type type)
        {
            StyleMetadata styleMetadata;
            var defaultFontName = AppConfigurtaionService.Configuration["HeaderDefaultStyle:DefaultFontName"];
            var defaultFontColor = AppConfigurtaionService.Configuration["HeaderDefaultStyle:DefaultFontColor"];
            short defaultFontSize = Convert.ToInt16(AppConfigurtaionService.Configuration["HeaderDefaultStyle:DefaultFontSize"]);
            var defaultBorderColor = AppConfigurtaionService.Configuration["HeaderDefaultStyle:DefaultBorderColor"];
            var defaultBackColor = AppConfigurtaionService.Configuration["HeaderDefaultStyle:DefaultBackColor"];
            foreach (var attr in type.GetCustomAttributes())
            {
                if (attr is StyleAttribute)
                {
                    StyleAttribute exportableAttribute = (attr as StyleAttribute);
                    styleMetadata = new StyleMetadata();

                    if (!string.IsNullOrWhiteSpace(exportableAttribute.FontName))
                        styleMetadata.FontName = exportableAttribute.FontName;
                    else
                        styleMetadata.FontName = defaultFontName;
                    if (!string.IsNullOrWhiteSpace(exportableAttribute.FontColor))
                        styleMetadata.FontColor = exportableAttribute.FontColor;
                    else
                        styleMetadata.FontColor = defaultFontColor;
                    if ((exportableAttribute.FontSize) > 0)
                        styleMetadata.FontSize = exportableAttribute.FontSize;
                    else
                        styleMetadata.FontSize = defaultFontSize;
                    if (!string.IsNullOrWhiteSpace(exportableAttribute.BorderColor))
                        styleMetadata.BorderColor = exportableAttribute.BorderColor;
                    else
                        styleMetadata.BorderColor = defaultBorderColor;
                    if (!string.IsNullOrWhiteSpace(exportableAttribute.BackColor))
                        styleMetadata.BackColor = exportableAttribute.BackColor;
                    else
                        styleMetadata.BackColor = defaultBackColor;
                    return styleMetadata;
                }

            }

            return new StyleMetadata(defaultFontColor, defaultFontName, defaultFontSize, defaultBorderColor, defaultBackColor);
        }

        public StyleMetadata GetPropStyleDefinition(PropertyInfo prop)
        {
            StyleMetadata styleMetadata;
            var defaultFontName = AppConfigurtaionService.Configuration["BodyDefaultStyle:DefaultFontName"];
            var defaultFontColor = AppConfigurtaionService.Configuration["BodyDefaultStyle:DefaultFontColor"];
            short defaultFontSize = Convert.ToInt16(AppConfigurtaionService.Configuration["BodyDefaultStyle:DefaultFontSize"]);
            var defaultBorderColor = AppConfigurtaionService.Configuration["BodyDefaultStyle:DefaultBorderColor"];
            var defaultBackColor = AppConfigurtaionService.Configuration["BodyDefaultStyle:DefaultBackColor"];

            var attrs = Attribute.GetCustomAttributes(prop);

            foreach (var attr in attrs)
            {
                if (attr is StyleAttribute)
                {
                    var exportableAttribute = (attr as StyleAttribute);
                    styleMetadata = new StyleMetadata();

                    if (!string.IsNullOrWhiteSpace(exportableAttribute.FontName))
                        styleMetadata.FontName = exportableAttribute.FontName;
                    else
                        styleMetadata.FontName = defaultFontName;
                    if (!string.IsNullOrWhiteSpace(exportableAttribute.FontColor))
                        styleMetadata.FontColor = exportableAttribute.FontColor;
                    else
                        styleMetadata.FontColor = defaultFontColor;
                    if ((exportableAttribute.FontSize) > 0)
                        styleMetadata.FontSize = exportableAttribute.FontSize;
                    else
                        styleMetadata.FontSize = defaultFontSize;
                    if (!string.IsNullOrWhiteSpace(exportableAttribute.BorderColor))
                        styleMetadata.BorderColor = exportableAttribute.BorderColor;
                    else
                        styleMetadata.BorderColor = defaultBorderColor;
                    if (!string.IsNullOrWhiteSpace(exportableAttribute.BackColor))
                        styleMetadata.BackColor = exportableAttribute.BackColor;
                    else
                        styleMetadata.BackColor = defaultBackColor;
                    return styleMetadata;
                }
            }

            return new StyleMetadata(defaultFontColor, defaultFontName, defaultFontSize, defaultBorderColor, defaultBackColor);
        }

        internal void SetHeaderToRow<T>(int rowsToSkip, IEnumerable<ColumnMetadata> columnMetas, IRow row,
             int rowCount)
        {
            //cells
            int cellCount = 0;
            if (rowCount - rowsToSkip == 0)
            {
                foreach (var columnMeta in columnMetas)
                {
                    var cell = row.CreateCell(cellCount);

                    var headerFormat = GetClassStyleDefinition(typeof(T));
                    ICellStyle headerCellStyle = MetaToCellStyle(headerFormat);
                    cell.CellStyle.CloneStyleFrom(headerCellStyle);
                    cell.SetCellValue(columnMeta.ColumnName);
                    cellCount += 1;
                    StyleBuilderProvider.DisposeCurrent();
                }
            }
        }

        internal void SetDataToRow<T>(IEnumerable<ColumnMetadata> columnMetas, IRow row, T data)
        {
            var ws2 = Stopwatch.StartNew();
            
            //cells
            int cellCount = 0;

            foreach (var columnMeta in columnMetas)
            {
                ws2.Start();
                var cell = row.CreateCell(cellCount);

                var propertyInfo = data.GetType().GetProperty(columnMeta.PropName);
                var propValue = propertyInfo.GetValue(data, null);
                var propType = propertyInfo.PropertyType;
                ws2.Stop();
                var stage1span = ws2.ElapsedMilliseconds;
                ws2.Restart();
                if (propValue == null)
                {
                    cell.SetCellValue("");
                    cellCount++;
                    continue;
                }


                var bodyFormat = GetPropStyleDefinition(propertyInfo);
                ws2.Stop();
                var stage2_1span = ws2.ElapsedMilliseconds;
                ws2.Restart();
                var bodyBaseCellStyle = MetaToCellStyle(bodyFormat);
                ws2.Stop();
                var stage2_2span = ws2.ElapsedMilliseconds;
                ws2.Restart();
                var bodyCellStyle = GetStyleWithFormat(bodyBaseCellStyle, columnMeta.Format);
                ws2.Stop();
                var stage2_3span = ws2.ElapsedMilliseconds;
                ws2.Restart();

                if (string.IsNullOrEmpty(columnMeta.FieldValueType))
                {
                    string colTypeDesc = propType.Name.ToLowerInvariant();
                    switch (colTypeDesc)
                    {
                        case "string":
                            var strCellValue = Convert.ToString(propValue);
                            cell.SetCellValue(strCellValue);
                            cell.CellStyle = bodyBaseCellStyle;
                            break;
                        case "datetime":
                            var dateCellValue = Convert.ToDateTime(propValue);
                            cell.SetCellValue(dateCellValue);
                            cell.CellStyle = bodyCellStyle;
                            break;
                        case "int":
                        case "int32":
                        case "decimal":
                        case "int64":
                        case "long":
                        case "double":
                        case "single":
                            var numCellValue = Convert.ToDouble(propValue);
                            cell.SetCellValue(numCellValue);
                            cell.CellStyle = bodyCellStyle;
                            cell.SetCellType(CellType.Numeric);
                            break;
                        case "boolean":
                        case "bool":
                            var cellValue = Convert.ToBoolean(propValue);
                            cell.SetCellValue(cellValue);
                            cell.CellStyle = bodyBaseCellStyle;
                            break;
                        case "formulatedtype`1":
                        case "styledtype`1":
                        case "commentedtype`1":
                        case "fulladvancedtype`1":
                            HandleAdvancedType(cell, propValue as IAdvancedType, propType, bodyFormat, columnMeta);
                            break;
                        default:
                            var anyCellValue = Convert.ToString(propValue);
                            cell.SetCellValue(anyCellValue);
                            cell.CellStyle = bodyBaseCellStyle;
                            break;
                    }
                }
                else
                {
                    var valueType = (FieldValueType)Enum.Parse(typeof(FieldValueType), columnMeta.FieldValueType);

                    switch (valueType)
                    {
                        case FieldValueType.Date:
                            var dateCellValue = Convert.ToDateTime(propValue);
                            cell.SetCellValue(dateCellValue);
                            cell.CellStyle = bodyCellStyle;
                            break;

                        case FieldValueType.Numeric:
                            var numericCellValue = Convert.ToDouble(propValue);
                            cell.SetCellValue(numericCellValue);
                            cell.CellStyle = bodyCellStyle;
                            cell.SetCellType(CellType.Numeric);
                            break;

                        case FieldValueType.Text:
                            var stringCellValue = propValue.ToString();
                            cell.SetCellValue(stringCellValue);
                            cell.CellStyle = bodyBaseCellStyle;
                            cell.SetCellType(CellType.String);
                            break;

                        case FieldValueType.Bool:
                            var boolCellValue = Convert.ToBoolean(propValue);
                            cell.SetCellValue(boolCellValue);
                            cell.CellStyle = bodyBaseCellStyle;
                            cell.SetCellType(CellType.Boolean);
                            break;

                        default:
                            var anyCellValue = Convert.ToString(propValue);
                            cell.SetCellValue(anyCellValue);
                            cell.CellStyle = bodyBaseCellStyle;
                            break;
                    }
                }

                ws2.Stop();
                var stage3span = ws2.ElapsedMilliseconds;

                cellCount += 1;

                StyleBuilderProvider.DisposeCurrent();
                Debug.WriteLine($"当前列循环{columnMeta.PropName}耗时{stage1span+stage2_1span + stage2_2span + stage2_3span + stage3span}ms|stage1:{stage1span}|stage2.1:{stage2_1span}|stage2.2:{stage2_2span}|stage2.3:{stage2_3span}|stage3:{stage3span}");
                ws2.Reset();
            }

        }

        private void HandleAdvancedType(ICell cell, IAdvancedType propValue, Type propType, StyleMetadata bodyFormat, ColumnMetadata columnMeta)
        {
            var bodyBaseCellStyle = MetaToCellStyle(bodyFormat);

            var bodyCellStyle = GetStyleWithFormat(bodyBaseCellStyle, columnMeta.Format);


            var gType = propType.GenericTypeArguments.FirstOrDefault();
            string gTypeDesc = gType.Name.ToLowerInvariant();

            var formulatedValue = propValue.GetValue();


            if (gTypeDesc == "string")
            {
                cell.SetCellValue(Convert.ToString(formulatedValue));
                cell.CellStyle = bodyBaseCellStyle;
                cell.SetCellType(CellType.String);

            }
            else if (gTypeDesc == "datetime")
            {
                cell.SetCellValue(Convert.ToDateTime(formulatedValue));
                cell.CellStyle = bodyCellStyle;
            }
            else if (gTypeDesc == "int" || gTypeDesc == "int32" || gTypeDesc == "decimal" ||
                     gTypeDesc == "int64" || gTypeDesc == "long" || gTypeDesc == "double" ||
                     gTypeDesc == "single")
            {
                cell.SetCellValue(Convert.ToDouble(formulatedValue));
                cell.CellStyle = bodyCellStyle;
                cell.SetCellType(CellType.Numeric);

            }
            else if (gTypeDesc == "boolean" || gTypeDesc == "bool")
            {
                cell.SetCellValue(Convert.ToBoolean(formulatedValue));
                cell.CellStyle = bodyBaseCellStyle;
                cell.SetCellType(CellType.Boolean);

            }

            if (propValue is IFormulatedType)
            {
                var formula = (propValue as IFormulatedType).Formula;

                cell.SetCellFormula(formula);
                cell.SetCellType(CellType.Formula);
                SetFormulatedValue(cell, formulatedValue);

            }
            else if (propValue is ICommentedType)
            {
                var comment = (propValue as ICommentedType).Comment;
                if (!string.IsNullOrEmpty(comment))
                {
                    cell.CellComment = StyleBuilderProvider.GetStyleBuilder(Document).GetComment(comment);
                }
                SetFormulatedValue(cell, formulatedValue);

            }

            else if (propValue is IStyledType)
            {

                var styleMeta = (propValue as IStyledType).StyleMetadata;
                if (styleMeta != null)
                {
                    if (string.IsNullOrEmpty(styleMeta.BackColor))
                    {
                        styleMeta.BackColor = bodyFormat.BackColor;
                    }
                    if (string.IsNullOrEmpty(styleMeta.BorderColor))
                    {
                        styleMeta.BorderColor = bodyFormat.BorderColor;
                    }
                    if (string.IsNullOrEmpty(styleMeta.FontColor))
                    {
                        styleMeta.FontColor = bodyFormat.FontColor;
                    }
                    if (string.IsNullOrEmpty(styleMeta.FontName))
                    {
                        styleMeta.FontName = bodyFormat.FontName;
                    }                

                    var specificCellStyle = MetaToCellStyle(styleMeta);

                    var specificBodyCellStyle = GetStyleWithFormat(specificCellStyle, columnMeta.Format);
                    cell.CellStyle = specificBodyCellStyle;
                }
                else
                {
                    cell.CellStyle = bodyCellStyle;

                }


                var comment = (propValue as IStyledType).Comment;
                if (!string.IsNullOrEmpty(comment))
                {
                    cell.CellComment = StyleBuilderProvider.GetStyleBuilder(Document).GetComment(comment);
                }
                SetFormulatedValue(cell, formulatedValue);

            }

            else if (propValue is IFullAdvancedType)
            {
                var formula = (propValue as IFormulatedType).Formula;

                cell.SetCellFormula(formula);
                cell.SetCellType(CellType.Formula);

                var styleMeta = (propValue as IFullAdvancedType).StyleMetadata;
                if (styleMeta != null)
                {
                    if (string.IsNullOrEmpty(styleMeta.BackColor))
                    {
                        styleMeta.BackColor = bodyFormat.BackColor;
                    }
                    if (string.IsNullOrEmpty(styleMeta.BorderColor))
                    {
                        styleMeta.BorderColor = bodyFormat.BorderColor;
                    }
                    if (string.IsNullOrEmpty(styleMeta.FontColor))
                    {
                        styleMeta.FontColor = bodyFormat.FontColor;
                    }
                    if (string.IsNullOrEmpty(styleMeta.FontName))
                    {
                        styleMeta.FontName = bodyFormat.FontName;
                    }
                    var specificCellStyle = MetaToCellStyle(styleMeta);

                    var specificBodyCellStyle = GetStyleWithFormat(specificCellStyle, columnMeta.Format);
                    cell.CellStyle = specificBodyCellStyle;
                }
                else
                {
                    cell.CellStyle = bodyCellStyle;

                }

                var comment = (propValue as IFullAdvancedType).Comment;
                if (!string.IsNullOrEmpty(comment))
                {
                    cell.CellComment = StyleBuilderProvider.GetStyleBuilder(Document).GetComment(comment);
                }

            }


        }

        private static void SetFormulatedValue(ICell cell, object formulatedValue)
        {
            if (formulatedValue is string)
            {
                cell.SetCellValue(formulatedValue as string);

            }
            else if (formulatedValue is DateTime)
            {
                cell.SetCellValue((DateTime)formulatedValue);

            }
            else if (formulatedValue is bool)
            {
                cell.SetCellValue((bool)formulatedValue);

            }
            else if (formulatedValue is int || formulatedValue is decimal ||
                 formulatedValue is long || formulatedValue is double || formulatedValue is float)
            {
                cell.SetCellValue((double)formulatedValue);

            }
        }
    }
}
