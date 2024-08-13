using ArthiPOS.shop;
using ArthiPOS.utill;
using CrystalDecisions.Shared.Json;
using DataMember;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
using CommonUtilities;
using DataMember.memberlog;

namespace ArthiPOS.Utill
{
    class SaleParser
    {
        //string Defaultpath = @"C:\aaSaleJson\";
        //string folderSales = @"DailySales\";
        //string folderProcessed = @"Processed\";
        public string filePath = "";
        public string fileProcessPath = "";
        public string filename = "";
        public bool SAVELOG = false;
        public AdminLog log;


        public SaleParser(string filename,bool SaveLog)
        {
            this.filename = filename;
            this.SAVELOG = SaveLog;
            log = LogUtill.getAdminInputLog();
            string folder= log.SalesInProccessedFolder;
            string filederProcess = log.SaleProcessedDir;
            filePath = string.Format("{0}{1}.json", folder, filename.Replace("-",""));
            fileProcessPath = string.Format("{0}{1}.json", filederProcess, filename.Replace("-",""));
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            if (!Directory.Exists(folder))
            {
                MessageBox.Show(ConstMessages._FolderNotCreated);
            }
        }
        public bool updateLandLord(Landlord oldLand,Landlord newLand)
        {
            using (StreamReader f = File.OpenText(filePath))
            {
                string json = f.ReadToEnd();
                f.Close();//close before writing file
                var list = JsonConvert.DeserializeObject<Wrapper>(json);
                int i = 0;
                foreach (var item in list.data)
                {

                    if (item.land_person.pkey == oldLand.land_person.pkey)
                    {

                        newLand.land_product.sale_remaining_product = oldLand.land_product.sale_remaining_product;
                        oldLand.bill_key=newLand.land_person.pkey;
                        oldLand.land_product = newLand.land_product;
                        oldLand.land_person = newLand.land_person;
                        oldLand.client = newLand.client;
                        oldLand.service = newLand.service;
                        oldLand.expense = newLand.expense;
                        for(int j=0;j<oldLand.customers.Count;j++)
                        {
                            oldLand.customers[j].product = newLand.land_product;
                            item.customers[j].product= newLand.land_product;
                        }

                        list.data[i].customers = item.customers;
                        list.data[i] = (oldLand);

                        break;

                    }
                    i++;
                }

                //open file stream
                var convertedJson = JsonConvert.SerializeObject(list, Formatting.Indented,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });

                using (var tw = new StreamWriter(File.Create(filePath)))
                {
                    tw.WriteLine(convertedJson);
                    tw.Close();
                }
                return true;
            }

        }

