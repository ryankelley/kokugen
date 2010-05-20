using Kokugen.Core.Domain;

namespace Kokugen.Tests
{
    public static class Stubs
    {
        public static CustomBoardColumn WorkColumn = new CustomBoardColumn{ ColumnOrder = 1, CardLimit = 3, Name = "Working"};
        public static BoardColumn BacklogColumn = new CustomBoardColumn{ ColumnOrder = 0, CardLimit = 0, Name = BoardColumn.BacklogName};
        public static BoardColumn ArchiveColumn = new CustomBoardColumn{ ColumnOrder = 0, CardLimit = 0, Name = BoardColumn.ArchiveName};

    }
}