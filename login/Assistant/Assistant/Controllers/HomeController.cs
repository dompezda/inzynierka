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
using System.Security.Cryptography.X509Certificates;
using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using MongoDB.Driver.Builders;

namespace Assistant.Controllers
{

    
    public class HomeController : Controller
    {

        public HomeController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        //private readonly UserManager<ApplicationUser> _userManager;
        //private readonly SignInManager<ApplicationUser> _signInManager;
        public string[] names = { "Kaileb", "Kairn", "Kaisonn", "Kaydn", "Kaydyn", "Kobi", "Koby", "Lawrie", "Majid", "Makensie", "Makenzie", "Makin", "Maksim", "Marius", "Mark", "Marko", "Markus", "Marley", "Marlin", "Marlon", "Maros", "Marshall", "Martin", "Marty", "Martyn", "Marvellous", "Marvin", "Marwan", "Maryk", "Marzuq", "Mashhood", "Mason", "Mason-Jay", "Masood", "Masson", "Matas", "Matej", "Mateusz", "Mathew", "Mathias", "Mathu", "Maximilian", "Maximillian", "Maximus", "Maxwell", "Maxx", "Mayeul", "Mayson", "Mazin", "Mcbride", "McCaulley", "McKade", "McKauley", "Meshach", "Meyzhward", "Micah", "Michael", "Michael-Alexander", "Michael-James", "Michal", "Michat", "Micheal", "Michee", "Mickey", "Miguel", "Mika", "Mikael", "Mikee", "Mikey", "Montague", "Montgomery", "Monty", "Moore", "Moosa", "Moray", "Morgan", "Munachi", "Muneeb", "Mungo", "Munir", "Murry", "Musa", "Musse", "Mustafa", "Mustapha", "Muzammil", "Muzzammil", "Nihaal", "Nihal", "Nikash", "Nikhil", "Niki", "Nikita", "Nikodem", "Nikolai", "Nikos", "Nilav", "Niraj", "Niro", "Niven", "Noah", "Noel", "Nolan", "Noor", "Norman", "Norrie", "Nuada", "Raithin", "Raja", "Rajab-Ali", "Ramone", "Ramsay", "Rayan", "Rayane" };
        public string[] surnames = { "Aaberg", "Aaby", "Aadland", "Aagaard", "Aakre", "Aaland", "Aalbers", "Aalderink", "Aalund", "Aamodt", "Aamot", "Aanderud", "Aanenson", "Aanerud", "Aarant", "Aardema", "Aarestad", "Aarhus", "Aaron", "Aarons", "Aaronson", "Aarsvold", "Aas", "Aasby", "Aase", "Aasen", "Aavang", "Abad", "Abadi", "Abadie", "Abair", "Abaja", "Abajian", "Abalos", "Abaloz", "Abar", "Abarca", "Abare", "Abascal", "Abasta", "Abate", "Abati", "Abatiell", "Abato", "Abatti", "Abaunza", "Abaya", "Abbadessa", "Abbamonte", "Abbas", "Abbasi", "Abbassi", "Abbate", "Abbatiello", "Abbay", "Abbe", "Haines", "Hainesworth", "Hainey", "Hainley", "Hainline", "Hains", "Hainsey", "Hainsworth", "Hair", "Haire", "Hairell", "Hairfield", "Hairgrove", "Hairr", "Hairster", "Hairston", "Haislett", "Haisley", "Haislip", "Haist", "Haisten", "Hait", "Haith", "Haithcock", "Haitz", "Hajdas", "Hajduk", "Hajdukiewicz", "Hajek", "Hakala", "Hakanson", "Hake", "Hakeem", "Hakel", "Haken", "Haker", "Hakes", "Hakey", "Hakim", "Hakimi", "Hakimian", "Hakkila", "Hal", "Halaas", "Halaby", "Halajian", "Halaliky", "Halama", "Halas", "Halasz", "Halat", "Mace", "Maceachern", "Maceda", "Macedo", "Macedonio", "Macek", "Macentee", "Macer", "Macera", "Macewen", "Macey", "Maceyak", "Macfarland", "Macfarlane", "Macgillivray", "Macgowan", "Macgregor", "Macguire", "Mach", "Macha", "Machacek", "Machado", "Machain", "Oxton", "Oya", "Oyabu", "Oyama", "Oyellette", "Oyen", "Oyer", "Oyervides", "Oyler", "Oyola", "Oyster", "Oyston", "Oyuela", "Oz", "Oza", "Ozaeta", "Ozaine", "Ozaki", "Ozane", "Ozawa", "Ozbun", "Ozburn", "Ozenne", "Ozer", "Ozga", "Ozier", "Ozimek", "Ozley", "Ozment" };
        
