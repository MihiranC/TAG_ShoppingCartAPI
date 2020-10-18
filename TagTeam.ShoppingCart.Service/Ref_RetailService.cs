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
    public class Ref_RetailService : IRef_Retail_interface
    {
        private readonly string _adminConnectionString;
        private readonly string _sCConnectionString;

        public Ref_RetailService(string adminConnectionString, string sCConnectionString)
        {
            _adminConnectionString = adminConnectionString;
            _sCConnectionString = sCConnectionString;

        }

        //to insert
        public async Task<BaseModel> Insert(Ref_RetailModel retail)
        {
            Ref_Retail retailToDB = new Ref_Retail();
            retailToDB.retailID = retail.retailID;
            retailToDB.code = retail.code;
            retailToDB.title = retail.title;
            retailToDB.description = retail.description;
            retailToDB.mainImageURL = retail.mainImageURL;
            retailToDB.colorCode = retail.colorCode;
            retailToDB.userID = retail.userID;
            retailToDB.commissionPercentage = retail.commissionPercentage;
            retailToDB.isActive = retail.isActive;

            try
            {

                string convertedImageData = retail.imageData.Substring(retail.imageData.LastIndexOf(',') + 1);
                byte[] image64 = Convert.FromBase64String(convertedImageData);

                SettingsService settings = new SettingsService(_adminConnectionString, _sCConnectionString);
                string imagePath = settings.SelectWithinProject("IMGP").Value;

                string filePath = imagePath + "\\Retail\\" + retailToDB.code + "\\Main.jpg";
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


                retailToDB.mainImageURL = filePath;

                using (var connection = new SqlConnection(_sCConnectionString))
                {
                    DynamicParameters para = new DynamicParameters();
                    string JsonData = JsonConvert.SerializeObject(retailToDB);
                    para.Add("@JsonData", JsonData, DbType.String);
                    para.Add("@Action", "I", DbType.String);

                    await connection.ExecuteAsync("[dbo].[TAG_AD_POPULATE_Retails]", para, commandType: System.Data.CommandType.StoredProcedure);

                    return new BaseModel() { code = "1000", description = "Success", data = retailToDB };
                }
            }
            catch (Exception ex)
            {
                return new BaseModel() { code = "998", description = ex.Message, data = retailToDB };
            }




        }


        //to Update
        public async Task<BaseModel> Update(UpdateData data)
        {
            Ref_RetailModel retail = data.NewData.ToObject<Ref_RetailModel>();

            Ref_Retail retailToDB = new Ref_Retail();
            retailToDB.retailID = retail.retailID;
            retailToDB.code = retail.code;
            retailToDB.title = retail.title;
            retailToDB.description = retail.description;
            retailToDB.mainImageURL = retail.mainImageURL;
            retailToDB.colorCode = retail.colorCode;
            retailToDB.userID = retail.userID;
            retailToDB.commissionPercentage = retail.commissionPercentage;
            retailToDB.isActive = retail.isActive;

            try
            {

                string convertedImageData = retail.imageData.Substring(retail.imageData.LastIndexOf(',') + 1);
                byte[] image64 = Convert.FromBase64String(convertedImageData);

                SettingsService settings = new SettingsService(_adminConnectionString, _sCConnectionString);
                string imagePath = settings.SelectWithinProject("IMGP").Value;

                string filePath = imagePath + "\\Retail\\" + retailToDB.code + "\\Main.jpg";
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


                retailToDB.mainImageURL = filePath;

                using (var connection = new SqlConnection(_sCConnectionString))
                {
                    DynamicParameters para = new DynamicParameters();
                    string JsonData = JsonConvert.SerializeObject(retailToDB);
                    string OldJsonData = JsonConvert.SerializeObject(data.OldData);
                    para.Add("@JsonData", JsonData, DbType.String);
                    para.Add("@OldJsonData", OldJsonData, DbType.String);
                    para.Add("@Action", "U", DbType.String);

                    await connection.ExecuteAsync("[dbo].[TAG_AD_POPULATE_Retails]", para, commandType: System.Data.CommandType.StoredProcedure);

                    return new BaseModel() { code = "1000", description = "Success", data = retailToDB };
                }
            }
            catch (Exception ex)
            {
                return new BaseModel() { code = "998", description = ex.Message, data = retailToDB };
            }

        }

        //to Delete
        public async Task<BaseModel> Delete(Ref_RetailModel retail)
        {
            Ref_Retail retailToDB = new Ref_Retail();
            retailToDB.retailID = retail.retailID;
            retailToDB.code = retail.code;
            retailToDB.title = retail.title;
            retailToDB.description = retail.description;
            retailToDB.mainImageURL = retail.mainImageURL;
            retailToDB.colorCode = retail.colorCode;
            retailToDB.userID = retail.userID;
            retailToDB.commissionPercentage = retail.commissionPercentage;
            retailToDB.isActive = retail.isActive;

            try
            {

                string convertedImageData = retail.imageData.Substring(retail.imageData.LastIndexOf(',') + 1);
                byte[] image64 = Convert.FromBase64String(convertedImageData);

                SettingsService settings = new SettingsService(_adminConnectionString, _sCConnectionString);
                string imagePath = settings.SelectWithinProject("IMGP").Value;

                string filePath = imagePath + "\\Retail\\" + retailToDB.code + "\\Main.jpg";
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    File.WriteAllBytes(filePath, image64);
                }


                retailToDB.mainImageURL = filePath;

                using (var connection = new SqlConnection(_sCConnectionString))
                {
                    DynamicParameters para = new DynamicParameters();
                    string JsonData = JsonConvert.SerializeObject(retailToDB);
                    para.Add("@JsonData", JsonData, DbType.String);
                    para.Add("@Action", "D", DbType.String);

                    await connection.ExecuteAsync("[dbo].[TAG_AD_POPULATE_Retails]", para, commandType: System.Data.CommandType.StoredProcedure);

                    return new BaseModel() { code = "1000", description = "Success", data = retailToDB };
                }
            }
            catch (Exception ex)
            {
                return new BaseModel() { code = "998", description = ex.Message, data = retailToDB };
            }

        }


        //to Select
        public async Task<BaseModel> Select(int retailID)
        {
            try
            {
                using (var connection = new SqlConnection(_sCConnectionString))
                {
                    DynamicParameters para = new DynamicParameters();
                    para.Add("@RetailID", retailID, DbType.Int32);
                    var Retail = await connection.QueryAsync<Ref_RetailModel>("TAG_AD_SELECT_Retails", para, commandType: System.Data.CommandType.StoredProcedure);

                    // get image file
                    List<Ref_RetailModel> retailModelList = new List<Ref_RetailModel>();
                    retailModelList = Retail.ToList();
                    for (int i = 0; i < retailModelList.Count; i++)
                    {
                        byte[] imageArray = System.IO.File.ReadAllBytes(retailModelList[i].mainImageURL);
                        string base64ImageRepresentation = Convert.ToBase64String(imageArray);

                        retailModelList[i].imageData = base64ImageRepresentation;
                    }

                    return new BaseModel() { code = "1000", description = "Success", data = retailModelList };
                }
            }
            catch (Exception ex)
            {
                return new BaseModel() { code = "998", description = ex.Message, data = retailID };
            }

        }
    }
}
