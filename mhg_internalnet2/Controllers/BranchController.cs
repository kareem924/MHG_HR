using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Entities;
using Domain.Service.Abstract;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using mhg_internalnet2.ViewModel;

namespace mhg_internalnet2.Controllers
{
    public class BranchController : Controller
    {
        private readonly IBranchService _branchService;
        private readonly IBrandService _brandService;
        public BranchController(IBranchService branchService, IBrandService brandService)
        {
            _branchService = branchService;
            _brandService = brandService;
        }
        // GET: Branch

        public ActionResult Index()
        {

            PopulateBrands();
            return View();
        }
        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            var model = new List<BranchModel>();
            foreach (var item in _branchService.GetAllBranches())
            {
                var branch = new BranchModel();
                branch.Address = item.Address;
                branch.BrandId = item.BrandId;
                branch.ClosingTime = item.ClosingTime;
                branch.Color = item.Color;
                branch.MobilePhone = item.MobilePhone;
                branch.OpenningTime = item.OpenningTime;
                branch.StoreId = item.StoreId;
                branch.StoreName = item.StoreName;
                branch.Telephone = item.Telephone;
                if (item.Brand != null)
                {
                    branch.brand = new BrandViewModel
                    {

                        BrandId = item.Brand.BrandId,
                        BrandName = item.Brand.BrandName
                    };
                }
                model.Add(branch);
            }
            return Json(model.ToDataSourceResult(request));
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, BranchModel store)
        {
            if (store != null && ModelState.IsValid)
            {
                var branch = new Branch() { Address = store.Address, StoreName = store.StoreName, ClosingTime = DateTime.Now, Color = store.Color, OpenningTime = DateTime.Now, MobilePhone = store.MobilePhone, Telephone = store.Telephone, BrandId = store.BrandId };
                _branchService.InsertBranch(branch);
            }
            return Json(new[] { store }.ToDataSourceResult(request, ModelState));
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, BranchModel store)
        {
            if (store != null && ModelState.IsValid)
            {
                var branch = new Branch() { Address = store.Address, StoreName = store.StoreName, ClosingTime = store.ClosingTime, Color = store.Color, OpenningTime = store.OpenningTime, MobilePhone = store.MobilePhone, Telephone = store.Telephone, BrandId = store.BrandId };
                _branchService.UpdateBranch(branch);
            }
            return Json(new[] { store }.ToDataSourceResult(request, ModelState));
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request, BranchModel store)
        {
            if (store != null)
            {
                var branch = new Branch() {BrandId=store.BrandId,StoreId=store.StoreId, Address = store.Address, StoreName = store.StoreName, ClosingTime = store.ClosingTime, Color = store.Color, OpenningTime = store.OpenningTime, MobilePhone = store.MobilePhone, Telephone = store.Telephone };
                _branchService.DeleteBranch(branch);
            }
            return Json(new[] { store }.ToDataSourceResult(request, ModelState));
        }

        #region methods
        private void PopulateBrands()
        {

            var brands = _brandService.GellAllBrands()
                        .Select(c => new BrandViewModel
                        {
                            BrandId = c.BrandId,
                            BrandName = c.BrandName
                        })
                        .OrderBy(e => e.BrandName);

            ViewData["brands"] = brands;
           

        }
        #endregion
    }
}