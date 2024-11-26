using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfCalculator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Обработчик события для кнопки "Рассчитать"
        private void Calculate(object sender, RoutedEventArgs e)
        {
            double num1, num2 = 0, result = 0;
            string operation = (Operation.SelectedItem as ComboBoxItem)?.Content.ToString();

            // Проверка корректности ввода для первого числа
            if (!double.TryParse(Number1.Text, out num1))
            {
                MessageBox.Show("Пожалуйста, введите корректное первое число.");
                return;
            }

            // Если второе число не пустое, проверяем его
            if (Number2.Visibility == Visibility.Visible && !string.IsNullOrWhiteSpace(Number2.Text))
            {
                if (!double.TryParse(Number2.Text, out num2))
                {
                    MessageBox.Show("Пожалуйста, введите корректное второе число.");
                    return;
                }
            }

            // Выполнение выбранной операции
            switch (operation)
            {
                case "Сложение (+)":
                    result = num1 + num2;
                    break;
                case "Вычитание (-)":
                    result = num1 - num2;
                    break;
                case "Умножение (*)":
                    result = num1 * num2;
                    break;
                case "Деление (/)":
                    if (num2 == 0)
                    {
                        MessageBox.Show("На ноль делить нельзя!");
                        return;
                    }
                    result = num1 / num2;
                    break;
                case "Возведение в степень (^)":
                    result = Math.Pow(num1, num2);
                    break;
                case "Факториал (!)":
                    if (num1 < 0 || num1 % 1 != 0)
                    {
                        MessageBox.Show("Число для факториала должно быть целым и неотрицательным.");
                        return;
                    }
                    result = Factorial((int)num1);
                    break;
                default:
                    MessageBox.Show("Выберите операцию.");
                    return;
            }

            ResultText.Text = $"Результат: {result}";
        }

        // Функция для вычисления факториала
        private double Factorial(int number)
        {
            if (number == 0) return 1;
            double fact = 1;
            for (int i = 1; i <= number; i++)
            {
                fact *= i;
            }
            return fact;
        }

        // Закрытие приложения
        private void CloseApp(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Обработчик для удаления текста по умолчанию при фокусе
        private void ClearPlaceholder(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && (textBox.Text == "Введите первое число" || textBox.Text == "Введите второе число (если нужно)"))
            {
                textBox.Text = "";
            }
        }

        // Обработчик для восстановления текста по умолчанию при потере фокуса
        private void SetPlaceholder(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
            {
                if (textBox.Name == "Number1")
                {
                    textBox.Text = "Введите первое число";
                }
                else if (textBox.Name == "Number2")
                {
                    textBox.Text = "Введите второе число (если нужно)";
                }
            }
        }
    }
}
