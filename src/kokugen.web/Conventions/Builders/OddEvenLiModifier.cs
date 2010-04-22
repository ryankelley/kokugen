using System;
using System.Collections.Generic;
using System.Linq;
using FubuMVC.UI.Configuration;
using Kokugen.Core;
using Kokugen.Core.Domain;
using Kokugen.Core.Services;
using Kokugen.Web.Actions.Board;
using Kokugen.Web.Actions.Board.Configure;
using Kokugen.Web.Actions.Card.Lists;
using Kokugen.Web.Actions.Project;
using Kokugen.Web.Actions.TimeRecord;

namespace Kokugen.Web.Conventions.Builders
{
    public class OddEvenLiModifier : PartialElementModifier
    {
        protected override bool matches(AccessorDef accessorDef)
        {
            return accessorDef.ModelType.IsType<ProjectListModel>() || accessorDef.ModelType.IsType<CardListModel>()
                || accessorDef.ModelType.IsType<TimeRecordListModel>() || accessorDef.ModelType.IsType<ProjectModel>();
        }

        public OddEvenLiModifier()
        {
            modifier = (request, tag, index, count) =>
                                                      {
                                                          if ((index % 2) == 0)
                                                              tag.AddClass("odd");
                                                          else
                                                              tag.AddClass("even");

                                                          if (index == 0)
                                                              tag.AddClass("first");

                                                          if (index == count - 1)
                                                              tag.AddClass("last");
                                                      };
       
        }
       
    }

    public class FixedItemBoardModifier : PartialElementModifier
    {
        protected override bool matches(AccessorDef accessorDef)
        {
            return accessorDef.ModelType.IsType<BoardConfigurationModel>();
        }

        public FixedItemBoardModifier()
        {
            modifier = (request, tag, index, count) =>
                           {
                               if (index == 0 || index == count - 1)
                                   tag.AddClass("fixed");
                               else
                                   tag.AddClass("draggable");
                           };
        }
    }

    public class BoardColumnIDAdder : PartialElementModifier
    {
        protected override bool matches(AccessorDef accessorDef)
        {
            var truefalse = accessorDef.Accessor.Name == "BoardColumns";
            return truefalse;
        }

        public BoardColumnIDAdder()
        {
            modifier = (request, tag, index, count) =>
                           {
                               if (index != 0 && index != count - 1)
                               {
                                   if(request.RawValue is IEnumerable<BoardColumn>)
                                   {
                                       var cols = (request.RawValue as IEnumerable<BoardColumn>).ToList();
                                       var col = cols[index] as CustomBoardColumn;
                                       tag.Id(col.Id.ToString());
                                   }
                               }
                                   //tag.ProjectId(request.RawValue.ToString());

                           };
        }
    }



    public class CardListItemModifier : PartialElementModifier
    {
        protected override bool matches(AccessorDef accessorDef)
        {
            return accessorDef.ModelType.IsType<CardListModel>() && accessorDef.Accessor.PropertyType.IsType<IEnumerable<CardViewDTO>>();
        }

        public CardListItemModifier()
        {
            modifier = (request, tag, index, count) =>
                           {
                               if(request.RawValue is IEnumerable<CardViewDTO>)
                               {
                                   var cards = (request.RawValue as IEnumerable<CardViewDTO>).ToList();
                                   var card = cards[index] as CardViewDTO;

                                   if (card.Status == CardStatus.Complete.DisplayName)
                                       tag.AddClass("completed");
                                   if (card.Status == CardStatus.Blocked.DisplayName)
                                       tag.AddClass("blocked");
                                   if (card.Status == CardStatus.Ready.DisplayName)
                                       tag.AddClass("ready");
                               }
                           };
        }
    }


    public abstract class PartialElementModifier : IPartialElementModifier
    {
        protected EachPartialTagModifier modifier;

        protected abstract bool matches(AccessorDef accessorDef);

        public EachPartialTagModifier CreateModifier(AccessorDef accessorDef)
        {
            var something = matches(accessorDef) ? modifier : null;
            return something;
        }
    }

   
}