#region Using Directives

using System.Collections.Generic;
using System.Linq;

#endregion

namespace Kokugen.Core.Domain
{
    public class Alias : DomainEntity
    {
        public string Host { get; set; }

        public bool Redirect { get; set; }
    }

    public class SiteConfiguration : DomainEntity
    {
        private readonly IList<Alias> _aliases = new List<Alias>();

        public virtual string Name { get; set; }
        public virtual string Host { get; set; }
        public virtual string LanguageDefault { get; set; }
        public virtual string PageTitleSeparator { get; set; }
        public virtual string ThemeDefault { get; set; }
        public virtual string FavIconUrl { get; set; }
        public virtual string ScriptsPath { get; set; }
        public virtual string CssPath { get; set; }
        public virtual string ImagesPath { get; set; }
        public virtual string EmailUsername { get; set; }
        public virtual double PostEditTimeout { get; set; }
        public virtual string SEORobots { get; set; }
        public virtual string GravatarDefault { get; set; }
        public virtual bool TrackbacksEnabled { get; set; }
        public virtual string CookieDomain { get; set; }
        //public virtual string PodcastUploadPath { get; set; }

        public IEnumerable<Alias> GetAliases()
        {
            return _aliases.AsEnumerable();
        }

        public void AddAlias(Alias alias)
        {
            _aliases.Add(alias);
        }

        public void RemoveAlias(Alias alias)
        {
            _aliases.Remove(alias);
        }
    }
}