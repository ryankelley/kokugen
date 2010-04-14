using System;
using Kokugen.Core;

namespace Kokugen.Web.Startables
{
    public class PasswordQuestionInitializer : IValueObjectInitializer
    {


        public void Start()
        {
            var questions = new[]
                                {
                                    new ValueObject("What's the name of your first grade teacher?"),
                                    new ValueObject("What's the name of your High School mascot?"),
                                    new ValueObject(("What's the name of your favorite sports team?"))
                                };


            ValueObjectRegistry.AddValueObjects("Question", questions);
        }
    }
}