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
    class Ref_RetailWiseImagesService : IRef_RetailWiseImages_interface
    {
        private readonly string _adminConnectionString;
        private readonly string _sCConnectionString;

        public Ref_RetailWiseImagesService(string adminConnectionString, string sCConnectionString)
        {
            _adminConnectionString = adminConnectionString;
            _sCConnectionString = sCConnectionString;

        }

        public async Task<BaseModel> Insert(Ref_RetailWiseImagesModel retailWiseImage)
        {
            Ref_RetailWiseImages retailWiseImageToDB = new Ref_RetailWiseImages();
            retailWiseImageToDB.imageID = retailWiseImage.imageID;
            retailWiseImageToDB.retailID = retailWiseImage.retailID;
            retailWiseImageToDB.title = retailWiseImage.title;
            retailWiseImageToDB.imageURL = retailWiseImage.imageURL;
            retailWiseImageToDB.userID = retailWiseImage.userID;


            try
            {

                string convertedImageData = retailWiseImage.imageData.Substring(retailWiseImage.imageData.LastIndexOf(',') + 1);
                byte[] image64 = Convert.FromBase64String(convertedImageData);

                SettingsService settings = new SettingsService(_adminConnectionString, _sCConnectionString);
                string imagePath = settings.SelectWithinProject("IMGP").Value;
                int count = Directory.GetFiles(imagePath + "\\Retail\\" + retailWiseImage.retailCode + "\\", "*", SearchOption.AllDirectories).Length;
                string filePath = imagePath + "\\Retail\\" + retailWiseImage.retailCode + "\\Retail_Wise_Images" + (count + 1).ToString() + ".jpg";
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


                retailWiseImageToDB.imageURL = filePath;

                using (var connection = new SqlConnection(_sCConnectionString))
                {
                    DynamicParameters para = new DynamicParameters();
                    string JsonData = JsonConvert.SerializeObject(retailWiseImageToDB);
                    para.Add("@JsonData", JsonData, DbType.String);
                    para.Add("@Action", "I", DbType.String);

                    await connection.ExecuteAsync("[dbo].[TAG_AD_POPULATE_RetailsWiseImages]", para, commandType: System.Data.CommandType.StoredProcedure);

                    return new BaseModel() { code = "1000", description = "Success", data = retailWiseImageToDB };
                }
            }
            catch (Exception ex)
            {
                return new BaseModel() { code = "998", description = ex.Message, data = retailWiseImageToDB };
            }

        }

        public async Task<BaseModel> Update(UpdateData data)
        {
            Ref_RetailWiseImagesModel retailWiseImage = data.NewData.ToObject<Ref_RetailWiseImagesModel>();

            Ref_RetailWiseImages retailWiseImageToDB = new Ref_RetailWiseImages();
            retailWiseImageToDB.imageID = retailWiseImage.imageID;
            retailWiseImageToDB.retailID = retailWiseImage.retailID;
            retailWiseImageToDB.title = retailWiseImage.title;
            retailWiseImageToDB.description = retailWiseImage.description;
            retailWiseImageToDB.imageURL = retailWiseImage.imageURL;
            retailWiseImageToDB.colorCode = retailWiseImage.colorCode;
            retailWiseImageToDB.userID = retailWiseImage.userID;

            try
            {

                string convertedImageData = retailWiseImage.imageData.Substring(retailWiseImage.imageData.LastIndexOf(',') + 1);
                byte[] image64 = Convert.FromBase64String(convertedImageData);

                //SettingsService settings = new SettingsService(_adminConnectionString, _sCConnectionString);
                //string imagePath = settings.SelectWithinProject("IMGP").Value;

                string filePath = retailWiseImage.imageURL;
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


                retailWiseImageToDB.imageURL = filePath;

                using (var connection = new SqlConnection(_sCConnectionString))
                {
                    DynamicParameters para = new DynamicParameters();
                    string JsonData = JsonConvert.SerializeObject(retailWiseImageToDB);
                    string OldJsonData = JsonConvert.SerializeObject(data.OldData);
                    para.Add("@JsonData", JsonData, DbType.String);
                    para.Add("@OldJsonData", OldJsonData, DbType.String);
                    para.Add("@Action", "U", DbType.String);

                    await connection.ExecuteAsync("[dbo].[TAG_AD_POPULATE_RetailsWiseImages]", para, commandType: System.Data.CommandType.StoredProcedure);

                    return new BaseModel() { code = "1000", description = "Success", data = retailWiseImageToDB };
                }
            }
            catch (Exception ex)
            {
                return new BaseModel() { code = "998", description = ex.Message, data = retailWiseImageToDB };
            }

        }

        public async Task<BaseModel> Delete(Ref_RetailWiseImagesModel retailWiseImage)
        {
            Ref_RetailWiseImages retailWiseImageToDB = new Ref_RetailWiseImages();
            retailWiseImageToDB.imageID = retailWiseImage.imageID;
            retailWiseImageToDB.retailID = retailWiseImage.retailID;
            retailWiseImageToDB.title = retailWiseImage.title;
            retailWiseImageToDB.description = retailWiseImage.description;
            retailWiseImageToDB.imageURL = retailWiseImage.imageURL;
            retailWiseImageToDB.colorCode = retailWiseImage.colorCode;
            retailWiseImageToDB.userID = retailWiseImage.userID;

            try
            {

                string convertedImageData = retailWiseImage.imageData.Substring(retailWiseImage.imageData.LastIndexOf(',') + 1);
                byte[] image64 = Convert.FromBase64String(convertedImageData);

                //SettingsService settings = new SettingsService(_adminConnectionString, _sCConnectionString);
                //string imagePath = settings.SelectWithinProject("IMGP").Value;

                string filePath = retailWiseImage.imageURL;
                //string filePath = imagePath + "\\Stock\\" + stockWiseImage.stockCode + "\\" + stockWiseImage.imageID.ToString() + ".jpg";
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    File.WriteAllBytes(filePath, image64);
                }


                retailWiseImageToDB.imageURL = filePath;

                using (var connection = new SqlConnection(_sCConnectionString))
                {
                    DynamicParameters para = new DynamicParameters();
                    string JsonData = JsonConvert.SerializeObject(retailWiseImageToDB);
                    para.Add("@JsonData", JsonData, DbType.String);
                    para.Add("@Action", "D", DbType.String);

                    await connection.ExecuteAsync("[dbo].[TAG_AD_POPULATE_RetailsWiseImages]", para, commandType: System.Data.CommandType.StoredProcedure);

                    return new BaseModel() { code = "1000", description = "Success", data = retailWiseImageToDB };
                }
            }
            catch (Exception ex)
            {
                return new BaseModel() { code = "998", description = ex.Message, data = retailWiseImageToDB };
            }

        }

        public async Task<BaseModel> Select(int imageID, int retailID)
        {
            try
            {
                using (var connection = new SqlConnection(_sCConnectionString))
                {
                    DynamicParameters para = new DynamicParameters();
                    para.Add("@ImageID", imageID, DbType.Int32);
                    para.Add("@stockID", retailID, DbType.Int32);
                    var RetailWiseImages = await connection.QueryAsync<Ref_RetailWiseImagesModel>("TAG_AD_SELECT_RetailsWiseImages", para, commandType: System.Data.CommandType.StoredProcedure);

                    // get image file
                    List<Ref_RetailWiseImagesModel> retailWiseImagesModelList = new List<Ref_RetailWiseImagesModel>();
                    retailWiseImagesModelList = RetailWiseImages.ToList();
                    for (int i = 0; i < retailWiseImagesModelList.Count; i++)
                    {
                        byte[] imageArray = System.IO.File.ReadAllBytes(retailWiseImagesModelList[i].imageURL);
                        string base64ImageRepresentation = Convert.ToBase64String(imageArray);

                        retailWiseImagesModelList[i].imageData = base64ImageRepresentation;
                    }

                    return new BaseModel() { code = "1000", description = "Success", data = retailWiseImagesModelList };
                }
            }
            catch (Exception ex)
            {
                return new BaseModel() { code = "998", description = ex.Message, data = imageID };
            }

        }
    }
}
