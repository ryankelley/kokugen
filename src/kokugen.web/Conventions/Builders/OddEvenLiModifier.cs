using System;
using FubuMVC.UI.Configuration;
using Kokugen.Core;
using Kokugen.Web.Actions.Project;

namespace Kokugen.Web.Conventions.Builders
{
    public class OddEvenLiModifier : IPartialElementModifier
    {
        private readonly Func<AccessorDef, bool> _matches;
        private readonly Func<AccessorDef, EachPartialTagModifier> _modifierBuilder;

        private bool matches(AccessorDef accessorDef)
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

        public EachPartialTagModifier CreateModifier(AccessorDef accessorDef)
        {
            var something = matches(accessorDef) ? modifier : null;
            return something;

        }
    }
}