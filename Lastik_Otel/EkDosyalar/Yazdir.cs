using System;
using Microsoft.Office.Interop.Word;
using System.Collections;

namespace Lastik_Otel.EkDosyalar
{
    public class Yazdir
    {
        #region Constructor
        public Application wordApp { get; set; }
        Document document { get; set; }
        Paragraph paragraph { get; set; }
        Table table { get; set; }
        object missing;
        #endregion

        public Yazdir()
        {
            missing = System.Reflection.Missing.Value;
        }


        public void CiktiAl(ArrayList arrayYazilacak)
        {
            GenelIslemler.killWordInstance();

            DocumentOlustur();
            TabloEkle(paragraph, 2, 2, arrayYazilacak);

            document.PrintOut();

            // document.SaveAs2(WdSaveFormat.wdFormatPDF);

            //document.PrintPreview();
            //wordApp.Visible = true;

            // wordApp.Quit(SaveChanges: false);

        }

        private void DocumentOlustur()
        {
            try
            {
                wordApp = new Application();

                wordApp.Visible = false;
                wordApp.ShowAnimation = false;

                document = wordApp.Documents.Add();

                document.PageSetup.BottomMargin = 30;
                document.PageSetup.TopMargin = 30;
                document.PageSetup.LeftMargin = 30;
                document.PageSetup.RightMargin = 30;

                document.Content.SetRange(0, 0);

                paragraph = document.Content.Paragraphs.Add();
                paragraph.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            }
            catch (Exception ex)
            {
                Loglama.Write(ex.ToString());
            }
        }
        private void TabloEkle(Paragraph paragraph, int Satır, int Sutun, ArrayList Yazilar)
        {
            try
            {
                table = document.Tables.Add(paragraph.Range, Satır, Sutun);
                table.Borders.Enable = 1;
                table.AutoFitBehavior(WdAutoFitBehavior.wdAutoFitFixed);

                table.Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                foreach (Row row in table.Rows)
                {
                    row.HeightRule = WdRowHeightRule.wdRowHeightExactly;
                    row.Height = 375;
                    
                    foreach (Cell cell in row.Cells)
                    {
                        cell.Range.Font.Size = 40;
                        cell.WordWrap = false;
                        cell.Range.Paragraphs.LineSpacing = 10f;

                        foreach (string str in Yazilar)
                        {
                            if (!String.IsNullOrEmpty(str))
                            {
                                cell.Range.Text += str;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Loglama.Write(ex.ToString());
            }
        }




        #region MyRegion
        //Microsoft.Office.Interop.Word.Application winword { get; set; }
        //Document document { get; set; }
        //Paragraph para1 { get; set; }
        //Paragraph para2 { get; set; }
        //object missing;


        //void headerEkle(Document document)
        //{
        //    //Add header into the document
        //    foreach (Section section in document.Sections)
        //    {
        //        //Get the header range and add the header details.
        //        Range headerRange = section.Headers[WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
        //        headerRange.Fields.Add(headerRange, WdFieldType.wdFieldPage);
        //        headerRange.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
        //        headerRange.Font.ColorIndex = WdColorIndex.wdBlue;
        //        headerRange.Font.Size = 10;
        //        headerRange.Text = "Header text goes here";
        //    }
        //}

        //void footerEkle(Document document)
        //{
        //    //Add the footers into the document
        //    foreach (Section wordSection in document.Sections)
        //    {
        //        //Get the footer range and add the footer details.
        //        Range footerRange = wordSection.Footers[WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
        //        footerRange.Font.ColorIndex = WdColorIndex.wdDarkRed;
        //        footerRange.Font.Size = 10;
        //        footerRange.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
        //        footerRange.Text = "Footer text goes here";
        //    }
        //}

        //void DokumanKaydet()
        //{
        //    //Save the document
        //    object filename = @"D:\temp1.docx";
        //    document.SaveAs2(ref filename);
        //    document.Close(ref missing, ref missing, ref missing);
        //    document = null;
        //    winword.Quit(ref missing, ref missing, ref missing);
        //    winword = null;
        //    MessageBox.Show("Document created successfully !");
        //}

        //void TextveHeaderEkle()
        //{
        //    //adding text to document
        //    document.Content.SetRange(0, 0);
        //    document.Content.Text = "This is test document " + Environment.NewLine;

        //    //Add paragraph with Heading 1 style
        //    para1 = document.Content.Paragraphs.Add(ref missing);
        //    object styleHeading1 = "Heading 1";
        //    para1.Range.set_Style(ref styleHeading1);
        //    para1.Range.Text = "Para 1 text";
        //    para1.Range.InsertParagraphAfter();

        //    //Add paragraph with Heading 2 style
        //    para2 = document.Content.Paragraphs.Add(ref missing);
        //    object styleHeading2 = "Heading 2";
        //    para2.Range.set_Style(ref styleHeading2);
        //    para2.Range.Text = "Para 2 text";
        //    para2.Range.InsertParagraphAfter();

        //}

        //public void deneme()
        //{
        //    try
        //    {
        //        winword = new Microsoft.Office.Interop.Word.Application();
        //        winword.ShowAnimation = false;
        //        winword.Visible = false;
        //        missing = System.Reflection.Missing.Value;

        //        //Create a new document
        //        document = winword.Documents.Add(ref missing, ref missing, ref missing, ref missing);

        //        //headerEkle(document);
        //        //footerEkle(document);
        //        //TextveHeaderEkle();

        //        //Create a 5X5 table and insert some dummy record

        //        para1 = document.Content.Paragraphs.Add(ref missing);
        //        object styleHeading1 = "Heading 1";
        //        para1.Range.Text = "Para 1 textasdddddddddddddddddddasdasdasdasdasd";
        //        para1.Range.InsertParagraphAfter();

        //        //Table firstTable = document.Tables.Add(para1.Range, 1, 1, ref missing, ref missing);
        //        //firstTable.Rows.Height = 400;
        //        //firstTable.Columns.Width = 150;

        //        //foreach (Row row in firstTable.Rows)
        //        //{
        //        //    foreach (Cell cell in row.Cells)
        //        //    {
        //        //        cell.Range.Text = "Column " + cell.ColumnIndex.ToString();
        //        //    }
        //        //}


        //        //     firstTable.Borders.Enable = 1;
        //        //foreach (Row row in firstTable.Rows)
        //        //{
        //        //    foreach (Cell cell in row.Cells)
        //        //    {
        //        //        //Header row
        //        //        if (cell.RowIndex == 1)
        //        //        {
        //        //            cell.Range.Text = "Column " + cell.ColumnIndex.ToString();
        //        //            cell.Range.Font.Bold = 1;
        //        //            //other format properties goes here
        //        //            cell.Range.Font.Name = "verdana";
        //        //            cell.Range.Font.Size = 20;
        //        //            //cell.Range.Font.ColorIndex = WdColorIndex.wdGray25;                            
        //        //            cell.Shading.BackgroundPatternColor = WdColor.wdColorGray25;
        //        //            //Center alignment for the Header cells
        //        //            cell.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
        //        //            cell.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

        //        //        }
        //        //        //Data row
        //        //        else
        //        //        {
        //        //            cell.Range.Text = (cell.RowIndex - 2 + cell.ColumnIndex).ToString();
        //        //        }
        //        //    }
        //        //}

        //        // winword.Visible = true;

        //        document.PrintOut(
        //            PrintZoomRow: 2,
        //            PrintZoomColumn: 2,
        //            Copies: 4,
        //            P);




        //        // DokumanKaydet();

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        #endregion
    }
}
