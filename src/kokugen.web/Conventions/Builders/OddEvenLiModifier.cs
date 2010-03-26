using System;
using FubuMVC.UI.Configuration;
using Kokugen.Core;
using Kokugen.Core.Domain;
using Kokugen.Web.Actions.Board;
using Kokugen.Web.Actions.Project;

namespace Kokugen.Web.Conventions.Builders
{
    public class OddEvenLiModifier : PartialElementModifier
    {
        protected override bool matches(AccessorDef accessorDef)
        {
            return accessorDef.ModelType.IsType<ProjectListModel>();
        }

        private EachPartialTagModifier modifier = (request, tag, index, count) =>
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

    public abstract class PartialElementModifier : IPartialElementModifier
    {
         private readonly Func<AccessorDef, bool> _matches;
        private readonly Func<AccessorDef, EachPartialTagModifier> _modifierBuilder;

        protected EachPartialTagModifier modifier;

        protected abstract bool matches(AccessorDef accessorDef);

        public EachPartialTagModifier CreateModifier(AccessorDef accessorDef)
        {
            var something = matches(accessorDef) ? modifier : null;
            return something;
        }
    }

   
}