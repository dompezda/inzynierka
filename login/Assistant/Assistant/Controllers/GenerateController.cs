﻿//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Microsoft.AspNetCore.Mvc;
//using Assistant.Models;
//using Assistant.Data;
//using System.Security.Claims;
//using Encog.Neural.Networks;
//using Encog.Neural.Networks.Layers;
//using Encog.Engine.Network.Activation;
//using Encog.ML.Data;
//using Encog.ML.Data.Basic;
//using System.Threading;
//using System.Diagnostics;
//using Encog.ML.Train;
//using Encog.Neural.Networks.Training.Propagation.Resilient;
//using Encog.Neural.Networks.Training.Lma;
//using Encog.Util;
//using Encog.Util.Arrayutil;
//using Encog.ML.Data.Versatile;
//using Encog.Neural.Networks.Training.Propagation.Back;
//using Encog.Neural.Networks.Training.Propagation.Manhattan;
//using Encog.Neural.Networks.Training.Propagation.Quick;
//using Encog.Neural.Networks.Training.Propagation.SCG;
//using MongoDB.Bson;
//using MongoDB.Driver;
//using Assistant.Models.NN;

//namespace Assistant.Controllers
//{
//    public class GenerateController : Controller
//    {


//        protected MongoDbContext db { get; set; }
//        public GenerateController(MongoDbContext GetMongoDbContext)
//        {
//            db = GetMongoDbContext;
//        }
//    public static int? currentlyEditedListId;
//        public IActionResult Index()
//        {
//            return View();
//        }
//        [HttpGet]
//        public IActionResult Select_mode()
//        {

//            return View();
//        }

//        [HttpPost]
//        public IActionResult Generate()
//        {




//            return View();
//        }


        
//        [HttpPost]
//        public IActionResult NeuralNetwork(int Days, int alghoritm)
//        {


//            List NewGeneratedList = new List();
//            List<int> firstList = new List<int>();
//            List<int> secondList = new List<int>();
//            List<int> idealList = new List<int>();
//            List<int> IdsBasedOnObjectId = new List<int>();
//            List<ObjectId> FirstListMongo = new List<ObjectId>();
//            List<ObjectId> SecondListMongo = new List<ObjectId>();
//            List<ObjectId> ThirdListMongo = new List<ObjectId>();
//            string alghoritmName = "";
//            int ProductCounter = 0;
//            int ListCounter = 0;
//            List<NeuralNetworkProduct> ParsedIdsProduct = new List<NeuralNetworkProduct>();
//            List<NeuralNetworkList> ParsedIdsList = new List<NeuralNetworkList>();
//            foreach (var item in db.Products.AsQueryable().ToList())
//            {
//                ParsedIdsProduct.Add(new NeuralNetworkProduct
//                {
//                    IdMongo = item.Id,
//                    IdNN = ProductCounter
//                });
//                ProductCounter++;
//            }
//            Stopwatch sw = new Stopwatch();
//            DateTime Today = DateTime.Now;

//            var userId =ObjectId.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            
            
            
//                //GetLists = db.List.Where(x => x.CreateDate >= Today.AddDays(-Days)).Where(o => o.UserId == userId).OrderBy(w => w.CreateDate).Select(p => p.Id).ToArray();
//            var GetLists = db.List.AsQueryable().Where(x => x.CreateDate >= Today.AddDays(-Days) && x.UserId == userId).ToList();
//            foreach (var item in GetLists)
//            {
//                ParsedIdsList.Add(new NeuralNetworkList
//                {
//                    IdMongoList = item.Id,
//                    IdNNList = ListCounter
//                });
//                ListCounter++;
//            }
//            List<ObjectId> ProductsAmount = new List<ObjectId>();
//            for (int i = 0; i < GetLists.Count; i++)
//            {
//                    var Products = db.ProductList.AsQueryable().Where(x => x.ListId == GetLists.ElementAt(i).Id).Select(w => w.ProductId).ToArray();
//                    for (int w = 0; w < Products.Length; w++)
//                    {
//                        ProductsAmount.Add(Products[w]);
//                    }
//            }
//            int average = ProductsAmount.Count / GetLists.Count;
//            //var OutPutIDs = ProductsAmount.Select(x => x).Distinct().ToList();
//            var OutPutIDs = ParsedIdsProduct.Select(x => x.IdNN).Distinct().ToList();
//            var network = new BasicNetwork();
//            CreateNetwork(network, OutPutIDs,average);




