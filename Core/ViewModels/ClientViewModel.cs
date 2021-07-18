using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ViewModels
{
    public class ClientViewModel
    {
        public int Id { get; set; }
        public int IdEmpresa { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public DateTime? DataNasc { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public DateTime DataExclusao { get; set; }
        public int CriadoPorUsuarioId { get; set; }
        public int ExcluirPorUsuarioId { get; set; }
    }
}
