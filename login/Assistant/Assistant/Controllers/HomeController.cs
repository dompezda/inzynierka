﻿﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Assistant.Models;
using System.Net;
using Assistant.Data;
using Assistant.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Newtonsoft.Json;

namespace Assistant.Controllers
{
    
    public class HomeController : Controller
    {


        public static ListViewModel listViewModel = new ListViewModel();
        public int IdList = 0;
        public string listName;
        public static bool isPrivateList;
        public static int? currentlyEditedListId = 0;
        public static int IdToShare = 0;

        protected ApplicationDbContext ApplicationDbContext { get; set; }





        [HttpGet]
        public IActionResult Connection_Error()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Starting_screen()
        {

            return View();
        }

        [HttpGet]
        public IActionResult Get_lists()
        {

            return View();
        }
        [HttpGet]
        public IActionResult Main_menu()
        {

            List<Product> frequentList = new List<Product>();
            using (var db = new ApplicationDbContext())
            {
                var prodIds = db.Products.Select(x => x.Id);
                int amount = db.Products.Count();
                if (amount / 2 > 5)
                {
                    amount = 5;
                }

                var groupedProd = db.ProductLists
                                   .GroupBy(q => q.ProductId)
                                   .OrderByDescending(gp => gp.Count())
                                   .Take(amount / 2)
                                   .Select(g => g.Key).ToList();

                foreach (var item in groupedProd)
                {
                    frequentList.Add(db.Products.Where(n => n.Id == item).FirstOrDefault());
                }

            }



            return View("~/Views/Utility/Main_menu.cshtml", frequentList);
        }
        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://clients3.google.com/generate_204"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        [HttpGet]
        public IActionResult Index()
        {
            bool Check = CheckForInternetConnection();

            if (Check == true)
            {
                return RedirectToAction(nameof(Main_menu));
            }
            else
            {
                return View("~/View/Utility/Connection_Error.cshtml");
            }

        }

        [HttpGet]
        public IActionResult Create_list(int? id)
        {

            if (id != null)
            {
                using (var db = new ApplicationDbContext())
                {
                    var listFromDB = db.ProductLists.Where(x => x.ListId == id).Select(v => v.Product).ToList();
                    listViewModel.productsToPartial = listFromDB;
                    currentlyEditedListId = id;


                }
            }
            else
            {
                using (var db = new ApplicationDbContext())
                {
                    listViewModel.productsToPartial =
                        db.ProductLists.Where(x => x.List.Id == currentlyEditedListId).Select(x => x.Product).ToList();


                }
            }
            listViewModel.productList = new ProductList();
            listViewModel.productList.List = new List();
            using (var db = new ApplicationDbContext())
            {
                var currentList = new List();
                if (id != null)
                {
                    currentList = db.Lists.Where(x => x.Id == id).FirstOrDefault();
                }
                else
                {
                    currentList = db.Lists.Where(x => x.Id == currentlyEditedListId).FirstOrDefault();
                }

                if (User.FindFirstValue(ClaimTypes.NameIdentifier) != null && currentList != null)
                {
                    listViewModel.productList.List.UserId = currentList.UserId;
                }
                if (currentList != null)
                    listViewModel.productList.List.Id = currentList.Id;
            }
            return View("~/Views/Home/Create_list.cshtml", listViewModel);
        }
        [HttpPost]
        public IActionResult Send_Name(string Name, bool IsPrivate)
        {
            listName = Name;
            isPrivateList = IsPrivate;
            using (var db = new ApplicationDbContext())
            {

                List currentlyEditedList;

                currentlyEditedList = new List { Name = listName };
                db.Lists.Add(currentlyEditedList);
                if (IsPrivate == true)
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    currentlyEditedList.UserId = userId;

                }
                currentlyEditedList.ProductList = new List<ProductList>();
                listViewModel.productsToPartial = new List<Product>();
                listViewModel.productList = new ProductList();
                listViewModel.productList.List = new List();
                listViewModel.productList.List.Id = currentlyEditedList.Id;
                db.SaveChanges();
                currentlyEditedListId = currentlyEditedList.Id;


            }
            return View("~/Views/Home/Create_list.cshtml", listViewModel);
        }