//            for (int i = 0; i < GetLists.Count() - 2; i++)
//            {
//                //GetData from DB
//                ObjectId FirstListObjectId = db.List.AsQueryable().Where(x => x.Id == GetLists[i].Id).Select(x=>x.Id).FirstOrDefault();
//                ObjectId SecondListObjectId = db.List.AsQueryable().Where(x => x.Id == GetLists[i+1].Id).Select(x => x.Id).FirstOrDefault();
//                ObjectId ThirdListObjectId = db.List.AsQueryable().Where(x => x.Id == GetLists[i+2].Id).Select(x => x.Id).FirstOrDefault();
//                int firstListId = ParsedIdsList.Where(x => x.IdMongoList == FirstListObjectId).Select(y => y.IdNNList).FirstOrDefault();
//                int secondListId = ParsedIdsList.Where(x => x.IdMongoList == SecondListObjectId).Select(y => y.IdNNList).FirstOrDefault();
//                int thirdListId = ParsedIdsList.Where(x => x.IdMongoList == ThirdListObjectId).Select(y => y.IdNNList).FirstOrDefault();

//                //firstList = db.ProductList.AsQueryable().Where(x => x.ListId == FirstListObjectId).Select(o => o.ProductId).ToList();
//                //    secondList = db.ProductList.AsQueryable().Where(x => x.ListId == GetLists[i + 1]).Select(o => o.ProductId).ToList();
//                //    idealList = db.ProductList.AsQueryable().Where(x => x.ListId == GetLists[i + 2]).Select(o => o.ProductId).ToList();

//                FirstListMongo = db.ProductList.AsQueryable().Where(x => x.ListId == FirstListObjectId).Select(x => x.ProductId).ToList();
//                for (int k = 0; k < FirstListMongo.Count(); k++)
//                {
//                    firstList.Add(ParsedIdsProduct.Where(x => x.IdMongo == FirstListMongo.ElementAt(i)).Select(y => y.IdNN).FirstOrDefault());
//                }
//                SecondListMongo = db.ProductList.AsQueryable().Where(x => x.ListId == SecondListObjectId).Select(x => x.ProductId).ToList();
//                for (int k = 0; k < SecondListMongo.Count(); k++)
//                {
//                    firstList.Add(ParsedIdsProduct.Where(x => x.IdMongo == SecondListMongo.ElementAt(i)).Select(y => y.IdNN).FirstOrDefault());
//                }
//                ThirdListMongo = db.ProductList.AsQueryable().Where(x => x.ListId == ThirdListObjectId).Select(x => x.ProductId).ToList();
//                for (int k = 0; k < ThirdListMongo.Count(); k++)
//                {
//                    firstList.Add(ParsedIdsProduct.Where(x => x.IdMongo == ThirdListMongo.ElementAt(i)).Select(y => y.IdNN).FirstOrDefault());
//                }

//                List<int> GetAllIds = new List<int>();
//                List<int> InputIDs = new List<int>();
//                List<int> IdealIDs = new List<int>();
//                IdealIDs = idealList.ToList();
//                InputIDs = firstList.Concat(secondList).Select(x => x).Distinct().ToList();


//                //Products for iteration
//                //OutPutIDs
//                double[][] InputArray = new double[2][];
//                double[][] OutputArray = new double[2][];

//                for (int k = 0; k < InputArray.Length; k++)
//                {
//                    InputArray[k] = new double[OutPutIDs.Count()];
//                    OutputArray[k] = new double[OutPutIDs.Count()];

//                }

//                for (int j = 0; j < OutPutIDs.Count; j++)
//                {
//                    InputArray[0][j] = OutPutIDs[j];
//                    OutputArray[0][j] = OutPutIDs[j];
//                }
//                for (int l = 0; l < OutPutIDs.Count; l++)
//                {
//                    InputArray[1][l] = 0;
//                    OutputArray[1][l] = 0;
//                }
//                for (int k = 0; k < OutPutIDs.Count; k++)
//                {
//                    var jeden = InputArray[0][k];
//                    for (int l = 0; l < InputIDs.Count; l++)
//                    {
//                        if (InputIDs[l] == InputArray[0][k])
//                        {
//                            var dwa = InputIDs[l];
//                            InputArray[1][k] = 1;
//                        }
//                    }

//                }


//                for (int k = 0; k < OutPutIDs.Count; k++)
//                {

//                    for (int l = 0; l < IdealIDs.Count; l++)
//                    {
//                        if (IdealIDs[l] == OutputArray[0][k])
//                        {

//                            OutputArray[1][k] = 1;
//                        }
//                    }

//                }

//                var trainingSet = new BasicMLDataSet(InputArray, OutputArray);



//                //train 
//                dynamic train= new Backpropagation(network, trainingSet, 0.3, 0.2); ;

             
//                switch (alghoritm)
//                {

