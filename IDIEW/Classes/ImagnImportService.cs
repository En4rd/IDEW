using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Word;
using System.IO;

namespace IDIEW.Classes
{
    class ImagnImportService
    {

        public class ImportConfig
        {
            public string ExcelFilePath { get; set; }
            public string WordFilePath { get; set; }
            public string[] Rangos { get; set; }
            public float AnchoCm { get; set; }
            public float AltoCm { get; set; }
            public int PaginaDestino { get; set; }
        }

        public class ImagenImportService
        {
            public async System.Threading.Tasks.Task ImportarAsync(
                ImportConfig config,
                Action<int, int> onProgress,
                Action<string, string> onError)
            {
                await System.Threading.Tasks.Task.Run(() =>
                {
                    int contadorImagenes = 0;
                    int totalRangos = config.Rangos.Length;
                    int totalHojas = 0;
                    int totalImagenes = 0;

                    var excelApp = new Microsoft.Office.Interop.Excel.Application();
                    var wordApp = new Microsoft.Office.Interop.Word.Application();
                    var workbook = excelApp.Workbooks.Open(config.ExcelFilePath);
                    var wordDoc = wordApp.Documents.Open(config.WordFilePath);

                    try
                    {
                        totalHojas = workbook.Sheets.Count;
                        totalImagenes = totalRangos * totalHojas;
                        wordApp.Selection.GoTo(What: WdGoToItem.wdGoToPage, Name: config.PaginaDestino.ToString());

                        foreach (Worksheet hoja in workbook.Sheets)
                        {
                            foreach (string rango in config.Rangos)
                            {
                                for (int intento = 0; intento <= 10; intento++)
                                {
                                    try
                                    {
                                        var excelRange = hoja.Range[rango];
                                        excelRange.CopyPicture(Microsoft.Office.Interop.Excel.XlPictureAppearance.xlScreen, Microsoft.Office.Interop.Excel.XlCopyPictureFormat.xlPicture);

                                        wordApp.Selection.Paste();
                                        var lastShape = wordDoc.InlineShapes[wordDoc.InlineShapes.Count];
                                        lastShape.LockAspectRatio = Microsoft.Office.Core.MsoTriState.msoFalse;

                                        lastShape.Width = config.AnchoCm * 28.35f;
                                        lastShape.Height = config.AltoCm * 28.35f;

                                        contadorImagenes++;
                                        onProgress?.Invoke(contadorImagenes, totalImagenes);
                                        break;
                                    }
                                    catch (Exception ex)
                                    {
                                        if (intento >= 10)
                                        {
                                            onError?.Invoke(ex.Message, hoja.Name);
                                        }
                                    }
                                }
                            }
                        }

                        wordDoc.Save();
                    }
                    finally
                    {
                        wordDoc.Close();
                        wordApp.Quit();
                        workbook.Close();
                        excelApp.Quit();
                    }
                });
            }
        }

    }
}
