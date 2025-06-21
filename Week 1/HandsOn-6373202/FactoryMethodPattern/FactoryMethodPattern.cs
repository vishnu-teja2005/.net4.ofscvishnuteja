using System;

namespace FactoryMethodPatternExample
{
    public abstract class Document
    {
        public abstract void Open();
    }
    public class WordDocument : Document
    {
        public override void Open()
        {
            Console.WriteLine("Opening Word Document...");
        }
    }

    public class PdfDocument : Document
    {
        public override void Open()
        {
            Console.WriteLine("Opening PDF Document...");
        }
    }

    public class ExcelDocument : Document
    {
        public override void Open()
        {
            Console.WriteLine("Opening Excel Spreadsheet...");
        }
    }
    public abstract class DocumentFactory
    {
        public abstract Document CreateDocument();
    }
    public class WordDocumentFactory : DocumentFactory
    {
        public override Document CreateDocument()
        {
            return new WordDocument();
        }
    }

    public class PdfDocumentFactory : DocumentFactory
    {
        public override Document CreateDocument()
        {
            return new PdfDocument();
        }
    }

    public class ExcelDocumentFactory : DocumentFactory
    {
        public override Document CreateDocument()
        {
            return new ExcelDocument();
        }
    }
    public class Program
    {
        static void Main(string[] args)
        {
            DocumentFactory factory;
           factory = new WordDocumentFactory();
           WordDocumentFactory wordDoc=new WordDocumentFactory();
            Document wordDoc = factory.CreateDocument();
            wordDoc.Open();
            factory = new PdfDocumentFactory();
            Document pdfDoc = factory.CreateDocument();
            pdfDoc.Open();
            factory = new ExcelDocumentFactory();
            Document excelDoc = factory.CreateDocument();
            excelDoc.Open();
        }
    }
}