//                    case 1: 
//                        train = new Backpropagation(network, trainingSet, 0.3, 0.2);
//                        alghoritmName = "Propagacj wstecznej";
//                        break;
//                    case 2: 
//                        train = new ManhattanPropagation(network, trainingSet, 0.00001);
//                        alghoritmName = "Propagacji Manhattan";
//                        break;
//                    case 3:
//                        train = new QuickPropagation(network, trainingSet);
//                        alghoritmName = "Szybkiej propagacji";
//                        break;
//                    case 4:
//                        train = new ResilientPropagation(network, trainingSet);
//                        alghoritmName = "Propagacji sprężystej";
//                        break;
//                    case 5:
//                        train = new ScaledConjugateGradient(network, trainingSet);
//                        alghoritmName = "Metody gradientu sprężonego";
//                        break;
//                    case 6:
//                        train = new LevenbergMarquardtTraining(network, trainingSet);
//                        break;
//                }




                
//                sw.Start();
//                int epoch = 1;
//                System.Diagnostics.Debug.WriteLine("test rozpoczety " + epoch);
//                double trainError = 0.005;
//                do
//                {
//                    train.Iteration();
//                    if (epoch % 1000 == 0)
//                    {
//                        System.Diagnostics.Debug.WriteLine($"Epoch #{epoch} Error: {train.Error}");
//                        trainError++;
//                    }
//                    epoch++;
//                }
//                while (train.Error > trainError);

//                System.Diagnostics.Debug.WriteLine($"Epoch #{epoch}");
//                train.FinishTraining();


//            }


//            //Generate new List
//            List<ObjectId> FirstofLastListsMongo = new List<ObjectId>();
//            List<ObjectId> LastListMongo = new List<ObjectId>();
//            List<int> FirstOfLastsList = new List<int>();
//            List<int> LastList = new List<int>();

//            FirstofLastListsMongo = db.ProductList.AsQueryable().Where(x => x.ListId == GetLists.ElementAt(GetLists.Count() - 2).Id).Select(o => o.ProductId).ToList();
//            LastListMongo = db.ProductList.AsQueryable().Where(x => x.ListId == GetLists.ElementAt(GetLists.Count() - 1).Id).Select(o => o.ProductId).ToList();
            
            
//            for (int k = 0; k < FirstofLastListsMongo.Count(); k++)
//            {
//                FirstOfLastsList.Add(ParsedIdsProduct.Where(x => x.IdMongo == FirstofLastListsMongo.ElementAt(k)).Select(w => w.IdNN).FirstOrDefault());
//            };

//            for (int k = 0; k < LastListMongo.Count(); k++)
//            {
//                LastList.Add(ParsedIdsProduct.Where(x => x.IdMongo == LastListMongo.ElementAt(k)).Select(w => w.IdNN).FirstOrDefault());
//            };

//            var FinalCheck = LastList.Concat(FirstOfLastsList);

//            var FinalIntInput = FinalCheck.Select(x => x).Distinct();
//            double[] FinalInput = new double[FinalIntInput.Count()];
//            for (int i = 0; i < FinalIntInput.Count(); i++)
//            {
//                FinalInput[i] = Convert.ToDouble(FinalIntInput.ElementAt(i));
//            }
//            double[] FinalInputTest = new double[OutPutIDs.Count];
//            for (int i = 0; i < OutPutIDs.Count; i++)
//            {
//                for (int q = 0; q < FinalInput.Length; q++)
//                {
//                    if (OutPutIDs[i] == FinalInput[q])
//                    {
//                        FinalInputTest[i] = 1;
//                    }
//                }
//            }


//            BasicMLData FinalMLData = new BasicMLData(FinalInputTest);
//            IMLData test0001 = network.Compute(FinalMLData);
//            var test0001111 = test0001[0];


//            double[,] GetOutPutIdsPhaseOne = new double[2, test0001.Count];
//            for (int i = 0; i < test0001.Count; i++)
//            {

//                GetOutPutIdsPhaseOne[0, i] = OutPutIDs.ElementAt(i);
//                GetOutPutIdsPhaseOne[1, i] = test0001[i];

//            }
            
//            List<int> OutPutProd = new List<int>();
//            for (int i = 0; i < OutPutIDs.Count(); i++)
//            {
//                if (GetOutPutIdsPhaseOne[1, i] != 1)
//                {
//                    OutPutProd.Add((int)GetOutPutIdsPhaseOne[0, i]);
//                }
//            }
//            double[] FinalTest = new double[test0001.Count];
//            for (int i = 0; i < test0001.Count; i++)
//            {
//                FinalTest[i] = test0001[i];
//            }



