using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TagTeam.ShoppingCart.Domain;
using TagTeam.ShoppingCart.Domain.CustomModels;

namespace TagTeam.ShoppingCart.Service.Interfaces
{
    public interface IRef_Stock_interface
    {
        Task<BaseModel> Insert(Ref_StockModel stock);
        Task<BaseModel> Update(UpdateData data);
        Task<BaseModel> Delete(Ref_StockModel data);
        Task<BaseModel> Select(int stockID);
    }
}
