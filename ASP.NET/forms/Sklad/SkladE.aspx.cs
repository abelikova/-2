﻿/*flexberryautogenerated="true"*/

namespace IIS.Склад
{
    using ICSSoft.STORMNET;
    using ICSSoft.STORMNET.Web.Controls;
    using ICSSoft.STORMNET.Web.AjaxControls;
    using ICSSoft.STORMNET.Web.Tools;
    using System;
    using System.Collections.Generic;
    using ICSSoft.STORMNET.Business;
    using ICSSoft.STORMNET.Business.LINQProvider;
    using System.Linq;
    using ICSSoft.STORMNET.Windows.Forms;
    using ICSSoft.STORMNET.FunctionalLanguage;

    public partial class СкладE : BaseEditForm<Склад>
    {
        /// <summary>
        /// Конструктор формы.
        /// </summary>
        public СкладE()
            : base(Склад.Views.СкладE)
        {
        }

        /// <summary>
        /// Путь до формы.
        /// </summary>
        public static string FormPath
        {
            get { return "~/forms/Sklad/SkladE.aspx"; }
        }

        /// <summary>
        /// Вызывается самым первым в Page_Load.
        /// </summary>
        protected override void Preload()
        {
            ctrlТоварНаСкладе.Operations.Delete = false;
            ctrlТоварНаСкладе.Operations.Add = false;
            ctrlТоварНаСкладе.Operations.PlusInRow = false;
        }

        /// <summary>
        /// Здесь лучше всего писать бизнес-логику, оперируя только объектом данных.
        /// </summary>
        protected override void PreApplyToControls()
        {
            if (!IsPostBack && (DataObject == null || DataObject.GetStatus(true) != ObjectStatus.Created))
            {
                var langdef = ExternalLangDef.LanguageDef;
                var lcs = LoadingCustomizationStruct.GetSimpleStruct(typeof(Товар), Товар.Views.ТоварE);
                lcs.LimitFunction = langdef.GetFunction(langdef.funcEQ,
                                new VariableDef(langdef.GuidType, Information.ExtractPropertyName<Товар>(x => x.__PrimaryKey)), "172E31C1-78A0-42BA-A8B4-3A5FDC682B61");
                //var товар = DataServiceProvider.DataService.LoadObjects(lcs);


                var ds = (SQLDataService)DataServiceProvider.DataService;
                var workers = ds.Query<Поставки>(Поставки.Views.ПоставкиL);
                var query = from w in workers
                                    where w.Склад == DataObject
                                    select w;
                List<Поставки> data = query.ToList(); // Вычитать данные в коллекцию.
                List<Поставки> data2 = data.GroupBy(t => t.Товар)
                    .Select(cl => new Поставки
                    {
                        Товар = cl.First().Товар,
                        Количестсво = cl.Sum(q => q.Количестсво),
                    }).ToList();
                var sklad = new Склад();
                data2.ForEach(delegate (Поставки name)
                {
                    //var foots = new DetailArrayOfТоварНаСкладе(sklad)
                    var foots = new ТоварНаСкладе { Товар = name.Товар, Количество = name.Количестсво };
                /*{
                     new ТоварНаСкладе { Товар = name.Товар,
                         Количество = name.Количестсво }
                     //new ТоварНаСкладе { Товар = data[0].Товар }
                };*/
                   sklad.ТоварНаСкладе.Add(foots); 
                });
                    DataObject = sklad;
            }
        }

        /// <summary>
        /// Здесь лучше всего изменять свойства контролов на странице,
        /// которые не обрабатываются WebBinder.
        /// </summary>
        protected override void PostApplyToControls()
        {
            Page.Validate();
        }

        /// <summary>
        /// Вызывается самым последним в Page_Load.
        /// </summary>
        protected override void Postload()
        {
        }

        /// <summary>
        /// Валидация объекта для сохранения.
        /// </summary>
        /// <returns>true - продолжать сохранение, иначе - прекратить.</returns>
        protected override bool PreSaveObject()
        {
            return base.PreSaveObject();
        }

        /// <summary>
        /// Нетривиальная логика сохранения объекта.
        /// </summary>
        /// <returns>Объект данных, который сохранился.</returns>
        protected override DataObject SaveObject()
        {
            return base.SaveObject();
        }
    }
}