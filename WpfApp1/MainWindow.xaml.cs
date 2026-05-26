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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int priceMargarita = 450;
        private int pricePepperoni = 550;
        private int priceHawaii = 500;
        private int priceCola = 100;
        private int priceJuice = 80;
        private int priceWater = 50;

        // Цены дополнительных ингредиентов
        private int priceCheese = 50;
        private int priceBacon = 70;
        private int priceOlives = 40;
        private int priceMushrooms = 60;

        // Стоимость доставки
        private int deliveryPrice = 150;

        // Скидка по промокоду
        private double discountRate = 0.1; // 10%

        public MainWindow()
        {
            InitializeComponent();
        }

        // Получаем цену выбранной пиццы
        private int GetPizzaPrice()
        {
            if (MargaritaRbtn.IsChecked == true)
                return priceMargarita;
            if (PepperoniRbtn.IsChecked == true)
                return pricePepperoni;
            else
                return priceHawaii;
        }

        // Получаем название пиццы
        private string GetPizzaName()
        {
            if (MargaritaRbtn.IsChecked == true)
                return "Маргарита";
            if (PepperoniRbtn.IsChecked == true)
                return "Пепперони";
            else
                return "Гавайская";
        }

        // Получаем количество
        private int GetQuantity()
        {
            if (int.TryParse(QuantityTb.Text, out int quantity))
                return quantity;
            return 1;
        }

        // Получаем цену напитков
        private int GetDrinkPrice()
        {
            if (DrinkChbx.IsChecked != true)
                return 0;

            if (ColaRbtn.IsChecked == true)
                return priceCola;
            if (JuiceRbtn.IsChecked == true)
                return priceJuice;
            else
                return priceWater;
        }

        // Получаем название напитков
        private string GetDrinkName()
        {
            if (DrinkChbx.IsChecked != true)
                return "не выбран";

            if (ColaRbtn.IsChecked == true)
                return "Кола";
            if (JuiceRbtn.IsChecked == true)
                return "Сок";
            else
                return "Вода";
        }

        // Получаем стоимость дополнительных ингредиентов
        private int GetExtrasPrice()
        {
            int extrasPrice = 0;
            if (CheeseChbx.IsChecked == true)
                extrasPrice += priceCheese;
            if (BaconChbx.IsChecked == true)
                extrasPrice += priceBacon;
            if (OlivesChbx.IsChecked == true)
                extrasPrice += priceOlives;
            if (MushroomsChbx.IsChecked == true)
                extrasPrice += priceMushrooms;
            return extrasPrice;
        }

        // Получаем список выбранных ингредиентов
        private string GetExtrasList()
        {
            List<string> extras = new List<string>();
            if (CheeseChbx.IsChecked == true) extras.Add("Сыр");
            if (BaconChbx.IsChecked == true) extras.Add("Бекон");
            if (OlivesChbx.IsChecked == true) extras.Add("Оливки");
            if (MushroomsChbx.IsChecked == true) extras.Add("Грибы");

            if (extras.Count == 0)
                return "нет";
            return string.Join(", ", extras);
        }

        // Получаем стоимость доставки
        private int GetDeliveryPrice()
        {
            if (DeliveryRbtn.IsChecked == true)
                return deliveryPrice;
            return 0;
        }

        // Получаем способ доставки
        private string GetDeliveryType()
        {
            if (DeliveryRbtn.IsChecked == true)
                return "Доставка";
            return "Самовывоз";
        }

        // Проверяем промокод и получаем скидку
        private double GetDiscount()
        {
            if (PromocodeTb.Text.ToUpper() == "PROMO10")
                return discountRate;
            return 0;
        }

        private void UpdateTotal()
        {
            int pizzaPrice = GetPizzaPrice();
            int quantity = GetQuantity();
            int drinkPrice = GetDrinkPrice();
            int extrasPrice = GetExtrasPrice();
            int deliveryPrice = GetDeliveryPrice();
            
    int subtotal = (pizzaPrice * quantity) + drinkPrice + extrasPrice + deliveryPrice;

            // Применяем скидку
            double discount = GetDiscount();
            int total = (int)(subtotal * (1 - discount));

            // Окрашиваем итоговую сумму зеленым, если заказ больше 1000 руб
            if (total > 1000)
                TotalTl.Foreground = new SolidColorBrush(Colors.Green);
            else
                TotalTl.Foreground = new SolidColorBrush(Colors.Red);

            TotalTl.Text = $"ИТОГО: {total} руб.";

            // Выделяем выбранные элементы жирным
            HighlightSelectedItems();
        }

        // Выделение выбранных элементов жирным шрифтом
        private void HighlightSelectedItems()
        {
            // Выделение пиццы
            MargaritaRbtn.FontWeight = MargaritaRbtn.IsChecked == true ? FontWeights.Bold : FontWeights.Normal;
            PepperoniRbtn.FontWeight = PepperoniRbtn.IsChecked == true ? FontWeights.Bold : FontWeights.Normal;
            HawaiiRbtn.FontWeight = HawaiiRbtn.IsChecked == true ? FontWeights.Bold : FontWeights.Normal;

            // Выделение напитков
            ColaRbtn.FontWeight = (DrinkChbx.IsChecked == true && ColaRbtn.IsChecked == true) ? FontWeights.Bold : FontWeights.Normal;
            JuiceRbtn.FontWeight = (DrinkChbx.IsChecked == true && JuiceRbtn.IsChecked == true) ? FontWeights.Bold : FontWeights.Normal;
            WaterRbtn.FontWeight = (DrinkChbx.IsChecked == true && WaterRbtn.IsChecked == true) ? FontWeights.Bold : FontWeights.Normal;

            // Выделение доставки
            PickupRbtn.FontWeight = PickupRbtn.IsChecked == true ? FontWeights.Bold : FontWeights.Normal;
            DeliveryRbtn.FontWeight = DeliveryRbtn.IsChecked == true ? FontWeights.Bold : FontWeights.Normal;
        }

        private void AnyParameter_Changed(object sender, RoutedEventArgs e)
        {
            UpdateTotal();
        }

        private void MinusBtn_Click(object sender, RoutedEventArgs e)
        {
            int quantity = GetQuantity();
            if (quantity > 1)
            {
                QuantityTb.Text = (quantity - 1).ToString();
                UpdateTotal();
            }
        }

        private void PlusBtn_Click(object sender, RoutedEventArgs e)
        {
            int quantity = GetQuantity();
            if (quantity < 10)
            {
                QuantityTb.Text = (quantity + 1).ToString();
                UpdateTotal();
            }
        }

        private void DrinkChbx_Click(object sender, RoutedEventArgs e)
        {
            bool isEnabled = DrinkChbx.IsChecked == true;
            DrinkPanel.IsEnabled = isEnabled;
            if (!isEnabled)
            {
                WaterRbtn.IsChecked = true;
            }
            UpdateTotal();
        }

        private void PromocodeTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PromocodeTb.Text.ToUpper() == "PROMO10")
            {
                PromocodeMessageTb.Text = "Промокод применен! Скидка 10%";
                PromocodeMessageTb.Foreground = new SolidColorBrush(Colors.Green);
            }
            else
            {
                PromocodeMessageTb.Text = "Введите PROMO10 для скидки 10%";
                PromocodeMessageTb.Foreground = new SolidColorBrush(Colors.Gray);
            }
            UpdateTotal();
        }

        private void OrderBtn_Click(object sender, RoutedEventArgs e)
        {
            string pizzaName = GetPizzaName();
            int quant = GetQuantity();
            string drinkName = GetDrinkName();
            string extrasList = GetExtrasList();
            string deliveryType = GetDeliveryType();
            int pizzaPrice = GetPizzaPrice();
            int drinkPrice = GetDrinkPrice();
            int extrasPrice = GetExtrasPrice();
            int deliveryPrice = GetDeliveryPrice();

            int subtotal = (pizzaPrice * quant) + drinkPrice + extrasPrice + deliveryPrice;
            double discount = GetDiscount();
            int total = (int)(subtotal * (1 - discount));

            string message = "  ВАШ ЗАКАЗ  \n\n" + $"Пицца: {pizzaName}, {quant} шт. = {pizzaPrice * quant} руб.\n" + $"Напиток: {drinkName}";

            if (drinkPrice > 0)
                message += $" = {drinkPrice} руб.\n";
            else
                message += "\n";

            message += $"Доп. ингредиенты: {extrasList}";
            if (extrasPrice > 0)
                message += $" = {extrasPrice} руб.\n";
            else
                message += "\n";

            message += $"Доставка: {deliveryType}";
            if (deliveryPrice > 0)
                message += $" = {deliveryPrice} руб.\n";
            else
                message += "\n";

            if (discount > 0)
                message += $"Скидка по промокоду: {discount * 100}% = -{subtotal - total} руб.\n";

            message += $"\nИТОГО К ОПЛАТЕ: {total} руб. ";

            MessageBox.Show(message, "Заказ оформлен", MessageBoxButton.OK, MessageBoxImage.Information);
            ResetAll();
        }

        private void ReserBtn_Click(object sender, RoutedEventArgs e)
        {
            ResetAll();
        }

        private void ResetAll()
        {
            MargaritaRbtn.IsChecked = true;
            QuantityTb.Text = "1";
            DrinkChbx.IsChecked = false;
            WaterRbtn.IsChecked = true;
            DrinkPanel.IsEnabled = false;

            // Сброс дополнительных ингредиентов
            CheeseChbx.IsChecked = false;
            BaconChbx.IsChecked = false;
            OlivesChbx.IsChecked = false;
            MushroomsChbx.IsChecked = false;

            // Сброс доставки
            PickupRbtn.IsChecked = true;

            // Сброс промокода
            PromocodeTb.Text = "";

            UpdateTotal();
        }
    }
}
