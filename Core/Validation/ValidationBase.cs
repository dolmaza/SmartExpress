using System.Collections.Generic;

namespace Core.Validation
{
    public class CustomError
    {
        #region Properties
        public int Code { get; set; }
        public int SubCode { get; set; }
        public string ErrorMessage { get; set; }
        #endregion Properties
    }

    public class CustomErrors
    {
        public static CustomError UsersContractNumberRequired { get; set; } = new CustomError { Code = 1, ErrorMessage = "" };
        public static CustomError UsersRoleRequired { get; set; } = new CustomError { Code = 2, ErrorMessage = "" };

        public static CustomError DictionariesCaptionRequired { get; set; } = new CustomError { Code = 3, ErrorMessage = "" };
        public static CustomError DictionariesDictionaryCodeRequired { get; set; } = new CustomError { Code = 4, ErrorMessage = "" };

        public static CustomError InvoicesInvoceNumberRequired { get; set; } = new CustomError { Code = 5, ErrorMessage = "" };



    }

    public class ValidationBase
    {
        #region Methods

        public static CustomError ValidateContractNumber(string contractNumber)
        {
            if (string.IsNullOrWhiteSpace(contractNumber))
            {
                return CustomErrors.UsersContractNumberRequired;
            }
            return null;
        }

        public static CustomError ValidateRole(int? roleID)
        {
            if (roleID == null)
            {
                return CustomErrors.UsersRoleRequired;
            }
            return null;
        }

        public static CustomError ValidateCaption(string caption)
        {
            if (string.IsNullOrWhiteSpace(caption))
            {
                return CustomErrors.DictionariesCaptionRequired;
            }

            return null;
        }

        public static CustomError ValidateDictionaryCode(int? dictionaryCode)
        {
            if (dictionaryCode == null)
            {
                return CustomErrors.DictionariesDictionaryCodeRequired;
            }

            return null;
        }

        public static CustomError ValidateInvoiceNubmer(string invoiceNumber)
        {
            if (string.IsNullOrWhiteSpace(invoiceNumber))
            {
                return CustomErrors.InvoicesInvoceNumberRequired;
            }

            return null;
        }

        #endregion Methods
    }

    public class Validation : ValidationBase
    {
        public static List<CustomError> ValidateCreateEditUserFrom(string contractNumber)
        {
            var errors = new List<CustomError>()
            {
                ValidateContractNumber(contractNumber)
            };

            errors.RemoveAll(e => e == null);

            return errors;
        }

        public static List<CustomError> ValdiateCreateEditDictionaryForm(string caption, int? dictionaryCode)
        {
            var errors = new List<CustomError>
            {
                ValidateCaption(caption),
                ValidateDictionaryCode(dictionaryCode)
            };
            errors.RemoveAll(e => e == null);

            return errors;
        }

        public static List<CustomError> ValidateCreateEditInvoiceForm(string invoiceNumber)
        {
            var errors = new List<CustomError>
            {
                ValidateInvoiceNubmer(invoiceNumber)
            };

            errors.RemoveAll(e => e == null);

            return errors;
        }
    }
}
