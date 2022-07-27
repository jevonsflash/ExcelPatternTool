﻿using System;
using System.Collections.Generic;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using ExcelPatternTool.Core.Excel.Core.Interfaces;
using ExcelPatternTool.Core.Excel.Models.Interfaces;

namespace ExcelPatternTool.Core.Excel.Core
{
    public class XlsxReader : BaseReader, IReader
    {
        MemoryStream mem;
        private ISheet sheet;
        public XlsxReader(byte[] data)
        {
            mem = new MemoryStream(data);
            var document = new XSSFWorkbook(mem);
            Document = document;
        }

        public XlsxReader(string filePath)
        {
            mem = null;
            var document = new XSSFWorkbook(filePath);
            Document = document;
        }

        public IEnumerable<T> ReadRows<T>(IImportOption importOption) where T : IExcelEntity
        {

            List<T> result = new List<T>();
            var columns = GetTypeDefinition(typeof(T));
            try
            {
                sheet = Document.GetSheet(importOption.SheetName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception("无法获取Sheet" + e.Message);
            }
            if (sheet==null)
            {
                throw new Exception($"没找到名称为{importOption.SheetName}的Sheet");

            }
            int firstRow = sheet.FirstRowNum;
            int lastRow = sheet.LastRowNum;
            for (int i = sheet.FirstRowNum + importOption.SkipRows; i <= lastRow; i++)
            {
                T objectInstance;
                IRow row = sheet.GetRow(i);
                if (row != null)
                {
                    try
                    {
                        objectInstance = GetDataToObject<T>(row, columns);

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw new Exception($"处理行失败,位置{row.RowNum}:{e.Message}");
                    }
                    result.Add(objectInstance);
                }
            }
            return result;

        }

        public IEnumerable<T> ReadRows<T>(int sheetNumber, int rowsToSkip) where T : IExcelEntity
        {
            List<T> result = new List<T>();
            var columns = GetTypeDefinition(typeof(T));
            try
            {
                sheet = Document.GetSheetAt(sheetNumber);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception("无法获取Sheet" + e.Message);
            }
            if (sheet == null)
            {
                throw new Exception($"没找到Index为{sheetNumber}的Sheet");

            }
            int firstRow = sheet.FirstRowNum;
            int lastRow = sheet.LastRowNum;
            for (int i = firstRow + rowsToSkip; i <= lastRow; i++)
            {
                T objectInstance;
                IRow row = sheet.GetRow(i);
                if (row != null)
                {
                    try
                    {


                        objectInstance = GetDataToObject<T>(row, columns);

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw new Exception($"处理行失败,位置{row.RowNum}:{e.Message}");
                    }
                    result.Add(objectInstance);
                }

            }
            return result;
        }


        public IEnumerable<IExcelEntity> ReadRows(Type entityType, IImportOption importOption)
        {

            List<IExcelEntity> result = new List<IExcelEntity>();
            var columns = GetTypeDefinition(entityType);
            try
            {
                sheet = Document.GetSheet(importOption.SheetName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception("无法获取Sheet" + e.Message);
            }
            if (sheet==null)
            {
                throw new Exception($"没找到名称为{importOption.SheetName}的Sheet");

            }
            int firstRow = sheet.FirstRowNum;
            int lastRow = sheet.LastRowNum;
            for (int i = sheet.FirstRowNum + importOption.SkipRows; i <= lastRow; i++)
            {
                IExcelEntity objectInstance;
                IRow row = sheet.GetRow(i);
                if (row != null)
                {
                    try
                    {
                        objectInstance = (IExcelEntity)GetDataToObject(entityType, row, columns);

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw new Exception($"处理行失败,位置{row.RowNum}:{e.Message}");
                    }
                    result.Add(objectInstance);
                }
            }
            return result;

        }

        public IEnumerable<IExcelEntity> ReadRows(Type entityType, int sheetNumber, int rowsToSkip)
        {
            List<IExcelEntity> result = new List<IExcelEntity>();
            var columns = GetTypeDefinition(entityType);
            try
            {
                sheet = Document.GetSheetAt(sheetNumber);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception("无法获取Sheet" + e.Message);
            }
            if (sheet == null)
            {
                throw new Exception($"没找到Index为{sheetNumber}的Sheet");

            }
            int firstRow = sheet.FirstRowNum;
            int lastRow = sheet.LastRowNum;
            for (int i = firstRow + rowsToSkip; i <= lastRow; i++)
            {
                IExcelEntity objectInstance;
                IRow row = sheet.GetRow(i);
                if (row != null)
                {
                    try
                    {


                        objectInstance = (IExcelEntity)GetDataToObject(entityType, row, columns);

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw new Exception($"处理行失败,位置{row.RowNum}:{e.Message}");
                    }
                    result.Add(objectInstance);
                }

            }
            return result;
        }

    }
}
