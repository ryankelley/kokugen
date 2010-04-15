using System;
using System.Collections.Generic;
using Kokugen.Web.Actions.Card;

namespace Kokugen.Web.Actions.Board
{
    public class BoardColumnDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CardLimit { get; set; }
    }
}