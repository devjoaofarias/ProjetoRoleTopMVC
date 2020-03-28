using System.Collections.Generic;
using RoleTopMVC.Models;

namespace RoleTopMVC.ViewModels
{
    public class AdministradorViewModel : BaseViewModel
    {
        public List<FormaPagamento> Alugueis {get;set;}
        public uint AlugueisAprovados {get;set;}
        public uint AlugueisReprovados {get;set;}
        public uint AlugueisPendentes {get;set;}
        public AdministradorViewModel() {
            this.Alugueis = new List<FormaPagamento>();
        }
    }
}