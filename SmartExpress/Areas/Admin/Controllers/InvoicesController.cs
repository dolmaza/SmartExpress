using Core;
using Core.Properties;
using Core.Utilities;
using SmartExpress.Admin.Models;
using SmartExpress.Admin.Reusable;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace SmartExpress.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    public class InvoicesController : BaseController
    {
        [Route("invoices", Name = "Invoices")]
        public ActionResult Index()
        {
            var model = new InvoiceViewModel
            {
                InvoiceCreateUrl = Url.RouteUrl("InvoicesCreate"),
                InvoicesJson = UnitOfWork.InvoiceRepository.GetAll().Include(i => i.MessageMode).ToList().Select(i => new InvoiceObject
                {
                    ID = i.ID,
                    ParentID = i.ParentID,
                    InvoiceNumber = i.InvoiceNumber,
                    CompanyName = i.CompanyName,
                    SenderAddress = i.SenderAddress,
                    ReceiveDate = i.ReceiveDate,
                    MessageMode = i.MessageMode.Caption,
                    Quantity = i.Quantity,
                    Weigth = i.Weigth,
                    UnitPrice = i.UnitPrice,
                    Direction = i.Direction,
                    ReceiverFirstnameLastname = $"{i.ReceiverFirstname} {i.ReceiverLastname}",
                    ReceiverTelephoneNumber = i.ReceiverTelephoneNumber,
                    ReceiverAddress = i.ReceiverAddress
                }).ToJson()
            };
            return View(model);
        }

        [Route("invoices/create", Name = "InvoicesCreate")]
        [Route("invoices/{parentID}/create")]
        public ActionResult CreateInvoice(int? parentID)
        {
            var model = new CreateEditInvoiceViewModel
            {
                SaveUrl = Url.RouteUrl("InvoicesCreate"),
                InvoicesUrl = Url.RouteUrl("Invoices"),
                Title = "ზედნადების დამატება",
                InvoiceObject = new InvoiceObject
                {
                    ParentID = parentID ?? 0
                }
            };
            return View("CreateEditInvoice", model);
        }

        [HttpPost]
        [Route("invoices/create")]
        [Route("invoices/{ParentID}/create")]
        public ActionResult CreateInvoice(InvoiceObject model)
        {
            var AR = new AjaxResponse();

            var invoice = new Invoice
            {
                ParentID = model.ParentID,
                InvoiceNumber = model.InvoiceNumber,
                MessageTypeID = model.MessageTypeID,
                ReceiveDate = model.ReceiveDate,
                DeliveryDate = model.DeliveryDate,
                UnitPrice = model.UnitPrice,
                TotalPrice = model.TotalPrice,
                Direction = model.Direction,
                MessageModeID = model.MessageModeID,
                PayerID = model.PayerID,
                FormOfPaymentID = model.FormOfPaymentID,
                Quantity = model.Quantity,
                Weigth = model.Weigth,

                CompanyName = model.CompanyName,
                SenderFirstname = model.SenderFirstname,
                SenderLastname = model.SenderLastname,
                SenderTelephoneNumber = model.SenderTelephoneNumber,
                SenderAddress = model.SenderAddress,

                ReceiverFirstname = model.ReceiverFirstname,
                ReceiverLastname = model.ReceiverLastname,
                ReceiverTelephoneNumber = model.ReceiverTelephoneNumber,
                ReceiverAddress = model.ReceiverAddress,
                WhoReceived = model.WhoReceived,
                WhoReceivedAdditional = model.WhoReceivedAdditional,
                CreateTime = DateTime.Now

            };

            UnitOfWork.InvoiceRepository.Add(invoice);

            if (UnitOfWork.InvoiceRepository.IsError)
            {
                AR.Data = new
                {
                    Message = Resources.Abort
                };
            }
            else
            {
                AR.IsSuccess = true;
                AR.Data = new
                {
                    RedirectUrl = Url.RouteUrl("InvoicesEdit", new { ID = invoice.ID })
                };
            }

            return Json(AR);
        }

        [HttpPost]
        [Route("invoices/sender/{ID}/information", Name = "GetSenderInformation")]
        public ActionResult GetSenderInformation(int? ID)
        {
            var AR = new AjaxResponse();
            var user = UnitOfWork.UserRepository.Get(ID);

            if (user == null)
            {
                return NotFound();
            }
            else
            {
                AR.IsSuccess = true;
                AR.Data = new
                {
                    Sender = user
                };

                return Json(AR);
            }

        }

        [Route("invoices/{ID}/edit", Name = "InvoicesEdit")]
        public ActionResult EditInvoice(int? ID)
        {
            var invoice = UnitOfWork.InvoiceRepository.Get(ID);
            if (invoice == null)
            {
                return NotFound();
            }
            else
            {
                var model = new CreateEditInvoiceViewModel
                {
                    SaveUrl = Url.RouteUrl("InvoicesEdit", new { ID = invoice.ID }),
                    InvoicesUrl = Url.RouteUrl("Invoices"),
                    Title = "ზედნადების რედაქტირება",
                    InvoiceObject = new InvoiceObject
                    {
                        InvoiceNumber = invoice.InvoiceNumber,
                        MessageTypeID = invoice.MessageTypeID,
                        ReceiveDate = invoice.ReceiveDate,
                        DeliveryDate = invoice.DeliveryDate,
                        UnitPrice = invoice.UnitPrice,
                        TotalPrice = invoice.TotalPrice,
                        Direction = invoice.Direction,
                        MessageModeID = invoice.MessageModeID,
                        PayerID = invoice.PayerID,
                        FormOfPaymentID = invoice.FormOfPaymentID,
                        Quantity = invoice.Quantity,
                        Weigth = invoice.Weigth,

                        CompanyName = invoice.CompanyName,
                        SenderFirstname = invoice.SenderFirstname,
                        SenderLastname = invoice.SenderLastname,
                        SenderTelephoneNumber = invoice.SenderTelephoneNumber,
                        SenderAddress = invoice.SenderAddress,

                        ReceiverFirstname = invoice.ReceiverFirstname,
                        ReceiverLastname = invoice.ReceiverLastname,
                        ReceiverTelephoneNumber = invoice.ReceiverTelephoneNumber,
                        ReceiverAddress = invoice.ReceiverAddress,
                        WhoReceived = invoice.WhoReceived,
                        WhoReceivedAdditional = invoice.WhoReceivedAdditional
                    }
                };
                return View("CreateEditInvoice", model);

            }
        }

        [HttpPost]
        [Route("invoices/{ID}/edit")]
        public ActionResult EditInvoice(InvoiceObject model)
        {
            var AR = new AjaxResponse();

            UnitOfWork.InvoiceRepository.Update(new Invoice
            {
                ID = model.ID,
                ParentID = model.ParentID,
                InvoiceNumber = model.InvoiceNumber,
                MessageTypeID = model.MessageTypeID,
                ReceiveDate = model.ReceiveDate,
                DeliveryDate = model.DeliveryDate,
                UnitPrice = model.UnitPrice,
                TotalPrice = model.TotalPrice,
                Direction = model.Direction,
                MessageModeID = model.MessageModeID,
                PayerID = model.PayerID,
                FormOfPaymentID = model.FormOfPaymentID,
                Quantity = model.Quantity,
                Weigth = model.Weigth,

                CompanyName = model.CompanyName,
                SenderFirstname = model.SenderFirstname,
                SenderLastname = model.SenderLastname,
                SenderTelephoneNumber = model.SenderTelephoneNumber,
                SenderAddress = model.SenderAddress,

                ReceiverFirstname = model.ReceiverFirstname,
                ReceiverLastname = model.ReceiverLastname,
                ReceiverTelephoneNumber = model.ReceiverTelephoneNumber,
                ReceiverAddress = model.ReceiverAddress,
                WhoReceived = model.WhoReceived,
                WhoReceivedAdditional = model.WhoReceivedAdditional,
                CreateTime = DateTime.Now

            });

            if (UnitOfWork.InvoiceRepository.IsError)
            {
                AR.Data = new
                {
                    Message = Resources.Abort
                };
            }
            else
            {
                AR.IsSuccess = true;
                AR.Data = new
                {
                    Message = Resources.Success
                };
            }
            return Json(AR);
        }

        [Route("invoices/{ID}/delete", Name = "InvoicesDelete")]
        public ActionResult DeleteInvoice(int? ID)
        {
            var invoice = UnitOfWork.InvoiceRepository.Get(ID);

            UnitOfWork.InvoiceRepository.Remove(invoice);
            if (UnitOfWork.InvoiceRepository.IsError)
            {
                InitErrorMessage(Resources.Abort);
            }

            return Redirect(Url.RouteUrl("Invoices"));
        }
    }
}