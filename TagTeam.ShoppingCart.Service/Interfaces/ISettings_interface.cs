using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TagTeam.ShoppingCart.Domain;
using TagTeam.ShoppingCart.Domain.CustomModels;

namespace TagTeam.ShoppingCart.Service.Interfaces
{
    public interface ISettings_interface
    {
        Task<BaseModel> Select(string code);
    }
}
