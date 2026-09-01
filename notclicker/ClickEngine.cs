using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace notclicker
{
    public class ClickEngine
    {
        //я знаю что мог написать это через Keyboardhook но узнал я про эту библиотеку только когда решил писать хуки клавиш
        //мной было принято решение оставить winapi метод в пользу скорости работы т.к хуки клавиатуры писать сложно а простой кликер лёгок в написании
        //импорт системмной дллки
        [DllImport("user32.dll")]
        //состояния мыши
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);
        private const uint MOUSEEVENTF_LEFTDOWN = 0x0002; //нажал
        private const uint MOUSEEVENTF_LEFTUP = 0x0004; //отпустил

        //проверка на кликанье
        public bool isclicking = false;
        //стандартная задержка между кликами
        private int clickdelay = 10;

        //защита от краша компа
        public int ClickDelay
        {
            get {  return clickdelay; } //возвращает нам clickdelay при попытке получить переменную из другой части кода
            set
            {
                if (value < 10) //проверка на задержку менее 10 мс
                {
                    clickdelay = 10;
                }
                else //если задержка приемлима то ставится то что поставил пользователь
                {
                    clickdelay = value;
                }
            }

        }
        public void Start()
        {
            if (!isclicking) //проверка кликает ли сейчас программа
            {
                isclicking = true; //установка состояния кликания в true
                _ = ClickAsync(); //включение самого кликера

            }
            

        }
        public void Stop() //метод остановки 
        {
            isclicking = false;
        }


        private async Task ClickAsync() //метод самого кликера
        {
            while (isclicking) //цикл при включенном isclicking
            {
                mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0); //нажатие
                mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0); //отжатие
                
                //ассинхронная пауза
                await Task.Delay(clickdelay);

            }



        }
    }
}
