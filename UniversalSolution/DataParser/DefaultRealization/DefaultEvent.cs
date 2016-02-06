﻿using DataParser.Interfaces;

namespace DataParser.DefaultRealization
{
    internal class DefaultEvent : IDataEvent
    {
        /// <summary>
        ///  П1
        /// </summary>
        public double P1 { get; set; }

        /// <summary>
        /// Х
        /// </summary>
        public double X { get; set; }

        /// <summary>
        ///П2
        /// </summary>
        public double P2 { get; set; }

        /// <summary>
        ///1Х
        /// </summary>
        public double X1 { get; set; }

        /// <summary>
        ///12
        /// </summary>
        public double I2 { get; set; }

        /// <summary>
        ///Х2
        /// </summary>
        public double X2 { get; set; }

        /// <summary>
        ///Фора 1
        /// </summary>
        public double Fora1 { get; set; }

        /// <summary>
        ///1
        /// </summary>
        public double I { get; set; }

        /// <summary>
        ///Фора 2
        /// </summary>
        public double Fora2 { get; set; }

        /// <summary>
        ///2
        public double II { get; set; }

        /// <summary>
        ///Тотал
        /// </summary>
        public double Total { get; set; }

        /// <summary>
        ///Б
        /// </summary>
        public double B { get; set; }

        /// <summary>
        ///М
        /// </summary>
        public double M { get; set; }
    }
}