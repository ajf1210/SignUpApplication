using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace SignUpApplication.ViewModel
{
    /// <summary>
    /// MainWindow ViewModel
    /// Relate View : RegEmpInfo(직원정보 입력화면), RegEmpPassword(비밀번호 입력화면), EmpInfo(직원등록정보 조회화면)
    /// </summary>
    public class MainViewModel : INotifyPropertyChanged
    {
        // 직원 정보 Model
        private Model.EmpInfo empInfo;

        // 화면 별 버튼 조작 Command
        public Command btn_cmd { get;set; }

        // 화면전환 Binding 용 UserControl 객체.
        private UserControl userControl;

        // 비밀번호 동일 여부 확인 변수.
        private string passwordCheck = string.Empty;

        public MainViewModel()
        {
            empInfo = new Model.EmpInfo();
            btn_cmd = new Command(Execute_func, CanExecute_func);
            this.userControl = new View.RegEmpInfo();

        }

        public string PasswordCheck
        {
            set { passwordCheck = value; }
            get { return passwordCheck; }
        }

        public Model.EmpInfo EmpInfo
        {
            get { return empInfo; }
            set { empInfo = value; OnPropertyChanged("EmpInfo"); }
        }

        public UserControl UserControl
        {
            get { return userControl; }
            set 
            { 
                userControl = value;
                OnPropertyChanged("UserControl");
             }
        }
        
        /// <summary>
        /// Command 동작 수행 Method
        /// Parameter에 따라 화면 전환 및 프로그램 종료 수행.
        /// </summary>
        /// <param name="obj">Binding된 Control에서 전달되는 Parameter</param>
        private void Execute_func(object obj)
        {
            switch(obj.ToString())
            {
                case "RegEmpInfo":
                    userControl = new View.RegEmpInfo();
                    break;
                case "RegEmpPassword":
                    userControl = new View.RegEmpPassword();
                    break;
                case "EmpInfo":
                    userControl = new View.EmpInfo();
                    break;
                case "Terminate":
                    Application.Current.Shutdown();
                    break;
            }
            OnPropertyChanged(nameof(userControl));
        }
        /// <summary>
        /// Command 동작 수행 조건 판별 Method
        /// </summary>
        /// <param name="obj">Binding된 Control에서 전달되는 Parameter</param>
        /// <returns></returns>
        private bool CanExecute_func(object obj)
        {
            //Button Command가 직원등록정보화면으로 넘어가는 경우
            //비밀번호 입력란과 확인란의 데이터를 비교 후 메시지 출력.
            if (obj.Equals("EmpInfo"))
            {
                if (this.empInfo.Password.Length > 0
                    && !empInfo.Password.Equals(this.passwordCheck))
                {
                    MessageBox.Show("비밀번호를 확인해주세요.", "알림");
                    return false;
                }
            }
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if(handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
