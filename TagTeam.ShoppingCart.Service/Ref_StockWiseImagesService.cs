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

namespace TagTeam.ShoppingCart.Service
{
    public class Ref_StockWiseImagesService : IRef_StockWiseImages_interface
    {
        private readonly string _adminConnectionString;
        private readonly string _sCConnectionString;

        public Ref_StockWiseImagesService(string adminConnectionString, string sCConnectionString)
        {
            _adminConnectionString = adminConnectionString;
            _sCConnectionString = sCConnectionString;

        }

        public async Task<BaseModel> Insert(Ref_StockWiseImagesModel stockWiseImage)
        {
            Ref_StockWiseImages stockWiseImageToDB = new Ref_StockWiseImages();
            stockWiseImageToDB.imageID = stockWiseImage.imageID;
            stockWiseImageToDB.stockID = stockWiseImage.stockID;
            stockWiseImageToDB.title = stockWiseImage.title;
            stockWiseImageToDB.imageURL = stockWiseImage.imageURL;
            stockWiseImageToDB.userID = stockWiseImage.userID;


            try
            {

                string convertedImageData = stockWiseImage.imageData.Substring(stockWiseImage.imageData.LastIndexOf(',') + 1);
                byte[] image64 = Convert.FromBase64String(convertedImageData);

                SettingsService settings = new SettingsService(_adminConnectionString, _sCConnectionString);
                string imagePath = settings.SelectWithinProject("IMGP").Value;
                int count = Directory.GetFiles(imagePath + "\\Stock\\" + stockWiseImage.stockCode + "\\", "*", SearchOption.AllDirectories).Length;
                string filePath = imagePath + "\\Stock\\" + stockWiseImage.stockCode + "\\Stock_Wise_Images"+ (count+1).ToString()+".jpg";
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


                stockWiseImageToDB.imageURL = filePath;

                using (var connection = new SqlConnection(_sCConnectionString))
                {
                    DynamicParameters para = new DynamicParameters();
                    string JsonData = JsonConvert.SerializeObject(stockWiseImageToDB);
                    para.Add("@JsonData", JsonData, DbType.String);
                    para.Add("@Action", "I", DbType.String);

                    await connection.ExecuteAsync("[dbo].[TAG_AD_POPULATE_StocksWiseImages]", para, commandType: System.Data.CommandType.StoredProcedure);

                    return new BaseModel() { code = "1000", description = "Success", data = stockWiseImageToDB };
                }
            }
            catch (Exception ex)
            {
                return new BaseModel() { code = "998", description = ex.Message, data = stockWiseImageToDB };
            }

        }

        public async Task<BaseModel> Update(UpdateData data)
        {
            Ref_StockWiseImagesModel stockWiseImage = data.NewData.ToObject<Ref_StockWiseImagesModel>();

            Ref_StockWiseImages stockWiseImageToDB = new Ref_StockWiseImages();
            stockWiseImageToDB.imageID = stockWiseImage.imageID;
            stockWiseImageToDB.stockID = stockWiseImage.stockID;
            stockWiseImageToDB.title = stockWiseImage.title;
            stockWiseImageToDB.description = stockWiseImage.description;
            stockWiseImageToDB.imageURL = stockWiseImage.imageURL;
            stockWiseImageToDB.colorCode = stockWiseImage.colorCode;
            stockWiseImageToDB.userID = stockWiseImage.userID;

            try
            {

                string convertedImageData = stockWiseImage.imageData.Substring(stockWiseImage.imageData.LastIndexOf(',') + 1);
                byte[] image64 = Convert.FromBase64String(convertedImageData);

                //SettingsService settings = new SettingsService(_adminConnectionString, _sCConnectionString);
                //string imagePath = settings.SelectWithinProject("IMGP").Value;

                string filePath = stockWiseImage.imageURL;
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


                stockWiseImageToDB.imageURL = filePath;

                using (var connection = new SqlConnection(_sCConnectionString))
                {
                    DynamicParameters para = new DynamicParameters();
                    string JsonData = JsonConvert.SerializeObject(stockWiseImageToDB);
                    string OldJsonData = JsonConvert.SerializeObject(data.OldData);
                    para.Add("@JsonData", JsonData, DbType.String);
                    para.Add("@OldJsonData", OldJsonData, DbType.String);
                    para.Add("@Action", "U", DbType.String);

                    await connection.ExecuteAsync("[dbo].[TAG_AD_POPULATE_StocksWiseImages]", para, commandType: System.Data.CommandType.StoredProcedure);

                    return new BaseModel() { code = "1000", description = "Success", data = stockWiseImageToDB };
                }
            }
            catch (Exception ex)
            {
                return new BaseModel() { code = "998", description = ex.Message, data = stockWiseImageToDB };
            }

        }