        public static ListViewModel listViewModel = new ListViewModel();
        public int IdList = 0;
        public string listName;
        public static string isPrivateList;
        public static ObjectId currentlyEditedListId;
        public static ObjectId IdToShare = ObjectId.Empty;

        //protected ApplicationDbContext ApplicationDbContext { get; set; }
        public MongoDbContext db = new MongoDbContext();
        private UserManager<ApplicationUser> _userManager;

        //public MongoDbContext MongoDbContext = new MongoDbContext();
   

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
            List<Product> ProdList = new List<Product>();
            ProdList = db.Products.AsQueryable().ToList();
            //Dictionary<ObjectId, int> GetCount = new Dictionary<ObjectId, int>();
            ////GetUsersData();
            //int amount = db.Products.AsQueryable().Count();
            //if (amount >= 5)
            //    amount = 5;

            //foreach (var item in ProdList)
            //{
            //    GetCount.Add(item.Id,0);
            //}
            //var ProdCheck = db.MongoLists.AsQueryable().Select(x=>x.ProductList).ToList();
            
            //foreach (var currentList in ProdCheck)
            //{
            //    foreach (var item in currentList)
            //    {
            //        GetCount[item.Id]+=1;
            //    }
            //}
            
            //var prodIds = db.Products.AsQueryable().ToList();
            //var itemsToCheck=GetCount.OrderByDescending(x => x.Value).ToList().Take(5).ToList();
            
           List<Product> sendListToFrequentList = new List<Product>();
            //foreach (var item in itemsToCheck)
            //{
            //    Product prod = db.Products.AsQueryable().Where(x => x.Id == item.Key).FirstOrDefault();
            //    sendListToFrequentList.Add(prod);
            //}
           

            return View("~/Views/Utility/Main_menu.cshtml", sendListToFrequentList);
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
            //bool Check = CheckForInternetConnection();

            //if (Check == true)
            //{
            //    return RedirectToAction(nameof(Main_menu));
            //}
            //else
            //{
            //    return View("~/View/Utility/Connection_Error.cshtml");
            //}
            return RedirectToAction(nameof(Main_menu));
        }

        [HttpPost]
        public IActionResult Send_Name(string Name)
        {
            MongoDBProdList currentlyEditedList = new MongoDBProdList();
            currentlyEditedList.UserId = ObjectId.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            currentlyEditedList.Name = Name;
            //listViewModel.productsToPartial = currentlyEditedList.ProductList;

            currentlyEditedListId = currentlyEditedList.Id;
            db.MongoLists.InsertOne(currentlyEditedList);


            return View("~/Views/Home/Create_list.cshtml", currentlyEditedList);
        }

        [HttpGet]
        public IActionResult Create_list()
        {

            var getListFromDB = db.MongoLists.Find(x => x.Id == currentlyEditedListId).FirstOrDefault();
            var Prods = getListFromDB.ProductList;
            //listViewModel.productList = new ProductList();
            return View("~/Views/Home/Create_list.cshtml", getListFromDB);
        }
       

        [HttpGet]
        public IActionResult List_name()
        {


            return View("~/Views/Utility/List_name.cshtml");
        }






