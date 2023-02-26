using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Api.Extensions
{
    public static class ExcelExtensions
    {
        public static readonly string EXCEL_CONTENT_TYPE = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        /* Convenience/Helper methods */

        public static string[] GenerateColumnRange(string start, int count, int step = 1)
        {
            // Sanitize input
            if (count < 0)
                return new string[0];
            // Get range of column names
            var initial = start.ToExcelColNumber();
            var list = new string[count];
            for (int i = 0; i < count; i += step)
                list[i] = (initial + i).ToExcelColName();
            return list;
        }

        public static string[] Columns(this string start, int count, int step = 1)
            => GenerateColumnRange(start, count, step);

        public static string[] ToStrings<T>(this IEnumerable<T> values)
            => values.Select(v => v.ToString()).ToArray();

        /* Column string <-> int conversions */

        public static string NextExcelColumn(this string columnName)
            => (columnName.ToExcelColNumber() + 1).ToExcelColName();

        public static string ToExcelColName(this int columnNumber)
        {
            int dividend = columnNumber;
            string columnName = string.Empty;
            int modulo;
            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (int)((dividend - modulo) / 26);
            }
            return columnName;
        }

        public static int ToExcelColNumber(this string columnName)      // (A = 1, B = 2,...,AA = 27,...,AAA = 703,...)
        {
            char[] characters = columnName.ToUpperInvariant().ToCharArray();
            int sum = 0;
            for (int i = 0; i < characters.Length; i++)
            {
                sum *= 26;
                sum += (characters[i] - 'A' + 1);
            }
            return sum;
        }

        /* Cells extension methods */

        public static void SetBackground(this ExcelRange range, Color color)
        => range.Style.Fill.BackgroundColor.SetColor(color);

        public static void Headerify(this ExcelRange range)
        {
            range.Style.Font.Bold = true;
            range.Style.Font.Size = 11;
            range.Style.Fill.PatternType = ExcelFillStyle.Solid;
            range.SetBackground(Color.LightGray);
            range.Style.Font.Color.SetColor(Color.LightSeaGreen);
            range.Style.WrapText = true;
            range.BorderizeThin();
        }

        public static void Borderize(this ExcelRange range, ExcelBorderStyle style)
        {
            range.Style.Border.Top.Style = style;
            range.Style.Border.Left.Style = style;
            range.Style.Border.Right.Style = style;
            range.Style.Border.Bottom.Style = style;
            range.Centralize();
        }

        public static void BorderizeThin(this ExcelRange range)
            => range.Borderize(ExcelBorderStyle.Thin);

        public static void BorderizeThick(this ExcelRange range)
            => range.Borderize(ExcelBorderStyle.Thick);

        public static void Centralize(this ExcelRange range)
        {
            range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
        }

        /* Worksheet extension methods */

        public static ExcelWorksheet WriteTitle(this ExcelWorksheet ws, string address, string title)
        {
            var range = ws.Cells[address];
            if (address.Contains(":"))  // if it's a multi cell address then merge
                range.Merge = true;
            range.Value = title;
            
            range.Style.Font.Bold = true;
            range.Style.Font.Size = 11;
            range.Style.Fill.PatternType = ExcelFillStyle.Solid;
            range.SetBackground(Color.LightSeaGreen);
            range.Style.Font.Color.SetColor(Color.White);
            range.Style.WrapText = true;
            range.BorderizeThin();
            return ws;
        }

        public static ExcelWorksheet WriteCell(this ExcelWorksheet ws, string address, object value, params Action<ExcelRange>[] actions)   // borderizes while writing a value to a cell
        {
            var cell = ws.Cells[address];
            if (address.Contains(":"))  // if it's a multi cell address then merge
                cell.Merge = true;
            cell.Value = value;
            BorderizeThin(cell);
            foreach (var action in actions)
                action.Invoke(cell);
            return ws;
        }

        public static ExcelWorksheet WriteCellFormula(this ExcelWorksheet ws, string address, string value, params Action<ExcelRange>[] actions)   // borderizes while writing a value to a cell
        {
            var cell = ws.Cells[address];
            if (address.Contains(":"))  // if it's a multi cell address then merge
                cell.Merge = true;
            cell.Formula = value;
            BorderizeThin(cell);
            foreach (var action in actions)
                action.Invoke(cell);
            return ws;
        }

        public static ExcelWorksheet AutoFitAllCells(this ExcelWorksheet ws)   // resize all cells to fit
        {
            ws.Cells[ws.Dimension.Address].AutoFitColumns();
            return ws;
        }

        // Note: last parameter is of variable-arity so we can add actions to be applied on each cell

        public static ExcelWorksheet WriteValuesVertically<T>(this ExcelWorksheet ws, string col, int startLine, IEnumerable<T> values, params Action<ExcelRange>[] actions)
        {
            for (int i = 0, line = startLine, count = values.Count(); i < count; i++, line++)
                ws.WriteCell(col + line, values.ElementAt(i), actions);
            return ws;
        }

        public static ExcelWorksheet WriteValuesHorizontally<T>(this ExcelWorksheet ws, string startCol, int line, IEnumerable<T> values, params Action<ExcelRange>[] actions)
        {
            for (int i = 0, col = startCol.ToExcelColNumber(), count = values.Count(); i < count; i++, col++)
                ws.WriteCell(col.ToExcelColName() + line, values.ElementAt(i), actions);
            return ws;
        }

        public static ExcelWorksheet WriteHeadersVertically(this ExcelWorksheet ws, string col, int startLine, string[] strs)
            => ws.WriteValuesVertically(col, startLine, strs, Headerify);

        public static ExcelWorksheet WriteCellHeader(this ExcelWorksheet ws, string startCol, string value)
            => ws.WriteCell(startCol, value, Headerify);

        public static ExcelWorksheet WriteHeadersHorizontally(this ExcelWorksheet ws, string startCol, int line, string[] strs)
            => ws.WriteValuesHorizontally(startCol, line, strs, Headerify);

        public static ExcelWorksheet WriteDictionaryHorizontally<K, V>(this ExcelWorksheet ws, string startCol, int startLine, Dictionary<K, V> dict, params Action<ExcelRange>[] actions)
            => ws.WriteValuesHorizontally(startCol, startLine, dict.Keys, actions)          // writes keys on 1st line
                .WriteValuesHorizontally(startCol, startLine + 1, dict.Values, actions);    // and values on 2nd

        public static ExcelWorksheet WriteDictionaryVertically<K, V>(this ExcelWorksheet ws, string startCol, int startLine, Dictionary<K, V> dict, params Action<ExcelRange>[] actions)
            => ws.WriteValuesVertically(startCol, startLine, dict.Keys, actions)                        // writes keys on 1st col
                .WriteValuesVertically(startCol.NextExcelColumn(), startLine, dict.Values, actions);    // and values on 2nd

        // Accessors specify how to access each property of object instances of type T
        public static ExcelWorksheet WriteObjectsHorizontally<T>(this ExcelWorksheet ws, string startCol, int startLine, List<T> entries, params Func<T, object>[] accessors)
        {
            int line = startLine;
            string[] cols = GenerateColumnRange(startCol, accessors.Length);
            foreach (var entry in entries)
            {
                for (int i = 0; i < cols.Length; i++)
                    ws.WriteCell(cols[i] + line, accessors[i].Invoke(entry));
                line++;
            }
            return ws;
        }

        public static ExcelWorksheet WriteObjectsVertically<T>(this ExcelWorksheet ws, string startCol, int startLine, List<T> entries, params Func<T, object>[] accessors)
        {
            int col = startCol.ToExcelColNumber();
            foreach (var entry in entries)
            {
                for (int i = 0, line = startLine; i < accessors.Length; i++, line++)
                    ws.WriteCell(col.ToExcelColName() + line, accessors[i].Invoke(entry));
                col++;
            }
            return ws;
        }

        /* Excel package and worksheet extension methods */

        public static ExcelPackage AddWorksheet(this ExcelPackage excelPackage, string name, params Action<ExcelWorksheet>[] actions)
        {
            var ws = excelPackage.Workbook.Worksheets.Add(name);
            // Apply changes to worksheet
            foreach (var action in actions)
                action.Invoke(ws);
            return excelPackage;
        }

        public static ExcelFileData ToDataFile(this ExcelPackage excelPackage, string filename)
        {
            // Automatically fit all cells before generating
            foreach (var worksheet in excelPackage.Workbook.Worksheets)
                worksheet.AutoFitAllCells();
            return new ExcelFileData
            {
                Name = filename,
                ContentType = EXCEL_CONTENT_TYPE,
                Bytes = excelPackage.GetAsByteArray()
            };
        }
    }

    public class ExcelFileData
    {
        public string Name { get; set; }
        public string ContentType { get; set; }
        public byte[] Bytes { get; set; }
    }
}