        public async Task<BaseModel> Delete(Ref_StockWiseImagesModel stockWiseImage)
        {
            Ref_StockWiseImages stockWiseImageToDB = new Ref_StockWiseImages();
            stockWiseImageToDB.imageID = stockWiseImage.imageID;
            stockWiseImageToDB.stockID = stockWiseImage.stockID;
            stockWiseImageToDB.title = stockWiseImage.title;
            stockWiseImageToDB.description = stockWiseImage.description;
            stockWiseImageToDB.imageURL = stockWiseImage.imageURL;
            stockWiseImageToDB.colorCode = stockWiseImage.colorCode;
            stockWiseImageToDB.userID = stockWiseImage.userID;

            try
            {

                string convertedImageData = stockWiseImage.imageData.Substring(stockWiseImage.imageData.LastIndexOf(',') + 1);
                byte[] image64 = Convert.FromBase64String(convertedImageData);

                //SettingsService settings = new SettingsService(_adminConnectionString, _sCConnectionString);
                //string imagePath = settings.SelectWithinProject("IMGP").Value;

                string filePath = stockWiseImage.imageURL;
                //string filePath = imagePath + "\\Stock\\" + stockWiseImage.stockCode + "\\" + stockWiseImage.imageID.ToString() + ".jpg";
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    File.WriteAllBytes(filePath, image64);
                }


                stockWiseImageToDB.imageURL = filePath;

                using (var connection = new SqlConnection(_sCConnectionString))
                {
                    DynamicParameters para = new DynamicParameters();
                    string JsonData = JsonConvert.SerializeObject(stockWiseImageToDB);
                    para.Add("@JsonData", JsonData, DbType.String);
                    para.Add("@Action", "D", DbType.String);

                    await connection.ExecuteAsync("[dbo].[TAG_AD_POPULATE_StocksWiseImages]", para, commandType: System.Data.CommandType.StoredProcedure);

                    return new BaseModel() { code = "1000", description = "Success", data = stockWiseImageToDB };
                }
            }
            catch (Exception ex)
            {
                return new BaseModel() { code = "998", description = ex.Message, data = stockWiseImageToDB };
            }

        }

        public async Task<BaseModel> Select(int imageID, int stockID)
        {
            try
            {
                using (var connection = new SqlConnection(_sCConnectionString))
                {
                    DynamicParameters para = new DynamicParameters();
                    para.Add("@ImageID", imageID, DbType.Int32);
                    para.Add("@stockID", stockID, DbType.Int32);
                    var StockWiseImages = await connection.QueryAsync<Ref_StockWiseImagesModel>("TAG_AD_SELECT_StocksWiseImages", para, commandType: System.Data.CommandType.StoredProcedure);

                    // get image file
                    List<Ref_StockWiseImagesModel> stockWiseImagesModelList = new List<Ref_StockWiseImagesModel>();
                    stockWiseImagesModelList = StockWiseImages.ToList();
                    for (int i = 0; i < stockWiseImagesModelList.Count; i++)
                    {
                        byte[] imageArray = System.IO.File.ReadAllBytes(stockWiseImagesModelList[i].imageURL);
                        string base64ImageRepresentation = Convert.ToBase64String(imageArray);

                        stockWiseImagesModelList[i].imageData = base64ImageRepresentation;
                    }

                    return new BaseModel() { code = "1000", description = "Success", data = stockWiseImagesModelList };
                }
            }
            catch (Exception ex)
            {
                return new BaseModel() { code = "998", description = ex.Message, data = imageID };
            }

        }
    }
}