//            var Inputt = FinalTest.OrderBy(x => x).Take(average).ToArray();
//            List<string> testo = new List<string>();

//            for (int i = 0; i < OutPutIDs.Count; i++)
//            {
//                for (int k = 0; k < Inputt.Length; k++)
//                {

//                    if (GetOutPutIdsPhaseOne[1, i] == Inputt[k])
//                    {
                        
//                            var Prod = db.Products.AsQueryable().Where(x => x.Id == GetOutPutIdsPhaseOne[0, i]).Select(w => w.Name).FirstOrDefault().ToString();
//                            testo.Add(Prod);

                        
//                    }
//                }

//            }

         
                
//                NewGeneratedList.UserId= ObjectId.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
//                NewGeneratedList.Name = "Lista wygenerowana przez algorytm " + alghoritmName;
                
//                db.Lists.Add(NewGeneratedList);
//                db.SaveChanges();
//                ProductList ProdListToAdd = new ProductList();
//                var Prods = new List<int>();
//                for (int i = 0; i < testo.Count; i++)
//                {
//                    var Prod = db.Products.AsQueryable().Where(x => x.Name == testo[i]).Select(w => w.Id).FirstOrDefault();
                    
                    
//                    if (!Prods.Contains(Prod)) 
//                    { 
//                ProdListToAdd.ListId = NewGeneratedList.Id;
//                ProdListToAdd.ProductId = Prod;
//                db.ProductList.Add(ProdListToAdd);
//                    }
//                    Prods.Add(Prod);
//                    db.SaveChanges();



//            }
        




//            sw.Stop();
//            System.Diagnostics.Debug.WriteLine($"czas: {sw.Elapsed}");

//            return RedirectToAction("Create_list","Home",new { id = NewGeneratedList.Id });
//        }


//        public BasicNetwork CreateNetwork(BasicNetwork Network, List<int> OutputNeurons,int average)
//        {
//            Network.AddLayer(new BasicLayer(null, true, OutputNeurons.Count));
//            Network.AddLayer(new BasicLayer(new ActivationSoftMax(), true, OutputNeurons.Count));
//            Network.AddLayer(new BasicLayer(new ActivationTANH(), true, OutputNeurons.Count));
//            Network.AddLayer(new BasicLayer(new ActivationLOG(), false, OutputNeurons.Count));
//            Network.Structure.FinalizeStructure();
//            Network.Reset();

//            return Network;
//        }

//        public List<double> GetAllProducts(List<int> input1,List<int> input2, List<int> output)
//        {
//            List<double> ListToReturn = new List<double>();
//            for (int i = 0; i < input1.Count; i++)
//            {
//                var number=Convert.ToDouble(input1[i]);
//                ListToReturn.Add(number);
//            }
//            for (int i = 0; i < input2.Count; i++)
//            {
//                var number = Convert.ToDouble(input2[i]);
//                ListToReturn.Add(number);
//            }
//            for (int i = 0; i < output.Count; i++)
//            {
//                var number = Convert.ToDouble(output[i]);
//                ListToReturn.Add(number);
//            }

//            return ListToReturn;
//        }
//        public double[] GetNormalizedArray(List<double> ArrayToNormalize)
//        {
//            var hi = 1;
//            var lo = 0;
//            var norm = new NormalizeArray { NormalizedHigh = hi, NormalizedLow = lo };
//            var Normalized = norm.Process(ArrayToNormalize.ToArray());
//            return Normalized;

//        }
//        public double[] GetDeNormalizerArray(double min,double max, double[] Array)
//        {
//            var difference = max - min;
//            double[] deNormalizedArray = new double[Array.Length];
//            for (int i = 0; i < Array.Length; i++)
//            {
//                deNormalizedArray[i] = (Array[i] * 10) + 1;
//            }
//            return deNormalizedArray;
//        }

//        public IActionResult Neural_Network_Settings()
//        {
//            return View();
//        }

//        public IActionResult Interpolation()
//        {
//            List<Product> products = new List<Product>();
//            ProductList helperList = new ProductList();
//            List currentlyEditedList = new List();
//            var productList = new ProductList();



//            using (var db = new ApplicationDbContext())
//            {
//                currentlyEditedList = new List { Name = "Lista z " + DateTime.Now.ToString() };
//                db.Lists.Add(currentlyEditedList);
//                db.SaveChanges();
//                currentlyEditedListId = currentlyEditedList.Id;

//                int newId = db.Lists.OrderBy(x => x.Id).Select(p => p.Id).Last();

//                var dateCheck = DateTime.Now.AddDays(-21);
//                var Lists = db.Lists.Where(x => x.CreateDate > dateCheck).Select(c => c.Id);


