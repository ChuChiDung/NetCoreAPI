using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCD_Baithi2324.Models;
[Table ("Student")]
public class Student : Person {
    [Key]
    public int MAHOCSINH { get; set;}
    public string TENHS { get; set;}
    public char TUOIHS { get; set;}

}