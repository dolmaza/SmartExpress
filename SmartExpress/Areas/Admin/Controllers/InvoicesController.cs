using Core;
using Core.Properties;
using Core.Utilities;
using Core.Validation;
using OfficeOpenXml;
using SmartExpress.Admin.Models;
using SmartExpress.Admin.Reusable;
using SmartExpress.Admin.Reusable.Helpers;
using SmartExpress.Reusable.Extentions;
using SmartExpress.Reusable.Utilities;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

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
                InvoicesExportToExcelUrl = Url.RouteUrl("InvoicesExportToExcel"),
                InvoicesByReceiveDateUrl = Url.RouteUrl("InvoicesByReceiveDate"),

                InvoicesJson = UnitOfWork.InvoiceRepository.GetAll()
                .OrderByDescending(i => i.ReceiveDate)
                .Include(i => i.MessageMode)
                .ToList()
                .Select(i => new InvoiceObject
                {
                    ID = i.ID,
                    ParentID = i.ParentID,
                    InvoiceNumber = i.InvoiceNumber,
                    CompanyName = i.CompanyName,
                    SenderAddress = i.SenderAddress,
                    ReceiveDate = i.ReceiveDate?.ToString(Resources.CustomDateFormat),
                    MessageMode = i.MessageMode.Caption,
                    Quantity = $"{i.Quantity:0.}",
                    Weigth = $"{i.Weigth:0.00}",
                    UnitPrice = $"{i.UnitPrice:0.00}",
                    Direction = i.Direction,
                    ReceiverFirstnameLastname = $"{i.ReceiverFirstname} {i.ReceiverLastname}",
                    ReceiverTelephoneNumber = i.ReceiverTelephoneNumber,
                    ReceiverAddress = i.ReceiverAddress
                }).ToJson()
            };
            return View(model);
        }

        [HttpPost]
        [Route("invoices/by-receive-date", Name = "InvoicesByReceiveDate")]
        public ActionResult InvoicesByReseiveDate(DateTime? dateFrom, DateTime? dateTo)
        {
            var ajaxResponse = new AjaxResponse();
            var invoicesJson = UnitOfWork.InvoiceRepository.GetInvociesByReceiveDate(dateFrom, dateTo)
                .Select(i => new InvoiceObject
                {
                    ID = i.ID,
                    ParentID = i.ParentID,
                    InvoiceNumber = i.InvoiceNumber,
                    CompanyName = i.CompanyName,
                    SenderAddress = i.SenderAddress,
                    ReceiveDate = i.ReceiveDate?.ToString(Resources.CustomDateFormat),
                    MessageMode = i.MessageMode.Caption,
                    Quantity = $"{i.Quantity:0.}",
                    Weigth = $"{i.Weigth:0.00}",
                    UnitPrice = $"{i.UnitPrice:0.00}",
                    Direction = i.Direction,
                    ReceiverFirstnameLastname = $"{i.ReceiverFirstname} {i.ReceiverLastname}",
                    ReceiverTelephoneNumber = i.ReceiverTelephoneNumber,
                    ReceiverAddress = i.ReceiverAddress
                }).ToJson();

            ajaxResponse.IsSuccess = true;
            ajaxResponse.Data = new
            {
                InvoicesJson = invoicesJson
            };
            return Json(ajaxResponse);
        }

        [Route("invoices/create", Name = "InvoicesCreate")]
        [Route("invoices/{parentID}/create", Name = "InvoicesCreateChild")]
        public ActionResult CreateInvoice(int? parentID)
        {
            GenerateSuccessErrorMessageContainer();
            var invoice = parentID != null ? UnitOfWork.InvoiceRepository.Get(parentID) : new Invoice();
            var model = new CreateEditInvoiceViewModel
            {
                SaveUrl = parentID == null ? Url.RouteUrl("InvoicesCreate") : Url.RouteUrl("InvoicesCreateChild", new { ParentID = parentID }),
                InvoicesUrl = Url.RouteUrl("Invoices"),
                GetSenderInformationUrl = Url.RouteUrl("GetSenderInformation"),
                Title = "ზედნადების დამატება",
                InvoiceObject = new InvoiceObject
                {
                    ParentID = parentID ?? 0,
                    InvoiceNumber = invoice.InvoiceNumber,
                    ReceiveDate = invoice.ReceiveDate?.ToString(Resources.CustomDateFormat),
                    DeliveryDate = invoice.DeliveryDate?.ToString(Resources.CustomDateFormat),
                    TotalPrice = $"{invoice.TotalPrice:0.00}",
                    Quantity = $"{invoice.Quantity:0.}",

                    UserID = invoice.UserID,
                    CompanyName = invoice.CompanyName,
                    SenderFirstname = invoice.SenderFirstname,
                    SenderLastname = invoice.SenderLastname,
                    SenderTelephoneNumber = invoice.SenderTelephoneNumber,
                    SenderAddress = invoice.SenderAddress,


                    MessageTypes = UnitOfWork.DictionaryRepository.GetAllByCodeAndLevel(1, 2, true).Select(m => new SimpleKeyValueObject<int?, string>
                    {
                        Key = m.ID,
                        Value = m.Caption
                    }).ToList(),

                    MessageModes = UnitOfWork.DictionaryRepository.GetAllByCodeAndLevel(1, 3, true).Select(m => new SimpleKeyValueObject<int?, string>
                    {
                        Key = m.ID,
                        Value = m.Caption
                    }).ToList(),

                    Payers = UnitOfWork.DictionaryRepository.GetAllByCodeAndLevel(1, 4, true).Select(m => new SimpleKeyValueObject<int?, string>
                    {
                        Key = m.ID,
                        Value = m.Caption
                    }).ToList(),

                    FormOfPaments = UnitOfWork.DictionaryRepository.GetAllByCodeAndLevel(1, 5, true).Select(m => new SimpleKeyValueObject<int?, string>
                    {
                        Key = m.ID,
                        Value = m.Caption
                    }).ToList(),

                    ContractNumbers = UnitOfWork.UserRepository.GetAll().Select(m => new SimpleKeyValueObject<int?, string>
                    {
                        Key = m.ID,
                        Value = m.ContractNumber
                    }).ToList(),
                }
            };

            return View("CreateEditInvoice", model);
        }

        [HttpPost]
        [Route("invoices/create")]
        [Route("invoices/{ParentID}/create")]
        public ActionResult CreateInvoice(InvoiceObject model)
        {
            var ajaxResponse = new AjaxResponse();
            var errors = Validation.ValidateCreateEditInvoiceForm(model.InvoiceNumber);

            if (errors.Count == 0)
            {
                var invoice = new Invoice
                {
                    ParentID = model.ParentID,
                    InvoiceNumber = model.InvoiceNumber,
                    MessageTypeID = model.MessageTypeID,
                    ReceiveDate = model.ReceiveDate.ToDateTime(),
                    DeliveryDate = model.DeliveryDate.ToDateTime(),
                    UnitPrice = model.UnitPrice.ToDecimal(),
                    TotalPrice = model.TotalPrice.ToDecimal(),
                    Direction = model.Direction,
                    MessageModeID = model.MessageModeID,
                    PayerID = model.PayerID,
                    FormOfPaymentID = model.FormOfPaymentID,
                    Quantity = model.Quantity.ToDecimal(),
                    Weigth = model.Weigth.ToDecimal(),

                    UserID = model.UserID,
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
                    ajaxResponse.Data = new
                    {
                        Message = Resources.Abort
                    };
                }
                else
                {
                    ajaxResponse.IsSuccess = true;
                    ajaxResponse.Data = new
                    {
                        RedirectUrl = Url.RouteUrl("InvoicesEdit", new { ID = invoice.ID })
                    };
                }

            }
            else
            {
                ajaxResponse.Data = new
                {
                    ErrorsJson = errors.ToJson()
                };
            }

            return Json(ajaxResponse);
        }

        [HttpPost]
        [Route("invoices/get-sender-information", Name = "GetSenderInformation")]
        public ActionResult GetSenderInformation(int? ID)
        {
            var ajaxResponse = new AjaxResponse();
            var user = UnitOfWork.UserRepository.Get(ID);

            if (user == null)
            {
                return NotFound();
            }
            else
            {
                ajaxResponse.IsSuccess = true;
                ajaxResponse.Data = new
                {
                    Sender = user,
                    IsSenderObjectNull = user.ID == null
                };

                return Json(ajaxResponse);
            }

        }

        [Route("invoices/{ID}/edit", Name = "InvoicesEdit")]
        public ActionResult EditInvoice(int? ID)
        {
            GenerateSuccessErrorMessageContainer();
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
                    GetSenderInformationUrl = Url.RouteUrl("GetSenderInformation"),
                    InvoicesUrl = Url.RouteUrl("Invoices"),
                    Title = "ზედნადების რედაქტირება",
                    AddNewInvoiceUrl = invoice.ParentID == null ? Url.RouteUrl("InvoicesCreateChild", new { parentID = invoice.ID }) : Url.RouteUrl("InvoicesCreateChild", new { parentID = invoice.ParentID }),
                    HasAddNewButton = true,
                    InvoiceObject = new InvoiceObject
                    {
                        ParentID = invoice.ParentID ?? 0,
                        InvoiceNumber = invoice.InvoiceNumber,
                        MessageTypeID = invoice.MessageTypeID,
                        ReceiveDate = invoice.ReceiveDate?.ToString(Resources.CustomDateFormat),
                        DeliveryDate = invoice.DeliveryDate?.ToString(Resources.CustomDateFormat),
                        UnitPrice = $"{invoice.UnitPrice:0.00}",
                        TotalPrice = $"{invoice.TotalPrice:0.00}",
                        Direction = invoice.Direction,
                        MessageModeID = invoice.MessageModeID,
                        PayerID = invoice.PayerID,
                        FormOfPaymentID = invoice.FormOfPaymentID,
                        Quantity = $"{invoice.Quantity:0.}",
                        Weigth = $"{invoice.Weigth:0.00}",

                        UserID = invoice.UserID,
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
                        WhoReceivedAdditional = invoice.WhoReceivedAdditional,

                        MessageTypes = UnitOfWork.DictionaryRepository.GetAllByCodeAndLevel(1, 2, true).Select(m => new SimpleKeyValueObject<int?, string>
                        {
                            Key = m.ID,
                            Value = m.Caption
                        }).ToList(),

                        MessageModes = UnitOfWork.DictionaryRepository.GetAllByCodeAndLevel(1, 3, true).Select(m => new SimpleKeyValueObject<int?, string>
                        {
                            Key = m.ID,
                            Value = m.Caption
                        }).ToList(),

                        Payers = UnitOfWork.DictionaryRepository.GetAllByCodeAndLevel(1, 4, true).Select(m => new SimpleKeyValueObject<int?, string>
                        {
                            Key = m.ID,
                            Value = m.Caption
                        }).ToList(),

                        FormOfPaments = UnitOfWork.DictionaryRepository.GetAllByCodeAndLevel(1, 5, true).Select(m => new SimpleKeyValueObject<int?, string>
                        {
                            Key = m.ID,
                            Value = m.Caption
                        }).ToList(),

                        ContractNumbers = UnitOfWork.UserRepository.GetAll().Select(m => new SimpleKeyValueObject<int?, string>
                        {
                            Key = m.ID,
                            Value = m.ContractNumber
                        }).ToList(),
                    }
                };
                return View("CreateEditInvoice", model);

            }
        }

        [HttpPost]
        [Route("invoices/{ID}/edit")]
        public ActionResult EditInvoice(InvoiceObject model)
        {
            var ajaxResponse = new AjaxResponse();
            var errors = Validation.ValidateCreateEditInvoiceForm(model.InvoiceNumber);

            if (errors.Count == 0)
            {
                UnitOfWork.InvoiceRepository.Update(new Invoice
                {
                    ID = model.ID,
                    ParentID = model.ParentID,
                    InvoiceNumber = model.InvoiceNumber,
                    MessageTypeID = model.MessageTypeID,
                    ReceiveDate = model.ReceiveDate.ToDateTime(),
                    DeliveryDate = model.DeliveryDate.ToDateTime(),
                    UnitPrice = model.UnitPrice.ToDecimal(),
                    TotalPrice = model.TotalPrice.ToDecimal(),
                    Direction = model.Direction,
                    MessageModeID = model.MessageModeID,
                    PayerID = model.PayerID,
                    FormOfPaymentID = model.FormOfPaymentID,
                    Quantity = model.Quantity.ToDecimal(),
                    Weigth = model.Weigth.ToDecimal(),

                    UserID = model.UserID,
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
                    ajaxResponse.Data = new
                    {
                        Message = Resources.Abort
                    };
                }
                else
                {
                    ajaxResponse.IsSuccess = true;
                    ajaxResponse.Data = new
                    {
                        Message = Resources.Success
                    };
                }
            }
            else
            {
                ajaxResponse.Data = new
                {
                    ErrorsJson = errors.ToJson()
                };
            }

            return Json(ajaxResponse);
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

        [Route("invoices/export-to-excel", Name = "InvoicesExportToExcel")]
        public ActionResult InvoicesExportToExcel(DateTime? dateFrom, DateTime? dateTo)
        {
            var invoices = UnitOfWork.InvoiceRepository.GetInvoicesForExportExcel(dateFrom, dateTo).Select(i => new InvoicesExportToExcelViewModel
            {
                InvoiceNumber = i.InvoiceNumber,
                MessageTypeCaption = i.MessageType.Caption,
                ReceiveDate = i.ReceiveDate?.ToString(Resources.CustomDateFormat),
                DeliveryDate = i.DeliveryDate?.ToString(Resources.CustomDateFormat),
                UnitPrice = $"{i.UnitPrice:0.00}",
                TotalPrice = $"{i.TotalPrice:0.00}",
                Direction = i.Direction,
                MessageModeCaption = i.MessageMode.Caption,
                PayerCaption = i.Payer.Caption,
                FormOfPaymentCaption = i.FormOfPayment.Caption,
                Quantity = $"{i.Quantity:0.}",
                Weigth = $"{i.Weigth:0.00}",

                ContractNumber = i.User.ContractNumber,
                CompanyName = i.CompanyName,
                SenderFirstname = i.SenderFirstname,
                SenderLastname = i.SenderLastname,
                SenderAddress = i.SenderAddress,
                SenderTelephoneNumber = i.SenderTelephoneNumber,

                ReceiverFirstname = i.ReceiverFirstname,
                ReceiverLastname = i.ReceiverLastname,
                ReceiverTelephoneNumber = i.ReceiverTelephoneNumber,
                ReceiverAddress = i.ReceiverAddress,
                WhoReceived = i.WhoReceived,
                WhoReceivedAdditional = i.WhoReceivedAdditional

            }).ToList();

            InvoiceHelper.InitInvoicesDataForExcel(invoices, this);

            return Redirect(Url.RouteUrl("Invoices"));
        }

        // TODO - Imort invoices from excel file
        [Route("invoices/import-from-excel", Name = "InvoicesImportFromExcel")]
        public ActionResult InvoicesImportFromExcel(HttpPostedFileBase file)
        {
            if (file == null)
            {
                InitErrorMessage(Resources.Abort);
            }
            else
            {
                var fileName = file.FileName;
                var fileContentType = file.ContentType;
                var fileBytes = new byte[file.ContentLength];
                var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));

                using (var package = new ExcelPackage(file.InputStream))
                {
                    var currentSheet = package.Workbook.Worksheets;
                    var workSheet = currentSheet.First();
                    var noOfCol = workSheet.Dimension.End.Column;
                    var noOfRow = workSheet.Dimension.End.Row;

                }
            }


            return Redirect(Url.RouteUrl("Invoices"));
        }
    }
}