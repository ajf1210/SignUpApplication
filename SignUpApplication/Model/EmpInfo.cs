using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignUpApplication.Model
{
    /// <summary>
    /// 직원 정보 Model
    /// </summary>
    public class EmpInfo : INotifyPropertyChanged
    {
        private string empName = string.Empty
            , empId= string.Empty
            , password = string.Empty
            , empNo = string.Empty;

        //직원 ID
        public string EmpId
        {
            set { empId = value; }
            get { return empId; }
        }
        //직원 번호
        public string EmpNo
        {
            set { empNo = value; }
            get { return empNo; }
        }
        
        //직원 이름
        public string EmpName
        {
            set { empName = value;}
            get { return empName; }
            
        }

        //비밀번호
        public string Password
        {
            set { password = value; }
            get { return password; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if(handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
