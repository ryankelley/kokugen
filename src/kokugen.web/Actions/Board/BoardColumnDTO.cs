using System;

namespace Kokugen.Web.Actions.Board
{
    public class BoardColumnDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Limit { get; set; }
    }
}