        [HttpGet]
        public IActionResult List_name()
        {


            return View("~/Views/Utility/List_name.cshtml");
        }

        [HttpGet]
        public IActionResult SelectList()
        {

            listViewModel.productLists = new List<ProductList>();
            using (var db = new ApplicationDbContext())
            {
                listViewModel.productLists = db.ProductLists.ToList();
            }
            using (var db = new ApplicationDbContext())
            {
                ViewBag.Lists = db.Lists.Include(x => x.ProductList).ThenInclude(x => x.Product).ToList();

            }


            return View();
        }




        [HttpGet]
        public IActionResult Private_list_load()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            using (var db = new ApplicationDbContext())
            {
                foreach (var item in db.Lists)
                {
                    var listToCheck = db.ProductLists.Where(n => n.ListId == item.Id).Count();

                    if (listToCheck == 0)
                    {
                        var ListToRemove = db.Lists.Where(n => n.Id == item.Id).FirstOrDefault();
                        db.Remove(ListToRemove);
                        db.SaveChanges();
                    }
                }
            }
            using (var db = new ApplicationDbContext())
            {
                listViewModel.productLists = db.ProductLists.ToList();
            }
            using (var db = new ApplicationDbContext())
            {
                ViewBag.Lists = db.Lists.Where(x => x.UserId == userId).Include(x => x.ProductList).ThenInclude(x => x.Product).ToList();
            }

