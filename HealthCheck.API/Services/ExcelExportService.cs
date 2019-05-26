using OfficeOpenXml;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;

namespace HealthCheck.API.Services
{
    public class ExcelExportService
    {
        public const string ExcelMimeType = @"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        public byte[] ExportToExcel<T>(IEnumerable<T> items, string worksheetName, bool useSpacedColumnHeaders = false, ExcelExportService.StringReplacementDelegate columnHeaderReplacer = null)
        {
            return ExportToExcel<T>(items, worksheetName, null, useSpacedColumnHeaders, columnHeaderReplacer);
        }

        private byte[] ExportToExcel<T>(IEnumerable<T> items, string worksheetName, MemberInfo[] membersToPrint, bool useSpacedColumnHeaders, StringReplacementDelegate columnHeaderReplacer)
        {
            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add(worksheetName);
                ExcelRange excelRange = worksheet.Cells["A1"];
                excelRange.Value = "Health Check - " + (object)$"{DateTime.Now:MMMM yyyy}";
                int dataRowStartIndex = 3;
                this.WriteDataToExcelPackage<T>(items, membersToPrint, worksheet, dataRowStartIndex, useSpacedColumnHeaders, columnHeaderReplacer);
                return excelPackage.GetAsByteArray();
            }
        }

        private void WriteDataToExcelPackage<T>(IEnumerable<T> items, MemberInfo[] membersToPrint, ExcelWorksheet worksheet, int dataRowStartIndex, bool useSpacedColumnHeaders, StringReplacementDelegate columnHeaderReplacer)
        {
            ExcelRange dataCell = worksheet.Cells["A" + (object)dataRowStartIndex];
            int index1 = dataRowStartIndex;
            int index2 = 1;
            int itemCount = Enumerable.Count<T>(items);

            if (itemCount > 0)
            {
                ExcelRangeBase excelRangeBase = this.InjectRange<T>(items, membersToPrint, worksheet, dataRowStartIndex, dataCell, itemCount);
                SetupWorksheetColumnHeaders(items, worksheet, dataRowStartIndex, useSpacedColumnHeaders, columnHeaderReplacer, dataCell, ref index1, ref index2, excelRangeBase);
                excelRangeBase.AutoFitColumns();
            }
        }

        private ExcelRangeBase InjectRange<T>(IEnumerable<T> items, MemberInfo[] membersToPrint, ExcelWorksheet worksheet, int dataRowStartIndex, ExcelRange dataCell, int itemCount)
        {
            ExcelRangeBase excelRangeBase = dataCell.LoadFromCollection<T>(items, true, OfficeOpenXml.Table.TableStyles.Light1, BindingFlags.Public | BindingFlags.Instance, membersToPrint);
            T customObject = Enumerable.First<T>(items);
            var properties = membersToPrint == null ? typeof(T).GetProperties() : membersToPrint.Select(x => (PropertyInfo)x).ToArray();
            FormatWorkSheet(worksheet, dataRowStartIndex, itemCount, customObject, properties);
            return excelRangeBase;
        }

        private void FormatWorkSheet<T>(ExcelWorksheet worksheet, int dataRowStartIndex, int itemCount, T customObject, PropertyInfo[] properties)
        {
            for (int index = 0; index < properties.Length; ++index)
            {
                PropertyInfo propertyInfo = properties[index];
                string format = null;

                if (propertyInfo.IsDefined(typeof(System.ComponentModel.DataAnnotations.DisplayFormatAttribute)))
                {
                    format = propertyInfo.GetCustomAttribute<System.ComponentModel.DataAnnotations.DisplayFormatAttribute>().DataFormatString;
                }
                else if (propertyInfo.PropertyType == typeof(DateTime) || propertyInfo.PropertyType == typeof(DateTime?))
                {
                    DateTime? dateTime = (DateTime?)propertyInfo.GetValue((object)customObject, (object[])null);

                    if (!dateTime.HasValue)
                    {
                        dateTime = default(DateTime);
                    }

                    format = $"{dateTime:YYYY:MMM:dd}";
                }
                else if (propertyInfo.PropertyType == typeof(decimal) || propertyInfo.PropertyType == typeof(decimal?))
                {
                    format = "R ###,###,##0.00";
                }

                if (format != null)
                {
                    worksheet.Cells[dataRowStartIndex, index + 1, dataRowStartIndex + itemCount, index + 1].Style.Numberformat.Format = format;
                }
            }
        }

        private void SetupWorksheetColumnHeaders<T>(IEnumerable<T> items, ExcelWorksheet worksheet, int dataRowStartIndex, bool useSpacedColumnHeaders, StringReplacementDelegate columnHeaderReplacer, ExcelRange dataCell, ref int index1, ref int index2, ExcelRangeBase excelRangeBase)
        {
            if (useSpacedColumnHeaders || columnHeaderReplacer != null)
            {
                ExcelTable fromRange = worksheet.Tables.GetFromRange(excelRangeBase);
                if (fromRange != null)
                {
                    XmlDocument tableXml = fromRange.TableXml;
                    XmlNamespaceManager nsmgr = new XmlNamespaceManager(fromRange.TableXml.NameTable);
                    nsmgr.AddNamespace("ns", "http://schemas.openxmlformats.org/spreadsheetml/2006/main");
                    XmlNodeList xmlNodeList = tableXml.SelectNodes("/ns:table/ns:tableColumns/ns:tableColumn", nsmgr);
                    if (xmlNodeList != null)
                    {
                        foreach (XmlNode xmlNode in xmlNodeList)
                        {
                            string str = xmlNode.Attributes["name"].Value;
                            if (useSpacedColumnHeaders)
                            {
                                str = PascalToSpacedString(str);
                            }
                            if (columnHeaderReplacer != null)
                            {
                                str = columnHeaderReplacer(str);
                            }
                            xmlNode.Attributes["name"].Value = str;

                        }
                    }
                }

                excelRangeBase.AutoFitColumns();
            }
            else
            {
                dataCell.AutoFitColumns();
            }
        }

        public string PascalToSpacedString(string input)
        {
            const string PascalCaseSplit = @"([a-z](?=[A-Z])|[A-Z](?=[A-Z][a-z]))";
            return Regex.Replace(input, PascalCaseSplit, "$1 ");
        }

        public delegate string StringReplacementDelegate(string stringToBeReplaced);
    }
}
