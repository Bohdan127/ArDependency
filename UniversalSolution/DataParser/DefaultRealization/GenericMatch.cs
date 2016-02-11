namespace DataParser.DefaultRealization
{
    public class GenericMatch
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Контора
        /// </summary>
        public string Office { get; set; }
        /// <summary>
        /// Вид спорта
        /// </summary>
        public string SportName { get; set; }
        /// <summary>
        /// Событие
        /// </summary>
        public string Champ { get; set; }
        /// <summary>
        /// Время игры
        /// </summary>
        public string GameTime { get; set; } //todo чи стрінг чи тайм????
        /// <summary>
        /// Тип ставки
        /// </summary>
        public string BetType { get; set; }
        /// <summary>
        /// Индикатор1
        /// </summary>
        public byte Indicator1 { get; set; }
        /// <summary>
        /// Индикатор2
        /// </summary>
        public double Indicator2 { get; set; }
        /// <summary>
        /// Индикатор3
        /// </summary>
        public double Indicator3 { get; set; }
        /// <summary>
        /// Коефициент
        /// </summary>
        public double Сoefficient { get; set; }
        /// <summary>
        /// % Вилки 
        /// </summary>
        public double ForkPercent { get; set; }
        /// <summary>
        /// Время  
        /// </summary>
        public string Time { get; set; } //todo знову який тип???????????
    }

}
