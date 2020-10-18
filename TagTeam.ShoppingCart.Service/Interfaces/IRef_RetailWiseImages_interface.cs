using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TagTeam.ShoppingCart.Domain;
using TagTeam.ShoppingCart.Domain.CustomModels;

namespace TagTeam.ShoppingCart.Service.Interfaces
{
    public interface IRef_RetailWiseImages_interface
    {
        Task<BaseModel> Insert(Ref_RetailWiseImagesModel retailWiseImages);
        Task<BaseModel> Update(UpdateData data);
        Task<BaseModel> Delete(Ref_RetailWiseImagesModel data);
        Task<BaseModel> Select(int imageID, int stockID);
    }
}
