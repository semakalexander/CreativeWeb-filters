using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CreativeWeb.Models
{
    public class Item
    {
        [HiddenInput(DisplayValue =false)]
        public int Id { get; set; }

        [Remote("CheckBarcode", "Admin",ErrorMessage ="Такий штрихкод уже є в базі")]
        [HiddenInput(DisplayValue = true)]
        [Display(Name="Штрихкод")]
        public string Barcode { get; set; }

        [Required(ErrorMessage ="Обов'язкове поле")]
        [Display(Name="Назва")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Обов'язкове поле")]
        [DataType(DataType.MultilineText)]
        [Display(Name="Опис")]
        public string Description { get; set; }

        [RegularExpression(@"^\d+(.\d+)?$", ErrorMessage = "Введіть коректне значення(Розділяйте крапкою)")]
        [Required(ErrorMessage = "Обов'язкове поле")]
        [Display(Name = "Ціна")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Обов'язкове поле")]
        [Display(Name = "Кількість")]
        public int Count { get; set; }

        [Remote("CheckType", "Admin",ErrorMessage ="Тип не може бути \"Вибрати\"")]
        [Required(ErrorMessage = "Обов'язкове поле")]
        [Display(Name = "Категорія")]
        public string Type { get; set; }

        [Display(Name="Зображення")]
        public string ImageSrc { get; set; }
    }
}