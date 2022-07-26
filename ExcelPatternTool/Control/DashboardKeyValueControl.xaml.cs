using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ExcelPatternTool.Control
{
    /// <summary>
    /// DashboardKeyValueControl.xaml 的交互逻辑
    /// </summary>
    public partial class DashboardKeyValueControl : UserControl
    {
        public DashboardKeyValueControl()
        {
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            //if (this.FontSize != default(double))
            //{
            //    this.KeyLabel.FontSize = FontSize;
            //    this.ValueLabel.FontSize = FontSize;

            //}


        }

        public static DependencyProperty KeyTextProperty =
            DependencyProperty.Register("KeyText", typeof(string), typeof(DashboardKeyValueControl), new FrameworkPropertyMetadata(KeyTextPropertyChanged));

        private static void KeyTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dashboardKeyValueControl = d as DashboardKeyValueControl;
            if (dashboardKeyValueControl != null)
                dashboardKeyValueControl.KeyLabel.Content = dashboardKeyValueControl.KeyText;
        }

        public string KeyText
        {
            get { return (string)GetValue(KeyTextProperty); }
            set { SetValue(KeyTextProperty, value); }
        }

        public static DependencyProperty ValueTextProperty =
          DependencyProperty.Register("ValueText", typeof(string), typeof(DashboardKeyValueControl), new FrameworkPropertyMetadata(ValueTextPropertyChanged));

        private static void ValueTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dashboardKeyValueControl = d as DashboardKeyValueControl;
            if (dashboardKeyValueControl != null)
                dashboardKeyValueControl.ValueLabel.Text = dashboardKeyValueControl.ValueText;
        }

        public string ValueText
        {
            get { return (string)GetValue(ValueTextProperty); }
            set { SetValue(ValueTextProperty, value); }
        }


        public static DependencyProperty UnitTextProperty =
          DependencyProperty.Register("UnitText", typeof(string), typeof(DashboardKeyValueControl), new FrameworkPropertyMetadata(UnitTextPropertyChanged));

        private static void UnitTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dashboardKeyValueControl = d as DashboardKeyValueControl;
            if (dashboardKeyValueControl != null)
                dashboardKeyValueControl.UnitLabel.Content = dashboardKeyValueControl.UnitText;
        }

        public string UnitText
        {
            get { return (string)GetValue(UnitTextProperty); }
            set { SetValue(UnitTextProperty, value); }
        }

        public static DependencyProperty TextSizeProperty =
          DependencyProperty.Register("TextSize", typeof(double), typeof(DashboardKeyValueControl), new FrameworkPropertyMetadata(TextSizePropertyChanged));

        private static void TextSizePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dashboardKeyValueControl = d as DashboardKeyValueControl;
            if (dashboardKeyValueControl != null)
            {
                dashboardKeyValueControl.ValueLabel.FontSize = (dashboardKeyValueControl.TextSize);
                dashboardKeyValueControl.KeyLabel.FontSize = (dashboardKeyValueControl.TextSize);
                dashboardKeyValueControl.UnitLabel.FontSize = dashboardKeyValueControl.TextSize;
            }
        }

        public double TextSize
        {
            get { return Convert.ToDouble(GetValue(TextSizeProperty)); }
            set { SetValue(TextSizeProperty, value); }
        }


        public static DependencyProperty TextColorProperty =
          DependencyProperty.Register("TextColor", typeof(Brush), typeof(DashboardKeyValueControl), new FrameworkPropertyMetadata(TextColorPropertyChanged));

        private static void TextColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dashboardKeyValueControl = d as DashboardKeyValueControl;
            if (dashboardKeyValueControl != null)
            {
                dashboardKeyValueControl.ValueLabel.Foreground = (dashboardKeyValueControl.TextColor);
                dashboardKeyValueControl.KeyLabel.Foreground = (dashboardKeyValueControl.TextColor);
                dashboardKeyValueControl.UnitLabel.Foreground = (dashboardKeyValueControl.TextColor);
            }
        }

        public Brush TextColor
        {
            get { return (Brush)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }
    }
}
