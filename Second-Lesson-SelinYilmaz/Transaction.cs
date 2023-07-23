using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SipayApi.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace SipayApi.Data.Domain;

[Table("Transaction", Schema = "dbo")]
public class Transaction : IdBaseModel
{

    public string AccountNumber { get; set; }
    public decimal CreditAmount { get; set; } 
    public decimal DebitAmount { get; set; }    
    public string Description { get; set; }
    public DateTime TransactionDate { get; set; } 
    public string ReferenceNumber { get; set; }
}



