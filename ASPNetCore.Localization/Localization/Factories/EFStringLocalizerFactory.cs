using ASPNetCore.Localization.Data;
using ASPNetCore.Localization.Localization.Localizer;
using Microsoft.Extensions.Localization;
using System;

namespace ASPNetCore.Localization.Localization.Factories
{
    public class EFStringLocalizerFactory : IStringLocalizerFactory
    {
        private readonly LocalizationDbContext _db;

        public EFStringLocalizerFactory(LocalizationDbContext db)
        {
            _db = db;
           
            //_db.AddRange(
            //    new Culture
            //    {
            //        Name = "en-US",
            //        Resources = en_US.GetList()
            //    },
            //    new Culture
            //    {
            //        Name = "fa-IR",
            //        Resources = fa_IR.GetList()
            //    }
            //);
            //_db.SaveChanges();
        }

        public IStringLocalizer Create(Type resourceSource)
        {
            return new EFStringLocalizer(_db);
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            return new EFStringLocalizer(_db);
        }
    }

}
