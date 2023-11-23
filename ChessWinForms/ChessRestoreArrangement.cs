namespace Sachy_Obrazky
{
    internal class ChessRestoreArrangement
    {
        private readonly string[] _arrangement;

        /// <summary>
        /// Создание объекта из принимаемого id расстановки и схемы расстановки из массива строк
        /// </summary>
        /// <param name="id"></param>
        /// <param name="arrangement"></param>
        public ChessRestoreArrangement(int id,string[] arrangement)
        {
            _arrangement = arrangement;
            Id = id;
        }

        /// <summary>
        /// Id конкретной шахматной расстановки
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Расстановка фигур, получаемая из записанной схемы, в виде массива строк
        /// </summary>
        public ChessArrangement Arrangement =>
            ChessArrangement.FromStringArray(_arrangement);
    }
}
