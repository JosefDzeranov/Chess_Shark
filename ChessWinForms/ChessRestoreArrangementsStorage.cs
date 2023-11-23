using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sachy_Obrazky
{
    public static class ChessRestoreArrangementsStorage
    {
        /// <summary>
        /// Создание схем расположения из файла сохранения
        /// </summary>
        /// <param name="fen"></param>
        /// <returns></returns>
        public static string[] CreateArrangementFromFen(string fen)
        {
            var arrangement = new string[8];
            var arrangementPart = fen.Split(' ')[0];  // выделяем партию
            var ranks = arrangementPart.Split('/').ToList(); // разбиваем партию по рядам

            for(var y = 0; y < 8; y++)
            {
                var stringBuilder = new StringBuilder();
                var rank = ranks[y];
                for(var x = 0; x < rank.Length; x++)
                {
                    var symbol = rank[x];
                    if(char.IsDigit(symbol))
                    {
                        for(var i = 0; i < symbol - '0'; i++) //числа в строке - означают пустые промежутки между фигурами количество пустых клеток до границ
                        {
                            stringBuilder.Append('#');
                        }
                    }
                    else
                    {
                        stringBuilder.Append(symbol);
                    }
                    arrangement[y] = stringBuilder.ToString();
                }
            }

            return arrangement;
        }
    }
}
