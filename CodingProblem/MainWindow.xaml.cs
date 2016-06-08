using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PatternLibraryDLL;
using System.ComponentModel;

namespace CodingProblem
{
    public class TextHandler : INotifyPropertyChanged
    {
        public TextHandler(TextBlock textBlockRef)
        {
            _textBlockRef = textBlockRef;
        }

        private TextBlock _textBlockRef;
        public event PropertyChangedEventHandler PropertyChanged;

        public string Text
        {
            get
            {
                return _textBlockRef.Text;
            }

            set
            {
                if (value != _textBlockRef.Text)
                {
                    _textBlockRef.Text = value;

                    if (this.PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Text"));
                    }
                }
            }
        }
    }

	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
        private TextHandler textHandler;
        private RegisterClass register;
        private DiagnoseClass diagnose;
        
        private CustomEventManager registerEventManager;
        private CustomEventManager diagnoseEventManager;
        private CustomEventListener registerListener;
        private CustomEventListener diagnoseListener;

        public MainWindow()
		{
			InitializeComponent();

            textHandler = new TextHandler(ResultText);
            register = new RegisterClass();
            diagnose = new DiagnoseClass();

            registerEventManager = new CustomEventManager();
            registerListener = new CustomEventListener(register, textHandler.Text);
            registerEventManager.Handler += registerListener.Execute;
            diagnoseEventManager = new CustomEventManager();
            diagnoseListener = new CustomEventListener(diagnose, textHandler.Text);
            diagnoseEventManager.Handler += diagnoseListener.Execute;
        }

		private void Register_Click(object sender, RoutedEventArgs e)
		{
            registerEventManager.Trigger();
            textHandler.Text = registerListener.output;
        }

        private void Diagnose_Click(object sender, RoutedEventArgs e)
        {
            diagnoseEventManager.Trigger();
            textHandler.Text = diagnoseListener.output;
        } 
    }
}
