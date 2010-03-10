namespace Kokugen.Web.Conventions
{
    public class ValueObject
    {
        public string Key { get; set; }

        public string LocalizedText ()
        {
            return Key;
        }
    }
}