using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RoleTopMVC.Models;

namespace RoleTopMVC.ViewModels
{
    public class FormaPagamentoViewModel : BaseViewModel
    {

        public List<string> tipoEvento = new List<string>();
        public List<string> tipoPacote = new List<string>();
        public List<string> publicoPrivado = new List<string>();
        public List<FormaPagamento> formasPagamentos = new List<FormaPagamento>();
        public FormaPagamento formaPagamento = new FormaPagamento();
        public Cliente cliente {get;set;}


    }
}