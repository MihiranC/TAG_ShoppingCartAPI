using Dapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TagTeam.ShoppingCart.Domain;
using TagTeam.ShoppingCart.Domain.CustomModels;
using TagTeam.ShoppingCart.Service;
using TagTeam.ShoppingCart.Service.Interfaces;

namespace TagTeam.ShoppingCart.Domain
{
    public class Ref_StockService : IRef_Stock_interface
    {
        private readonly string _adminConnectionString;
        private readonly string _sCConnectionString;

        public Ref_StockService(string adminConnectionString, string sCConnectionString)
        {
            _adminConnectionString = adminConnectionString;
            _sCConnectionString = sCConnectionString;

        }

        public async Task<BaseModel> Insert(Ref_StockModel stock)
        {
            Ref_Stock stockToDB = new Ref_Stock();
            stockToDB.stockID = stock.stockID;
            stockToDB.code = stock.code;
            stockToDB.title = stock.title;
            stockToDB.description = stock.description;
            stockToDB.mainImageURL = stock.mainImageURL;
            stockToDB.colorCode = stock.colorCode;
            stockToDB.userID = stock.userID;

            try
            {

                string convertedImageData = stock.imageData.Substring(stock.imageData.LastIndexOf(',') + 1);
                byte[] image64 = Convert.FromBase64String(convertedImageData);

                SettingsService settings = new SettingsService(_adminConnectionString, _sCConnectionString);
                string imagePath = settings.SelectWithinProject("IMGP").Value;

                string filePath = imagePath + "\\Stock\\" + stockToDB.code+ "\\Main.jpg";
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    File.WriteAllBytes(filePath, image64);
                }
                else
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                    File.WriteAllBytes(filePath, image64);
                }


                stockToDB.mainImageURL = filePath;

                using (var connection = new SqlConnection(_sCConnectionString))
                {
                    DynamicParameters para = new DynamicParameters();
                    string JsonData = JsonConvert.SerializeObject(stockToDB);
                    para.Add("@JsonData", JsonData, DbType.String);
                    para.Add("@Action", "I", DbType.String);

                    await connection.ExecuteAsync("[dbo].[TAG_AD_POPULATE_Stocks]", para, commandType: System.Data.CommandType.StoredProcedure);

                    return new BaseModel() { code = "1000", description = "Success", data = stockToDB };
                }
            }
            catch (Exception ex)
            {
                return new BaseModel() { code = "998", description = ex.Message, data = stockToDB };
            }

        }

        public async Task<BaseModel> Update(UpdateData data)
        {
            Ref_StockModel stock = data.NewData.ToObject<Ref_StockModel>();

            Ref_Stock stockToDB = new Ref_Stock();
            stockToDB.stockID = stock.stockID;
            stockToDB.code = stock.code;
            stockToDB.title = stock.title;
            stockToDB.description = stock.description;
            stockToDB.mainImageURL = stock.mainImageURL;
            stockToDB.colorCode = stock.colorCode;
            stockToDB.userID = stock.userID;

            try
            {

                string convertedImageData = stock.imageData.Substring(stock.imageData.LastIndexOf(',') + 1);
                byte[] image64 = Convert.FromBase64String(convertedImageData);

                SettingsService settings = new SettingsService(_adminConnectionString, _sCConnectionString);
                string imagePath = settings.SelectWithinProject("IMGP").Value;

                string filePath = imagePath + "\\Stock\\" + stockToDB.code+ "\\Main.jpg";
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    File.WriteAllBytes(filePath, image64);
                }
                else
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                    File.WriteAllBytes(filePath, image64);
                }


                stockToDB.mainImageURL = filePath;

                using (var connection = new SqlConnection(_sCConnectionString))
                {
                    DynamicParameters para = new DynamicParameters();
                    string JsonData = JsonConvert.SerializeObject(stockToDB);
                    string OldJsonData = JsonConvert.SerializeObject(data.OldData);
                    para.Add("@JsonData", JsonData, DbType.String);
                    para.Add("@OldJsonData", OldJsonData, DbType.String);
                    para.Add("@Action", "U", DbType.String);

                    await connection.ExecuteAsync("[dbo].[TAG_AD_POPULATE_Stocks]", para, commandType: System.Data.CommandType.StoredProcedure);

                    return new BaseModel() { code = "1000", description = "Success", data = stockToDB };
                }
            }
            catch (Exception ex)
            {
                return new BaseModel() { code = "998", description = ex.Message, data = stockToDB };
            }

        }

        public async Task<BaseModel> Delete(Ref_StockModel stock)
        {
            Ref_Stock stockToDB = new Ref_Stock();
            stockToDB.stockID = stock.stockID;
            stockToDB.code = stock.code;
            stockToDB.title = stock.title;
            stockToDB.description = stock.description;
            stockToDB.mainImageURL = stock.mainImageURL;
            stockToDB.colorCode = stock.colorCode;
            stockToDB.userID = stock.userID;

            try
            {

                string convertedImageData = stock.imageData.Substring(stock.imageData.LastIndexOf(',') + 1);
                byte[] image64 = Convert.FromBase64String(convertedImageData);

                SettingsService settings = new SettingsService(_adminConnectionString, _sCConnectionString);
                string imagePath = settings.SelectWithinProject("IMGP").Value;

                string filePath = imagePath + "\\Stock\\" + stockToDB.code+ "\\Main.jpg";
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    File.WriteAllBytes(filePath, image64);
                }


                stockToDB.mainImageURL = filePath;

                using (var connection = new SqlConnection(_sCConnectionString))
                {
                    DynamicParameters para = new DynamicParameters();
                    string JsonData = JsonConvert.SerializeObject(stockToDB);
                    para.Add("@JsonData", JsonData, DbType.String);
                    para.Add("@Action", "D", DbType.String);

                    await connection.ExecuteAsync("[dbo].[TAG_AD_POPULATE_Stocks]", para, commandType: System.Data.CommandType.StoredProcedure);

                    return new BaseModel() { code = "1000", description = "Success", data = stockToDB };
                }
            }
            catch (Exception ex)
            {
                return new BaseModel() { code = "998", description = ex.Message, data = stockToDB };
            }

        }

        public async Task<BaseModel> Select(int stockID)
        {
            try
            {
                using (var connection = new SqlConnection(_sCConnectionString))
                {
                    DynamicParameters para = new DynamicParameters();
                    para.Add("@StockID", stockID, DbType.Int32);
                    var Stock = await connection.QueryAsync<Ref_StockModel>("TAG_AD_SELECT_Stocks", para, commandType: System.Data.CommandType.StoredProcedure);

                    // get image file
                    List<Ref_StockModel> stockModelList = new List<Ref_StockModel>();
                    stockModelList = Stock.ToList();
                    for (int i = 0; i < stockModelList.Count; i++)
                    {
                        byte[] imageArray = System.IO.File.ReadAllBytes(stockModelList[i].mainImageURL);
                        string base64ImageRepresentation = Convert.ToBase64String(imageArray);

                        stockModelList[i].imageData = base64ImageRepresentation;
                    }

                    return new BaseModel() { code = "1000", description = "Success", data = stockModelList };
                }
            }
            catch (Exception ex)
            {
                return new BaseModel() { code = "998", description = ex.Message, data = stockID };
            }

        }
    }
}