        public bool updateLandLord(Landlord land)
        {
            using (StreamReader f = File.OpenText(filePath))
            {
                string json = f.ReadToEnd();
                f.Close();//close before writing file
                var list = JsonConvert.DeserializeObject<Wrapper>(json);
                int i = 0;
                foreach (var item in list.data)
                {

                    if (item.land_person.pkey == land.land_person.pkey)
                    {
                        list.data[i].status = land.status;
                        list.data[i].customers = item.customers;
                        list.data[i] = (land);

                        break;

                    }
                    i++;
                }

                //open file stream
                var convertedJson= JsonConvert.SerializeObject(list, Formatting.Indented,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });

                using (var tw = new StreamWriter(File.Create(filePath)))
                {
                    tw.WriteLine(convertedJson);
                    tw.Close();
                }
                return true;
            }

        }
        public bool updateLandLord(string _file,Wrapper wrapper)
        {
            if (File.Exists(_file))
            {
                //open file stream
                var convertedJson = JsonConvert.SerializeObject(wrapper, Formatting.Indented,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });

                using (var tw = new StreamWriter(File.Create(_file)))
                {
                    tw.WriteLine(convertedJson);
                    tw.Close();
                }
                return true;
            }
            else
            {
                File.Create(filePath);
            }
            return false;

        }
        public bool DeleteLandlord(string bill)
        {
            if (File.Exists(filePath))
            {
                using (StreamReader f = File.OpenText(filePath))
                {
                    string json = f.ReadToEnd();
                    f.Close();//close before writing file
                    var list = JsonConvert.DeserializeObject<Wrapper>(json);
                    foreach (var item in list.data)
                    {
                        if (item.land_person.pkey== bill)
                        {
                            list.data.Remove(item);
                            break;

                        }

                    }

                    File.Delete(filePath);
                    var convertedJson = JsonConvert.SerializeObject(list, Formatting.Indented);

                    using (var tw = new StreamWriter(File.Create(filePath)))
                    {
                        tw.WriteLine(convertedJson);
                        tw.Close();
                    }
                    return true;
                }
            }
            else
            {
                return false;
            }

        }

        public bool writeJsonWrapper(Wrapper wrap,string dbstatus)
        {
            //var list = JsonConvert.DeserializeObject<List<Person>>(myJsonString);
            //list.Add(new Person(1234, "carl2");
            //var convertedJson = JsonConvert.SerializeObject(list, Formatting.Indented);
            if (!SAVELOG)
            {
                return false;
            }
            try
            {
                using (StreamReader f = File.OpenText(filePath))
                {
                    string json = f.ReadToEnd();
                    f.Close();//close before writing file
                    wrap = LoadTodaySale(filePath);
                    return updateLandLord(filePath, wrap);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Add Error : " + ex.Message.ToString());
                return false;

            }
        }

        public bool writeJson(Landlord land,string dbStatus)
        {
            //var list = JsonConvert.DeserializeObject<List<Person>>(myJsonString);
            //list.Add(new Person(1234, "carl2");
            //var convertedJson = JsonConvert.SerializeObject(list, Formatting.Indented);
            if (!SAVELOG)
            {
                return false;
            }
            try
            {

                if (!File.Exists(filePath))
                {
                    return createJsonFile(land, dbStatus);
                }
                else
                {

                    using (StreamReader f= File.OpenText(filePath))
                    {
                        string json = f.ReadToEnd();
                        f.Close();//close before writing file
                        if (json == "") 
                        { 
                            File.Delete(filePath);
                            return createJsonFile(land, dbStatus) ? true : false;
                        }
                        else
                        {
                            Wrapper wraper = LoadTodaySale(filePath);
                            wraper.data.Add(land);
                            return updateLandLord(filePath,wraper);
                        }
                        //return createJsonFile(land, dbStatus)?true:false;



                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Add Error : " + ex.Message.ToString());
                return false;

            }


        }
        private bool createJsonFile(Landlord land,string dbStatus)
        {
            Wrapper w = new Wrapper();
            if (dbStatus != "")
            {
                w.db_status = dbStatus;
            }
            w.date = this.filename;
            w.data.Add(land);
            var jsonObject = JsonConvert.SerializeObject(w, Formatting.Indented,
                 new JsonSerializerSettings()
                 {
                     ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                 });
            using (var tw = new StreamWriter(File.Create(filePath)))
            {

                tw.WriteLine(jsonObject);
                tw.Close();
                return true;
            }
        }
        public  DataSet JsonToDataSet(List<ReportData> rp)
        {
            //Wrapper w = new Wrapper();
            //w.reportData=rp;
            var jsonObject = JsonConvert.SerializeObject(rp, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            DataSet myDataSet = JsonConvert.DeserializeObject<DataSet>(jsonObject);
            return myDataSet;
        }
       
        public DataTable LoadDataTableTodaySale()
        {
            try
            {
                using (StreamReader f = File.OpenText(filePath))
                {
                    string json = f.ReadToEnd();
                    f.Close();//close before writing file
                    DataTable dt = (DataTable)JsonConvert.DeserializeObject(json, (typeof(DataTable)));

                    return dt;

                }
            }
            catch (Exception ex)
            {

                return null;

            }
        }
        public List<Landlord> LoadTodaySale()
        {
            try
            {
                if (File.Exists(filePath) == false)
                    return null;

                using (StreamReader f = File.OpenText(filePath))
                {
                    string json = f.ReadToEnd();
                    f.Close();//close before writing file

                    var list = JsonConvert.DeserializeObject<Wrapper>(json);
                    if (list.date != "")
                    {
                        return list.data;
                    }
                    else
                    {
                        return null;
                    }

                }
            }
            catch(FileNotFoundException e) { return null; }
            catch (Exception ex)
            {
                //Console.Write(ex);
                return null;

            }
        }
        public List<Landlord> LoadProcessedTodaySale()
        {
            try
            {
                using (StreamReader f = File.OpenText(fileProcessPath))
                {
                    string json = f.ReadToEnd();
                    f.Close();//close before writing file

                    var list = JsonConvert.DeserializeObject<Wrapper>(json);
                    if (list.date != "")
                    {
                        return list.data;
                    }
                    else
                    {
                        return null;
                    }

                }
            }
            catch (Exception ex)
            {
                return null;

            }
        }
        public Wrapper LoadWraper()
        {
            try
            {
                using (StreamReader f = File.OpenText(filePath))
                {
                    string json = f.ReadToEnd();
                    f.Close();//close before writing file

                    var list = JsonConvert.DeserializeObject<Wrapper>(json);
                    if (list.date != "")
                    {
                        return list;
                    }
                    else
                    {
                        return null;
                    }

                }
            }
            catch (Exception ex)
            {
                return null;

            }
        }
        public List<Landlord> LoadTodayExpense()
        {
            try
            {
                using (StreamReader f = File.OpenText(filePath))
                {
                    string json = f.ReadToEnd();
                    f.Close();//close before writing file

                    var list = JsonConvert.DeserializeObject<Wrapper>(json);
                    if (list.date != "")
                    {
                        return list.data;
                    }
                    else
                    {
                        return null;
                    }

                }
            }
            catch (Exception ex)
            {
                return null;

            }
        }
        public Wrapper LoadTodaySale(string path)
        {
            try
            {
                using (StreamReader f = File.OpenText(path))
                {
                    string json = f.ReadToEnd();
                    f.Close();//close before writing file

                    var list = JsonConvert.DeserializeObject<Wrapper>(json);
                    if (list.date != "")
                    {
                        return list;
                    }
                    else
                    {
                        return null;
                    }

                }
            }
            catch (Exception ex)
            {
                return null;

            }
        }
    

        public bool DeleteCustomer(string billkey)
        {
            using (StreamReader f = File.OpenText(filePath))
            {
                string json = f.ReadToEnd();
                f.Close();//close before writing file
                var list = JsonConvert.DeserializeObject<Wrapper>(json);
                int i = 0;
                foreach (var item in list.data)
                {
                    if (item.land_person.pkey == billkey)
                    {
                        list.data[i].customers.Clear();
                        list.data[i].total_sale = 0;
                        list.data[i].UpdateTotal();
                        list.data[i].land_product.sale_remaining_product = list.data[i].land_product.total_Quantity;

                        break;

                    }
                    i++;

                }

                File.Delete(filePath);
                var convertedJson = JsonConvert.SerializeObject(list, Formatting.Indented);

                using (var tw = new StreamWriter(File.Create(filePath)))
                {
                    tw.WriteLine(convertedJson);
                    tw.Close();
                }
                return true;
            }
           
        }
        public bool DeleteCustomer(string landBillKey,int index)
        {
            if(File.Exists(filePath))
            {
                using (StreamReader f = File.OpenText(filePath))
                {
                    string json = f.ReadToEnd();
                    f.Close();//close before writing file
                    var list = JsonConvert.DeserializeObject<Wrapper>(json);
                    int i = 0;
                    
                    foreach (var item in list.data)
                    {
                        if (item.land_person.pkey == landBillKey)
                        {
                            try
                            {

                                Customer cus = list.data[i].customers[index];
                                //list.data[i].land_product.sale_remaining_product += cus.total_quantity;
                                list.data[i].customers.RemoveAt(index);
                                list.data[i].UpdateTotal();
                                break;

                            }
                            catch (ArgumentOutOfRangeException e)
                            {
                                break;
                            }
                        }
                        i++;

                    }

                    File.Delete(filePath);
                    var convertedJson = JsonConvert.SerializeObject(list, Formatting.Indented);

                    using (var tw = new StreamWriter(File.Create(filePath)))
                    {
                        tw.WriteLine(convertedJson);
                        tw.Close();
                    }
                    return true;
                }
            }
            return false;

        }
        public FileInfo[] getAllFiles(string pathDir,bool desc)
        {
            //Defaultpath + folderSales
            DirectoryInfo dir = new DirectoryInfo(pathDir);

            if (!dir.Exists)
                return  null;
            //FileInfo[] files = dir.GetFiles().OrderByDescending(p => p.CreationTime).ToArray();
            if(desc)
                return dir.GetFiles().OrderByDescending(p => p.CreationTime).ToArray();
            else
                return dir.GetFiles().OrderBy(p => p.CreationTime).ToArray();
            //return files;
        }
        public void putSaleinProcess()
        {
            String directoryName = log.SaleProcessedDir;
            if (!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);

            DirectoryInfo dirInfo = new DirectoryInfo(directoryName);
            if (dirInfo.Exists == false)
                Directory.CreateDirectory(directoryName);

            List<String> list_Sales = Directory
                               .GetFiles(log.SalesInProccessedFolder, "*.*", SearchOption.AllDirectories).ToList();

            foreach (string file in list_Sales)
            {
                FileInfo mFile = new FileInfo(file);
                // to remove name collisions
                if (new FileInfo(dirInfo + "\\" + mFile.Name).Exists == false)
                {
                    mFile.MoveTo(dirInfo + "\\" + mFile.Name);
                }
            }
        }
        public void moveSaleFromProcesstoSale(string date)
        {
            string fpath = log.SaleProcessedDir + date.Replace("-", "") + ".json";


            String directoryName = log.SaleProcessedDir;
            if (!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);

            DirectoryInfo dirInfo = new DirectoryInfo(directoryName);
            if (dirInfo.Exists == false)
                Directory.CreateDirectory(directoryName);


            FileInfo mFile = new FileInfo(fpath);


            // to remove name collisions
            if (new FileInfo(directoryName + "\\" + mFile.Name).Exists == true)
            {
                mFile.MoveTo(log.SalesInProccessedFolder + "\\" + mFile.Name);
            }
           
        }
        public void moveSaleinProcess(string sourceFile)
        {


            String directoryName = log.SaleProcessedDir;
            if (!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);

            DirectoryInfo dirInfo = new DirectoryInfo(directoryName);
            if (dirInfo.Exists == false)
                Directory.CreateDirectory(directoryName);

            
            FileInfo mFile = new FileInfo(sourceFile);

            if (File.Exists(dirInfo + "\\" + mFile.Name))
            {
                File.Delete(dirInfo + "\\" + mFile.Name);
            }

            // to remove name collisions
            if (new FileInfo(dirInfo + "\\" + mFile.Name).Exists == false)
            {
                //MessageBox.Show("Merage Existed File");
                saleInsertedStatus(true);
                mFile.MoveTo(dirInfo + "\\" + mFile.Name);
            }
            else
            {
                saleInsertedStatus(true);
                updateLandLord(filePath, mergeBothFiles());
                mFile.MoveTo(dirInfo + "\\" + mFile.Name);
            }
        }

        internal Wrapper mergeBothFiles()
        {
            Wrapper processIn = LoadTodaySale(filePath);
            Wrapper processfile = LoadTodaySale(fileProcessPath);
            if(processfile!=null)
                processIn.data.AddRange(processfile.data);
            return processIn;
        }
        public bool saleInsertedStatus(bool dbstatus)
        {
            using (StreamReader f = File.OpenText(filePath))
            {
                string json = f.ReadToEnd();
                f.Close();//close before writing file
                var list = JsonConvert.DeserializeObject<Wrapper>(json);
                int i = 0;
                foreach (var item in list.data)
                {

                        list.data[i].isRecordSaleInserted =dbstatus;
                }

                //open file stream
                var convertedJson= JsonConvert.SerializeObject(list, Formatting.Indented,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });

                using (var tw = new StreamWriter(File.Create(filePath)))
                {
                    tw.WriteLine(convertedJson);
                    tw.Close();
                }
                return true;
            }

        }

        internal void deletProcessFile()
        {
            File.Delete(fileProcessPath);
        }
        internal void deletSaleFile()
        {
            File.Delete(filePath);
        }
    }




    public class Wrapper
    {
        public string date = "";
        public string db_status = "None";

        public List<Landlord> data = new List<Landlord>();
        public Expense totalexpense=new Expense();
        public List<ShopExpense> lsExpense = new List<ShopExpense>();
        public List<ReportData> reportData = new List<ReportData>();
    }
    public class ShopExpense
    {
        public string name;
        public string amount;
        public string exploc;
        public string key;
        public string date;
    }
}
