using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Sachy_Obrazky
{
    internal class ChessPartyHistory
    {
        public Guid Id;
        private string _nameParty;
        public List<ChessArrangement> partyHistory;
        private int _counter;

        public ChessPartyHistory(string name, string line)
        {
            line = StringOptimize(line);
            var startPosition = ChessRestoreArrangementsStorage.CreateArrangementFromFen(
                "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR");
            _nameParty = name;
            partyHistory = new List<ChessArrangement>()
            {
                ChessArrangement.FromStringArray(startPosition)
            };
            DeserilazeStringStep(line);
        }

        public static string StringOptimize(string line)
        {
            line = line.Trim();
            line = line.Replace("\n", " ");
            line = line.Replace("\r", " ");
            line = line.Replace(",", " ");
            bool flagSpace = line.Contains("  ");
            while (flagSpace)
            {
                line = line.Replace("  ", " ");
                flagSpace = line.Contains("  ");
            }
            line = line.Replace(" -", "-");
            line = line.Replace("- ", "-");
            return line;
        }

        public string GetName()
        {
            return _nameParty;
        }

        public ChessArrangement GetStartPosition()
        {
            _counter = 0;
            return GetPosition();
        }

        public ChessArrangement GetEndPosition()
        {
            _counter = partyHistory.Count - 1;
            return GetPosition();
        }

        /// <summary>
        /// Вывод позиции следующего хода (и переключение счётчика)
        /// </summary>
        /// <returns></returns>
        public ChessArrangement GetNextPosition()
        {
            if (_counter != partyHistory.Count - 1)
            {
                _counter++;
            }
            return GetPosition();
        }

        public ChessArrangement GetPreviousPosition()
        {
            if (_counter != 0)
            {
                _counter--;
            }

            return GetPosition();
        }

        public int GetNumberEndStep()
        {
            return partyHistory.Count - 1;
        }

        /// <summary>
        /// Вывод текущего хода
        /// </summary>
        /// <returns></returns>
        public int GetNumberCurrentStep()
        {
            return _counter;
        }

        private ChessArrangement GetPosition()
        {
            return partyHistory[_counter];
        }

        private void DeserilazeStringStep(string line)
        {
            var partyString = line;

            var partySteps = partyString.Split(' ');
            for (int i = 0; i < partySteps.Length; i++)
            {
                if ((i+1)%2==0) //первый ход - нулевой (первоначальная расстановка), поэтому черный, чтобы следующий был белый
                    partyHistory.Last().whiteColorPiece = true;
                else
                    partyHistory.Last().whiteColorPiece = false;
                
                partyHistory.Add(GenerateStep(partySteps[i]));
            }
        }

        /// <summary>
        /// Возвращение нового шага исходя из принятого действия
        /// </summary>
        /// <param name="partyStep"></param>
        /// <returns></returns>
        private ChessArrangement GenerateStep(string partyStep)
        {
            var lastStepPosition = partyHistory.Last().Arrangement;
            var newLastStepPos = SetPositionOnBoard(lastStepPosition);

            var lastStep = new ChessArrangement(newLastStepPos);
            partyStep = partyStep.ToLower();
            var listO = partyStep.Where(e => e == 'o').ToList();
            if (listO.Count > 0) // проверка на рокировку
            {
                Castiling(lastStep, listO);
            }
            else
            {
                var poses = SplitPoses(partyStep);
                MovePiece(lastStep, poses);
            }
            return lastStep;
        }

        /// <summary>
        /// Обновляем позицию всех фигур на доске в соответствии с предыдущим шагом
        /// </summary>
        /// <param name="lastStepPosition"></param>
        /// <returns></returns>
        private ChessPiece[,] SetPositionOnBoard(ChessPiece[,] lastStepPosition)
        {
            var newLastStepPos = new ChessPiece[8, 8];
            
            for (int i = 0; i < 8; i++)
            {
                for (int y = 0; y < 8; y++)
                {
                    newLastStepPos[i, y] = lastStepPosition[i, y];
                }
            }
            return newLastStepPos;
        }

        /// <summary>
        /// Изменение позиции фигур, в соответствии с принятыми координатами фигур
        /// </summary>
        /// <param name="lastStep">ситуация на доске</param>
        /// <param name="poses">координаты фигур</param>
        private void MovePiece(ChessArrangement lastStep, string[] poses)
        {
            var pose0 = GetCoordinatePoseStandart(poses[0]);
            var pose1 = GetCoordinatePoseStandart(poses[1]);
            if (lastStep.TryForEmpty(pose1[0], pose1[1]))
            {
                lastStep.MovePiece(pose0[0], pose0[1], pose1[0], pose1[1]);
            }
            else
            {
                lastStep.RemovePiece(pose1[0], pose1[1]);
                lastStep.MovePiece(pose0[0], pose0[1], pose1[0], pose1[1]);
            }
        }

        /// <summary>
        /// Разбиваем строку хода на две позиции - начальная и конечная
        /// </summary>
        /// <param name="partyStep"></param>
        /// <returns></returns>
        private string[] SplitPoses(string partyStep)
        {
            string[] poses = new string[2];

            if (partyStep.IndexOf("-") != -1)
            {
                poses = partyStep.Split('-');
            }
            else if (partyStep.IndexOf("x") != -1)
            {
                poses = partyStep.Split('x');
            }

            return poses;
        }

        /// <summary>
        /// Рокировка
        /// </summary>
        /// <param name="lastStep"></param>
        /// <param name="listO"></param>
        private void Castiling(ChessArrangement lastStep, List<char> listO)
        {

            if (partyHistory[partyHistory.Count - 1].whiteColorPiece)
            {
                //черные в этом ходу
                if (listO.Count == 2)
                {
                    CastilingSmallBlack(lastStep);
                }

                if (listO.Count == 3)
                {
                    CastilingLargeBlack(lastStep);
                }
            }
            else
            {
                //белые в этом ходу
                if (listO.Count == 2)
                {
                    CastilingSmallWhite(lastStep);
                }

                if (listO.Count == 3)
                {
                    CastilingLargeWhite(lastStep);
                }
            }
        }

        private void CastilingLargeWhite(ChessArrangement lastStep)
        {
            var k = new Point(4, 7);
            var r = new Point(0, 7);
            lastStep.MovePiece(k.X, k.Y, k.X - 2, k.Y);
            lastStep.MovePiece(r.X, r.Y, r.X + 3, r.Y);
        }

        private void CastilingSmallWhite(ChessArrangement lastStep)
        {
            var k = new Point(4, 7);
            var r = new Point(7, 7);
            lastStep.MovePiece(k.X, k.Y, k.X + 2, k.Y);
            lastStep.MovePiece(r.X, r.Y, r.X - 2, r.Y);
        }

        private void CastilingLargeBlack(ChessArrangement lastStep)
        {
            var k = new Point(4, 0);
            var r = new Point(0, 0);
            lastStep.MovePiece(k.X, k.Y, k.X - 2, k.Y);
            lastStep.MovePiece(r.X, r.Y, r.X + 3, r.Y);
        }

        private void CastilingSmallBlack(ChessArrangement lastStep)
        {
            var k = new Point(4, 0);
            var r = new Point(7, 0);
            lastStep.MovePiece(k.X, k.Y, k.X + 2, k.Y);
            lastStep.MovePiece(r.X, r.Y, r.X - 2, r.Y);
        }

        /// <summary>
        /// Перевод буквенно-цифрового обозначения позиции в цифровой (для двойного массива)
        /// </summary>
        /// <param name="pose"></param>
        /// <returns></returns>
        private int[] GetCoordinatePoseStandart(string pose)
        {
            int x1;
            int y1;
            pose = pose.ToLower();
            string poseCoord = "";
            if (pose.Length > 2)
            {
                poseCoord = pose.Substring(pose.Length - 2, 2);
            }
            else if (pose.Length == 2)
            {
                poseCoord = pose;
            }
            x1 = poseCoord[0] - 97;
            y1 = 7 - (poseCoord[1] - 49);
            return new[] { x1, y1 };
        }
    }
}