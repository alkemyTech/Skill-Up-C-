using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Core.Models;
using AlkemyWallet.Core.Services;
using AlkemyWallet.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AlkemyWallet.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class TransactionsController: ControllerBase
    {
        
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;

        public TransactionsController(ITransactionService transactionService, IMapper mapper)
        {
            _transactionService = transactionService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactions()
        {
            int userId = 2;
            var transactions = await _transactionService.GetTransactions(userId);
            var transactionsForShow = _mapper.Map<IEnumerable<TransactionDTO>>(transactions);
            return Ok(transactionsForShow);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransactionsById(int id)
        {
            int userId = 2;
            var trans = await _transactionService.GetTransactionsById(id, userId);
            if (trans is null)
            {
                return NotFound("No existe ningún catalogo con el id especificado");
            }
            var transForShow = _mapper.Map<TransactionDTO>(trans);
            return Ok(transForShow);
        }
        
    }
}

