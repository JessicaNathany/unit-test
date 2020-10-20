using BancoX.Repository.Inteface;
using BancoX.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoX.Service
{
    public class TesouroDiretoService : ITesouroDiretoService
    {
        private ITesouroDiretoRepository TesouroDiretoRepository { get; set; }
        public TesouroDiretoService(ITesouroDiretoRepository tesouroDiretoRepository)
        {
            TesouroDiretoRepository = tesouroDiretoRepository;
        }

        public double CalculaDescontoImpostoRenda(int idTesouro, double valorTitulo, DateTime dataAplicacao, DateTime dataVencimento, out string mensagemErro)
        {
            mensagemErro = "";

            if(idTesouro == 0)
            {
                mensagemErro = "Tesouro Direto inválida!";
                return 0;
            }

            // criar a regra pra calcular o IR de seis meses

            var tesouro = TesouroDiretoRepository.GetById(idTesouro);

            return 0;
        }
    }
}
