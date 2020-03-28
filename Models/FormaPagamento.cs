using System;
using RoleTopMVC.Enums;

namespace RoleTopMVC.Models
{
    public class FormaPagamento
    {   
        public ulong Id {get;set;}
        public Cliente Cliente {get;set;}
        public string NomeCartao {get;set;}
        public string Email {get;set;}
        public string NumeroCartao {get;set;}
        public string Cvv {get;set;}
        public string Telefone {get;set;}
        public DateTime DataValidade {get;set;}
        public DateTime DataEvento {get;set;}
        public string tipoEvento {get;set;}
        public string publicoPrivado {get;set;}
        public string tipoPacote {get;set;}
         public uint Status { get; set; }
         public double PrecoTotal {get;set;}
        public FormaPagamento () {
            this.Id = 1;
            this.Cliente = new Cliente();
            this.Status = (uint) StatusAluguel.PENDENTE;
        }

         public FormaPagamento (ulong Id, Cliente cliente, string NomeCartao, string Email, string NumeroCartao, string Cvv, string Telefone, DateTime DataValidade, DateTime DataEvento, string tipoEvento, string publicoPrivado, string tipoPacote, uint Status, double PrecoTotal) {
            this.Id = 1;
            this.Cliente = new Cliente();
            this.Status = (uint) StatusAluguel.PENDENTE;
        }
    }
}