        [HttpGet]
        public IActionResult Private_list_load()
        {
            var userId = ObjectId.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                foreach (var item in db.MongoLists.AsQueryable().ToList())
                {
                    if (item.ProductList.Count() == 0)
                    {

                    var ListToRemove=db.MongoLists.AsQueryable().Where(x => x.Id == item.Id).FirstOrDefault();
                    db.MongoLists.DeleteOne(a => a.Id == ListToRemove.Id);
                }
                }
 

            //var Ids = db.List.AsQueryable().Where(x => x.UserId == userId).Select(x=>x.Id).ToList();
            List<MongoDBProdList> Lists = db.MongoLists.AsQueryable().Where(x => x.UserId == userId).ToList();


            //ViewBag.Lists = db.MongoLists.AsQueryable().Where(x => x.UserId == userId).ToList().FirstOrDefault();
            

            return View("~/Views/ListDisplay/Private_list_load.cshtml", Lists);
        }
        //[HttpGet]
        //public IActionResult Public_list_load()
        //{
      
        //        foreach (var item in db.List.AsQueryable())
        //        {
        //            var listToCheck = db.ProductList.AsQueryable().Where(n => n.ListId == item.Id).Count();

        //            if (listToCheck == 0)
        //            {
        //                var ListToRemove = db.List.AsQueryable().Where(n => n.Id == item.Id).FirstOrDefault();
        //                db.List.DeleteOne(x => x.Id == ListToRemove.Id);  
        //            }
        //        }
            

        //        listViewModel.productLists = db.ProductList.AsQueryable().ToList();

        //    var Ids = db.List.AsQueryable().Where(x => x.UserId == null).Include(x => x.ProductList).ThenInclude(x => x.ProductId).ToList();
        //    List<ListOfProducts> GetLists = new List<ListOfProducts>();
        //    foreach (var item in Ids)
        //    {
        //        var items = db.List.AsQueryable().Where(x => x.Id == item.Id).FirstOrDefault();
        //        GetLists.Add(items);
        //    }


        //    ViewBag.Lists = GetLists;
        //    //ViewBag.Lists = db.List.AsQueryable().Where(x => x.UserId == null).Include(x => x.ProductList).ThenInclude(x => x.Product).ToList();
            

        //    return View("~/Views/ListDisplay/Public_list_load.cshtml", listViewModel.productLists);
        //}




        //[HttpGet]
        //public IActionResult Load_List()
        //{
            
        //        foreach (var item in db.List.AsQueryable())
        //        {
        //            var listToCheck = db.ProductList.AsQueryable().Where(n => n.ListId == item.Id).Count();

        //            if (listToCheck == 0)
        //            {
        //                var ListToRemove = db.List.AsQueryable().Where(n => n.Id == item.Id).FirstOrDefault();
        //            db.List.DeleteOne(x => x.Id == ListToRemove.Id);

        //        }
        //        }
            
            
        //        listViewModel.productLists = db.ProductList.AsQueryable().ToList();

        //    var Ids = db.List.AsQueryable().Where(x => x.UserId == null).Include(x => x.ProductList).ThenInclude(x => x.ProductId).ToList();
        //    List<ListOfProducts> GetLists = new List<ListOfProducts>();
        //    foreach (var item in Ids)
        //    {
        //        var items = db.List.AsQueryable().Where(x => x.Id == item.Id).FirstOrDefault();
        //        GetLists.Add(items);
        //    }


        //    ViewBag.Lists = GetLists;
        //    //ViewBag.Lists = db.List.AsQueryable().Where(x => x.UserId == null).Include(x => x.ProductList).ThenInclude(x => x.Product).ToList();
            

        //    return View("~/Views/ListDisplay/Load_list.cshtml", listViewModel.productLists);
        //}

        [HttpPost]
        public IActionResult Edit_List(string Id)
        {
            MongoDBProdList listToEdit = new MongoDBProdList();
            listToEdit = db.MongoLists.Find(x => x.Id == ObjectId.Parse(Id)).FirstOrDefault();
            currentlyEditedListId = listToEdit.Id;
            //listToEdit.ProductList = new List<ProductList>();
            
            //    listToEdit = db.List.AsQueryable().Include(x => x.ProductList).Where(n => n.Id == list.Id).FirstOrDefault();
            
           
            //    foreach (var item in listToEdit.ProductList)
            //    {
            //        var tempItem = db.Products.AsQueryable().Where(x => x.Id == item.ProductId);
            //    }
            
            //currentlyEditedListId = listToEdit.Id;
            //var amount = listToEdit.ProductList.Count();          
            return RedirectToAction(nameof(Create_list), listToEdit);

        }


