using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TagTeam.ShoppingCart.Domain.CustomModels;

namespace TagTeam.ShoppingCart.Service.Interfaces
{
    public interface ITest_Interface
    {
        Task<BaseModel> Select();
    }
}
