using HtmlTags;

namespace Kokugen.Core
{
    public static class ModelExtensions
    {
        public static string ToJson(this object model)
        {
            return JsonUtil.ToJson(model);
        }
    }
}