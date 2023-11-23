using System.Collections.Generic;
using System.Drawing;

namespace Sachy_Obrazky
{
    /// <summary>
    /// Шахматное поле
    /// </summary>
    public class ChessArrangement
    {
        /// <summary>
        /// Выводит true, если очередной ход на текущем поле - белыми фигурами
        /// </summary>
        public bool whiteColorPiece { get; set; }
        /// <summary>
        /// Двойной массив, в котором сохраняются позиции фигур
        /// </summary>
        public readonly ChessPiece[,] Arrangement;

        /// <summary>
        /// Чистое поле
        /// </summary>
        public static ChessPiece[,] Empty
        {
            get
            {
                var arrangement = new ChessPiece[8, 8];
                for (var y = 0; y < 8; y++)
                {
                    for (var x = 0; x < 8; x++)
                    {
                        arrangement[y, x] = ChessPiece.Empty;
                    }
                }
                return arrangement;
            }
        }


        /// <summary>
        /// Создаём объект, который хранит позицию шахмат и может с ней работать, по принимаемому массиву ChessPiece[,]
        /// </summary>
        /// <param name="arrangement"></param>
        public ChessArrangement(ChessPiece[,] arrangement)
        {
            Arrangement = arrangement;
        }

        /// <summary>
        /// Удалить фигуру
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void RemovePiece(int x, int y)  
        {
            Arrangement[y, x] = ChessPiece.Empty;
        }
        public ChessPiece GetPiece(int x, int y)
        {
            return Arrangement[y, x];
        }
        /// <summary>
        /// Установить фигуру на позицию
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="piece"></param>
        public void SetPiece(int x, int y, ChessPiece piece)
        {
            Arrangement[y, x] = piece;
        }

        /// <summary>
        /// Переместить фигуру с одной позиции на другую
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        public void MovePiece(int x1, int y1, int x2, int y2)
        {
            (Arrangement[y1, x1], Arrangement[y2, x2]) = (Arrangement[y2, x2], Arrangement[y1, x1]);
        }

        /// <summary>
        /// Сравнить расположение фигур с принимаемой схемой расположения
        /// </summary>
        /// <param name="arrangement"></param>
        /// <returns></returns>
        public bool EqualsArrangement(ChessPiece[,] arrangement)
        {
            for (var y = 0; y < 8; y++)
            {
                for (var x = 0; x < 8; x++)
                {
                    if (Arrangement[y, x] != arrangement[y, x])
                        return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Сравнить правильность расположения фигуры на определённой клетке с фигурой на принимаемой схемой расположения
        /// </summary>
        /// <param name="arrangement"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool EqualsArrangementInPosition(ChessPiece[,] arrangement, int x, int y)
        {
            return Arrangement[y, x] == arrangement[y, x];
        }

        /// <summary>
        /// Получить первую координату, где находится искомая фигура
        /// </summary>
        /// <param name="piece"></param>
        /// <returns></returns>
        public Point? GetPieceFirstPositionOrNull(ChessPiece piece)
        {
            for (var y = 0; y < 8; y++)
            {
                for (var x = 0; x < 8; x++)
                {
                    if (Arrangement[y, x] == piece)
                        return new Point(x, y);
                }
            }
            return null;
        }

        /// <summary>
        /// Создание схемы расположения фигур из принимаемого массива строк
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static ChessArrangement FromStringArray(string[] s)
        {
            var arrangement = new ChessPiece[8, 8];

            for (var y = 0; y < 8; y++)
            {
                for (var x = 0; x < 8; x++)
                {
                    var symbol = s[y][x];
                    var piece = GetPieceBySymbol(symbol);
                    arrangement[y, x] = piece;
                }
            }

            return new ChessArrangement(arrangement);
        }

        /// <summary>
        /// Преобразование символа в фигуру-энумератор ChessPiece
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">Ошибка в случае не соответствия символу ни одному из нужных</exception>
        public static ChessPiece GetPieceBySymbol(char symbol)
        {
            switch (symbol)
            {
                case '#':
                    return ChessPiece.Empty;
                case 'P':
                    return ChessPiece.WhitePawn;
                case 'K':
                    return ChessPiece.WhiteKing;
                case 'R':
                    return ChessPiece.WhiteRook;
                case 'N':
                    return ChessPiece.WhiteKnight;
                case 'B':
                    return ChessPiece.WhiteBishop;
                case 'Q':
                    return ChessPiece.WhiteQueen;
                case 'p':
                    return ChessPiece.BlackPawn;
                case 'k':
                    return ChessPiece.BlackKing;
                case 'r':
                    return ChessPiece.BlackRook;
                case 'n':
                    return ChessPiece.BlackKnight;
                case 'b':
                    return ChessPiece.BlackBishop;
                case 'q':
                    return ChessPiece.BlackQueen;
                default:
                    throw new System.ArgumentException();
            }
        }

        public bool TryForEmpty(int x, int y)
        {
            if (Arrangement[y, x] == ChessPiece.Empty)
            {
                return true;
            }
            return false;
        }
    }
}

