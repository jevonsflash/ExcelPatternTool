using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Workshop.Infrastructure.Attributes;
using Workshop.Infrastructure.Models;

namespace Workshop.Infrastructure.Core
{
    public class BaseWriter
    {
        private readonly IWorkbook _workbook;
        public IWorkbook Document { get; set; }

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
            ICellStyle cellStyle = this.Document.CreateCellStyle();
            cellStyle.CloneStyleFrom(baseStyle);

            if (string.IsNullOrWhiteSpace(dataFormat))
                return cellStyle;


            IDataFormat dataFormat2 = this.Document.CreateDataFormat();
            cellStyle.DataFormat = dataFormat2.GetFormat(dataFormat);
            return cellStyle;



        }

        internal IFont CreateFont(StyleMetadata rowStyle)
        {
            IColor fontColor = StyleBuilderProvider.GetStyleBuilder(this.Document).GetColor(rowStyle.FontColor);
            return StyleBuilderProvider.GetStyleBuilder(this.Document).GetFont(rowStyle.FontSize, rowStyle.FontName, fontColor);
        }



        public  StyleMetadata GetClassStyle(Type type)
        {
            StyleMetadata styleMetadata;
            var defaultFontName = "Calibry";
            var defaultFontColor = "#FFFFFF";
            short defaultFontSize = 11;
            var defaultBorderColor = "#000000";
            var defaultBackColor = "#888888";
            foreach (var ct in type.GetCustomAttributes())
            {
                if (ct != null && ct.GetType() == typeof(StyleAttribute))
                {
                    StyleAttribute exportableAttribute = (ct as StyleAttribute);
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

        internal ICellStyle CreateCellStyle(StyleMetadata rowStyle)
        {
            IColor borderColor = StyleBuilderProvider.GetStyleBuilder(this.Document).GetColor(rowStyle.BorderColor);
            IColor backColor = StyleBuilderProvider.GetStyleBuilder(this.Document).GetColor(rowStyle.BackColor);
            IFont font = this.CreateFont(rowStyle);
            ICellStyle cellStyle;
            var styleBuilder = StyleBuilderProvider.GetStyleBuilder(this.Document);
            cellStyle = styleBuilder.GetCellStyle(backColor, borderColor, font);

            return cellStyle;
        }
        internal void SetDataToRow<T>(int rowsToSkip, bool genHeader, IEnumerable<ColumnMetadata> columnMetas, IRow fila, T data, int row)
        {
            //cells
            int cellCount = 0;

            foreach (var columnMeta in columnMetas)
            {
                var currentCell = fila.CreateCell(cellCount);

                var propValue = data.GetType().GetProperty(columnMeta.PropName).GetValue(data, null);
                var propType = data.GetType().GetProperty(columnMeta.PropName).PropertyType;


                //代表第一行
                if (row - rowsToSkip == 0)
                {
                    if (genHeader)
                    {
                        var headerFormat = GetClassStyle(typeof(T));
                        ICellStyle cell = CreateCellStyle(headerFormat);
                        fila.GetCell(cellCount).CellStyle = cell;
                        fila.GetCell(cellCount).SetCellValue(columnMeta.ColumnName);
                    }
                }
                else
                {
                    if (propValue == null)
                    {
                        fila.GetCell(cellCount).SetCellValue("");
                        cellCount++;
                        continue;
                    }

                    var currentStyles = GetStyleWithFormat(currentCell.CellStyle, columnMeta.Format);

                    if (string.IsNullOrEmpty(columnMeta.FieldValueType))
                    {
                        string colTypeDesc = propType.Name.ToLowerInvariant();
                        switch (colTypeDesc)
                        {
                            case "string":
                                var strCellValue = Convert.ToString(propValue);
                                fila.GetCell(cellCount).SetCellValue(strCellValue);
                                break;
                            case "datetime":
                                var dateCellValue = Convert.ToDateTime(propValue);
                                fila.GetCell(cellCount).SetCellValue(dateCellValue);
                                fila.GetCell(cellCount).CellStyle = currentStyles;
                                break;
                            case "int":
                            case "int32":
                            case "decimal":
                            case "int64":
                            case "long":
                            case "double":
                            case "single":
                                var numCellValue = Convert.ToDouble(propValue);
                                fila.GetCell(cellCount).SetCellValue(numCellValue);
                                fila.GetCell(cellCount).CellStyle = currentStyles;
                                fila.GetCell(cellCount).SetCellType(CellType.Numeric);
                                break;
                            case "boolean":
                            case "bool":
                                var cellValue = Convert.ToBoolean(propValue);
                                fila.GetCell(cellCount).SetCellValue(cellValue);
                                break;
                            default:
                                var anyCellValue = Convert.ToString(propValue);
                                fila.GetCell(cellCount).SetCellValue(anyCellValue);
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
                                fila.GetCell(cellCount).SetCellValue(dateCellValue);
                                fila.GetCell(cellCount).CellStyle = currentStyles;
                                break;

                            case FieldValueType.Numeric:
                                var numericCellValue = Convert.ToDouble(propValue);
                                fila.GetCell(cellCount).SetCellValue(numericCellValue);
                                fila.GetCell(cellCount).CellStyle = currentStyles;
                                fila.GetCell(cellCount).SetCellType(CellType.Numeric);
                                break;

                            case FieldValueType.Text:
                                var stringCellValue = propValue.ToString();
                                fila.GetCell(cellCount).SetCellValue(stringCellValue);
                                fila.GetCell(cellCount).SetCellType(CellType.String);
                                break;

                            case FieldValueType.Bool:
                                var boolCellValue = Convert.ToBoolean(propValue);
                                fila.GetCell(cellCount).SetCellValue(boolCellValue);
                                fila.GetCell(cellCount).SetCellType(CellType.Boolean);
                                break;

                            default:
                                var anyCellValue = Convert.ToString(propValue);
                                fila.GetCell(cellCount).SetCellValue(anyCellValue);
                                break;
                        }
                    }

                }
                cellCount += 1;

            }
        }

    }
}
