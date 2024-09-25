using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Нехай існує кілька стратегій торгів на криптобіржі.
//Кожна стратегія -це об'єкт, що приймає історію (список) цін криптовалюти
//і визначає ціну купівлі та ціну продажу. Існує кілька стратегій:
//"Жадібна" - ціна купівлі рівна мінімальній із цін в історії, а ціна продажу - максимальні та
//"Стратегія середньої ціни" для якої ціна купівлі та ціна продажу рівні середньому арифметичному цін історії.  
//Також нехай існує кілька Криптобірж (Binance, Coinbase, тощо) і
//Клас, що має методи для отримання історії торгів та поточного курсу
//(для простоти можна історію вважати константою, а поточний курс - випадковим числом)
//Необхідно створити Підсистему торгового бота, яка за певною стратегією торгів та поточним
//значенням курсу приймає рішення продавати (якщо поточна ціна вища за ціну продажу), купувати (
//якщо поточна ціна нижча за ціну купівлі) чи тримати. Передбачити, що в майбутньому може бути
//більше стратегій та криптобірж.
namespace Cry7ptoBot
{
    public abstract class GetData
    {
        public abstract (double sell, double buy) getPrices(List<double> dataprices);
    }
    class Agressivestrategy : GetData
    {
        public override (double sell, double buy) getPrices(List<double> dataprices)
        {
            double min = dataprices.Min(); 
            double max = dataprices.Max();
            return (max, min); 
        }
    }
    class PoliteStrategy : GetData
    {
        public override (double sell, double buy) getPrices(List<double> dataprices)
        {
            double average = dataprices.Average();
            return (average, average); 
        }
    }
    public class DataSource
    {
        public List<double> dataPrices()
        {
            return new List<double> { 123, 345, 657, 456, 34, 578356, 342, 65, 545, 56 };
        }
        public double dataCurrentPrices()
        {
            Random rand = new Random();
            return rand.Next(50, 600);
        }
    }
    class CryptoBot
    {
        private DataSource _data;
        private GetData _priceHistory;
        public CryptoBot(DataSource data, GetData dataSource)
        {
            _data = data;
            _priceHistory = dataSource;
        }
        public void Traiding()
        {
            var pricesHistory = _data.dataPrices();
            var (sell, buy) = _priceHistory.getPrices(pricesHistory);
            double currentPrices = _data.dataCurrentPrices();
            if (currentPrices < buy)
            {
                Console.WriteLine("Buy");
            }
            else if (currentPrices > sell)
            {
                Console.WriteLine("Sell");
            }
            else
            {
                Console.WriteLine("Keep");
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var exchange = new DataSource();
            GetData strategy = new Agressivestrategy(); 
            var bot = new CryptoBot(exchange, strategy);
            bot.Traiding(); 
        }
    }
}