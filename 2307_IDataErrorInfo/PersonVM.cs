using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WpfApp6
{
    public class PersonVM : IDataErrorInfo
    {
        private string mobileNumber;
        public string MobileNumber
        {
            get { return mobileNumber; }
            set
            {
                mobileNumber = value;

            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;

            }
        }

        public string Error
        {
            get
            {
                return string.Empty;
            }
        }

        public string this[string propertyName]
        {
            get
            {
                return GetErrorForProperty(propertyName);
            }
        }

        private string GetErrorForProperty(string propertyName)
        {
            switch (propertyName)
            {
                case "MobileNumber":
                    Regex regex = new Regex(@"^\D?(\d{3})\D?\D?(\d{3})\D?(\d{4})$");
                    if (MobileNumber == null || regex.Match(MobileNumber) == Match.Empty)
                        return "Invalid Phone Format";
                    else return string.Empty;
                default:
                    return string.Empty;
            }
        }

    }
}
