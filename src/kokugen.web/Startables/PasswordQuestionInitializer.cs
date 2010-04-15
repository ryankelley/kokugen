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
                                    new ValueObject("What's the name of your favorite sports team?"),
                                    new ValueObject("What was your childhood nickname?"),
                                    new ValueObject("In what city did you meet your spouse/significant other?"),
                                    new ValueObject("What is the name of your favorite childhood friend?"),
                                    new ValueObject("What is the middle name of your youngest child?"),
                                    new ValueObject("What is your oldest sibling's middle name?"),
                                    new ValueObject("What school did you attend for sixth grade?"),
                                    new ValueObject("Where were you when you had your first kiss?"),
                                    new ValueObject("What is your maternal grandmother's maiden name?"),
                                    new ValueObject("What is the name of the place your wedding reception was held?"),
                                };


            ValueObjectRegistry.AddValueObjects("Question", questions);
        }
    }
}