//                foreach (var item in Lists)
//                {
//                    var prodId = db.ProductLists.Where(x => x.ListId == item).Select(c => c.ProductId);
//                    foreach (var q in prodId)
//                    {
//                        var productToCheck = db.Products.Where(p => p.Id == q).FirstOrDefault();
//                        int amount = db.ProductLists.Where(p => p.ProductId == q).Count();
//                        if (!products.Contains(productToCheck) && amount >= 2)
//                        {
//                            products.Add(productToCheck);
//                        }
//                    }

//                }


//                foreach (var itemToAdd in products)
//                {
//                    var listsWithTheProduct = db.ProductLists.Where(x => x.ProductId == itemToAdd.Id).Select(w => w.List.CreateDate);
//                    var twoLastUses = listsWithTheProduct.OrderByDescending(x => x.Date).Take(2);
//                    var EndDate = twoLastUses.First();
//                    var StartDate = twoLastUses.Last();
//                    var interval = (EndDate - StartDate).TotalDays;
//                    var TodayPlus = DateTime.Now.AddDays(3);
//                    var TodayMinus = DateTime.Now.AddDays(-3);
//                    var dateToCheck = EndDate.AddDays(interval);
//                    if (dateToCheck >= TodayMinus && dateToCheck <= TodayPlus)
//                    {
                        
//                        currentlyEditedList = db.Lists.Single(x => x.Id == currentlyEditedListId);
//                        if (User.FindFirstValue(ClaimTypes.NameIdentifier) != null)
//                        {
//                            currentlyEditedList.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
//                        }
//                        productList.List = currentlyEditedList;
//                        productList.Product = itemToAdd;
//                        db.ProductLists.Add(productList);
//                        db.SaveChanges();
//                    }


//                }


//            }

//            if (currentlyEditedList.UserId == null)
//            {
//                return RedirectToAction("Public_list_load", "Home");

//            }
//            else
//            {
//                return RedirectToAction("Private_list_load", "Home");
//            }
//        }

//        public IActionResult Ranking()
//        {
//            List NewGeneratedList = new List();
//            NewGeneratedList.UserId = ObjectId.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
//            NewGeneratedList.Name = "Lista wygenerowana przez Ranking";
//            var userId = ObjectId.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
//            List<int> GetLists = new List<int>();
//            List<int> ProdsIDs = new List<int>();
//            List<int> ProdsAmount = new List<int>();
            
//            using (var db = new ApplicationDbContext())
//            {
//                GetLists = db.Lists.Where(x => x.UserId == userId).Select(w => w.Id).ToList();
                
//                for (int i = 0; i < GetLists.Count; i++)
//                {
//                    var Prod = db.ProductLists.Where(x => x.ListId == GetLists[i]).Select(w => w.ProductId).ToList();
//                    for (int w = 0; w < Prod.Count; w++)
//                    {
//                        if (!ProdsIDs.Contains(Prod[w]))
//                        {
//                            ProdsIDs.Add(Prod[w]);
//                        }
//                    }
                    
//                }
//                int[,] GetRank = new int[2, ProdsIDs.Count];

//                for (int i = 0; i < ProdsIDs.Count; i++)
//                {
//                    GetRank[0, i] = ProdsIDs[i];
//                }

//                for (int i = 0; i < ProdsIDs.Count; i++)
//                {
//                    GetRank[1,i]=db.ProductLists.Where(x=>x.ProductId==ProdsIDs[i]).Count();
//                }
//                double sum = 0;
//                for (int i = 0; i < ProdsIDs.Count; i++)
//                {
//                    sum += GetRank[1, i];
//                }
//                double necessaryAmount = sum*2 / ProdsIDs.Count();
//                List<int> IDsToList = new List<int>();

//                for (int i = 0; i < ProdsIDs.Count; i++)
//                {
//                    if (GetRank[1, i] >= necessaryAmount)
//                    {
//                        IDsToList.Add(GetRank[0, i]);
//                    }
//                }
                

//                db.Lists.Add(NewGeneratedList);
//                db.SaveChanges();
//                ProductList ProdListToAdd = new ProductList();
//                ProdListToAdd.ListId = NewGeneratedList.Id;
//                for (int i = 0; i < IDsToList.Count; i++)
//                {
//                    ProdListToAdd.ProductId = IDsToList[i];
//                    db.ProductLists.Add(ProdListToAdd);
//                    db.SaveChanges();
//                }

//                }



//                return RedirectToAction("Create_list", "Home", new { id = NewGeneratedList.Id });
//        }
//    }
//}



        