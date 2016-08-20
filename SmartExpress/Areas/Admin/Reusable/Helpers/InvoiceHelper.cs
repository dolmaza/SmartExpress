using SmartExpress.Admin.Models;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SmartExpress.Admin.Reusable.Helpers
{
    public class InvoiceHelper
    {
        public static void InitInvoicesDataForExcel(IEnumerable<InvoicesExportToExcelViewModel> invoices, Controller controller)
        {
            var data = new DataTable("invoices");

            data.Columns.Add("ზედნ. №", typeof(string));
            data.Columns.Add("გზავნილის ტიპი", typeof(string));
            data.Columns.Add("მიღების თარიღი", typeof(string));
            data.Columns.Add("ჩაბარების თარიღი", typeof(string));
            data.Columns.Add("ერთეულის ფასი", typeof(string));
            data.Columns.Add("საერთო ფასი", typeof(string));
            data.Columns.Add("მიმართულება", typeof(string));
            data.Columns.Add("რეჟიმი", typeof(string));
            data.Columns.Add("გადამხდელი", typeof(string));
            data.Columns.Add("გადახდის ფორმა", typeof(string));
            data.Columns.Add("რაოდენობა", typeof(string));
            data.Columns.Add("წონა", typeof(string));
            data.Columns.Add("ხელშეკრულების №", typeof(string));
            data.Columns.Add("კომპანია", typeof(string));
            data.Columns.Add("გამგზავნის სახელი", typeof(string));
            data.Columns.Add("გამგზავნის გვარი", typeof(string));
            data.Columns.Add("გამგზავნის ტელეფონი", typeof(string));
            data.Columns.Add("გამგზავნის მისამართი", typeof(string));
            data.Columns.Add("მიმღების სახელი", typeof(string));
            data.Columns.Add("მიმღების გვარი", typeof(string));
            data.Columns.Add("მიმღების ტელეფონი", typeof(string));
            data.Columns.Add("მიმღების მისამართი", typeof(string));
            data.Columns.Add("ვინ მიიღო", typeof(string));
            data.Columns.Add("დამატებითი", typeof(string));

            foreach (var invoice in invoices)
            {
                data.Rows.Add
                    (
                        invoice.InvoiceNumber,
                        invoice.MessageTypeCaption,
                        invoice.ReceiveDate,
                        invoice.DeliveryDate,
                        invoice.UnitPrice,
                        invoice.TotalPrice,
                        invoice.Direction,
                        invoice.MessageModeCaption,
                        invoice.PayerCaption,
                        invoice.FormOfPaymentCaption,
                        invoice.Quantity,
                        invoice.Weigth,
                        invoice.ContractNumber,
                        invoice.CompanyName,
                        invoice.SenderFirstname,
                        invoice.SenderLastname,
                        invoice.SenderTelephoneNumber,
                        invoice.SenderAddress,
                        invoice.ReceiverFirstname,
                        invoice.ReceiverLastname,
                        invoice.ReceiverTelephoneNumber,
                        invoice.ReceiverAddress,
                        invoice.WhoReceived,
                        invoice.WhoReceivedAdditional
                    );

            }

            ExportToExcel(data, controller);

        }

        public static void ExportToExcel(DataTable data, Controller controller)
        {
            var gridview = new GridView
            {
                DataSource = data
            };
            gridview.DataBind();

            controller.Response.ClearContent();
            controller.Response.Buffer = true;

            controller.Response.AddHeader("content-disposition", "attachment; filename = Invoices.xls");
            controller.Response.ContentType = "application/ms-excel";
            controller.Response.Charset = "";

            using (var sw = new StringWriter())
            {
                using (var htw = new HtmlTextWriter(sw))
                {
                    gridview.RenderControl(htw);

                    controller.Response.Output.Write(sw.ToString());
                    controller.Response.Flush();
                    controller.Response.End();
                }
            }
        }
    }
}