using System.Windows;
using System.Windows.Controls;

namespace SignUpApplication.Component
{
    /// <summary>
    /// PasswordBox Password 바인딩을 위한 Custom Attached Property
    /// </summary>
    public class PasswordHelper
    {
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.RegisterAttached("Password",
            typeof(string), typeof(PasswordHelper),
            new FrameworkPropertyMetadata(string.Empty, OnPasswordPropertyChanged));

        public static readonly DependencyProperty AttachProperty =
            DependencyProperty.RegisterAttached("Attach",
            typeof(bool), typeof(PasswordHelper), new PropertyMetadata(false, Attach));

        private static readonly DependencyProperty IsUpdatingProperty =
            DependencyProperty.RegisterAttached("IsUpdating", typeof(bool),
            typeof(PasswordHelper));


        public static void SetAttach(DependencyObject dp, bool value)
        {
            dp.SetValue(AttachProperty, value);
        }

        public static bool GetAttach(DependencyObject dp)
        {
            return (bool)dp.GetValue(AttachProperty);
        }

        public static string GetPassword(DependencyObject dp)
        {
            return (string)dp.GetValue(PasswordProperty);
        }

        public static void SetPassword(DependencyObject dp, string value)
        {
            dp.SetValue(PasswordProperty, value);
        }

        private static bool GetIsUpdating(DependencyObject dp)
        {
            return (bool)dp.GetValue(IsUpdatingProperty);
        }

        private static void SetIsUpdating(DependencyObject dp, bool value)
        {
            dp.SetValue(IsUpdatingProperty, value);
        }

        /// <summary>
        /// PasswordHelper.Password Property Changed EventHandler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnPasswordPropertyChanged(DependencyObject sender,
            DependencyPropertyChangedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            passwordBox.PasswordChanged -= PasswordChanged;

            // PasswordChanged Method에 의한 변경이 아닌 Property 직접 변경이 될 경우
            // PasswordBox의 Password를 변경된 값과 동일하게 변경.
            if (!(bool)GetIsUpdating(passwordBox)) //PasswordBox 입력 통한 변경 시 아래 코드가 동작하지 않도록 하는 조건.
            {
                passwordBox.Password = (string)e.NewValue;
            }
            passwordBox.PasswordChanged += PasswordChanged;
        }

        /// <summary>
        /// Attach 설정 Method
        /// Attach 시 PasswordChanged EventHandler 지정
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Attach(DependencyObject sender,
            DependencyPropertyChangedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;

            if (passwordBox == null)
                return;

            if ((bool)e.OldValue) //변경 전 값이 True인 경우 EventHandler 해제
            {
                passwordBox.PasswordChanged -= PasswordChanged;
            }

            if ((bool)e.NewValue) //변경 후 값이 True인 경우 EventHandler 지정
            {
                passwordBox.PasswordChanged += PasswordChanged;
            }
        }
        /// <summary>
        /// PasswordChanged Event Handler
        /// PasswordBox Password 변경 처리 Method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            SetIsUpdating(passwordBox, true);//Password 업데이트에 대한 재귀호출 방지 Flag
            SetPassword(passwordBox, passwordBox.Password);//Password Attached Property 업데이트.
            SetIsUpdating(passwordBox, false);
        }
    }
}