        [HttpPost]
        public IActionResult ChangeType(ObjectId ListId, ObjectId UserId)
        {
            
                var listToChange = db.List.AsQueryable().Where(x => x.UserId == UserId).Where(p => p.Id == ListId).FirstOrDefault();

                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    listToChange.UserId = ObjectId.Empty;
                    return RedirectToAction(nameof(Private_list_load));

        }







        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            //product.Id = ObjectId.GenerateNewId();

            if (product.Name != null)
            {

                var getList = db.MongoLists.AsQueryable().Where(x => x.Id == currentlyEditedListId).FirstOrDefault();
                var ProdList = db.Products.AsQueryable().Select(p => p.Name).ToList();
                    if (!ProdList.Contains(product.Name))
                    {
                    db.Products.InsertOne(product);
                    getList.ProductList.Add(product);
                    }
                    else
                    {
                    getList.ProductList.Add(product);
                    }

                var CurrentList = db.MongoLists.Find(x=>x.Id==currentlyEditedListId).FirstOrDefault();
                CurrentList.ProductList.Add(product);
                FilterDefinition<MongoDBProdList> filter = new BsonDocument("_id", currentlyEditedListId);
                db.MongoLists.ReplaceOne(filter, CurrentList);

            }
            return RedirectToAction(nameof(Create_list));
        }

        [HttpPost]
        public IActionResult FinishList(ObjectId? id)
        {
            ListOfProducts ToCheck = new ListOfProducts();
            string userId = null;
            string checkPrivate = null;
            
                if (id == null)
                {
                    id = currentlyEditedListId;
                }
                

                if (currentlyEditedListId != null && isPrivateList == "on")
                {
                    ToCheck = db.List.AsQueryable().Where(x => x.Id == currentlyEditedListId).FirstOrDefault();
                    checkPrivate = db.List.AsQueryable().Where(x => x.Id == currentlyEditedListId).Select(w => w.UserId).ToString();

                }
                
                userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                //if(db.Lists.Where(x => x.Id == id).Select(w => w.UserId).FirstOrDefault().ToString()!=null)
                //{ 
                //check = db.Lists.Where(x => x.Id == id).Select(w => w.UserId).FirstOrDefault().ToString();
                //}
                //else
                //{
                //    check = null;
                //}
            

            if (ToCheck.UserId != null)
            {
                return RedirectToAction(nameof(Private_list_load));
            }
            else
            {
                return RedirectToAction(nameof(Public_list_load));
            }
            
        }



        [HttpGet]
        public IActionResult Share_list()
        {
            var userId = ObjectId.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                foreach (var item in db.List.AsQueryable().ToList())
                {
                    var listToCheck = db.ProductList.AsQueryable().Where(n => n.ListId == item.Id).Count();

                    if (listToCheck == 0)
                    {
                        var ListToRemove = db.List.AsQueryable().Where(n => n.Id == item.Id).FirstOrDefault();
                        db.List.DeleteOne(x => x.Id == ListToRemove.Id);
                    }
                }
            
          
                listViewModel.productLists = db.ProductList.AsQueryable().ToList();

            var Ids = db.List.AsQueryable().Where(x => x.UserId == userId).Include(x => x.ProductList).ThenInclude(x => x.ProductId).ToList();
            List<ListOfProducts> GetLists = new List<ListOfProducts>();
            foreach (var item in Ids)
            {
                var items = db.List.AsQueryable().Where(x => x.Id == item.Id).FirstOrDefault();
                GetLists.Add(items);
            }


            ViewBag.Lists = GetLists;
            //ViewBag.Lists = db.List.AsQueryable().Where(x => x.UserId == userId).Include(x => x.ProductList).ThenInclude(x => x.Product).ToList();
            

            return View("~/Views/Utility/Share_list.cshtml", listViewModel.productLists);
        }

        [HttpPost]
        public IActionResult Get_share_email(ObjectId ListId)
        {
            IdToShare = ListId;
            return View();
        }

        //[HttpPost]
        //public IActionResult Send_email(string mail)
        //{
           
        //    List ListToShare = new List();
        //    List<string> usersEmails = new List<string>();
        //    bool Exist = false;
            
        //        usersEmails = db.Users.AsQueryable().Select(x => x.Email).ToList();
            
        //    foreach (var item in usersEmails)
        //    {
        //        if (item == mail)
        //        {
        //            Exist = true;
        //        }
        //    }

        //    if (Exist == true)
        //    {
        //        ProductList newProdToSave = new ProductList();
        //        ObjectId NewUserId;
               
        //            var friendMail = User.FindFirstValue(ClaimTypes.Name);
        //            NewUserId = db.Users.AsQueryable().Where(x => x.Email == mail).Select(y => y.Id).FirstOrDefault();
        //            List newList = new List();
        //            newList.UserId = NewUserId;

        //            newList.Name = "Lista udostepniona przez " + friendMail;

        //            db.List.InsertOne(newList);
                    
        //            var getProd = db.ProductList.AsQueryable().Where(x => x.ListId == IdToShare).Select(w => w.ProductId);
        //            foreach (var item in getProd)
        //            {

        //                var newProd = db.Products.AsQueryable().Where(x => x.Id == item).FirstOrDefault();
        //                newProdToSave.ListId = newList.Id;
        //                newProdToSave.List = newList;
        //                newProdToSave.ProductId = newProd.Id;
        //                newProdToSave.Product = newProd;
        //                db.ProductList.InsertOne(newProdToSave);

        //            }



                

        //    }
        //    else
        //    {

        //        ViewData["ErrorMessage"] = "Adres E-mail niepoprawny lub nie isnieje w bazie";
        //    }

        //        var userId = ObjectId.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        //        ViewBag.Lists = db.List.AsQueryable().Where(x => x.UserId == userId).Include(x => x.ProductList).ThenInclude(x => x.Product).ToList();
            
        //    return View("~/Views/ListDisplay/Private_list_load.cshtml");
        //}

        [HttpPost]
        public IActionResult DeleteProduct(ObjectId IdToRemove)
        {
            
                var ProductToCheck = db.Products.AsQueryable().Where(n => n.Id == IdToRemove).FirstOrDefault();
                var amount = db.ProductList.AsQueryable().Where(p => p.ProductId == ProductToCheck.Id).Count();
                if (amount == 1)
                {
                    db.Products.DeleteOne(x=>x.Id== IdToRemove);
                }
                else
                {
                    
                    var ToRemove = db.ProductList.AsQueryable().Where(n => n.ListId == currentlyEditedListId).Where(p => p.ProductId == ProductToCheck.Id).FirstOrDefault();
                    db.ProductList.DeleteOne(x=>x.ListId==currentlyEditedListId && x.ProductId==IdToRemove);
                }

            
            return RedirectToAction(nameof(Create_list));
        }


        [HttpPost]
        public IActionResult DeleteList(ListOfProducts list)
        {
            ListOfProducts ToRemove = new ListOfProducts();


            ToRemove = db.List.AsQueryable().Include(x => x.ProductList).Where(n => n.Id == list.Id).FirstOrDefault();
 
            db.List.DeleteOne(x => x.Id == ToRemove.Id);

            
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
       
        public JsonResult ListToShop(ObjectId id)
        {

            List<string> Products = new List<string>();
            List<ObjectId> currentList = new List<ObjectId>();

                currentList = db.ProductList.AsQueryable().Where(w => w.ListId == id).Select(p => p.ProductId).ToList();
                foreach (var item in currentList)
                {
                Products.Add(db.Products.AsQueryable().Where(x => x.Id == item).Select(w => w.Name).FirstOrDefault());
                }
            
            List<OfflineProduct> ListToCache = new List<OfflineProduct>();
            JsonSerializer serializer = new JsonSerializer();
            for (int i = 0; i < currentList.Count; i++)
            {
                var Prod = new OfflineProduct();
                Prod.ID = i;
                Prod.Name = Products[i];
                Prod.Done = false;
                ListToCache.Add(Prod);
            }
            string json = JsonConvert.SerializeObject(ListToCache);

            return Json(json);
        }


        [HttpPost]
        public IActionResult ShoppingView(ObjectId id)
        {
            var JsonData = ListToShop(id);

            return View(JsonData);
        }

        //[HttpGet]
        //public IActionResult SelectList()
        //{

        //    listViewModel.productLists = new List<ProductList>();

        //    listViewModel.productLists = db.ProductList.AsQueryable().ToList();


        //    var Ids = db.List.AsQueryable().ThenInclude(x => x.ProductId).ToList();
        //    List<List> GetLists = new List<List>();
        //    foreach (var item in Ids)
        //    {
        //        var items = db.List.AsQueryable().Where(x => x.Id == item.Id).FirstOrDefault();
        //        GetLists.Add(items);
        //    }


        //    ViewBag.Lists = GetLists;

        //    return View();
        //}
        
        public async void GetUsersData()
        {
            db.Users.DeleteMany("{}");
            db.MongoLists.DeleteMany("{}");
            List<ApplicationUser> UsersToAdd = new List<ApplicationUser>();

            for (int i = 0; i < 79; i++)
            {
                var User = CreateNewUserToMongoDBAsync();
                UsersToAdd.Add(User.Result);
            }
            await db.Users.InsertManyAsync(UsersToAdd);

        }
        public async Task<ApplicationUser> CreateNewUserToMongoDBAsync()
        {
            
            //IPasswordHasher<ApplicationUser> MyInterface=null;
            
            Random GetName = new Random();
            Random GetSurename = new Random();
            int Name = GetName.Next(0, names.Length - 1);
            int Surname = GetSurename.Next(0, surnames.Length - 1);
            var email = String.Concat(names[Name], surnames[Surname], "@gmail.com");
            ApplicationUser UserToReturn = new ApplicationUser();

            UserToReturn.Id = ObjectId.GenerateNewId().ToString();
            UserToReturn.Email = email;
            UserToReturn.UserName = email;
            UserToReturn.NormalizedEmail = email.ToUpper();
            UserToReturn.NormalizedName = email.ToUpper();
            UserToReturn.Age = GenerateAge();
            UserToReturn.Education = GenerateEducation();
            UserToReturn.Gender = GenerateGender();
            UserToReturn.Salary = GenerateSalary();
            UserToReturn.Home = GenerateHome();
            UserToReturn.PasswordHash = _userManager.PasswordHasher.HashPassword(UserToReturn, "Dominik123!");
            await _userManager.CreateAsync(UserToReturn, "Dominik123!");
            
            return UserToReturn;
        }




        string GenerateHome()
        {
            string[] HomeArray =
            {
                "kujawsko-pomorskie",
                "lubelskie",
                "lubuskie",
                "łódzkie",
                "małopolskie",
                "mazowieckie",
                "opolskie",
                "podkarpackie",
                "podlaskie",
                "pomorskie",
                "śląskie",
                "świętokrzyskie",
                "warmińsko-mazurskie",
                "wielkopolskie",
                "zachodniopomorskie",
                "abroad"

            };

            Random rndHome = new Random();
            int Index = rndHome.Next(0, HomeArray.Length);
            return HomeArray[Index];
        }
        string GenerateGender()
        {
            string[] GenderArray =
            {
                "Female",
                "Male"
            };

            Random rndHome = new Random();
            int Index = rndHome.Next(0, GenderArray.Length);
            return GenderArray[Index];
        }
        string GenerateEducation()
        {
            string[] EducationArray =
            {
                "podstawowe",
                "gimnazjalne",
                "zasadnicze zawodowe",
                "zasadnicze branzowe",
                "srednie",
                "wyzsze"
            };

            Random rndHome = new Random();
            int Index = rndHome.Next(0, EducationArray.Length);
            return EducationArray[Index];
        }
        string GenerateAge()
        {
            string[] AgeArray =
            {
                "ponizej 18 roku zycia",
                "powyzej 18 roku zycia"
            };

            Random rndHome = new Random();
            int Index = rndHome.Next(0, AgeArray.Length);
            return AgeArray[Index];
        }
        string GenerateSalary()
        {
            string[] SalaryArray =
            {
            "Ponizej 3000zl",
            "3000-5000zl",
            "Powyzej 5000zł"
            };

            Random rndHome = new Random();
            int Index = rndHome.Next(0, SalaryArray.Length);
            return SalaryArray[Index];
        }
    }


}