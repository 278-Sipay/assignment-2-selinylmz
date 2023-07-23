using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SipayApi.Base;
using SipayApi.Data.Domain;
using SipayApi.Data.Repository;
using SipayApi.Schema;

namespace SipayApi.Service;



[ApiController]
[Route("sipy/api/[controller]")]
public class TransactionController : ControllerBase
{
    private static List<Transaction> _transactions = new List<Transaction>
        {
            new Transaction { AccountNumber = "123547689", DebitAmount = 100, Description = "Transaction 1", TransactionDate = new DateTime(2023, 1, 1), ReferenceNumber = "REF001" },
            new Transaction { AccountNumber = "987654321", CreditAmount = -30, Description = "Transaction 2", TransactionDate = new DateTime(2023, 2, 1), ReferenceNumber = "REF002" },
            new Transaction { AccountNumber = "123547689", DebitAmount = 240, Description = "Transaction 3", TransactionDate = new DateTime(2023, 3, 1), ReferenceNumber = "REF003" },
            new Transaction { AccountNumber = "886754312", CreditAmount = -45, Description = "Transaction 4", TransactionDate = new DateTime(2023, 4, 1), ReferenceNumber = "REF004" }
        };

    [HttpGet("GetByParameter")]
    public IActionResult GetByParameter(
        string accountNumber = null,
        decimal? minAmountCredit = null,
        decimal? maxAmountCredit = null,
        decimal? minAmountDebit = null,
        decimal? maxAmountDebit = null,
        string description = null,
        DateTime? beginDate = null,
        DateTime? endDate = null,
        string referenceNumber = null)
    {

        var query = _transactions.AsQueryable();

        if (!string.IsNullOrEmpty(accountNumber))
        {
            query = query.Where(t => t.AccountNumber == accountNumber);
        }

        if (minAmountCredit.HasValue)
        {
            query = query.Where(t => t.CreditAmount >= minAmountCredit && t.DebitAmount > 0);
        }

        if (maxAmountCredit.HasValue)
        {
            query = query.Where(t => t.DebitAmount <= maxAmountCredit && t.CreditAmount > 0);
        }

        if (minAmountDebit.HasValue)
        {
            query = query.Where(t => t.DebitAmount >= minAmountDebit && t.CreditAmount < 0);
        }

        if (maxAmountDebit.HasValue)
        {
            query = query.Where(t => t.CreditAmount <= maxAmountDebit && t.DebitAmount < 0);
        }

        if (!string.IsNullOrEmpty(description))
        {
            query = query.Where(t => t.Description.ToLower().Contains(description.ToLower()));
        }

        if (beginDate.HasValue)
        {
            query = query.Where(t => t.TransactionDate >= beginDate);
        }

        if (endDate.HasValue)
        {
            query = query.Where(t => t.TransactionDate <= endDate);
        }

        if (!string.IsNullOrEmpty(referenceNumber))
        {
            query = query.Where(t => t.ReferenceNumber == referenceNumber);
        }

        var transactions = query.ToList();

        if (transactions.Count == 0)
        {
            return NotFound("No transactions found.");
        }

        return Ok(transactions);
    }


}
