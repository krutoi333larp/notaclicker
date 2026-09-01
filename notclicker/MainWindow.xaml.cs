using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using KeyboardHook;
using KeyboardHook.Enums;
using KeyboardHook.Interfaces;
//скрипт предназначен для работы с окном а так же хука клавиш
namespace notclicker
{
    
    public partial class MainWindow : Window
    {
        
        private ClickEngine engine = new ClickEngine(); //движок кликов
        private readonly IKeyboardHook _hook = KeyboardHookFactory.Create(); //хук клавы
        private KeyboardKey _bind; //кнопка бинда
        private string bindtype = "hold"; //тип бинда
        private bool isalistening = false; //проверка на считывание бинда
        private bool pressed = false; //проверка на нажатие для тогл бинда
        public MainWindow()
        {
            
            InitializeComponent();
            _hook.KeyDown += onglobalkeydown;//подписка на нажатие клавиш
            _hook.KeyUp += onglobalkeyup;//подписка на отпускание клавиш

        }
        private void buttonbindclick(object sender, RoutedEventArgs e)
        {
            if (!isalistening)
            {
                isalistening = true; //
                BindBtn.Content = "press key"; //меняет текст кнопки

            }    



        }
        private void onglobalkeydown(KeyboardKey key) //key кнопка которая передалась после нажатия
        {
            

            if (key != _bind) return; //если нажали не нашу кнопку иди нахуй

            else if (key == _bind) //если короче клавиша это бинд 
            {
                if (bindtype == "hold") //проверка на тип
                {
                    if (engine.isclicking == false) //проверка на кликанье(о да проверка в проверке в проверке)
                    {
                        engine.Start();//поехали
                    }
                }
            }

        }
        private void onglobalkeyup(KeyboardKey key) //отжатие клавиши
        {
            if (isalistening)
            {
                _bind = key; //ставит бинд в нажатую кнопку
                isalistening = false; //меняет слушание кнопки на false
                Dispatcher.Invoke(() => //говорит кнопке поменять свой текст
                {
                    BindBtn.Content = $"Бинд: {key}";
                });
            }
            if (key != _bind) return; //если нажали не нашу кнопку иди нахуй

            if (key  == _bind) //проверка что ртпущенная кнопка это бинд
            {
                if (bindtype == "hold" || engine.isclicking == true) //проверка на то что у на сработает холд
                {
                    engine.Stop(); //выключение
                }
                else if (bindtype == "toggle") //альтернативный случай с тоглом
                {
                    pressed = !pressed; //изменение состояния нажатости кнопки
                    if (pressed) //проверка на включенность бинда
                    {
                        engine.Start(); //поехали
                    }
                    else //альтернативный случай
                    { 
                        engine.Stop(); //выключение
                    }
                }

            }



        }
        private void RadioButton_Checked(object sender, RoutedEventArgs e) //переключатель
        {
            if (sender is RadioButton radioButton) //проверка на то что отправитель сигнала кнопка
            {
                bindtype = radioButton.Content.ToString().ToLower(); //перевод в выбранный режим
            }    

        }
        private void InputDelay_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (engine == null || InputDelay == null) return; //проверка на существование движка и делея
            
            if (int.TryParse(InputDelay.Text, out int newDelay)) //перевод делея в int
            {
                engine.ClickDelay = newDelay; //установка нового делея

            }
        }
        
    }
}