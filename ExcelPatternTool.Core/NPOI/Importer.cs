﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ExcelPatternTool.Contracts;
using ExcelPatternTool.Core.EntityProxy;

namespace ExcelPatternTool.Core.NPOI
{

    public class Importer
    {
        IReader excelReader;

        public bool LoadXls(byte[] data)
        {
            excelReader = new XlsReader(data);
            return true;
        }
        public bool LoadXlsx(byte[] data)
        {
            excelReader = new XlsxReader(data);
            return true;
        }

        public bool LoadXls(string filePath)
        {
            excelReader = new XlsReader(filePath);
            return true;
        }
        public bool LoadXlsx(string filePath)
        {
            excelReader = new XlsxReader(filePath);
            return true;
        }

        public IEnumerable<T> Process<T>(IImportOption importOption) where T : IExcelEntity
        {
            IEnumerable<T> result;
            if (string.IsNullOrEmpty(importOption.SheetName))
            {
                result = excelReader.ReadRows<T>(importOption.SheetNumber, importOption.SkipRows);

            }
            else
            {
                result = excelReader.ReadRows<T>(importOption);
            }
            return result;

        }

        public IEnumerable<IExcelEntity> Process(Type entityType, IImportOption importOption)
        {
            IEnumerable<IExcelEntity> result;
            if (string.IsNullOrEmpty(importOption.SheetName))
            {
                result = excelReader.ReadRows(entityType, importOption.SheetNumber, importOption.SkipRows);

            }
            else
            {
                result = excelReader.ReadRows(entityType, importOption);
            }
            return result;

        }

    }
}