            return View("~/Views/ListDisplay/Private_list_load.cshtml", listViewModel.productLists);
        }
        [HttpGet]
        public IActionResult Public_list_load()
        {
            using (var db = new ApplicationDbContext())
            {
                foreach (var item in db.Lists)
                {
                    var listToCheck = db.ProductLists.Where(n => n.ListId == item.Id).Count();

                    if (listToCheck == 0)
                    {
                        var ListToRemove = db.Lists.Where(n => n.Id == item.Id).FirstOrDefault();
                        db.Remove(ListToRemove);
                        db.SaveChanges();
                    }
                }
            }
            using (var db = new ApplicationDbContext())
            {
                listViewModel.productLists = db.ProductLists.ToList();
            }
            using (var db = new ApplicationDbContext())
            {
                ViewBag.Lists = db.Lists.Where(x => x.UserId == null).Include(x => x.ProductList).ThenInclude(x => x.Product).ToList();
            }

            return View("~/Views/ListDisplay/Public_list_load.cshtml", listViewModel.productLists);
        }




        [HttpGet]
        public IActionResult Load_List()
        {
            using (var db = new ApplicationDbContext())
            {
                foreach (var item in db.Lists)
                {
                    var listToCheck = db.ProductLists.Where(n => n.ListId == item.Id).Count();

                    if (listToCheck == 0)
                    {
                        var ListToRemove = db.Lists.Where(n => n.Id == item.Id).FirstOrDefault();
                        db.Remove(ListToRemove);
                        db.SaveChanges();
                    }
                }
            }
            using (var db = new ApplicationDbContext())
            {
                listViewModel.productLists = db.ProductLists.ToList();
            }
            using (var db = new ApplicationDbContext())
            {
                ViewBag.Lists = db.Lists.Where(x => x.UserId == null).Include(x => x.ProductList).ThenInclude(x => x.Product).ToList();
            }

            return View("~/Views/ListDisplay/Load_list.cshtml", listViewModel.productLists);
        }

        [HttpPost]
        public IActionResult Edit_List(List list)
        {
            List listToEdit = new List();
            using (var db = new ApplicationDbContext())
            {
                listToEdit = db.Lists.Include(x => x.ProductList)
                 .Where(n => n.Id == list.Id).FirstOrDefault();
            }
            using (var db = new ApplicationDbContext())
            {
                foreach (var item in listToEdit.ProductList)
                {
                    var tempItem = db.Products.Where(x => x.Id == item.ProductId);
                    item.Product = tempItem.FirstOrDefault();
                }
            }
            currentlyEditedListId = listToEdit.Id;
            var amount = listToEdit.ProductList.Count();
            return RedirectToAction(nameof(Create_list), listToEdit);

        }


        [HttpPost]
        public IActionResult ChangeType(int ListId, string UserId)
        {
            using (var db = new ApplicationDbContext())
            {
                var listToChange = db.Lists.Where(x => x.UserId == UserId).Where(p => p.Id == ListId).FirstOrDefault();
                if (listToChange.UserId == null)
                {
                    listToChange.UserId = null;
                    db.SaveChanges();
                    return RedirectToAction(nameof(Public_list_load));
                }
                if (listToChange.UserId != null)
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    listToChange.UserId = userId;
                    db.SaveChanges();
                    return RedirectToAction(nameof(Private_list_load));
                }
            }


            return RedirectToAction(nameof(Create_list));
        }







        [HttpPost]
        public IActionResult AddProduct(ListViewModel recvListViewModel)
        {

            var productList = new ProductList();
            if (recvListViewModel.product.Name != null)
            {

                using (var db = new ApplicationDbContext())
                {
                    var ProdList = db.Products.Select(p => p.Name);
                    if (!ProdList.Contains(recvListViewModel.product.Name))
                    {
                        db.Products.Add(recvListViewModel.product);
                    }
                    else
                    {
                        recvListViewModel.product = db.Products.Where(p => p.Name == recvListViewModel.product.Name).FirstOrDefault();
                    }

                    var currentlyEditedList = db.Lists.Single(x => x.Id == currentlyEditedListId);

                    productList.List = currentlyEditedList;
                    productList.Product = recvListViewModel.product;
                    db.ProductLists.Add(productList);
                    db.SaveChanges();
                }

            }
            return RedirectToAction(nameof(Create_list));
        }

        [HttpPost]
        public IActionResult FinishList(int? id)
        {
            List ToCheck = new List();

            string checkPrivate = null;
            using (var db = new ApplicationDbContext())
            {
                if (id == null)
                {
                    id = currentlyEditedListId;
                }
                var count = db.ProductLists.Where(x => x.ListId == currentlyEditedListId)
                .Select(x => x.Product).Count();


                if (currentlyEditedListId != null && isPrivateList == true)
                {
                    ToCheck = db.Lists.Where(x => x.Id == currentlyEditedListId).FirstOrDefault();
                    checkPrivate = db.Lists.Where(x => x.Id == currentlyEditedListId).Select(w => w.UserId).ToString();

                }

            }

            if (isPrivateList==true)
            {
                return RedirectToAction(nameof(Private_list_load));
            }
            else
            {
                return RedirectToAction(nameof(Public_list_load));
            }
            //return RedirectToAction(nameof(Load_List));
        }



        [HttpGet]
        public IActionResult Share_list()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            using (var db = new ApplicationDbContext())
            {
                foreach (var item in db.Lists)
                {
                    var listToCheck = db.ProductLists.Where(n => n.ListId == item.Id).Count();

                    if (listToCheck == 0)
                    {
                        var ListToRemove = db.Lists.Where(n => n.Id == item.Id).FirstOrDefault();
                        db.Remove(ListToRemove);
                        db.SaveChanges();
                    }
                }
            }
            using (var db = new ApplicationDbContext())
            {
                listViewModel.productLists = db.ProductLists.ToList();
            }
            using (var db = new ApplicationDbContext())
            {
                ViewBag.Lists = db.Lists.Where(x => x.UserId == userId).Include(x => x.ProductList).ThenInclude(x => x.Product).ToList();
            }

            return View("~/Views/Utility/Share_list.cshtml", listViewModel.productLists);
        }

        [HttpPost]
        public IActionResult Get_share_email(int ListId)
        {
            IdToShare = ListId;
            return View();
        }

        [HttpPost]
        public IActionResult Send_email(string mail)
        {
            int test = IdToShare;
            List ListToShare = new List();
            List<string> usersEmails = new List<string>();
            bool Exist = false;
            using (var db = new ApplicationDbContext())
            {
                usersEmails = db.Users.Select(x => x.Email).ToList();
            }
            foreach (var item in usersEmails)
            {
                if (item == mail)
                {
                    Exist = true;
                }
            }

            if (Exist == true)
            {
                ProductList newProdToSave = new ProductList();
                string NewUserId;
                using (var db = new ApplicationDbContext())
                {
                    var friendMail = User.FindFirstValue(ClaimTypes.Name);
                    NewUserId = db.Users.Where(x => x.Email == mail).Select(y => y.Id).FirstOrDefault();
                    List newList = new List();
                    newList.UserId = NewUserId;

                    newList.Name = "Lista udostepniona przez " + friendMail;
                    //newList.Name = "list shared by " + friendMail;
                    db.Lists.Add(newList);
                    db.SaveChanges();
                    var getProd = db.ProductLists.Where(x => x.ListId == IdToShare).Select(w => w.ProductId);
                    foreach (var item in getProd)
                    {

                        var newProd = db.Products.Where(x => x.Id == item).FirstOrDefault();
                        newProdToSave.ListId = newList.Id;
                        newProdToSave.List = newList;
                        newProdToSave.ProductId = newProd.Id;
                        newProdToSave.Product = newProd;
                        db.ProductLists.Add(newProdToSave);

                        db.SaveChanges();

                    }



                }

            }
            else
            {

                ViewData["ErrorMessage"] = "mail niepoprawny lub nie isnieje w bazie";
            }
            using (var db = new ApplicationDbContext())
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                ViewBag.Lists = db.Lists.Where(x => x.UserId == userId).Include(x => x.ProductList).ThenInclude(x => x.Product).ToList();
            }
            return View("~/Views/ListDisplay/Private_list_load.cshtml");
        }

        [HttpPost]
        public IActionResult DeleteProduct(int IdToRemove)
        {
            using (var db = new ApplicationDbContext())
            {
                var ProductToCheck = db.Products.Where(n => n.Id == IdToRemove).FirstOrDefault();
                var amount = db.ProductLists.Where(p => p.ProductId == ProductToCheck.Id).Count();
                if (amount == 1)
                {
                    Product ToRemove = db.Products.Include(x => x.ProductList)
                                       .Where(n => n.Id == IdToRemove).FirstOrDefault();
                    db.Products.Remove(ToRemove);
                    db.SaveChanges();
                }
                else
                {
                    var newListId = db.Lists.Last();
                    var ToRemove = db.ProductLists.Where(n => n.ListId == newListId.Id).Where(p => p.ProductId == ProductToCheck.Id).FirstOrDefault();
                    db.Remove(ToRemove);
                    db.SaveChanges();
                }

            }
            return RedirectToAction(nameof(Create_list));
        }


        [HttpPost]
        public IActionResult DeleteList(List list)
        {
            List ToRemove = new List();
            using (var db = new ApplicationDbContext())
            {

                ToRemove = db.Lists.Include(x => x.ProductList)
                     .Where(n => n.Id == list.Id).FirstOrDefault();
                db.Lists.Remove(ToRemove);
                db.SaveChanges();

            }
            if (ToRemove.UserId != null)
            {
                return RedirectToAction(nameof(Private_list_load));
            }
            else
            {
                return RedirectToAction(nameof(Public_list_load));
            }

        }



        [HttpGet]
        public IActionResult ProductList(List<Product> list)
        {

            return PartialView(list);
        }


        [HttpGet]
        public IActionResult FrequentList(List<Product> list)
        {

            return PartialView(list);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet("{id}")]

        [Route("[controller]/[action]/{id?}")]
        //[Route("{id}")]
        public JsonResult ListToShop(int id)
        {

            List<string> Products = new List<string>();
            List<string> currentList = new List<string>();
            using (var db = new ApplicationDbContext())
            {
                currentList = db.ProductLists.Where(w => w.ListId == id).Select(p => p.Product.Name).ToList();
                foreach (var item in currentList)
                {
                    Products.Add(item);
                }
            }
            List<OfflineProduct> ListToCache = new List<OfflineProduct>();
            JsonSerializer serializer = new JsonSerializer();
            for (int i = 0; i < currentList.Count; i++)
            {
                var Prod = new OfflineProduct();
                Prod.ID = i;
                Prod.Name = currentList[i];
                Prod.Done = false;
                ListToCache.Add(Prod);
            }
            string json = JsonConvert.SerializeObject(ListToCache);

            return Json(json);
        }


        [HttpPost]
        public IActionResult ShoppingView(int id)
        {
            var JsonData = ListToShop(id);

            return View(JsonData);
        }
    }
}